using UnityEngine;
using static EventsNamespace.EventBus;

public class AnimationStateController : MonoBehaviour
{
    Animator animator;

	private void OnEnable()
	{
		//RUNNING
		onRunStart += StartRunningAnimation;
		onRunStop += StopRunningAnimation;

		//ATTACKING
		onAttackStart += StartAttackingAnimation;
		onAttackStop += StopAttackingAnimation;
	}

	private void OnDisable()
	{
		//RUNNING
		onRunStart -= StartRunningAnimation;
		onRunStop -= StopRunningAnimation;

		//ATTACKING
		onAttackStart -= StartAttackingAnimation;
		onAttackStop -= StopAttackingAnimation;
	}

	void Start()
    {
        animator = GetComponent<Animator>();
    }

    void StartRunningAnimation()
	{
		animator.SetBool("isRunning", true);
	}

	void StopRunningAnimation()
	{
		animator.SetBool("isRunning", false);
	}

	void StartAttackingAnimation()
	{
		animator.SetBool("isAttacking", true);
	}
	void StopAttackingAnimation()
	{
		animator.SetBool("isAttacking", false);
	}
}
