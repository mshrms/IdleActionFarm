using System;
using UnityEngine;
using static EventsNamespace.EventBus;

public class Farmer : MonoBehaviour
{
	public float attackTime;
	public int moneyCount;


    private bool isAttacking = false;
	private float remainingAttackTime;
	private FarmerController farmerController;


	private void OnEnable()
	{
		onWheatFound += OnWheatFound;

		onAttackStart += AttackStart;
		onAttackStop += AttackStop;
	}

	private void OnDisable()
	{
		onWheatFound -= OnWheatFound;

		onAttackStart -= AttackStart;
		onAttackStop -= AttackStop;
	}

	void Start()
    {
		farmerController = GetComponent<FarmerController>();   
    }

	void Update()
	{
		CheckForAttackEnd();
	}

	private void CheckForAttackEnd()
	{
		if (isAttacking)
		{
			remainingAttackTime -= Time.deltaTime;
			if (remainingAttackTime < 0)
			{
				isAttacking = false;
				onAttackStop?.Invoke();
			}
		}
	}

	private void OnWheatFound()
	{
		if (!isAttacking && !farmerController.joystickIsActive)
		{
			isAttacking = true;
			remainingAttackTime = attackTime;

			onAttackStart?.Invoke();
		}
	}

	public void AttackStart()
	{

	}
	public void AttackStop()
	{

	}
}
