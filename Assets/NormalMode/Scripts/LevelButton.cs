using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{

    public int levelIndex;


    public void loadLevel()
    {
        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
    }

}
