using UnityEngine;
using System.Collections;

/**
 * Extends feature of rect to support inch, percent screen and dpi
 * 
 */
public class FormattedRect : MonoBehaviour
{
    public enum RectType { Standard, MinMax }
    public enum eFormat { pixel, percent, inch };

    public RectType rectType;

    public float width;
    public eFormat widthFormat;

    public float height;
    public eFormat heightFormat;

    public float x;
    public eFormat xFormat;

    public float y;
    public eFormat yFormat;

    public float x2;
    public eFormat x2Format;

    public float y2;
    public eFormat y2Format;

    public TextAnchor anchor;

    float safeDpi = 200f;

    public Rect GetRect()
    {
        if (rectType == RectType.Standard)
        {
            return GetStandardRect();
        }
        else
        {
            return GetMinMaxRect();
        }
    }

    public Rect GetStandardRect()
    {
        float aWidth = GetTruePixel(width, widthFormat, Screen.width);
        float aHeight = GetTruePixel(height, heightFormat, Screen.height);
        float aX = GetTruePixel(x, xFormat, Screen.width);
        float aY = GetTruePixel(y, yFormat, Screen.height);

        if (anchor == TextAnchor.UpperCenter)
        {
            return new Rect((Screen.width - aWidth) * 0.5f + aX,
                aY,
                aWidth,
                aHeight);
        }
        else if (anchor == TextAnchor.UpperRight)
        {
            return new Rect(Screen.width - aWidth - aX,
                aY,
                aWidth,
                aHeight);
        }
        else if (anchor == TextAnchor.MiddleLeft)
        {
            return new Rect(aX,
                (Screen.height - aHeight) * 0.5f + aY,
                aWidth,
                aHeight);
        }
        else if (anchor == TextAnchor.MiddleCenter)
        {
            return new Rect((Screen.width - aWidth) * 0.5f + aX,
                (Screen.height - aHeight) * 0.5f + aY,
                aWidth,
                aHeight);
        }
        else if (anchor == TextAnchor.MiddleRight)
        {
            return new Rect(Screen.width - aWidth - aX,
                (Screen.height - aHeight) * 0.5f + aY,
                aWidth,
                aHeight);
        }
        else if (anchor == TextAnchor.LowerLeft)
        {
            return new Rect(aX,
                Screen.height - aHeight + aY,
                aWidth,
                aHeight);
        }
        else if (anchor == TextAnchor.LowerCenter)
        {
            return new Rect((Screen.width - aWidth) * 0.5f + aX,
                Screen.height - aHeight + aY,
                aWidth,
                aHeight);
        }
        else if (anchor == TextAnchor.LowerRight)
        {
            return new Rect(Screen.width - aWidth - aX,
                Screen.height - aHeight + aY,
                aWidth,
                aHeight);
        }
        else
        {
            return new Rect(aX,
                aY,
                aWidth,
                aHeight);
        }
    }

    public Rect GetMinMaxRect()
    {
        float aX = GetTruePixel(x, xFormat, Screen.width);
        float aY = GetTruePixel(y, yFormat, Screen.height);
        float aX2 = GetTruePixel(x2, x2Format, Screen.width);
        float aY2 = GetTruePixel(y2, y2Format, Screen.height);

        return Rect.MinMaxRect(aX, aY, Screen.width - aX2, Screen.height - aY2);
    }

    public float GetTruePixel(float value, eFormat format, float baseValue)
    {
        if (format == eFormat.percent)
            return PercentToPixel(value, baseValue);
        else if (format == eFormat.inch)
            return InchToPixel(value);
        else
            return value;
    }

    public float PercentToPixel(float percent, float baseValue)
    {
        return baseValue * percent / 100;
    }

    public float InchToPixel(float inch)
    {
        if (Screen.dpi == 0f)
            return inch * safeDpi;
        else
            return inch * Screen.dpi;
    }
}