using UnityEngine;

/*
 * SwitchCameraController script handle switching camera 
 * especially used in loading canvas to do camera
 * switching, change device screen orientation, and
 * doing player teleportation.
 */
public class SwitchCameraController : MonoBehaviour
{
    public GameObject[] CameraList;
    public int Manager;

    [Header("Player Attributes")]
    public PlayerManager PlayerManager;

    [Header("Canvas")]
    public GameObject ARCanvas;
    public GameObject FreeViewCanvas;

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

                ARCanvas.SetActive(false);

                Screen.orientation = ScreenOrientation.LandscapeLeft;

                FreeViewCanvas.SetActive(true);

                break;

            case 1: // Change camera to AR
                SwitchCamera(1, 0);
                Manager = 0;

                PlayerManager.ResetPlayerPosition();

                FreeViewCanvas.SetActive(false);

                Screen.orientation = ScreenOrientation.Portrait;

                ARCanvas.SetActive(true);

                break;
        }
    }

    private void SwitchCamera(int fromIdx, int toIdx)
    {
        CameraList[fromIdx].SetActive(false);
        CameraList[toIdx].SetActive(true);
    }
}
