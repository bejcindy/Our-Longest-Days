using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectManager : MonoBehaviour
{
    [Header("PostProcessing Effects")]
    public PostProcessVolume distortion;
    public PostProcessVolume bloomcolor;
    public PostProcessVolume ambientlight;

    //Each represents a Post Processing effect being used in the game
    private LensDistortion lensDistortion;
    private ChromaticAberration chromAberration;
    private Grain grain;
    private ColorGrading colorGrading;
    private Bloom bloom;
    private AmbientOcclusion ambientOcc;

    // Start is called before the first frame update
    void Start()
    {
        //TryGetSettings takes gets the settings of the Post Processing Volume for each effect specified.
        distortion.profile.TryGetSettings(out lensDistortion);
        //After taking the Post Processing Profile above and specifying which effect it refers to
        //then the properties of the effect can be controlled and set here.
        lensDistortion.intensity.value = 0;
        lensDistortion.scale.value = 0.8f;

        distortion.profile.TryGetSettings(out chromAberration);
        chromAberration.intensity.value = 0;

        distortion.profile.TryGetSettings(out grain);
        grain.intensity.value = 0;
        grain.size.value = 0;

        bloomcolor.profile.TryGetSettings(out colorGrading);
        colorGrading.brightness.value = 53f;
        colorGrading.contrast.value = 57f;

        bloomcolor.profile.TryGetSettings(out bloom);
        bloom.intensity.value = 5;
        bloom.threshold.value = 0.33f;
        bloom.diffusion.value = 8.57f;

        ambientlight.profile.TryGetSettings(out ambientOcc);
        ambientOcc.intensity.value = 3.14f;
        ambientOcc.radius.value = 4.72f;
    }

    /// <summary>
    /// Lerps and controls the values of each effect property over time.
    /// </summary>
    public void DistortionEffects()
    {
        lensDistortion.intensity.value = Mathf.Lerp(lensDistortion.intensity.value, 100, .05f * Time.deltaTime);
        grain.intensity.value = Mathf.Lerp(grain.intensity.value, 1, .05f * Time.deltaTime);
        grain.size.value = Mathf.Lerp(grain.size.value, 3f, .05f * Time.deltaTime);
        lensDistortion.scale.value = Mathf.Lerp(lensDistortion.scale.value, 0, .005f * Time.deltaTime);
        chromAberration.intensity.value = Mathf.Lerp(chromAberration.intensity.value, 1, .05f * Time.deltaTime);
        colorGrading.brightness.value = Mathf.Lerp(colorGrading.brightness.value, 0, .005f * Time.deltaTime);
        colorGrading.contrast.value = Mathf.Lerp(colorGrading.contrast.value, 0, .005f * Time.deltaTime);
        bloom.intensity.value = Mathf.Lerp(bloom.intensity.value, 0, .005f * Time.deltaTime);
        bloom.threshold.value = Mathf.Lerp(bloom.threshold.value, 1, .005f * Time.deltaTime);
        bloom.diffusion.value = Mathf.Lerp(bloom.diffusion.value, 1, .005f * Time.deltaTime);
        ambientOcc.intensity.value = Mathf.Lerp(ambientOcc.intensity.value, 4, .005f * Time.deltaTime);
        ambientOcc.radius.value = Mathf.Lerp(ambientOcc.radius.value, 12, .005f * Time.deltaTime);
    }
}
