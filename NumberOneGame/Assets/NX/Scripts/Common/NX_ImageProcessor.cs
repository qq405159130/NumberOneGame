using UnityEngine;
using System.Collections;

public static class NX_ImageProcessor
{
    public enum ImageMixType
    {
        Normal,
        Dissolve,

        Darken,
        Multiply,
        ColorBurn,
        LinearBurn,

        Lighten,
        Screen,
        ColorDodge,
        LinearDodge,

        Overlay,
        SoftLight,
        HardLight,
        VividLight,
        LinearLight,
        PinLight,

        Difference,
        Exclusion,

        Hue,
        Saturation,
        Color,
        Luminosity
    }

    public struct WH
    {
        public int w, h;
    }

    public enum ResultWH
    {
        OnlyA,
        OnlyB,
        MaxAB,
        MinAB
    }

    private const float FULL = 1;
    private const float HALF = 0.5f;

    public static ResultWH m_resultWH = ResultWH.OnlyA;

	#region === 字段 === 
	
	#endregion
	
	#region === 属性 === 
	
	#endregion
	
	#region === 方法 === 
	
	#region === 公有函数 === 

    public static Texture2D ImageMix(Texture2D imageA, Texture2D imageB,ImageMixType mixType)
    {
        Texture2D result;

        switch (mixType)
        {
            case ImageMixType.Normal:
                result = IM_Normal(imageA, imageB);
                break;
            case ImageMixType.Dissolve:
                result = IM_Dissolve(imageA, imageB);
                break;
            case ImageMixType.Darken:
                result = IM_Darken(imageA, imageB);
                break;
            case ImageMixType.Multiply:
                result = IM_Multiply(imageA, imageB);
                break;
            case ImageMixType.ColorBurn:
                result = IM_ColorBurn(imageA, imageB);
                break;
            case ImageMixType.LinearBurn:
                result = IM_LinearBurn(imageA, imageB);
                break;
            case ImageMixType.Lighten:
                result = IM_Lighten(imageA, imageB);
                break;
            case ImageMixType.Screen:
                result = IM_Screen(imageA, imageB);
                break;
            case ImageMixType.ColorDodge:
                result = IM_ColorDodge(imageA, imageB);
                break;
            case ImageMixType.LinearDodge:
                result = IM_LinearDodge(imageA, imageB);
                break;
            case ImageMixType.Overlay:
                result = IM_Overlay(imageA, imageB);
                break;
            case ImageMixType.SoftLight:
                result = IM_SoftLight(imageA, imageB);
                break;
            case ImageMixType.HardLight:
                result = IM_HardLight(imageA, imageB);
                break;
            case ImageMixType.VividLight:
                result = IM_VividLight(imageA, imageB);
                break;
            case ImageMixType.LinearLight:
                result = IM_LinearLight(imageA, imageB);
                break;
            case ImageMixType.PinLight:
                result = IM_PinLight(imageA, imageB);
                break;
            case ImageMixType.Difference:
                result = IM_Difference(imageA, imageB);
                break;
            case ImageMixType.Exclusion:
                result = IM_Exclusion(imageA, imageB);
                break;
            case ImageMixType.Hue:
                result = IM_Hue(imageA, imageB);
                break;
            case ImageMixType.Saturation:
                result = IM_Saturation(imageA, imageB);
                break;
            case ImageMixType.Color:
                result = IM_Color(imageA, imageB);
                break;
            case ImageMixType.Luminosity:
                result = IM_Luminosity(imageA, imageB);
                break;
            default:
                result = new Texture2D(1, 1);
                break;
        }

        return result;
    }
	#endregion
	
	#region === 私有函数 === 

