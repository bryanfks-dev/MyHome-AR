using UnityEngine;

/*
 * FreeViewManager script handle some game logic
 * for free viewing to explore around the house.
 */
public class FreeViewManager : MonoBehaviour
{
    [Header("House Models Checkpoint")]
    public GameObject[] Models;

    private static int modelId;

    public static void SetModelId(int id)
    {
        modelId = id;
    }

    public GameObject GetModel() => Models[modelId];
}
