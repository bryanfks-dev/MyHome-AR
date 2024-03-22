using UnityEngine;
using UnityEngine.UI;

public class ARManager : MonoBehaviour
{
    public Canvas UICanvas;
    public Slider PercentSlider;

    private MarkerData data;
    private Animator animator;
    private float oldSliderValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SwitchToInGameCanvas()
    {
        for (int i = 0; i < UICanvas.transform.childCount; i++)
        {
            GameObject childObj = UICanvas.transform.GetChild(i).gameObject;

            if (childObj.name != "Loading")
            {
                childObj.SetActive(childObj.name == "Ingame");
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

        animator.Play(data.animationName, 0, currStep / steps);
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

        animator.PlayInFixedTime(data.animationName);

        SwitchToInGameCanvas();
        InitSlider(data.steps);
    }
}
