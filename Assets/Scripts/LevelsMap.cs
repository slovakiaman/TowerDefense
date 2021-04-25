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
    private GameObject parent;
    [SerializeField]
    private GameObject viewPort;
    private int levelsOnBiom = 4;
    [SerializeField]
    private int biomIndex = 0;
    [SerializeField]
    private int maxBiomIndex = 0;
    [SerializeField]
    private GameObject previousBiomButton;
    [SerializeField]
    private GameObject nextBiomButton;
    [SerializeField]
    private Transform biomsParent;

    private List<Level> levels = new List<Level>();

    void Start()
    {
        int scenesCount = SceneManager.sceneCountInBuildSettings;
        bool nextlevel = true;
        bool locked = false;
        for (int i = 0;i < scenesCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            if (path.Contains("Assets/Scenes/Levels/"))
            {
                string levelName = path.Remove(0, 21);
                levelName = levelName.Remove(levelName.Length - 6);
                ulong score = 0;
                nextlevel = ScoreManager.instance.getScoreByLevel(levelName, out score);
                levels.Add(new Level(levelName, score, i,locked));
                if(!locked && score == 0)
                {
                    if(levels.Count % levelsOnBiom != 0)
                    {
                        biomIndex = levels.Count / levelsOnBiom;
                    }
                    else
                    {
                        biomIndex = (levels.Count / levelsOnBiom) -1 ;
                    }
                    
                }
                locked = !nextlevel;
            }
            
        }
        if (levels.Count%levelsOnBiom == 0)
        {
            maxBiomIndex = (levels.Count / levelsOnBiom) - 1;
        }
        else
        {
            maxBiomIndex = levels.Count / levelsOnBiom;
        }
        showLevels();
    }

    void Update()
    {
        float width = viewPort.GetComponent<RectTransform>().rect.width;
        float y = (width / 5) / 6 * 10;
        Vector2 newSize = new Vector2(width / 5, y);
        parent.GetComponent<GridLayoutGroup>().cellSize = newSize;
    }

    public void nextBiom()
    {
        biomsParent.Find(biomIndex + "").gameObject.SetActive(false);
        biomIndex++;
        showLevels();
    }

    public void previousBiom()
    {
        biomsParent.Find(biomIndex + "").gameObject.SetActive(false);
        biomIndex--;
        showLevels();
    }

    public void showLevels()
    {
        if (biomIndex == 0)
        {
            previousBiomButton.SetActive(false);
        }
        else
        {
            previousBiomButton.SetActive(true);
        }

        if (biomIndex >= maxBiomIndex)
        {
            nextBiomButton.SetActive(false);
        }
        else
        {
            nextBiomButton.SetActive(true);
        }
        for (int i = parent.transform.childCount; i > 0; i--)
        {
            Destroy(parent.transform.GetChild(i-1).gameObject);
        }

        biomsParent.Find(biomIndex + "").gameObject.SetActive(true);

        for (int i = 0 + biomIndex * levelsOnBiom; i < levelsOnBiom + biomIndex * levelsOnBiom; i++)
        {
            if(levels.Count-1 < i )
            {
                break;
            }
            Level level = levels[i];
            GameObject gameObject = Instantiate(prefabButton);
            if (level.isLocked)
            {
                gameObject.GetComponent<Button>().interactable = false;
                gameObject.transform.Find("DisabledBtn").gameObject.SetActive(true);
                gameObject.transform.Find("Level_image").Find("Locked").gameObject.SetActive(true);
            }
            else
            {
                if (gameObject.transform.Find("Level_image").Find(level.levelName) != null)
                {
                    gameObject.transform.Find("Level_image").Find(level.levelName).gameObject.SetActive(true);
                }
            }
            gameObject.name = level.levelName;
            gameObject.GetComponent<LevelButton>().levelIndex = level.index;
            Transform _text = gameObject.transform.Find("Level_score").Find("Text");
            _text.GetComponent<Text>().text = "BESTSCORE\n" + level.levelScore;
            Transform name_text = gameObject.transform.Find("Level_name").Find("Text");
            name_text.GetComponent<Text>().text = level.levelName;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.SetParent(parent.transform);
        }
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Scenes/MainMenuScene", LoadSceneMode.Single);
    }
}

[System.Serializable]
public class Level
{
    public string levelName;
    public ulong levelScore;
    public int index;
    public bool isLocked;

    public Level(string name, ulong score,int _index,bool _isLocked)
    {
        levelName = name;
        levelScore = score;
        index = _index;
        isLocked = _isLocked;
    }
}
