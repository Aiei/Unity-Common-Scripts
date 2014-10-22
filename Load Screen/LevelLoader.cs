using UnityEngine;
using System.Collections;

public class LevelLoader : MonoBehaviour
{
    public LoadingScreen loadingScreen;

    public void Load(string levelName)
    {
        loadingScreen = (LoadingScreen)Instantiate(loadingScreen);
        DontDestroyOnLoad(loadingScreen.gameObject);
        Application.LoadLevel(levelName);
    }
}
