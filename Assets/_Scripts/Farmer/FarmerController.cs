using UnityEngine;
using static EventsNamespace.EventBus;

public class FarmerController : MonoBehaviour
{
    public float moveSpeed;

    [SerializeField] private Joystick joystick;
    private CharacterController characterController;
    [HideInInspector] public bool joystickIsActive;
    private Vector3 moveDirection;

	private void Start()
	{
        characterController = GetComponent<CharacterController>();
	}

	void Update()
    {
        moveDirection = CheckJoystickInput();
        MoveFarmer(moveDirection);
    }

	
	private Vector2 CheckJoystickInput()
    {
        if (joystick.Direction != Vector2.zero)
		{
            onRunStart?.Invoke();
            joystickIsActive = true;
        }
		else
		{
            onRunStop?.Invoke();
            joystickIsActive = false;
        }
        return joystick.Direction;
    }

    private void MoveFarmer(Vector2 direction)
    {
		if (joystickIsActive)
		{
            Vector3 moveVector = new Vector3(direction.x, 0, direction.y);
            moveVector *= moveSpeed;

            characterController.SimpleMove(moveVector);

            transform.forward = moveVector;
        }
    }
}
