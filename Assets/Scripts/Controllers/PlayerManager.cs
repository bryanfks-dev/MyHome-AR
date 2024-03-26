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
    public FixedJoystick MoveJoystick;
    public float MoveSpeed;

    public float ViewSpeed;
    public TouchFieldController TouchFieldController;

    private static GameObject playerGO;
    private Rigidbody playerRigid;
    public Camera PlayerCamera;

    private static Vector3 initPos;
    public FreeViewManager FreeViewManager;

    private float xRotate;
    private float yRotate;

    // Start is called before the first frame update
    void Start()
    {
        playerGO = gameObject;
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

        // Add Gravity
        playerRigid.AddForce((playerRigid.mass * playerRigid.mass) * 10 * Physics.gravity);
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
            modelTransform.position.y + 100f, modelTransform.position.z);

        ChangePos(newPos);
    }

    public void ResetPlayerPosition()
    {
        ChangePos(initPos);
    }
}
