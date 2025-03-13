using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float MoveSpeed = 0.5f;      // Speed for lateral movement
    public float RotationSpeed = 50f;   // Speed for rotation
    public float ZoomSpeed = 2f;        // Speed for zooming in and out
    public float MinZoom = 2f;          // Minimum distance to prevent too close zoom
    public float MaxZoom = 10f;         // Maximum distance to prevent too far zoom
    public GameObject Container;        // Target object to orbit around
    private Vector3 offset;             // Distance offset between camera and target
    private Vector3 initPos;            // Initial position of the camera

    void Start()
    {
        initPos = transform.position;
        offset = transform.position - Container.transform.position;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        RotateAroundTarget(horizontalInput);

        float verticalInput = Input.GetAxis("Vertical");
        Zoom(verticalInput);

    	if (Input.GetKeyDown(KeyCode.R))// Reset camera position
        {
            transform.position = initPos;
            offset = transform.position - Container.transform.position;
            transform.LookAt(Container.transform);
        }
    }

    private void RotateAroundTarget(float direction)
    {
        transform.RotateAround(Container.transform.position, Vector3.up, RotationSpeed * direction * Time.deltaTime);
        offset = transform.position - Container.transform.position;
        transform.LookAt(Container.transform);
    }

    private void Zoom(float input)
    {
        float distance = offset.magnitude;
        distance -= input * ZoomSpeed * Time.deltaTime;
        distance = Mathf.Clamp(distance, MinZoom, MaxZoom); // Clamp the zoom distance

        offset = offset.normalized * distance;
        transform.position = Container.transform.position + offset;
        transform.LookAt(Container.transform);
    }
}