using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class OtherBtnController : MonoBehaviour, IPointerClickHandler
{
    public GameObject OtherContent;
    public FreeViewManager FreeViewManager;

    [Header("Content Canvas")]
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

    public void ChangeSkyBox()
    {
        OtherBtnHandler();
        FreeViewManager.ChangeSkyBox();
    }

    public void ExitHandler()
    {
        OtherBtnHandler();

        SwitchCameraController.ManageCamera();
    }
}
