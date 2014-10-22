using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
    public Texture texture;

    protected int levelIndex;

    protected Rect rect;

    public void Start()
    {
        levelIndex = Application.loadedLevel;
        rect = Rect.MinMaxRect(0, 0, Screen.width, Screen.height);
    }

    public void OnGUI()
    {
        if (Application.loadedLevel == levelIndex)
        {
            GUI.DrawTexture(rect, texture);
        }
    }
}
