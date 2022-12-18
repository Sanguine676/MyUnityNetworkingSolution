using UnityEngine;
//

//
public static class Material_ExtensionMethods
{
    //
    public enum RenderingMode
    {
        Opaque,
        Transparent,
    }

    //
    public static void SetRenderingMode(this Material _material, RenderingMode _renderingMode)
    {
        switch (_renderingMode)
        {
            case RenderingMode.Opaque:
                _material.SetOverrideTag("RenderType", "");
                _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                _material.SetInt("_ZWrite", 1);
                _material.DisableKeyword("_ALPHATEST_ON");
                _material.DisableKeyword("_ALPHABLEND_ON");
                _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                _material.renderQueue = -1;
                break;

            case RenderingMode.Transparent:
                _material.SetOverrideTag("RenderType", "Transparent");
                _material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                _material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                _material.SetInt("_ZWrite", 0);
                _material.DisableKeyword("_ALPHATEST_ON");
                _material.EnableKeyword("_ALPHABLEND_ON");
                _material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                _material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                break;
        }
    }
}
