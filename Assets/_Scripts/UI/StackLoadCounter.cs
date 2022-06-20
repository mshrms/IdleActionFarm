using UnityEngine;
using static EventsNamespace.EventBus;
using TMPro;
using System.Text;
using DG.Tweening;

public class StackLoadCounter : MonoBehaviour
{
    [SerializeField] private FarmerInventory inventory;
    [SerializeField] private TMP_Text stackLoadText;
    [SerializeField] private Transform haySprite;
    private float maxStackLoad;
    private StringBuilder stringBuilder;

    public float animationDuration;
    public Vector3 textTargetScale;
    public Vector3 spriteScaleModifiers;
    private Vector3 textStartScale;
    private Vector3 spriteStartScale;

    private void OnEnable()
	{
        onHayPickup += UpdateStackLoadText;
        onHaySold += UpdateStackLoadText;

        onHayPickup += ShakeText;
        onHaySold += ShakeText;

        onHayPickup += OnHayPickup;
        onHaySold += OnHaySold;
    }

	private void OnDisable()
	{
        onHayPickup -= UpdateStackLoadText;
        onHaySold -= UpdateStackLoadText;

        onHayPickup -= ShakeText;
        onHaySold -= ShakeText;

        onHayPickup -= OnHayPickup;
        onHaySold -= OnHaySold;
    }


	void Start()
    {
        textStartScale = stackLoadText.transform.localScale;
        spriteStartScale = haySprite.transform.localScale;

        maxStackLoad = inventory.maxItems;
        stackLoadText.text = "0/" + maxStackLoad;

        stringBuilder = new StringBuilder();
    }

    private void UpdateStackLoadText()
	{
        stringBuilder.Clear();
        stringBuilder.Append($"{inventory.hayPacks.Count}/{maxStackLoad}");
        stackLoadText.text = stringBuilder.ToString();
	}

    private void OnHayPickup()
	{
        var spriteTransform = haySprite.transform;
        spriteTransform.DOKill();
        spriteTransform.transform.localScale = spriteStartScale;

        spriteTransform.transform.DOScale(spriteStartScale + spriteScaleModifiers, animationDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            spriteTransform.transform.DOScale(spriteStartScale, animationDuration).SetEase(Ease.InOutCubic);
        });
    }

    private void OnHaySold()
    {
        var spriteTransform = haySprite.transform;
        spriteTransform.DOKill();
        spriteTransform.transform.localScale = spriteStartScale;

        spriteTransform.transform.DOScale(spriteStartScale - spriteScaleModifiers, animationDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            spriteTransform.transform.DOScale(spriteStartScale, animationDuration).SetEase(Ease.InOutCubic);
        });
    }

    private void ShakeText()
	{
        var textTransform = stackLoadText.transform;
        textTransform.DOKill();
        textTransform.transform.localScale = textStartScale;

        textTransform.transform.DOScale(textTargetScale, animationDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
		{
            textTransform.transform.DOScale(textStartScale, animationDuration).SetEase(Ease.InOutCubic);
        });

    }
}
