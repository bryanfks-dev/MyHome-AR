using UnityEngine;

public class FreeViewManager : MonoBehaviour
{
    public GameObject[] Models;

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

    public GameObject GetModel() => Models[modelId];
}
