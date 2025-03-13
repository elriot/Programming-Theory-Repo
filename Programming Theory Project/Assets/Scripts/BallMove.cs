using UnityEngine;

public class BallMove : MonoBehaviour
{
	private Rigidbody ballRb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
		{
			ballRb.useGravity = true;
		}
    }
}
