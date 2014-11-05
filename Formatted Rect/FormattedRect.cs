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

    float safeDpi = 220f;

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

    public Rect CenteredRect(float x, float y, float width, float height)
    {
        return new Rect(x - width / 2, y - height / 2, width, height);
    }

    public Rect AnchorRect(float _x, float _y, float _width, float _height, TextAnchor _anchor)
    {
        if (_anchor == TextAnchor.UpperCenter)
        {
            return new Rect((Screen.width - _width) * 0.5f + _x,
                _y,
                _width,
                _height);
        }
        else if (_anchor == TextAnchor.UpperRight)
        {
            return new Rect(Screen.width - _width - _x,
                _y,
                _width,
                _height);
        }
        else if (_anchor == TextAnchor.MiddleLeft)
        {
            return new Rect(_x,
                (Screen.height - _height) * 0.5f + _y,
                _width,
                _height);
        }
        else if (_anchor == TextAnchor.MiddleCenter)
        {
            return new Rect((Screen.width - _width) * 0.5f + _x,
                (Screen.height - _height) * 0.5f + _y,
                _width,
                _height);
        }
        else if (_anchor == TextAnchor.MiddleRight)
        {
            return new Rect(Screen.width - _width - _x,
                (Screen.height - _height) * 0.5f + _y,
                _width,
                _height);
        }
        else if (_anchor == TextAnchor.LowerLeft)
        {
            return new Rect(_x,
                Screen.height - _height - _y,
                _width,
                _height);
        }
        else if (_anchor == TextAnchor.LowerCenter)
        {
            return new Rect((Screen.width - _width) * 0.5f + _x,
                Screen.height - _height + _y,
                _width,
                _height);
        }
        else if (_anchor == TextAnchor.LowerRight)
        {
            return new Rect(Screen.width - _width - _x,
                Screen.height - _height - _y,
                _width,
                _height);
        }
        else
        {
            return new Rect(_x,
                _y,
                _width,
                _height);
        }
    }

    public Rect GetStandardRect()
    {
        float aWidth = GetTruePixel(width, widthFormat, Screen.width);
        float aHeight = GetTruePixel(height, heightFormat, Screen.height);
        float aX = GetTruePixel(x, xFormat, Screen.width);
        float aY = GetTruePixel(y, yFormat, Screen.height);

        return AnchorRect(aX, aY, aWidth, aHeight, anchor);
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

    public float GetTruePixel(float value, eFormat format)
    {
        if (format == eFormat.percent)
            return PercentToPixel(value, Screen.width);
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

    public Rect LimitEdge(Rect r)
    {
        float gap;

        if (r.xMin < 0)
        {
            gap = Mathf.Abs(r.xMin);
            r.x += gap;
        }
        else if (r.xMax > Screen.width)
        {
            gap = r.xMax - Screen.width;
            r.x -= gap;
        }
        else if (r.yMin < 0)
        {
            gap = Mathf.Abs(r.yMin);
            r.y += gap;
        }
        else if (r.yMax > Screen.height)
        {
            gap = r.yMax - Screen.height;
            r.y -= gap;
        }

        return r;
    }
}