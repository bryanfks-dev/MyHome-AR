using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Main Scene is a scene that acting like a "bridge" between home page scene and 
 * intro page scene, other than that, main scene could also be the selector of 
 * these 2 scene to initializing and start the application.
 */
public class MainSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        JsonFile.InitJson();

        // Check if user has passes intro scene
        if (JsonFile.data.passed_intro_screen)
        {
            SceneManager.LoadScene("HomeScene", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene("IntroScene", LoadSceneMode.Additive);
        }
    }
}
