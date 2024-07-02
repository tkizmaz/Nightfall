using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private Material sihouetteShader;
    [SerializeField]
    Material defaultMaterial;
    [SerializeField]
    private List<Renderer> enemyRenderer;

    public void ChangeToSilhouette()
    {
        foreach(Renderer renderer in enemyRenderer)
        {
            renderer.material = sihouetteShader;
        }
    }

    public void ChangeToDefault()
    {
        foreach(Renderer renderer in enemyRenderer)
        {
            renderer.material = defaultMaterial;
        }
    }
}
