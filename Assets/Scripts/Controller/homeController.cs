using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class homeController : MonoBehaviour
{
    public GameObject GetStartedPopUp;

    private void ignoreRaycastTarget()
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

    private void obeyRaycastTarget()
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

    private void popUpButtonHandler()
    {
        JsonFile.writePassedGetStarted(true);

        // Hide popup
        GetStartedPopUp.SetActive(false);

        obeyRaycastTarget();
    }

    private void getStartedPopUpHandler()
    {
        // Check if user has passed get started popup
        if (JsonFile.data.passed_get_started_popup)
        {
            // Hide popup
            GetStartedPopUp.SetActive(false);
        }
        else
        {
            ignoreRaycastTarget();

            // Handle button in get started popup
            // Get popup button
            int popUpChildrenLen = GetStartedPopUp.transform.childCount;

            Button button = GetStartedPopUp.transform
                .GetChild(popUpChildrenLen - 1).GetComponent<Button>();

            // Add click listener to button
            button.onClick.AddListener(popUpButtonHandler);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        JsonFile.initJson();
        getStartedPopUpHandler();
    }
}
