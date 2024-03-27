using UnityEngine;

/*
 * PlayerManager script handle any logic and physics
 * related to player and player camera, includes
 * movement logic, body and camera rotation logic, 
 * teleporation logic and even gravity physics logic.
 */
[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider))]
public class PlayerManager : MonoBehaviour
{
    [Header("Player Attributes")]
    private static GameObject playerGO;
    private Rigidbody playerRigid;
    private CapsuleCollider playerCollider;
    public GameObject IsGroundRay;
    public Camera PlayerCamera;

    [Header("Player Movement")]
    public FixedJoystick MoveJoystick;
    public float MoveSpeed;

    [Header("Player Rotation")]
    public float ViewSpeed;
    public TouchFieldController TouchFieldController;
    public FreeViewManager FreeViewManager;

    [Header("Player Step Climb")]
    public GameObject StepUpper;
    public GameObject StepLower;
    public float StepHeight;
    public float StepSmooth;

    private static Vector3 initPos;
    private float xRotate;
    private float yRotate;

    // Start is called before the first frame update
    void Start()
    {
        // HACK: Debugging purpose
        if (JsonFile.data == null)
        {
            JsonFile.InitJson();
        }

        playerGO = gameObject;
        playerRigid = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();

        LoadConfigs();

        StepUpper.transform.localPosition += Vector3.up * StepHeight;

        initPos = ParsePos(transform.position);
    }

    void FixedUpdate()
    {
        if (ParsePos(playerGO.transform.position) != initPos)
        {
            // Player movement handler
            playerRigid.velocity = MoveJoystick.Horizontal * MoveSpeed * transform.right + 
                MoveJoystick.Vertical * MoveSpeed * transform.forward;

            // Camera and player body rotation handler
            // Calculate x and y rotation value depends on given
            // sensitivity and how long user tap the screen
            yRotate += TouchFieldController.TouchDist.x * ViewSpeed * Time.deltaTime;
            xRotate -= TouchFieldController.TouchDist.y * ViewSpeed * Time.deltaTime;

            // Prevent player to rotate more than 90 deg and -90 deg on vertical axis
            xRotate = Mathf.Clamp(xRotate, -90f, 90f);

            // Rotate player on horizontal axis
            playerRigid.rotation = Quaternion.Euler(yRotate * Vector3.up);

            // Rotate camera on vertical axis
            PlayerCamera.transform.localRotation = Quaternion.Euler(Vector3.right * xRotate);
        }

        if (!IsGrounded()) 
        {
            // Add Gravity
            playerRigid.AddForce((playerRigid.mass * playerRigid.mass) * Physics.gravity);
        }

        StepClimb();
    }

    public void LoadConfigs()
    {
        PlayerCamera.fieldOfView = JsonFile.data.fov;
        ViewSpeed = JsonFile.data.view_sens;
    }

    private bool IsGrounded()
    {
        float playerHeight = playerCollider.height / 2;

        if (Physics.Raycast(IsGroundRay.transform.position, Vector3.down, out _, playerHeight))
        {
            return true;
        }

        return false;
    }

    private void StepClimb()
    {
        if (Physics.Raycast(StepLower.transform.position, transform.TransformDirection(Vector3.forward), out _  , 1f))
        {
            if (!Physics.Raycast(StepUpper.transform.position, transform.TransformDirection(Vector3.forward), out _, 2f))
            {
                playerRigid.position -= new Vector3(0, -StepSmooth * Time.deltaTime, 0);
            }
        }

        if (Physics.Raycast(StepLower.transform.position, transform.TransformDirection(1.5f, 0, 1), out _, 1f))
        {
            if (!Physics.Raycast(StepUpper.transform.position, transform.TransformDirection(1.5f, 0, 1), out _, 2f))
            {
                playerRigid.position -= new Vector3(0f, -StepSmooth * Time.deltaTime, 0f);
            }
        }

        if (Physics.Raycast(StepLower.transform.position, transform.TransformDirection(-1.5f, 0, 1), out _, 1f))
        {
            if (!Physics.Raycast(StepUpper.transform.position, transform.TransformDirection(-1.5f, 0, 1), out _, 2f))
            {
                playerRigid.position -= new Vector3(0f, -StepSmooth * Time.deltaTime, 0f);
            }
        }
    }

    private Vector3 ParsePos(Vector3 oldVal)
    {
        // ParsePos method, to get only x and z axis value
        // from a vector with y value = 0

        // Using addition of 2 vectors
        Vector3 newVal = Vector3.right * oldVal.x + Vector3.forward * oldVal.z;

        return newVal;
    }

    public void ChangePos(Vector3 pos)
    {
        playerGO.SetActive(false); // Deactivate player
        playerGO.transform.position = pos; // Change player position
        playerGO.SetActive(true); // Activate player back
    }

    public void TeleportPlayerToHouse()
    {
        // Get house center position
        Transform modelTransform = FreeViewManager.GetModel().transform;

        Vector3 newPos = new Vector3(modelTransform.position.x,
            modelTransform.position.y + 2f, modelTransform.position.z);

        ChangePos(newPos);

        Debug.Log($"{newPos} {playerGO.transform.position}");
    }

    public void ResetPlayerPosition()
    {
        ChangePos(initPos);
    }
}
