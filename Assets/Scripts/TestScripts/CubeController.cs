using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField]
    private Material sihouetteShader;
    private Material defaultMaterial;
    private Renderer cubeRenderer;
    public float speed = 100.0f;
    // Update is called once per frame

    public void ChangeToSilhouette()
    {
        cubeRenderer.material = sihouetteShader;
    }

    public void ChangeToDefault()
    {
        cubeRenderer.material = defaultMaterial;
    }

    private void Start() 
    {
        cubeRenderer = GetComponent<Renderer>();
        defaultMaterial = cubeRenderer.material;
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.PingPong(Time.time * 10 * speed, 20), transform.position.y, transform.position.z);
    }
}
