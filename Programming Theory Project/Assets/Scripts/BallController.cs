using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
	public Rigidbody rb;
	public float moveSpeed = 3f;
	public bool isDropped;
	// private float lastInputTime = 0f;
    // private float inputCooldown = 2f;
	public string BallName {get; set;}

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
	}

	void Update()
	{
        // if (!isDropped && Input.GetKeyDown(KeyCode.Space) && ((lastInputTime == 0f) || (Time.time - lastInputTime >= inputCooldown)))
        // {
        //     Drop();
        //     lastInputTime = Time.time;
        // }
	}

	// void FixedUpdate()
	// {
	// 	MoveBall();
	// }

	// private void MoveBall()
	// {
	// 	if (isDropped) return;

	// 	float horizontalInput = Input.GetAxis("Arrow Horizontal");
	// 	float verticalInput = Input.GetAxis("Arrow Vertical");

	// 	Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
	// 	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	// }

	// private void Drop()
	// {
	// 	rb.useGravity = true;
	// 	isDropped = true;
	// }

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.CompareTag("Wall"))
			return;

		BallController otherBall = collision.gameObject.GetComponent<BallController>();

		if (otherBall != null && gameObject.tag == otherBall.tag && gameObject.GetInstanceID() > otherBall.GetInstanceID())
		{
			Debug.Log("Merge");
			Vector3 mergePosition = (transform.position + otherBall.transform.position) / 2;
			int nextLevel = GameManager.Instance.GetBallIndexByTag(gameObject.tag) + 1;

			GameManager.Instance.SpawnMergeBall(mergePosition, nextLevel);
			
			Debug.Log($"collision curr {gameObject.name}, collision obj {collision.gameObject.name} ");
			Destroy(gameObject);
			Destroy(otherBall.gameObject);
			GameManager.Instance.BallMovementCompleted(this);
		}
		else if(collision.gameObject.CompareTag("Floor") || (otherBall != null && otherBall.isDropped && isDropped))
		{
			Debug.Log("collised with ball or floor");
			GameManager.Instance.BallMovementCompleted(this);
		}
		else
		{
			Debug.Log("Nothing happened");
		}
	}

	public void Drop()
	{
		// if(rb == null) return;
		isDropped = true;
		if(rb == null)
		{
			Debug.Log("no rigid body");
			return;
		}
		rb.useGravity = true;
	}
}