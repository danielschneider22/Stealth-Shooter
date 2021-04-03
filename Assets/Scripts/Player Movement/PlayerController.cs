using System.Collections.Generic;
using MLAPI;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : NetworkBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement

	private Rigidbody2D m_Rigidbody2D;
	public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	public Animator animator;
	private Vector3 m_Velocity = Vector3.zero;

	public ParticleSystem footstepsParticleSystem;
	public GameObject footstepsPosition;

	public SpriteRenderer spriteRenderer;

	public Animator feetAnimator;

	public Transform gunPosition;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private AudioManager audioManager;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		audioManager = FindObjectOfType<AudioManager>();
	}

	public void Move(float moveHorizontal, float moveVertical)
	{
		//only control the player if grounded or airControl is turned on
		// Move the character by finding the target velocity
		Vector3 targetVelocity = new Vector2(moveHorizontal * 10f, moveVertical * 10f);
		// And then smoothing it out and applying it to the character
		m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

		if(moveHorizontal == 0 && moveVertical == 0)
        {
			feetAnimator.speed = 0;
		} else
        {
			feetAnimator.speed = 1;
        }

	}

	public void RotateTowardsMouse()
    {
		Vector3 mousePos = Input.mousePosition;
		Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
		if(Vector2.Distance(worldMousePos, gunPosition.position) > 2f)
        {
			Vector3 vectorToTarget = new Vector3(worldMousePos.x + 1.25f, worldMousePos.y - 1.25f, 0) - gunPosition.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = q;
		}
		
	}

	public void RotateWithJoystick(Joystick joystick)
	{
		/*Vector3 mousePos = Input.mousePosition;
		Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
		if (Vector2.Distance(worldMousePos, gunPosition.position) > 2f)
		{
			Vector3 vectorToTarget = new Vector3(worldMousePos.x + 1.25f, worldMousePos.y - 1.25f, 0) - gunPosition.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = q;
		}*/
		float angle = Mathf.Atan2(joystick.Direction.y, joystick.Direction.x) * Mathf.Rad2Deg;
		if(angle != 0)
        {
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = q;
		}
		
		// transform.rotation = Quaternion.LookRotation(new Vector3(joystick.Horizontal, joystick.Vertical, 0));

	}


}