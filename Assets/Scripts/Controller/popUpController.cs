using UnityEngine;
using UnityEngine.EventSystems;

public class popUpController : MonoBehaviour, IPointerClickHandler
{
    private GameObject button;
    private GameObject popUpModal;

    private void HidePopUp()
    {
        // Make sure pop-up modal is not null
        if (popUpModal != null)
        {
            popUpModal.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get current gameobject
        button = gameObject;

        // Get children gameobject from current gameobject
        popUpModal = button.transform.GetChild(0).gameObject;

        HidePopUp();
    }

    // OnPointerClick is called once current gameobject is clicked
    public void OnPointerClick(PointerEventData eventData)
    {
        // Set popup modal activate state
        popUpModal.SetActive(!popUpModal.activeSelf);
    }
}
