using UnityEngine;

public class SwitchCameraController : MonoBehaviour
{
    public GameObject[] CameraList;

    public int Manager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCamera()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }

    public void ManageCamera()
    {
        if (Manager == 0)
        {
            SwitchCamera(0, 1);
            Manager = 1;
        }
        else
        {
            SwitchCamera(1, 0);
            Manager = 0;
        }
    }

    private void SwitchCamera(int fromIdx, int toIdx)
    {
        CameraList[fromIdx].SetActive(false);
        CameraList[toIdx].SetActive(true);
    }
}
