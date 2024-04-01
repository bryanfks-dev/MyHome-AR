using UnityEngine;

/*
 * FreeViewManager script handle some game logic
 * for free viewing to explore around the house.
 */
public class FreeViewManager : MonoBehaviour
{
    [Header("Player Attribute(s)")]
    public GameObject PlayerCam;

    [Header("House Models Checkpoint")]
    public GameObject[] Models;

    [Header("Sky Render Material(s)")]
    public Material LightSky;
    public Material StarySky;

    [Header("Skybox Manager")]
    public int SkyBoxId = 0;

    private static int modelId;

    // Start is called before the first frame update
    void Start()
    {
        SwitchSkyMaterial(LightSky);
    }

        public static void SetModelId(int id)
    {
        modelId = id;
    }

    public GameObject GetModel() => Models[modelId];

    private void SwitchSkyMaterial(Material material)
    {
        RenderSettings.skybox = material;
        DynamicGI.UpdateEnvironment();
    }

    public void ChangeSkyBox()
    {
        switch (SkyBoxId)
        {
            case 0:
                SkyBoxId = 1;

                SwitchSkyMaterial(StarySky);

                break;

            case 1:
                SkyBoxId = 0;

                SwitchSkyMaterial(LightSky);

                break;
        }
    }
}