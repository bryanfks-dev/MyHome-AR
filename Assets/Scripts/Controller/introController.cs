using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class introController : MonoBehaviour
{
    public int pageCount;
    public Color selectedPageColor;
    public Color unselectedPageColor;
    public GameObject[] dynamicGameobjects;

    private int _currPageIdx = 0;
    private GameObject nextBtn;
    private GameObject skipBtn;
    private List<GameObject> dots;

    private List<GameObject> getChildren(GameObject go)
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < go.transform.childCount; i++)
        {
            list.Add(go.transform.GetChild(i).gameObject);
        }

        return list;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize gameobjects
        nextBtn = GameObject.Find("Next btn");
        skipBtn = GameObject.Find("Skip btn");
        dots = getChildren(GameObject.Find("Progress"));

        displayPage();
        updateProgressColor();
    }

    private void displayPage()
    {
        foreach (GameObject go in dynamicGameobjects)
        {
            // Iterate through go children
            for (int i = 0; i < go.transform.childCount; i++) {
                GameObject childObj = go.transform.GetChild(i).gameObject;

                childObj.SetActive(i == _currPageIdx);
            }
        }
    }

    private void changeBtnText(Text btn, string text)
    {
        btn.text = text;
    }

    private void hideSkipButton()
    {
        skipBtn.GetComponent<Text>().color = Color.white;
    }

    private void updateProgressColor()
    {
        // Display page content according to current page index
        for (int i = 0; i < pageCount; i++)
        {
            Image goAsImg = dots[i].GetComponent<Image>();

            if (i == _currPageIdx)
            {
                goAsImg.color = selectedPageColor;
            }
            else
            {
                goAsImg.color = unselectedPageColor;
            }
        }
    }

    public void nextBtnHandler()
    {
        if (_currPageIdx < pageCount - 2)
        {
            // Update index
            _currPageIdx++;

            displayPage();
            updateProgressColor();
        }
        else if (_currPageIdx == pageCount - 2)
        {
            skipBtnHandler();
        }
        // _currPageIdx at the last page index
        else
        {
            // Write json file
            JsonFile.writePassedIntro(true);

            // Unload current scene
            SceneManager.UnloadSceneAsync("IntroScene");

            // Switch scene
            SceneManager.LoadSceneAsync("HomeScene", LoadSceneMode.Additive);
        }
    }

    public void skipBtnHandler()
    {
        // Update index
        _currPageIdx = pageCount - 1;

        displayPage();
        hideSkipButton();
        updateProgressColor();

        changeBtnText(nextBtn.GetComponent<Text>(), "Done");
    }
}