    private static Texture2D IM_Normal(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                int index = i*wh.w + j;
                colors[index].r = Mathf_Normal(colorsA[index].r, colorsB[index].r);
                colors[index].g = Mathf_Normal(colorsA[index].g, colorsB[index].g);
                colors[index].b = Mathf_Normal(colorsA[index].b, colorsB[index].b);
                colors[index].a = Mathf_Normal(colorsA[index].a, colorsB[index].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Dissolve(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Dissolve(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Dissolve(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Dissolve(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Dissolve(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Darken(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Darken(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Darken(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Darken(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Darken(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Multiply(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Multiply(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Multiply(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Multiply(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Multiply(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_ColorBurn(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_ColorBurn(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_ColorBurn(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_ColorBurn(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_ColorBurn(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }

    private static Texture2D IM_LinearBurn(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_LinearBurn(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_LinearBurn(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_LinearBurn(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_LinearBurn(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Lighten(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Lighten (colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Lighten(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Lighten(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Lighten(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Screen(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Screen(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Screen(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Screen(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Screen(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_ColorDodge(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_ColorDodge(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_ColorDodge(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_ColorDodge(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_ColorDodge(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_LinearDodge(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_LinearDodge(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_LinearDodge(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_LinearDodge(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_LinearDodge(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Overlay(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Overlay(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Overlay(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Overlay(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Overlay(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_SoftLight(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_SoftLight(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_SoftLight(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_SoftLight(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_SoftLight(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_HardLight(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_HardLight(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_HardLight(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_HardLight(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_HardLight(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_VividLight(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_VividLight(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_VividLight(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_VividLight(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_VividLight(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_LinearLight(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_LinearLight(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_LinearLight(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_LinearLight(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_LinearLight(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_PinLight(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_PinLight(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_PinLight(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_PinLight(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_PinLight(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Difference(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Difference(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Difference(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Difference(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Difference(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Exclusion(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Exclusion(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Exclusion(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Exclusion(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Exclusion(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Hue(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Hue(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Hue(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Hue(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Hue(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Saturation(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Saturation(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Saturation(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Saturation(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Saturation(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Color(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Color(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Color(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Color(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Color(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }
    private static Texture2D IM_Luminosity(Texture2D imageA, Texture2D imageB)
    {
        Color[] colorsA = imageA.GetPixels();
        Color[] colorsB = imageB.GetPixels();

        WH wh = GetWidthHeght(imageA, imageB);
        Texture2D result = new Texture2D(wh.w, wh.h);
        Color[] colors = new Color[wh.w * wh.h];

        for (int i = 0; i < wh.w; i++)
        {
            for (int j = 0; j < wh.h; j++)
            {
                colors[i * wh.w + j].r = Mathf_Luminosity(colorsA[i * wh.w + j].r, colorsB[i * wh.w + j].r);
                colors[i * wh.w + j].g = Mathf_Luminosity(colorsA[i * wh.w + j].g, colorsB[i * wh.w + j].g);
                colors[i * wh.w + j].b = Mathf_Luminosity(colorsA[i * wh.w + j].b, colorsB[i * wh.w + j].b);
                colors[i * wh.w + j].a = Mathf_Luminosity(colorsA[i * wh.w + j].a, colorsB[i * wh.w + j].a);
            }
        }
        result.SetPixels(colors);
        result.Apply();
        return result;
    }

    private static WH GetWidthHeght(Texture2D imageA, Texture2D imageB)
    {
        WH wh = new WH();
        switch (m_resultWH)
        {
            case ResultWH.OnlyA:
                wh.w = imageA.width;
                wh.h = imageA.height;
                break;
            case ResultWH.OnlyB:
                wh.w = imageB.width;
                wh.h = imageB.height;
                break;
            case ResultWH.MaxAB:
                wh.w = imageA.width >= imageB.width ? imageA.width : imageB.width;
                wh.h = imageA.height >= imageB.height ? imageA.height : imageB.height;
                break;
            case ResultWH.MinAB:
                wh.w = imageA.width < imageB.width ? imageA.width : imageB.width;
                wh.h = imageA.height < imageB.height ? imageA.height : imageB.height;
                break;
        }
        return wh;
    }


    private static float Mathf_Normal(float valueA, float valueB)
    {
        float value = valueB;
        return value;
    }
   
    private static float Mathf_Dissolve(float valueA, float valueB)
    {
        float value = valueB;
        return value;
    }
    private static float Mathf_Darken(float valueA, float valueB)
    {
        return Mathf.Min(valueA, valueB);
    }

   //Multiply,
    private static float Mathf_Multiply(float valueA, float valueB)
    {
        return valueA * valueB / FULL;
    }
   //ColorBurn,
    private static float Mathf_ColorBurn(float valueA, float valueB)
    {
        return valueA - (FULL - valueA) * (FULL - valueB) / valueB;
    }
   //LinearBurn,
    private static float Mathf_LinearBurn(float valueA, float valueB)
    {
        return valueA + valueB - FULL;
    }
   //Lighten,
    private static float Mathf_Lighten(float valueA, float valueB)
    {
        return Mathf.Max(valueA, valueB);
    }
   //Screen,
    private static float Mathf_Screen(float valueA, float valueB)
    {
        return FULL - (FULL - valueA) * (FULL - valueB) / FULL;
    }
   //ColorDodge,
    private static float Mathf_ColorDodge(float valueA, float valueB)
    {
        return valueA + valueA * valueB / (FULL - valueB);
    }
   //LinearDodge,
    private static float Mathf_LinearDodge(float valueA, float valueB)
    {
        return valueA + valueB;
    }
   //Overlay,
    private static float Mathf_Overlay(float valueA, float valueB)
    {
        return valueA > HALF ? FULL - (FULL - valueA) * (FULL - valueB)/HALF : valueA * valueB / HALF;
    }
   //SoftLight,
    private static float Mathf_SoftLight(float valueA, float valueB)
    {
        return valueB > HALF
            ? valueA*(FULL - valueB)/HALF + Mathf.Sqrt(valueA/FULL)*(2*valueB - FULL)
            : valueA*valueB/HALF + (valueA/FULL)*(valueA/FULL)*(FULL - 2*valueB);
    }
   //HardLight,
    private static float Mathf_HardLight(float valueA, float valueB)
    {
        return valueB > HALF
            ? FULL - (FULL - valueA)*(FULL - valueB)/HALF
            : valueA*valueB/HALF;
    }
   //VividLight,
    private static float Mathf_VividLight(float valueA, float valueB)
    {
        return valueB > HALF
            ? valueA + valueA*(2*valueB - FULL)/2*(FULL - valueB)
            : valueA - (FULL - valueA)*(FULL - 2*valueB)/(2*valueB);
    }
   //LinearLight,
    private static float Mathf_LinearLight(float valueA, float valueB)
    {
        return valueA + 2 * valueB - FULL;
    }
   //PinLight,
    private static float Mathf_PinLight(float valueA, float valueB)
    {
        return valueB > HALF
            ? Mathf.Min(valueA, 2*valueB - FULL)
            : Mathf.Min(valueA, 2*valueB);
    }
   //Difference,
    private static float Mathf_Difference(float valueA, float valueB)
    {
        return Mathf.Abs(valueA - valueB);
    }
   //Exclusion,
    private static float Mathf_Exclusion(float valueA, float valueB)
    {
        return valueA + valueB - valueA * valueB / HALF;
    }
   //Hue,
    private static float Mathf_Hue(float valueA, float valueB)
    {
        float value = valueB;
        return value;
    }
   //Saturation,
    private static float Mathf_Saturation(float valueA, float valueB)
    {
        float value = valueB;
        return value;
    }
   //Color,
    private static float Mathf_Color(float valueA, float valueB)
    {
        float value = valueB;
        return value;
    }
   //Luminosity
    private static float Mathf_Luminosity(float valueA, float valueB)
    {
        float value = valueB;
        return value;
    }

	#endregion
	
	#endregion
}
