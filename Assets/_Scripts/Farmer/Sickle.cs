using System.Collections.Generic;
using UnityEngine;
using static EventsNamespace.EventBus;

public class Sickle : MonoBehaviour
{
	public float colliderActivationOffset;

	[SerializeField] private TrailRenderer sickleTrail;
	private Collider sickleCollider;
	private MeshRenderer sickleRenderer;
	private List<Collider> collisionsDuringAttack;

	private void OnEnable()
	{
		onAttackStart += SickleAttackStart;
		onAttackStop += SickleAttackStop;
	}

	private void OnDisable()
	{
		onAttackStart -= SickleAttackStart;
		onAttackStop -= SickleAttackStop;
	}

	void Start()
	{
		sickleCollider = GetComponent<Collider>();
		sickleRenderer = GetComponent<MeshRenderer>();

		sickleCollider.enabled = false;
		sickleRenderer.enabled = false;
		sickleTrail.emitting = false;


		collisionsDuringAttack = new List<Collider>();
	}

	void SickleAttackStart()
	{
		sickleRenderer.enabled = true;
		Invoke("StartAttackWithAppliedOffset", colliderActivationOffset);
	}

	void StartAttackWithAppliedOffset()
	{
		sickleCollider.enabled = true;
		sickleTrail.emitting = true;
	}

	void SickleAttackStop()
	{
		sickleCollider.enabled = false;
		sickleTrail.emitting = false;
		sickleRenderer.enabled = false;

		collisionsDuringAttack.Clear();
	}


	private void OnTriggerEnter(Collider other)
	{
		bool matched = IsRepeatingCollision(other);

		if (!matched)
		{
			Wheat wheatComponent = other.GetComponent<Wheat>();

			collisionsDuringAttack.Add(other);

			if (wheatComponent.isGrowing == false)
			{
				wheatComponent.CutWheat();
			}

		}
	}

	private bool IsRepeatingCollision(Collider colliderToCheck)
	{
		if (collisionsDuringAttack.Count > 0)
		{
			foreach (Collider registeredCollider in collisionsDuringAttack)
			{
				if (colliderToCheck == registeredCollider)
				{
					return true;
				}
			}
		}

		return false;
	}
}
