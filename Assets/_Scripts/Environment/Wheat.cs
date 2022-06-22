using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static EventsNamespace.EventBus;

public class Wheat : MonoBehaviour
{
    [Header("/// Growth Settings ///")]
    public float growthTime;
    public float growingYOffset;
    public int sparkleParticleCount;
    [HideInInspector] public bool isGrowing = false;
    public List<Mesh> growStages;
    [SerializeField] private MeshFilter wheatMesh;
    private Vector3 startScale;
    private Vector3 startPosition;
    private int currentLife = 3;
    [SerializeField] private ParticleSystem sparkleParticleSystem;

    [Header("/// On Hit Settings ///")]
    public int wheatHitParticleCount;
    public float shakeIntensity;
    public float shakeDuration;
    public int shakeVibrato;
    public float shakeStrength;
    public float shakeRandomness;
    [SerializeField] private ParticleSystem wheatCutParticleSystem;
    private HayPackDropper hayPackDropper;

    private void Start()
	{
        startScale = wheatMesh.transform.localScale;
        startPosition = wheatMesh.transform.localPosition;

        hayPackDropper = GetComponent<HayPackDropper>();
    }

	public void CutWheat()
	{
        wheatCutParticleSystem.Emit(wheatHitParticleCount);

        transform.DOKill();

        onWheatCut?.Invoke();
        hayPackDropper.DropHayPack();

        wheatMesh.transform.DOShakeScale(shakeDuration, shakeStrength, shakeVibrato, shakeRandomness).SetEase(Ease.OutSine);

        currentLife--;
        wheatMesh.mesh = growStages[currentLife];

        if (currentLife <= 0)
		{
            isGrowing = true;

            onGrowthStart?.Invoke();

            StartGrowth();
		}
	}

    private void StartGrowth()
    {
        if (isGrowing)
        {
            Growth();
        }
    }

    private void Growth()
	{
        Vector3 meshPosition = startPosition;

        wheatMesh.transform.localScale = new Vector3(startScale.x, 0.1f, startScale.z);
        wheatMesh.transform.DOScaleY(startScale.y, growthTime).SetEase(Ease.InOutSine).OnComplete(() =>
		{
            onGrowthStop?.Invoke();
            StopGrowth();
		}) ;

        float fromY = meshPosition.y + growingYOffset;
        float toY = startPosition.y;

        Vector3 fromMeshPosition = new Vector3(meshPosition.x, fromY, meshPosition.z);

        wheatMesh.transform.localPosition = fromMeshPosition;

        wheatMesh.transform.DOLocalMoveY(toY, growthTime).SetEase(Ease.InOutSine);
    }

    private void StopGrowth()
    {
        isGrowing = false;
        currentLife = 3;

        wheatMesh.transform.localScale = startScale;
        wheatMesh.transform.localPosition = startPosition;

        wheatMesh.mesh = growStages[currentLife];

        wheatMesh.transform.DOShakeScale(shakeDuration, shakeStrength, shakeVibrato * 2, shakeRandomness).SetEase(Ease.OutSine);

        sparkleParticleSystem.Emit(sparkleParticleCount);
    }
}
