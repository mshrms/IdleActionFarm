using System.Collections.Generic;
using UnityEngine;
using static EventsNamespace.EventBus;
using DG.Tweening;

public class FarmerInventory : MonoBehaviour
{
	[Header("/// Inventory Settings ///")]
	public int maxItems;
	public float hayPacksVerticalSpacing;
	public float hayPacksHorizontalSpacing;
	public int stackMaxHeight;
	public List<GameObject> hayPacks;

	private int currentStackHeight;
	private int currentStackWidth;
	private Vector3 inventoryOnSpinePos;
	private Vector3 newHayPackPosition;

	[Header("/// Hay Pack Add/Sell Settings ///")]
	public Vector2 hayPackSpeedRange;
	public float jumpHeight;
	public float haySellDuration;
	public float sellCooldownTime;

	[SerializeField] private Transform sellPoint;
	private bool isSelling = false;
	private float cooldown;


	private void OnEnable()
	{
		onSellZoneEnter += StartSellingHay;
		onSellZoneExit += StopSellingHay;
	}

	private void OnDisable()
	{
		onSellZoneEnter -= StartSellingHay;
		onSellZoneExit -= StopSellingHay;
	}

	void Start()
	{
		hayPacks = new List<GameObject>();
		inventoryOnSpinePos = transform.localPosition;

		newHayPackPosition = inventoryOnSpinePos;

		currentStackHeight = 0;
		currentStackWidth = 0;
	}

	private void Update()
	{
		if (isSelling && cooldown <= 0f)
		{
			if (hayPacks.Count > 0)
			{
				SellHayPack();
			}
			else
			{
				Debug.Log("Nothing to sell. Inventory is empty");
			}
		}
		if (cooldown > 0)
		{
			cooldown -= Time.deltaTime;	
		}
	}

	public void AddHayPack(GameObject hayPack)
	{
		if (hayPacks.Count < maxItems)
		{
			hayPack.GetComponent<Collider>().enabled = false;
			hayPack.GetComponent<Rigidbody>().isKinematic = true;

			hayPacks.Add(hayPack);
			hayPack.transform.SetParent(transform);

			float duration = CalculateDuration();

			IncreaseStackHeight();

			newHayPackPosition = CalculateNextHayPosition();

			hayPack.transform.DOLocalRotate(transform.localRotation.eulerAngles, duration).SetEase(Ease.InOutCubic);
			hayPack.transform.DOLocalJump(newHayPackPosition, jumpHeight, 1, duration).SetEase(Ease.InOutCubic);

			onHayPickup?.Invoke();
		}
		else
		{
			Debug.Log("Inventory is full");
			onInventoryFull?.Invoke();
		}
	}

	private void SellHayPack()
	{
		cooldown = sellCooldownTime;
		GameObject hayToSell = hayPacks[hayPacks.Count - 1];

		hayPacks.Remove(hayToSell);

		DecreaseStackHeight();

		hayToSell.transform.DOScale(0.3f, haySellDuration)
			.SetEase(Ease.InOutCubic);

		hayToSell.transform.DOJump
			(sellPoint.position, 0.2f, 1, haySellDuration)
			.SetEase(Ease.InOutCubic)
			.OnComplete(() =>
			{
				onHaySold?.Invoke();

				transform.DOKill();
				Destroy(hayToSell);
			});
	}

	private void IncreaseStackHeight()
	{
		//если добавлен первый блок, значит ширина уже 1, а не 0
		if (hayPacks.Count == 1)
		{
			currentStackWidth = 1;
		}

		//увеличиваем высоту
		currentStackHeight++;

		//если высота текущей колонны превышает максимальную высоту колонны, добавить блок уже в новую колонну
		if (currentStackHeight > stackMaxHeight)
		{
			//один блок в новой колонне даст высоту 1 и увеличит ширину
			currentStackHeight = 1;
			currentStackWidth++;
		}
	}

	private void DecreaseStackHeight()
	{
		currentStackHeight--;

		if (currentStackHeight < 1)
		{
			currentStackWidth--;
			currentStackHeight = stackMaxHeight;
		}

		if (hayPacks.Count < 1)
		{
			currentStackWidth = 0;
			currentStackHeight = 0;
		}
	}

	private Vector3 CalculateNextHayPosition()
	{
		float yOffsets = currentStackHeight * hayPacksVerticalSpacing;
		float zOffsets = currentStackWidth * hayPacksHorizontalSpacing;

		Vector3 offsets = new Vector3(0, yOffsets, zOffsets);
		Vector3 position = offsets + inventoryOnSpinePos;

		return position;
	}

	private float CalculateDuration()
	{
		return Random.Range(hayPackSpeedRange.x, hayPackSpeedRange.y);
	}

	private void StartSellingHay()
	{
		isSelling = true;
	}

	private void StopSellingHay()
	{
		isSelling = false;
	}
}