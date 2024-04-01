using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    [Header("Introduction Scene Attribute(s)")]
    public int PageCount;
    public int CurrPageIdx = 0;

    [Header("Page Attribute(s)")]
    public Color SelectedPageColor;
    public Color UnselectedPageColor;
    public GameObject[] DynamicGameobjects;
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
        foreach (GameObject go in DynamicGameobjects)
        {
            // Iterate through go children
            for (int i = 0; i < go.transform.childCount; i++) {
                GameObject childObj = go.transform.GetChild(i).gameObject;

                childObj.SetActive(i == CurrPageIdx);
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
        for (int i = 0; i < PageCount; i++)
        {
            Image goAsImg = dots[i].GetComponent<Image>();

            if (i == CurrPageIdx)
            {
                goAsImg.color = SelectedPageColor;
            }
            else
            {
                goAsImg.color = UnselectedPageColor;
            }
        }
    }

    public void NextBtnHandler()
    {
        if (CurrPageIdx < PageCount - 2)
        {
            // Update index
            CurrPageIdx++;

            DisplayPage();
            UpdateProgressColor();
        }
        else if (CurrPageIdx == PageCount - 2)
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
        CurrPageIdx = PageCount - 1;

        DisplayPage();
        HideSkipButton();
        UpdateProgressColor();

        ChangeBtnText(nextBtn.GetComponent<Text>(), "Done");
    }
}