using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
	private Rigidbody rb;
	public float moveSpeed = 3f;
	public bool isDropped;
	public int Point;
	// private float lastInputTime = 0f;
	// private float inputCooldown = 2f;
	public string BallName { get; set; }
	private GameManager gameManager;

	void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}
	void Start()
	{
		rb.useGravity = isDropped;
		gameManager = GameManager.Instance;
	}

	void Update()
	{
		// if (!isDropped && Input.GetKeyDown(KeyCode.Space) && ((lastInputTime == 0f) || (Time.time - lastInputTime >= inputCooldown)))
		// {
		//     Drop();
		//     lastInputTime = Time.time;
		// }
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Wall"))
			return;

		BallController otherBall = collision.gameObject.GetComponent<BallController>();

		if (otherBall != null && gameObject.tag == otherBall.tag && gameObject.GetInstanceID() > otherBall.GetInstanceID())
		{
			//Debug.Log("Merge!");
			Vector3 mergePosition = (transform.position + otherBall.transform.position) / 2;
			int nextLevel = gameManager.GetBallIndexByTag(gameObject.tag) + 1;

			if(nextLevel < GameManager.Instance.GetLastLevel())
			{
				gameManager.SpawnMergeBall(mergePosition, nextLevel);

				// Debug.Log($"collision curr {gameObject.name}, collision obj {collision.gameObject.name} ");
				Destroy(gameObject);
				Destroy(otherBall.gameObject);
				gameManager.BallMovementCompleted(this);
			}
		}
		else if (collision.gameObject.CompareTag("Floor") || (otherBall != null && otherBall.isDropped && isDropped))
		{
			// Debug.Log("collised with ball or floor");
			gameManager.BallMovementCompleted(this);
		}
		// else if(collision.gameObject.CompareTag("GameOverLine"))
		// {
		// 	Debug.LogWarning("GAME OVER!!!");
		// }
	}

	public void Drop()
	{
		isDropped = true;

		if (rb == null)
		{
			//Debug.LogError("Rigidbody is missing!");
			return;
		}

		rb.useGravity = true;
	}
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log($"OnTriggerEnter !! {other.gameObject.tag}, currentBall : {gameObject.tag}");
		if(other.CompareTag("GameOverLine") && !gameManager.isCurrentBall(gameObject))
		{
			gameManager.GameOver();
		}
	}
}