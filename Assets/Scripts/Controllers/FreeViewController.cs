using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FreeViewController : MonoBehaviour
{
    public GameObject InstructionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        // Debugging purpose
        if (JsonFile.data == null)
        {
            JsonFile.InitJson();
        }

        InstructionHandler();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UnderstandBtnHandler()
    {
        JsonFile.WritePassedInstruction(true);

        // Hide canvas
        InstructionCanvas.SetActive(false);
    }

    private IEnumerator WaitForBtn(Button btn, int seconds)
    {
        btn.interactable = false;

        yield return new WaitForSeconds(seconds);

        btn.interactable = true;
    }

    private void InstructionHandler()
    {
        // Check if user has passed get started popup
        if (JsonFile.data.passed_instrcution)
        {
            // Hide canvas
            InstructionCanvas.SetActive(false);
        }
        else
        {
            Button understandBtn = GetComponentInChildren<Button>();

            StartCoroutine(WaitForBtn(understandBtn, 3));
        }
    }
}
