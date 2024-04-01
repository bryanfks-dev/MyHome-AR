using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("Related Script(s)")]
    public PlayerManager PlayerManager;

    [Header("Configs")]
    public GameObject[] ConfigWrappers;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ConfigWrappers.Length; i++)
        {
            Text configText = ConfigWrappers[i].transform.GetChild(0).GetComponent<Text>();
            Slider configSlider = ConfigWrappers[i].transform.GetChild(1).GetComponent<Slider>();

            int value = 0;

            switch (i)
            {
                case 0:
                    value = (int)JsonFile.data.fov;
                    break;

                case 1:
                    value = (int)JsonFile.data.view_sens;
                    break;
            }

            configSlider.value = value;

            // Replace "(...)" with current slider value using regex
            configText.text = Regex.Replace(configText.text, @"\(.*?\)", $"({(int)value})");
        }
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

    public void UpdateSliderValue(int currWrapperId)
    {
        GameObject currGO = ConfigWrappers[currWrapperId];

        Text configText = currGO.transform.GetChild(0).GetComponent<Text>();
        Slider slider = currGO.transform.GetChild(1).GetComponent<Slider>();

        // Replace "(...)" with current slider value using regex
        configText.text = Regex.Replace(configText.text, @"\(.*?\)", $"({(int)slider.value})");
    }
}