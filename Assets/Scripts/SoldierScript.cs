using UnityEngine;
using System.Collections;

public class SoldierScript : MonoBehaviour {

    public Vector2 speed = new Vector2(1, 0);

	public LayerMask whatIsGround;

	// -1 = left; 1 = right;
	public int direction = -1;

    private Vector2 movement;
	
	private float distToGround;
	private float distToEdge;

	private Animator anim;


	void Start()
	{
		distToGround = collider2D.bounds.extents.y;
		distToEdge = collider2D.bounds.extents.x;

		anim = GetComponent<Animator> ();

		if (direction == -1)
		{
			anim.SetBool ("facingRight", false);
		}
	}

    void Update()
    {
        if (!isGrounded() || hitsWall ()) 
		{
			direction = -direction;
			anim.SetBool("facingRight",!anim.GetBool("facingRight"));
		}
        movement = new Vector2(speed.x * direction, rigidbody2D.velocity.y);
		anim.SetFloat ("speed", Mathf.Abs(speed.x));

    }

    void FixedUpdate()
    {
        rigidbody2D.velocity = movement;
    }

    bool isGrounded() 
	{
		return Physics2D.Raycast(new Vector2(transform.position.x + distToEdge * direction, transform.position.y), -Vector2.up, distToGround + 0.1f, whatIsGround);
    }

	bool hitsWall()
	{
		if(direction == -1)
			return Physics2D.Raycast(new Vector2(transform.position.x + distToEdge * direction, transform.position.y), -Vector2.right,  0.1f, whatIsGround);
		else
			return Physics2D.Raycast(new Vector2(transform.position.x + distToEdge * direction, transform.position.y), Vector2.right,  0.1f, whatIsGround);
	}


}
