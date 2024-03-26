using UnityEngine;

/*
 * FreeViewManager script handle game logic
 * for free viewing to explore around the house.
 */
public class FreeViewManager : MonoBehaviour
{
    public GameObject[] Models;

    private static int modelId;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetModelId(int id)
    {
        modelId = id;
    }

    public GameObject GetModel() => Models[modelId];
}
