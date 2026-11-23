using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameoverText;
    public Text timeText;
    public Text recordText;
    public Text levelText;

    public float levelDuration = 10f;
    public float[] bulletSpeeds = { 5f, 6f, 8f, 10f, 11f };
    public float[] spawnRateScales = { 1.4f, 1.1f, 0.9f, 0.7f, 0.6f };


    private float surviveTime;
    private bool isGameover;
    private int level; // 1/2/3/4/MAX

    public float CurrentBulletSpeed => bulletSpeeds[level];
    public float CurrentSpawnScale => spawnRateScales[level];

    public CustomizeManager customize;

    public bool IsGameover() { return isGameover; }
    public int GetLevel() { return level; }

    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        surviveTime = 0;
        isGameover = false;
        UpdateLevelText();
    }


    void Update()
    {
        if (!isGameover)
        {
            surviveTime += Time.deltaTime;
            timeText.text = "Time : " + (int)surviveTime;
            int newLevel = Mathf.Min((int)(surviveTime / levelDuration), 4);
            if (newLevel != level)
            {
                level = newLevel;
                UpdateLevelText();
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
    void UpdateLevelText()
    {
        if (levelText == null) return;
        levelText.text = level >= 4 ? "Lv MAX" : "Lv " + (level + 1);
    }

    public void EndGame()
    {
        isGameover = true;
        gameoverText.SetActive(true);
        recordText.gameObject.SetActive(true);

        float bestTime = PlayerPrefs.GetFloat("BestTime");

        if (surviveTime > bestTime) {
            bestTime = surviveTime;
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        recordText.text = "Best Time : " + (int)bestTime;
        customize.ShowButton();
    }
}
