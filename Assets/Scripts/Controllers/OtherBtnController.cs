using UnityEngine;
using UnityEngine.EventSystems;

public class OtherBtnController : MonoBehaviour, IPointerClickHandler
{
    public GameObject OtherContent;
    public SwitchCameraController SwitchCameraController;

    [Header("Content Canvas")]
    public GameObject CurrentParentCanvas;
    public GameObject CurrentCanvas;
    public GameObject HomeCanvas;
    public GameObject SettingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        OtherBtnHandler();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Set other content active state
        OtherBtnHandler();
    }

    private void OtherBtnHandler()
    {
        OtherContent.SetActive(!OtherContent.activeSelf);
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void DisplaySettingsHandler()
    {
        CurrentCanvas.SetActive(false);
        SettingsCanvas.SetActive(true);
    }

    public void ExitHandler()
    {
        // Switch camera
        SwitchCameraController.ChangeCamera();

        CurrentParentCanvas.SetActive(false);
        HomeCanvas.SetActive(true);
    }
}
