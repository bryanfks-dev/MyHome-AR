using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class HomeController : MonoBehaviour
{
    public GameObject GetStartedPopUp;

    // Start is called before the first frame update
    void Start()
    {
        // Debugging purpose
        JsonFile.initJson();
        GetStartedPopUpHandler();
    }

    private void IgnoreRaycastTarget()
    {
        GameObject currGameObj = gameObject;

        // Iterate all current gameobject children
        for (int i = 0; i < currGameObj.transform.childCount; i++)
        {
            try
            {
                Transform child = currGameObj.transform.GetChild(i);
                SVGImage childImg = child.GetComponent<SVGImage>();

                // Ignore gameobject, if not popup
                if (child.gameObject != GetStartedPopUp)
                {
                    childImg.raycastTarget = false;
                }
            }
            catch { } // Ignore exception
        }
    }

    private void ObeyRaycastTarget()
    {
        GameObject currGameObj = gameObject;

        // Iterate all current gameobject children
        for (int i = 0; i < currGameObj.transform.childCount; i++)
        {
            try
            {
                Transform child = currGameObj.transform.GetChild(i);
                SVGImage childImg = child.GetComponent<SVGImage>();

                childImg.raycastTarget = true;
            }
            catch { } // Ignore exception
        }
    }

    private void PopUpButtonHandler()
    {
        JsonFile.writePassedGetStarted(true);

        // Hide popup
        GetStartedPopUp.SetActive(false);

        ObeyRaycastTarget();
    }

    private void GetStartedPopUpHandler()
    {
        // Check if user has passed get started popup
        if (JsonFile.data.passed_get_started_popup)
        {
            // Hide popup
            GetStartedPopUp.SetActive(false);
        }
        else
        {
            IgnoreRaycastTarget();

            // Handle button in get started popup
            // Get popup button
            int popUpChildrenLen = GetStartedPopUp.transform.childCount;

            Button button = GetStartedPopUp.transform
                .GetChild(popUpChildrenLen - 1).GetComponent<Button>();

            // Add click listener to button
            button.onClick.AddListener(PopUpButtonHandler);
        }
    }
}
