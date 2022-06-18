using System.Collections.Generic;
using UnityEngine;
using static EventsNamespace.EventBus;
using DG.Tweening;

public class FarmerInventory : MonoBehaviour
{
	public List<GameObject> hayPacks;
	public int maxItems;
	public float hayPacksVerticalSpacing;
	public float hayPacksHorizontalSpacing;
	public int stackMaxHeight;
	public float hayPackMoveDuration;
	public float jumpHeight;
	private Vector3 inventoryOnSpinePos;
	private Vector3 nextHayPackPosition;
	private int currentStackHeight;


	void Start()
	{
		hayPacks = new List<GameObject>();
		inventoryOnSpinePos = transform.localPosition;

		//new
		nextHayPackPosition = inventoryOnSpinePos;
		currentStackHeight = 0;
	}

	public void AddHayPack(GameObject hayPack)
	{
		if (hayPacks.Count < maxItems)
		{
			hayPack.GetComponent<Collider>().enabled = false;
			hayPack.GetComponent<Rigidbody>().isKinematic = true;

			hayPacks.Add(hayPack);
			hayPack.transform.SetParent(transform);

			//UpdateInventoryOnSpine();


			//new
			hayPack.transform.DOLocalRotate(transform.localRotation.eulerAngles, hayPackMoveDuration);
			hayPack.transform.DOLocalJump(nextHayPackPosition, jumpHeight, 1, hayPackMoveDuration);

			currentStackHeight++;

			if (currentStackHeight == stackMaxHeight)
			{
				//обнулить позицию по Y, добавить спейсинг по Z
				nextHayPackPosition.y = inventoryOnSpinePos.y;
				nextHayPackPosition.z += hayPacksHorizontalSpacing;

				currentStackHeight = 0;
			}
			else
			{
				nextHayPackPosition.y += hayPacksVerticalSpacing;
			}
		}
		else
		{
			Debug.Log("Inventory FULL");
			onInventoryFull?.Invoke();
		}
	}

	//public void UpdateInventoryOnSpine()
	//{
	//	Vector3 nextHayPos = inventoryOnSpinePos;

	//	if (hayPacks.Count > 0)
	//	{
	//		for (int i = 0; i < hayPacks.Count; i++)
	//		{
	//			hayPacks[i].transform.DOLocalRotate(transform.localRotation.eulerAngles, hayPackMoveDuration);
	//			//hayPacks[i].transform.DOLocalMove(nextHayPos, hayPackMoveDuration).SetEase(Ease.OutCubic);
	//			hayPacks[i].transform.DOLocalJump(nextHayPos, 2, 1, hayPackMoveDuration);

	//			nextHayPos.y += hayPacksSpacing;
	//		}
	//	}
	//	else
	//	{
	//		Debug.Log("Inventory is Empty");
	//	}
	//}
}