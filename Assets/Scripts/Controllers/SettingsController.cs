using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    public PlayerManager PlayerManager;

    [Header("Configs")]
    public GameObject[] ConfigWrappers;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SaveConfigs()
    {
        /*
        * Inside each config wrapper, there are always gonna
        * be text and slider, and text always gonna be the 
        * first child, then followed up by slider.
        * Config indexes:
        * 1. FOV
        * 2. View Sensitivity
        */
        for (int i = 0; i < ConfigWrappers.Length; i++)
        {
            Slider slider = ConfigWrappers[i].transform.GetChild(1).GetComponent<Slider>();

            switch (i)
            {
                case 0:
                    JsonFile.WriteFOV(slider.value);
                    break;

                case 1:
                    JsonFile.WriteViewSens(slider.value);
                    break;
            }
        }
    }

    public void CloseBtnHandler()
    {
        SaveConfigs();
        PlayerManager.LoadConfigs();

        gameObject.SetActive(false);
    }

    public void UpdateSliderValue(int id)
    {
        GameObject currGO = ConfigWrappers[id];

        Text configText = currGO.transform.GetChild(0).GetComponent<Text>();
        Slider slider = currGO.transform.GetChild(1).GetComponent<Slider>();

        // Replace "(...)" with current slider value using regex
        configText.text = Regex.Replace(configText.text, @"\(.*?\)", $"({(int)slider.value})");
    }
}