using UnityEngine;
using static EventsNamespace.EventBus;
using DG.Tweening;

public class SellZone : MonoBehaviour
{
	public float scaleDuration;
	public float scaleModifier;
	private Vector3 startScale;

	private void Start()
	{
		startScale = transform.localScale;
	}

	private void OnEnable()
	{
		onSellZoneEnter += ScaleSellZoneUp;
		onSellZoneExit += ScaleSellZoneDown;
	}

	private void OnDisable()
	{
		onSellZoneEnter -= ScaleSellZoneUp;
		onSellZoneExit -= ScaleSellZoneDown;
	}

	private void OnTriggerEnter(Collider other)
	{
		onSellZoneEnter?.Invoke();
	}

	private void OnTriggerExit(Collider other)
	{
		onSellZoneExit?.Invoke();
	}


	private void ScaleSellZoneUp()
	{
		transform.DOKill();
		transform.localScale = startScale;

		transform.DOScaleX(scaleModifier, scaleDuration).SetEase(Ease.InOutCubic);
		transform.DOScaleY(scaleModifier, scaleDuration).SetEase(Ease.InOutCubic);
	}

	private void ScaleSellZoneDown()
	{
		transform.DOKill();
		transform.DOScale(startScale, scaleDuration).SetEase(Ease.InOutCubic);
	}

}
