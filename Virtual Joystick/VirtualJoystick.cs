using UnityEngine;
using System.Collections;

public class VirtualJoystick : MonoBehaviour
{
    TextAnchor anchor = TextAnchor.LowerLeft;
    FormattedRect.eFormat format = FormattedRect.eFormat.inch;

    public float interactionSize = 1.1f;
    public float visualSize = 0.8f;
    public float thumbSize = 0.4f;

    public Texture image1;
    public Texture image2;

    Vector2 defaultCenter;
    Vector2 center;

    Vector2 thumb;
    public float thumbDamp = 0.42f;

    int fingerIndex = -1;

    bool bVisible = false;
    public float activeFade = 1f;
    public float inactiveFade = 5f;
    public float activeOpacity = 1.0f;
    public float inactiveOpacity = 0.3f;
    float currentOpacity;

    float pInteractionSize;
    float pVisualSize;
    float pThumbSize;

    Rect interactionRect;
    Rect visualRect;
    Rect thumbRect;

    FormattedRect FRect;
    
    Vector2 axis = Vector2.zero;

    void FullReset()
    {
        center = defaultCenter;
        thumb = center;
        axis = Vector2.zero;
        fingerIndex = -1;
    }

    void Reset()
    {
        thumb = center;
        axis = Vector2.zero;
        fingerIndex = -1;
        Invoke("FullReset", 1.0f);
    }

    void HandleTouches()
    {
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                OnTouchStarted(touch);
            }
            else if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                OnTouchMoved(touch);
            }
            else
            {
                OnTouchEnded(touch);
            }
        }
    }

    void OnTouchStarted(Touch touch)
    {
        if (interactionRect.Contains(touch.position) && fingerIndex == -1)
        {
            fingerIndex = touch.fingerId;

            center = touch.position;
            // Make sure the center box never set outside screen
            center.x = Mathf.Clamp(center.x, pVisualSize / 2, Screen.width - pVisualSize / 2);
            center.y = Mathf.Clamp(center.y, pVisualSize / 2, Screen.height - pVisualSize / 2);
            thumb = center;

            SetVisibility(true);

            CancelInvoke("FullReset");
        }
    }

    void OnTouchMoved(Touch touch)
    {
        if (touch.fingerId == fingerIndex)
        {
            thumb = touch.position;
            axis = thumb - center;
            axis = axis.normalized;
        }
    }

    void OnTouchEnded(Touch touch)
    {
        if (touch.fingerId == fingerIndex)
        {
            Reset();

            SetVisibility(false);
        }
    }

    Vector2 InvertY(Vector2 v)
    {
        return new Vector2(v.x, Screen.height - v.y);
    }

    Rect InvertY(Rect r)
    {
        return new Rect(r.x, Screen.height - r.y - r.height, r.width, r.height);
    }

    // Set actual pixel value based on desired format with FormattedRect
    void RescaleSizes()
    {
        pInteractionSize = FRect.GetTruePixel(interactionSize, format);
        pVisualSize = FRect.GetTruePixel(visualSize, format);
        pThumbSize = FRect.GetTruePixel(thumbSize, format);
    }

    void MakeRects()
    {
        // Define center after the rect built
        center = interactionRect.center;
    }

    void SetInteraction()
    {
        // Align those rects to screen anchor default lower left
        interactionRect = FRect.AnchorRect(0, 0, pInteractionSize, pInteractionSize, anchor);
        // Invert y because of unity bullshit screen coordinate system
        interactionRect = InvertY(interactionRect);
        // Set default center with offset
        defaultCenter = Vector2.Lerp(interactionRect.center,
            new Vector2(interactionRect.xMax, interactionRect.yMax),
            0.1f);
    }

    void SetVisual()
    {
        visualRect = FRect.CenteredRect(center.x, center.y, pVisualSize, pVisualSize);
        visualRect = InvertY(visualRect);
    }

    void SetThumb()
    {
        thumbRect = FRect.CenteredRect(thumb.x, thumb.y, pThumbSize, pThumbSize);
        thumbRect = InvertY(thumbRect);

        if (Vector2.Distance(thumb, center) > pVisualSize * thumbDamp)
        {
            float t = (pVisualSize * thumbDamp) / Vector2.Distance(thumb, center);
            Vector2 aThumb = Vector2.Lerp(center, thumb, t);

            thumbRect = FRect.CenteredRect(aThumb.x, aThumb.y, pThumbSize, pThumbSize);
            thumbRect = InvertY(thumbRect);
        }
    }

    void SetVisibility(bool b)
    {
        bVisible = b;
    }

    void UpdateVisibility()
    {
        if (bVisible == true)
        {
            currentOpacity += inactiveFade * Time.deltaTime;
        }
        else
        {
            currentOpacity -= activeFade * Time.deltaTime;
        }
        currentOpacity = Mathf.Clamp(currentOpacity, inactiveOpacity, activeOpacity);
    }

    void Start()
    {
        FRect = gameObject.AddComponent<FormattedRect>();

        currentOpacity = inactiveOpacity;

        RescaleSizes();
        SetInteraction();
        center = defaultCenter;
        SetVisual();
        thumb = center;
        SetThumb();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            HandleTouches();
        }

        SetVisual();
        SetThumb();
        UpdateVisibility();
    }

    void OnGUI()
    {
        GUI.color = new Color(1, 1, 1, currentOpacity);
        GUI.DrawTexture(visualRect, image1);
        GUI.DrawTexture(thumbRect, image2);
    }
}