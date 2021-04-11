using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMap : MonoBehaviour
{
    [SerializeField]
    private GameObject prefabButton;
    [SerializeField]
    private Transform parent;

    void Start()
    {
        int scenesCount = SceneManager.sceneCountInBuildSettings;
        bool nextlevel = true;
        for (int i = 0;i < scenesCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            if (path.Contains("Assets/Scenes/Levels/"))
            {
                string levelName = path.Remove(0, 21);
                levelName = levelName.Remove(levelName.Length - 6);
                ulong score = 0;
                nextlevel = ScoreManager.instance.getScoreByLevel(levelName, out score);
                GameObject gameObject = Instantiate(prefabButton);
                gameObject.name = levelName;
                gameObject.GetComponent<LevelButton>().levelIndex = i;
                Transform _text = gameObject.transform.Find("Text");
                _text.GetComponent<Text>().text = "BESTSCORE\n" + score;
                Transform button = gameObject.transform.Find("Button");
                button.GetChild(0).GetComponent<Text>().text = levelName;
                RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
                rectTransform.SetParent(parent);          
            }
            if (!nextlevel)
            {
                return;
            }
        }
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene", LoadSceneMode.Single);
    }
}
