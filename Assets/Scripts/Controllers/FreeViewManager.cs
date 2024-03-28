using UnityEngine;

/*
 * FreeViewManager script handle some game logic
 * for free viewing to explore around the house.
 */
public class FreeViewManager : MonoBehaviour
{
    [Header("House Models Checkpoint")]
    public GameObject[] Models;

    [Header("Sky Render Material(s)")]
    public Material LightSky;
    public Material StarySky;


    [Header("Sky Changer Manager")]
    public int SkyBoxId = 0;

    private static int modelId;

    // Start is called before the first frame update
    void Start()
    {
        // Initalize skybox material
        RenderSettings.skybox = LightSky;
    }

    public static void SetModelId(int id)
    {
        modelId = id;
    }

    public GameObject GetModel() => Models[modelId];

    public void ChangeSkyBox()
    {
        switch (SkyBoxId)
        {
            case 0:
                SkyBoxId = 1;

                RenderSettings.skybox = StarySky;
                break;

            case 1:
                SkyBoxId = 0;
                RenderSettings.skybox = LightSky;
                break;
        }
    }
}