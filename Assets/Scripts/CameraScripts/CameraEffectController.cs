using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraEffectController : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume volume;
    private Vignette vignette;
    private float intensity = 0f;

    // Start is called before the first frame update
    void Start()
    {
        volume.profile.TryGetSettings(out vignette);
        if(!vignette)
        {
            Debug.LogError("Vignette component not found in PostProcessVolume profile.");
        }
        else
        {
            vignette.enabled.Override(false);
        }
    }

    public void ApplyVignetteEffect()
    {
        StartCoroutine(ApplyVignette());
    }
    
    private IEnumerator ApplyVignette()
    {
        intensity = 0.5f;
        vignette.enabled.Override(true);
        vignette.intensity.Override(intensity);
        yield return new WaitForSeconds(0.5f);

        while(intensity > 0)
        {
            intensity -= 0.01f;
            if(intensity <0) intensity = 0;
    
            vignette.intensity.Override(intensity);

            yield return new WaitForSeconds(0.01f);
        }

        vignette.enabled.Override(false);
        yield break;
    }
}
