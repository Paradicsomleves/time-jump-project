using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SkyBoxSet : MonoBehaviour
{
    public Material pastSkybox;
    public Material presentSkybox;

    public Color pastFogColor;
    public Color presentFogColor;

    private void OnEnable()
    {
        GameManager.Jumping += SetSkybox;
    }
    private void OnDisable()
    {
        GameManager.Jumping -= SetSkybox;
    }

    private void SetSkybox()
    {
        if (GameManager.instance.isPast)
        {
            RenderSettings.skybox = presentSkybox;
            RenderSettings.fogColor = presentFogColor;

            DynamicGI.UpdateEnvironment();
        }
        else
        {
            RenderSettings.skybox = pastSkybox;
            RenderSettings.fogColor = pastFogColor;

            DynamicGI.UpdateEnvironment();
        }

    }
}
