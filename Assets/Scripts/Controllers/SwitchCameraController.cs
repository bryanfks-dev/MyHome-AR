using UnityEngine;

public class SwitchCameraController : MonoBehaviour
{
    public GameObject Player;
    public GameObject[] Models;
    public GameObject[] CameraList;
    public int Manager;

    static private int modelId;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    static public void SetModelId(int id)
    {
        modelId = id;
    }

    private void ChangePos(Vector3 pos)
    {
        // WTF WHY???
        Player.SetActive(false);
        Player.transform.position = pos;
        Player.SetActive(true);
    }

    private void ResetPlayerPosition()
    {
        ChangePos(PlayerManager.GetPlayerInitPos());
    }

    private void TeleportPlayer()
    {
        Transform modelTransform = Models[modelId].transform;

        Vector3 newPos = new Vector3(modelTransform.position.x, 
            modelTransform.position.y + 100f, modelTransform.position.z);

        ChangePos(newPos);
    }

    public void ChangeCamera()
    {
        GetComponent<Animator>().SetTrigger("Change");
    }

    public void ManageCamera()
    {
        switch (Manager)
        {
            case 0:
                SwitchCamera(0, 1);
                Manager = 1;

                TeleportPlayer();

                Screen.orientation = ScreenOrientation.LandscapeLeft;

                break;

            case 1:
                SwitchCamera(1, 0);
                Manager = 0;

                ResetPlayerPosition();

                Screen.orientation = ScreenOrientation.Portrait;

                break;
        }
    }

    private void SwitchCamera(int fromIdx, int toIdx)
    {
        CameraList[fromIdx].SetActive(false);
        CameraList[toIdx].SetActive(true);
    }
}
