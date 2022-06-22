using UnityEngine;
using System.Text;
using TMPro;
using DG.Tweening;
using static EventsNamespace.EventBus;

public class StackLoadCounter : MonoBehaviour
{
    public float animationDuration;
    public Vector3 textScaleModifiers;
    public Vector3 spriteScaleModifiers;

    [SerializeField] private FarmerInventory inventory;
    [SerializeField] private TMP_Text stackLoadText;
    [SerializeField] private Transform haySprite;

    private float maxStackLoad;
    private StringBuilder stringBuilder;
    private Vector3 textStartScale;
    private Vector3 spriteStartScale;
    private Color textStartColor;

    private void OnEnable()
	{
        onHayPickup += UpdateStackLoadText;
        onHaySold += UpdateStackLoadText;

        onHayPickup += ScaleTextUp;
        onHaySold += ScaleTextDown;

        onHayPickup += OnHayPickup;
        onHaySold += OnHaySold;
    }

	private void OnDisable()
	{
        onHayPickup -= UpdateStackLoadText;
        onHaySold -= UpdateStackLoadText;

        onHayPickup -= ScaleTextUp;
        onHaySold -= ScaleTextDown;

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

        textStartColor = stackLoadText.color;
    }

    private void UpdateStackLoadText()
	{
        stringBuilder.Clear();
        stringBuilder.Append($"{inventory.hayPacks.Count}/{maxStackLoad}");
        stackLoadText.text = stringBuilder.ToString();

        if (inventory.hayPacks.Count == maxStackLoad)
		{
            stackLoadText.color = Color.red;
		}
		else
		{
            stackLoadText.color = textStartColor;
		}
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

    private void ScaleTextUp()
	{
        var textTransform = stackLoadText.transform;
        textTransform.DOKill();
        textTransform.transform.localScale = textStartScale;

        textTransform.transform.DOScale(textStartScale + textScaleModifiers, animationDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
		{
            textTransform.transform.DOScale(textStartScale, animationDuration).SetEase(Ease.InOutCubic);
        });

    }

    private void ScaleTextDown()
    {
        var textTransform = stackLoadText.transform;
        textTransform.DOKill();
        textTransform.transform.localScale = textStartScale;

        textTransform.transform.DOScale(textStartScale - textScaleModifiers, animationDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            textTransform.transform.DOScale(textStartScale, animationDuration).SetEase(Ease.InOutCubic);
        });

    }
}
