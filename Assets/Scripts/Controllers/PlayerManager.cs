using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerManager : MonoBehaviour
{
    public FixedJoystick MoveJoystick;
    public float MoveSpeed;

    public float ViewSpeed;
    public TouchFieldController TouchFieldController;

    public Camera PlayerCamera;

    private Rigidbody playerRigid;

    static private Vector3 initPos;

    private float xRotate;
    private float yRotate;

    // Start is called before the first frame update
    void Start()
    {
        playerRigid = GetComponent<Rigidbody>();

        initPos = ParsePos(transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (ParsePos(transform.position) != initPos)
        {
            // Player movement handler
            playerRigid.velocity = transform.right * MoveJoystick.Horizontal * MoveSpeed + 
                transform.forward * MoveJoystick.Vertical * MoveSpeed;

            // Camera and player body rotation handler
            yRotate += TouchFieldController.TouchDist.x * ViewSpeed * Time.deltaTime;
            xRotate -= TouchFieldController.TouchDist.y * ViewSpeed * Time.deltaTime;

            // Prevent player to rotate more than 90 deg on vertical axis
            xRotate = Mathf.Clamp(xRotate, -90f, 90f);

            // Rotate player on to horizontal axis
            playerRigid.rotation = Quaternion.Euler(yRotate * Vector3.up);

            PlayerCamera.transform.localRotation = Quaternion.Euler(Vector3.right * xRotate);
        }

        // Add Gravity
        playerRigid.AddForce(Physics.gravity * (playerRigid.mass * playerRigid.mass));
    }

    static public Vector3 GetPlayerInitPos() => initPos;

    private Vector3 ParsePos(Vector3 oldVal)
    {
        Vector3 newVal = Vector3.right * oldVal.x + Vector3.forward * oldVal.z;

        return newVal;
    }
}
