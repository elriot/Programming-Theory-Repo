using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 3f;
    public bool isMovable = false;
    // public int level; // 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void Update()
    {
        if (isMovable && Input.GetKeyDown(KeyCode.Space))
        {
            Drop();
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
        transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void Drop()
    {
        rb.useGravity = true;
        isMovable = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        BallController otherBall = collision.gameObject.GetComponent<BallController>();

        if (otherBall != null && gameObject.tag == otherBall.tag && this.GetInstanceID() < otherBall.GetInstanceID())
        {
            Vector3 mergePosition = (transform.position + otherBall.transform.position) / 2;
            int nextLevel = GetBallIndexByTag(gameObject.tag) + 1;

            GameManager.Instance.SpawnMergeBall(mergePosition, nextLevel);

            // 기존 볼 파괴
            Destroy(gameObject);
            Destroy(otherBall.gameObject);
        }
    }

	private int GetBallIndexByTag(string tag)
    {
        if (string.IsNullOrEmpty(tag) || !tag.Contains("_"))
        {
            Debug.LogError($"Invalid tag format: {tag}");
            return -1;
        }

        string indexString = tag.Substring(tag.LastIndexOf("_") + 1);
        return int.TryParse(indexString, out int index) ? index : -1;
    }
}