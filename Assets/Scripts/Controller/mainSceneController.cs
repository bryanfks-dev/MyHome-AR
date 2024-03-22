using UnityEngine;
using UnityEngine.SceneManagement;

public class mainSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        JsonFile.initJson();

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
