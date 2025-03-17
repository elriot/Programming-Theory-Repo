using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
	private Rigidbody rb;
	public float moveSpeed = 3f;
	public bool isMovable = false;
	private float lastInputTime = 0f;
    private float inputCooldown = 2f;
	public string BallName {get; set;}

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.useGravity = false;
	}

	void Update()
	{
        if (isMovable && Input.GetKeyDown(KeyCode.Space) && ((lastInputTime == 0f) || (Time.time - lastInputTime >= inputCooldown)))
        {
            Drop();
            lastInputTime = Time.time;
        }
	}

	void FixedUpdate()
	{
		MoveBall();
	}

	private void MoveBall()
	{
		if (!isMovable) return;

		float horizontalInput = Input.GetAxis("Arrow Horizontal");
		float verticalInput = Input.GetAxis("Arrow Vertical");

		Vector3 movement = new Vector3(horizontalInput, 0, verticalInput).normalized;
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}

	private void Drop()
	{
		rb.useGravity = true;
		isMovable = false;
		GameManager.Instance.SpawnBall();
	}

	private void OnCollisionEnter(Collision collision)
	{
		BallController otherBall = collision.gameObject.GetComponent<BallController>();

		if (otherBall != null && gameObject.tag == otherBall.tag && gameObject.GetInstanceID() > otherBall.GetInstanceID())
		{
			Vector3 mergePosition = (transform.position + otherBall.transform.position) / 2;
			int nextLevel = GameManager.Instance.GetBallIndexByTag(gameObject.tag) + 1;

			GameManager.Instance.SpawnMergeBall(mergePosition, nextLevel);
			GameManager.Instance.BallMovementCompleted(this);
			Debug.Log($"collision curr {gameObject.name}, collision obj {collision.gameObject.name} ");
			Destroy(gameObject);
			Destroy(otherBall.gameObject);
		}
		else if(collision.gameObject.CompareTag("Floor"))
		{
			Debug.Log($"hit the floot {BallName}");
			GameManager.Instance.BallMovementCompleted(this);
		}
	}
}