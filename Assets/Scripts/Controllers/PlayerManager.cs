using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class PlayerManager : MonoBehaviour
{
    public FixedJoystick Joystick;
    public Rigidbody Player;
    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Player.velocity = new Vector3(Joystick.Horizontal * MoveSpeed,
               Player.velocity.y, Joystick.Vertical * MoveSpeed);
    }
}
