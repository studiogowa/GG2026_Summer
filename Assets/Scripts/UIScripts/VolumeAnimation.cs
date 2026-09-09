using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[ExecuteAlways]
public class VolumeAnimation : MonoBehaviour
{
    [Range(0f, 100f)] public float focusDist = 100f;

    private Volume volume;
    private DepthOfField depthOfField;

    void LateUpdate()
    {
        if (volume == null)
            volume = GetComponent<Volume>();
        
        if (volume != null && depthOfField == null)
            volume.profile.TryGet(out depthOfField);
        
        if (depthOfField != null)
            depthOfField.focusDistance.value = focusDist;
    }
}
