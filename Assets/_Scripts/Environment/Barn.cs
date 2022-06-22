using UnityEngine;
using static EventsNamespace.EventBus;
using DG.Tweening;

public class Barn : MonoBehaviour
{
	public Vector3 targetScale;
	public float scaleDuration;

	[SerializeField] private GameObject barnModelGO;
	private Vector3 startScale;
	

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

		barnModelGO.transform.DOScale(targetScale, scaleDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
		{
			barnModelGO.transform.DOScale(startScale, scaleDuration).SetEase(Ease.InOutCubic);
		});
	}
}
