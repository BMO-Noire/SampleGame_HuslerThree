using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class MNG_HAMMERGAME : MonoBehaviour
{
    public static MNG_HAMMERGAME instance;
    public bool gameStart = false;
    public bool gameOver = true;

    public int score;
    public int combo;

    public float IntervalRangeMin;
    public float IntervalRangeMax;
    private float intervalRangeMin;
    private float intervalRangeMax;

    public float DefaultAnimationSpeed;
    public float SpeedModifier;
    public float DifficultyModifier;
    private float defaultAnimationSpeed;
    private float speedModifier;
    private float difficultyModifier;

    private float startTime = 4f;
    private float gameTime = 30f;
    public float StartTime;
    public float GameTime;

    public List<GameObject> PlaceList;
    public List<GameObject> Animals;
    public GameObject wrongImage;
    public TMP_Text scoreText;
    public TMP_Text comboText;

    public TMP_Text startTimer;
    public TMP_Text gameTimer;

    public GameObject gameOverUI;
    public TMP_Text gameOverCombo;
    public TMP_Text gameOverScore;

    public GameObject Field;



    private List<int> emptyPlaceList = new List<int>();
    private float timer;
    private float intervalTime;
    void Awake()
    {
        MNG_HAMMERGAME.instance = this;
        defaultAnimationSpeed = DefaultAnimationSpeed;
        speedModifier = SpeedModifier;
        difficultyModifier = DifficultyModifier;
        intervalRangeMax = IntervalRangeMax;
        intervalRangeMin = IntervalRangeMin;
        gameTime = GameTime;
        startTime = StartTime;
    }
    // Start is called before the first frame update
    void Start()
    {
        JsManager.onGameLoaded();
        //JsManager.Hello();
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameStart)
        {
            if (Mathf.Floor(StartTime) <= 0)
            {
                startTimer.gameObject.SetActive(false);

                if (Mathf.Floor(GameTime) <= 0)
                {
                    if (gameOver)
                    {
                        //Field.SetActive(false);
                        gameOverUI.SetActive(true);
                        gameOver = false;
                        gameStart = false;
                        gameOverScore.text = score.ToString() + " Score";
                        gameOverCombo.text = combo.ToString() + " Combo";
                        JsManager.gameEnd(score.ToString());
                    }

                }
                else
                {
                    GameTime -= Time.deltaTime;
                    gameTimer.text = Mathf.Floor(GameTime).ToString() + "s";
                    if (GameTime <= 6)
                    {
                        gameTimer.color = new Color(255, 0, 0);
                    }
                    float currentTime = Time.time;
                    if (currentTime - timer >= intervalTime)
                    {
                        intervalTime = Random.Range(IntervalRangeMax, IntervalRangeMax);
                        timer = Time.time;
                        MakeObject();
                    }
                }
            }
            else
            {
                StartTime -= Time.deltaTime;
                startTimer.text = Mathf.Floor(StartTime).ToString();
            }
        }
        if (combo >= 15)
        {
            comboText.color = new Color32(0, 0, 255, 255);
        }
        else if (combo >= 10)
        {
            comboText.color = new Color32(0, 0, 160, 255);
        }
        else if (combo >= 5)
        {
            comboText.color = new Color32(0, 0, 80, 255);
        }
        else if (combo == 0)
        {
            comboText.color = new Color32(0, 0, 0, 255);
        }
        scoreText.text = score.ToString() + " Score";
        comboText.text = combo.ToString() + " Combo";
    }
    void MakeObject()
    {
        if (emptyPlaceList.Count == 0) return;
        int Type = Random.Range(0, 3);
        int PlaceNum = emptyPlaceList[Random.Range(0, emptyPlaceList.Count-1)];
        emptyPlaceList.Remove(PlaceNum);
        GameObject obj = Instantiate(Animals[Type], PlaceList[PlaceNum].transform);
        obj.GetComponent<HammerGameObject>().parentIdx = PlaceNum;
    }

    public void ProcessObject(int type, int parentIdx, bool isClicked)
    {
        emptyPlaceList.Add(parentIdx);
        if (isClicked)
        { 
            switch (type)
            {
                case 1: // Rat
                    score += 1 + combo;
                    combo++;
                    break;
                case 2: // Ferret
                    score += (1 + combo) * 2;
                    combo++;
                    break;
                case 3: // Tiger
                    wrongImage.SetActive(true);
                    Invoke("wrongImageInvoke", 0.5f);
                    score -= (1 + combo) * 2;
                    combo = 0;
                    break;
            }
        }
        else
        {
            if (type != 3)
            {
                combo = 0;
            }
        }
        DefaultAnimationSpeed = defaultAnimationSpeed + (defaultAnimationSpeed * Mathf.Pow(DifficultyModifier, combo));
    }

    public void Initialize()
    {
        JsManager.gameStart();
        score = 0;
        combo = 0;
        IntervalRangeMin = intervalRangeMin;
        IntervalRangeMax = intervalRangeMax;
        SpeedModifier = speedModifier;
        DifficultyModifier = difficultyModifier;
        DefaultAnimationSpeed = defaultAnimationSpeed;
        StartTime = startTime;
        GameTime = gameTime;
        timer = Time.time;
        intervalTime = Random.Range(IntervalRangeMax, IntervalRangeMax);
        emptyPlaceList.Clear();
        for (int i = 0; i < 8; i++)
        {
            emptyPlaceList.Add(i);
        }

        gameTimer.color = new Color(0, 0, 0);
        gameOverUI.SetActive(false);
        startTimer.gameObject.SetActive(true);
        Field.SetActive(true);

        gameStart = true;
        gameOver = true;
    }
    public void ReturnMain()
    {
        gameStart = false;
    }
    public void Restart()
    {

        Initialize();
    }

    void wrongImageInvoke()
    {
        wrongImage.SetActive(false);
    }
}
