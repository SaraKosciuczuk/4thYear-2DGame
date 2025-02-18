using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;	// How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings

	public int playerHealth = 6;
	public float collisionCooldown = 1f;
    private float lastCollisionTime = 0f;

	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private Animator animator;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;

	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }

	private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
    }

	void Start()
	{
		playerHealth = 6;

	}

	void Update()
	{
		if(playerHealth <= 0)
		{
			this.gameObject.SetActive(false);
			SceneManager.LoadScene("GameOVER");
		}
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}

	}

	public void Move(float move, bool crouch, bool jump)
	{

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				Flip();
			}
		}

		// Set the idle parameter to true when the player is not moving or jumping
    	animator.SetBool("idle", move == 0f && m_Grounded && !jump);

		// If the player should jump...
		if (m_Grounded && jump)
		{
    		m_Grounded = false;
    		m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

    		// Set the jumping parameter to true when the player jumps
    		animator.SetBool("jumping", true);
		}
		else
		{
    		// Set the jumping parameter to false when the player lands or is not jumping
    		animator.SetBool("jumping", false);

    		// Set the idle parameter to true when the player is not moving or jumping
    		animator.SetBool("idle", move == 0f && m_Grounded);
		}

		animator.SetBool("walking", move != 0f);

	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		transform.Rotate(0f, 180f, 0f);
	}

	public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("EnemyCollider") && Time.time >= lastCollisionTime + collisionCooldown)
        {
            Debug.Log("collided, -1 health");
            playerHealth -= 1;
			lastCollisionTime = Time.time;
        }

		if(collision.gameObject.CompareTag("EnemyDamager") && Time.time >= lastCollisionTime + collisionCooldown)
        {
            Debug.Log("collided, -1 health");
            playerHealth -= 1;
			lastCollisionTime = Time.time;
        }
    }
}
