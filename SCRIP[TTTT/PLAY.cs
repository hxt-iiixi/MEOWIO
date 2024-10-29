using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PLAY : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVELS");
    }
    public void Costumize()
    {
        SceneManager.LoadSceneAsync(sceneName: "Character Selection");
    }
    public void Settings()
    {
        SceneManager.LoadSceneAsync(sceneName: "SETTINGS");
    }
    public void Back()
    {
        SceneManager.LoadSceneAsync(sceneName: "MAIN");
    }

    public void Level1()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 1");
    }
    public void Level2()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 2");
    }
    public void Level3()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 3");
    }
    public void Level4()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 4");
    }
    public void Level5()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 5");
    }
    public void Level6()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 6");
    }
    public void Level7()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 7");
    }
    public void Level8()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 8");
    }
    public void Level9()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 9");
    }
    public void Level10()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVEL 10");
    }

    public void LevelS ()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEVELS");
    }

    public void User()
    {
        SceneManager.LoadSceneAsync(sceneName: "USER");
    }

    public void LOGIN()
    {
        SceneManager.LoadSceneAsync(sceneName: "LOG IN");
    }

    public void SIGNUP()
    {
        SceneManager.LoadSceneAsync(sceneName: "SIGN UP");
    }

    public void LEADER()
    {
        SceneManager.LoadSceneAsync(sceneName: "LEAD");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
