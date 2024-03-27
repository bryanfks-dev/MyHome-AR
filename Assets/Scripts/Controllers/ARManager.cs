using UnityEngine;
using UnityEngine.UI;

/*
 * ARManager script handle game logic related
 * to Augemented Reality technology.
 */
public class ARManager : MonoBehaviour
{
    public Canvas UICanvas;

    [Header("Slider Attribute(s)")]
    public Slider PercentSlider;
    private static float oldSliderValue;

    private static MarkerData data;
    private static Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void SwitchToInGameCanvas()
    {
        for (int i = 0; i < UICanvas.transform.childCount; i++)
        {
            GameObject childObj = UICanvas.transform.GetChild(i).gameObject;

            if (childObj.name != "Loading")
            {
                childObj.SetActive(childObj.name == "AR Canvas");
            }
        }
    }

    public void InitSlider(int steps)
    {
        oldSliderValue = steps;

        PercentSlider.minValue = 0f;
        PercentSlider.maxValue = steps;
        PercentSlider.value = steps;
    }

    private void UpdateObject(float currStep, int steps)
    {
        animator.speed = 0f;

        animator.Play($"{data.animationName}2", 0, currStep / steps);
    }

    public void UpdateSliderValue()
    {
        if (oldSliderValue != PercentSlider.value)
        {
            UpdateObject(PercentSlider.value, data.steps);
        }

        oldSliderValue = (int)PercentSlider.value;
    }

    public void OnSpawnHandler(GameObject markerObject)
    {
        data = markerObject.GetComponent<MarkerData>();
        animator = markerObject.GetComponent<Animator>();

        animator.PlayInFixedTime($"{data.animationName}1");

        SwitchToInGameCanvas();
        InitSlider(data.steps);

        FreeViewManager.SetModelId(data.id);
    }
}
