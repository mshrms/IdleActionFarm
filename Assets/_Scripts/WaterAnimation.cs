using UnityEngine;
using DG.Tweening;

public class WaterAnimation : MonoBehaviour
{
    public float waterAnimationSpeed;
    public Vector3 moveDirection;

    void Start()
    {
        AnimateWater();
    }

    void AnimateWater()
	{
        transform.DOMove(transform.position + moveDirection, waterAnimationSpeed)
            .SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
