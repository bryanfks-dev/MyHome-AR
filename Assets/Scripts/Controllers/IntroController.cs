using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public int pageCount;
    public Color selectedPageColor;
    public Color unselectedPageColor;
    public GameObject[] dynamicGameobjects;

    private int currPageIdx = 0;
    private GameObject nextBtn;
    private GameObject skipBtn;
    private List<GameObject> dots;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize gameobjects
        nextBtn = GameObject.Find("Next btn");
        skipBtn = GameObject.Find("Skip btn");
        dots = GetChildren(GameObject.Find("Progress"));

        DisplayPage();
        UpdateProgressColor();
    }

    private List<GameObject> GetChildren(GameObject go)
    {
        List<GameObject> list = new List<GameObject>();

        for (int i = 0; i < go.transform.childCount; i++)
        {
            list.Add(go.transform.GetChild(i).gameObject);
        }

        return list;
    }

    private void DisplayPage()
    {
        foreach (GameObject go in dynamicGameobjects)
        {
            // Iterate through go children
            for (int i = 0; i < go.transform.childCount; i++) {
                GameObject childObj = go.transform.GetChild(i).gameObject;

                childObj.SetActive(i == currPageIdx);
            }
        }
    }

    private void ChangeBtnText(Text btn, string text)
    {
        btn.text = text;
    }

    private void HideSkipButton()
    {
        skipBtn.GetComponent<Text>().color = Color.white;
    }

    private void UpdateProgressColor()
    {
        // Display page content according to current page index
        for (int i = 0; i < pageCount; i++)
        {
            Image goAsImg = dots[i].GetComponent<Image>();

            if (i == currPageIdx)
            {
                goAsImg.color = selectedPageColor;
            }
            else
            {
                goAsImg.color = unselectedPageColor;
            }
        }
    }

    public void NextBtnHandler()
    {
        if (currPageIdx < pageCount - 2)
        {
            // Update index
            currPageIdx++;

            DisplayPage();
            UpdateProgressColor();
        }
        else if (currPageIdx == pageCount - 2)
        {
            SkipBtnHandler();
        }
        // _currPageIdx at the last page index
        else
        {
            // Write json file
            JsonFile.WritePassedIntro(true);

            // Unload current scene
            SceneManager.UnloadSceneAsync("IntroScene");

            // Switch scene
            SceneManager.LoadSceneAsync("HomeScene", LoadSceneMode.Additive);
        }
    }

    public void SkipBtnHandler()
    {
        // Update index
        currPageIdx = pageCount - 1;

        DisplayPage();
        HideSkipButton();
        UpdateProgressColor();

        ChangeBtnText(nextBtn.GetComponent<Text>(), "Done");
    }
}