using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * SwitchCameraController script handle switching camera 
 * especially used in loading canvas to do camera
 * switching, change device screen orientation, and
 * doing player teleportation.
 */
public class SwitchCameraController : MonoBehaviour
{
    public static int Manager;

    public void ChangeCamera()
    {
        // Trigger a parameter called "Change" in animator
        GetComponent<Animator>().SetTrigger("Change");
    }

    public static void ManageCamera()
    {
        switch (Manager)
        {
            case 0: // Change camera to Free View
                Manager = 1;

                // Unload current scene
                SceneManager.UnloadSceneAsync("HomeScene");

                Screen.orientation = ScreenOrientation.LandscapeLeft;

                // Load Free view scene
                SceneManager.LoadSceneAsync("FreeViewScene", LoadSceneMode.Additive);

                break;

            case 1: // Change camera to AR
                Manager = 0;

                // Unload current scene
                SceneManager.UnloadSceneAsync("FreeViewScene");

                Screen.orientation = ScreenOrientation.Portrait;

                // Load Home scene
                SceneManager.LoadSceneAsync("HomeScene", LoadSceneMode.Additive);

                break;
        }
    }
}
