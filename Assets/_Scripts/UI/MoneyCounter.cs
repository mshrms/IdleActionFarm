using UnityEngine;
using TMPro;
using System.Text;
using DG.Tweening;
using static EventsNamespace.EventBus;

public class MoneyCounter : MonoBehaviour
{
    public int hayPrice;
    public float updateSpeed;
    public float coinMoveSpeed;

    public float animationDuration;
    public Vector3 textScaleModifiers;
    public Vector3 spriteScaleModifiers;

    [SerializeField] private TMP_Text moneyCountText;
    [SerializeField] private Transform coinSpawnerTransform;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private RectTransform coinSprite;
    [SerializeField] private Canvas canvas;

    private int moneyBalance = 0;
    private StringBuilder stringBuilder;
    private int currentBalance;
    private Vector3 textStartScale;
    private Vector3 spriteStartScale;
    private RectTransform canvasRect;

    private void OnEnable()
    {
        onHaySold += SendCoinToCounter;
    }

    private void OnDisable()
    {
        onHaySold -= SendCoinToCounter;
    }

    void Start()
    {
        textStartScale = moneyCountText.transform.localScale;
        spriteStartScale = coinSprite.transform.localScale;

        moneyCountText.text = "0";

        stringBuilder = new StringBuilder();

        canvasRect = canvas.GetComponent<RectTransform>();

    }

    private void SendCoinToCounter()
    {
        GameObject coinInstance = Instantiate(coinPrefab, canvasRect);
        coinInstance.transform.SetAsFirstSibling();

        RectTransform coinRectTransform = coinInstance.GetComponent<RectTransform>();
        coinRectTransform.anchoredPosition = CalculateScreenPosition();

        Vector3 coinStartScale = coinRectTransform.localScale;
        coinRectTransform.localScale = Vector3.zero;
        coinRectTransform.DOScale(coinStartScale, coinMoveSpeed).SetEase(Ease.OutCubic);

        coinRectTransform.DOMove(coinSprite.position, coinMoveSpeed).SetEase(Ease.InExpo).OnComplete(() =>
		{
            UpdateMoneyCountText();
            ScaleMoneyText();
            ScaleSprite();

            DeleteCoin(coinInstance);
        });
    }

    private void DeleteCoin(GameObject coinInstance)
	{
        DOTween.Kill(coinInstance);
        Destroy(coinInstance);
    }

    private Vector2 CalculateScreenPosition()
	{
        Vector2 viewportPosition = Camera.main.WorldToViewportPoint(coinSpawnerTransform.position);

        Vector2 coinSpawnerScreenPosition = new Vector2(
            (viewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
            (viewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f)
            );

        return coinSpawnerScreenPosition;
    }

    private void UpdateMoneyCountText()
    {
        moneyBalance += hayPrice;

        DOTween.Kill(this);
        DOTween.To(() => currentBalance, x => currentBalance = x, moneyBalance, updateSpeed).SetEase(Ease.OutCubic).OnUpdate(() =>
		{
            stringBuilder.Clear();
            stringBuilder.Append(currentBalance);

            moneyCountText.text = stringBuilder.ToString();
        });

    }

    private void ScaleSprite()
    {
        var spriteTransform = coinSprite.transform;
        spriteTransform.DOKill();
        spriteTransform.transform.localScale = spriteStartScale;

        spriteTransform.transform.DOScale(spriteStartScale + spriteScaleModifiers, animationDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            spriteTransform.transform.DOScale(spriteStartScale, animationDuration).SetEase(Ease.InOutCubic);
        });
    }

    private void ScaleMoneyText()
    {
        var textTransform = moneyCountText.transform;
        textTransform.DOKill();
        textTransform.transform.localScale = textStartScale;

        textTransform.transform.DOScale(textStartScale + textScaleModifiers, animationDuration).SetEase(Ease.InOutCubic).OnComplete(() =>
        {
            textTransform.transform.DOScale(textStartScale, animationDuration).SetEase(Ease.InOutCubic);
        });
    }
}
