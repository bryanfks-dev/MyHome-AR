using UnityEngine;
using UnityEngine.EventSystems;

public class OtherBtnController : MonoBehaviour, IPointerClickHandler
{
    public GameObject OtherContent;
    public SwitchCameraController SwitchCameraController;

    [Header("Content Canvas")]
    public GameObject CurrentParentCanvas;
    public GameObject HomeCanvas;
    public GameObject SettingsCanvas;

    // Start is called before the first frame update
    void Start()
    {
        OtherBtnHandler();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Set other content active state
        OtherBtnHandler();
    }

    private void OtherBtnHandler()
    {
        OtherContent.SetActive(!OtherContent.activeSelf);
    }

    public void DisplaySettingsHandler()
    {
        OtherBtnHandler();
        SettingsCanvas.SetActive(true);
    }

    public void ExitHandler()
    {
        OtherBtnHandler();

        // Switch camera
        SwitchCameraController.ChangeCamera();

        CurrentParentCanvas.SetActive(false);
        HomeCanvas.SetActive(true);
    }
}
