using UnityEngine;

[ExecuteInEditMode]
public class ShaderApplier : MonoBehaviour
{
    public float intensity;
    public Material material;
    // Creates a private material used to the effect
    void Awake()
    {
        //material = new Material(Shader.Find("Hidden/BWDiffuse"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (intensity == 0)
        {
            Graphics.Blit(source, destination);
            return;
        }
        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, material);
    }
}