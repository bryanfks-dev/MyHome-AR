using Unity.VectorGraphics;
using UnityEngine;

public class HomeController : MonoBehaviour
{
    public GameObject GetStartedPopUp;

    // Start is called before the first frame update
    void Start()
    {
        // Debugging purpose
        if (JsonFile.data == null)
        {
            JsonFile.InitJson();
        }

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

    public void GetStartedBtnHandler()
    {
        JsonFile.WritePassedGetStarted(true);

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
        }
    }
}
