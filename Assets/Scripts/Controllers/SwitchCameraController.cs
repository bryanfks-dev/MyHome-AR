using UnityEngine;

public class SwitchCameraController : MonoBehaviour
{
    public GameObject[] CameraList;
    public int Manager;

    public PlayerManager PlayerManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCamera()
    {
        // Trigger a parameter called "Change" in animator
        GetComponent<Animator>().SetTrigger("Change");
    }

    public void ManageCamera()
    {
        switch (Manager)
        {
            case 0: // Change camera to Free View
                SwitchCamera(0, 1);
                Manager = 1;

                PlayerManager.TeleportPlayerToHouse();

                Screen.orientation = ScreenOrientation.LandscapeLeft;

                break;

            case 1: // Change camera to AR
                SwitchCamera(1, 0);
                Manager = 0;

                PlayerManager.ResetPlayerPosition();

                Screen.orientation = ScreenOrientation.Portrait;

                break;
        }
    }

    private void SwitchCamera(int fromIdx, int toIdx)
    {
        CameraList[fromIdx].SetActive(false);
        CameraList[toIdx].SetActive(true);
    }
}
