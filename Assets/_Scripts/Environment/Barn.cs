using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EventsNamespace.EventBus;
using DG.Tweening;

public class Barn : MonoBehaviour
{
	public float scaleModifier;
	public float scaleDuration;
	private Vector3 startScale;
	[SerializeField] private GameObject barnModelGO;

	private void Start()
	{
		startScale = transform.localScale;
	}
	private void OnEnable()
	{
		onHaySold += AnimateBarnOnSell;
	}

	private void OnDisable()
	{
		onHaySold -= AnimateBarnOnSell;
	}

	private void AnimateBarnOnSell()
	{
		barnModelGO.transform.DOKill();
		barnModelGO.transform.localScale = startScale;

		barnModelGO.transform.DOScaleY(scaleModifier, scaleDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
		{
			barnModelGO.transform.DOScale(startScale, scaleDuration).SetEase(Ease.InOutCubic);
		});
	}
}
