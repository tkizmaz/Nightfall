using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraEffectController : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume volume;
    private Vignette vignette;
    private float intensity = 0f;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
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

    public void ApplyOnDamageEffects()
    {
        ApplyVignetteEffect();
        ShakeCameraEffect();
    }

    public void ApplyVignetteEffect()
    {
        StartCoroutine(ApplyVignette());
    }

    public void ShakeCameraEffect()
    {
        StartCoroutine(ShakeCamera());
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

    private IEnumerator ShakeCamera()
    {
        Vector3 originalPos = mainCamera.transform.localPosition;
        float elapsed = 0.0f;

        float duration = 0.1f;
        float magnitude = 0.1f;
        while(elapsed < duration)
        {
            float x = Random.Range(-0.25f, 0.25f) * magnitude;
            float y = Random.Range(-0.5f, 0.5f) * magnitude;

            mainCamera.transform.localPosition = new Vector3(x, originalPos.y + y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCamera.transform.localPosition = originalPos;
    }
}
