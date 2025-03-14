using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
	private Rigidbody rb;
	public float moveSpeed = 3f;
	private GameObject cameraContainer;
	public bool isMovable = true;
	public bool isDropped = false;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
		rb.useGravity = false;

		cameraContainer = GameObject.Find("Camera");
		if (cameraContainer == null)
		{
			Debug.LogError("Camera container not found!");
		}
    }

    void FixedUpdate()
	{
		MoveBall();

		if (isMovable && Input.GetKeyDown(KeyCode.Space))
		{
			Drop();
		}
	}

	private void MoveBall()
	{
		if(!isMovable) return;
		
		float horizontalInput = Input.GetAxis("Arrow Horizontal"); // Left, Right Arrow
		float verticalInput = Input.GetAxis("Arrow Vertical");     // Up, Down Arrow

		Vector3 forward = cameraContainer.transform.forward;
		Vector3 right = cameraContainer.transform.right;

		// Only move horizontally (ignore Y axis)w
		forward.y = 0f;
		right.y = 0f;

		forward.Normalize();
		right.Normalize();

		Vector3 movement = (right * horizontalInput + forward * verticalInput).normalized;

		// Move the Rigidbody to avoid physics conflicts
		transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
		// rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
	}

	void Drop()
	{
		rb.useGravity = true;
		isMovable = false;
		isDropped = true;
	}
}
