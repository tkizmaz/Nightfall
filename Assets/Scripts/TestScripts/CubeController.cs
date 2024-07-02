using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private Material sihouetteShader;
    [SerializeField]
    List<Material> defaultMaterials;
    private Renderer enemyRenderer;

    public void ChangeToSilhouette()
    {
        foreach(Material material in defaultMaterials)
        {
            enemyRenderer.material = sihouetteShader;
        }
    }

    public void ChangeToDefault()
    {
        foreach(Material material in defaultMaterials)
        {
            enemyRenderer.material = material;
        }
    }

    private void Start() 
    {
        enemyRenderer = GetComponent<Renderer>();
        foreach(Material material in defaultMaterials)
        {
            defaultMaterials.Add(material);
        }
    }
}
