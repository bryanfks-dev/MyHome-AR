using UnityEngine;

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

    private void InstructionHandler()
    {
        // Check if user has passed get started popup
        if (JsonFile.data.passed_instrcution)
        {
            // Hide canvas
            InstructionCanvas.SetActive(false);
        }
    }
}
