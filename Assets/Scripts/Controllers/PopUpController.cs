using UnityEngine;
using UnityEngine.EventSystems;

public class PopUpController : MonoBehaviour, IPointerClickHandler
{
    private GameObject button;
    private GameObject popUpModal;

    // Start is called before the first frame update
    void Start()
    {
        // Get current gameobject
        button = gameObject;

        // Get children gameobject from current gameobject
        popUpModal = button.transform.GetChild(0).gameObject;

        HidePopUp();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Set popup modal activate state
        popUpModal.SetActive(!popUpModal.activeSelf);
    }

    private void HidePopUp()
    {
        // Make sure pop-up modal is not null
        popUpModal.SetActive(false);
    }
}
