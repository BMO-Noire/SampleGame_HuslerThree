using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MNG_COLORGAME : MonoBehaviour
{
    public static MNG_COLORGAME instance;

    public List<GameObject> imageList = new List<GameObject>();
    public List<GameObject> colorList = new List<GameObject>();

    public List<GameObject> TempList = new List<GameObject>();

    public Transform parent;
    public TMP_Text scoreText;
    public TMP_Text comboText;

    public TMP_Text startTimer;
    public TMP_Text gameTimer;

    public GameObject gameOverUI;
    public TMP_Text gameOverCombo;
    public TMP_Text gameOverScore;

    public GameObject wrongImage;

    int totalScore = 0;
    int combo = 0;
    int score = 10;
    private float startTime = 4f;
    private float gameTime = 20f;
    public static bool gameStart = false;
    public bool bGameOver = true;

    void Awake()
    {
        MNG_COLORGAME.instance = this;
    }
    void Start()
    {
        JsManager.onGameLoaded();
        CreateColor();
        scoreText.text = totalScore.ToString() + " Score";
        comboText.text = combo.ToString() + " Combo";
        JsManager.gameStart();
    }

    void Update()
    {
        if (gameStart)
        {

            if (Mathf.Floor(startTime) <= 0)
            {
                startTimer.gameObject.SetActive(false);

                if (Mathf.Floor(gameTime) <= 0)
                {
                    if (bGameOver)
                    {
                        gameOverUI.SetActive(true);
                        JsManager.gameEnd(score.ToString());
                        bGameOver = false;
                    }

                }
                else
                {
                    gameTime -= Time.deltaTime;
                    gameTimer.text = Mathf.Floor(gameTime).ToString() + "s";
                    if (gameTime <= 6)
                    {
                        gameTimer.color = new Color(255, 0, 0);
                    }
                }
            }
            else
            {
                startTime -= Time.deltaTime;
                startTimer.text = Mathf.Floor(startTime).ToString();
            }
        }

        if (combo > 30)
        {
            score = 20;
        }
        else if (combo > 20)
        {
            score = 15;
        }
        else if (combo > 10)
        {
            score = 12;
        }

        if (combo >= 30)
        {
            comboText.color = new Color32(0, 0, 255, 255);
        }
        else if (combo >= 20)
        {
            comboText.color = new Color32(0, 0, 160, 255);
        }
        else if (combo >= 10)
        {
            comboText.color = new Color32(0, 0, 80, 255);
        }
        else if (combo == 0)
        {
            comboText.color = new Color32(0, 0, 0, 255);
        }
    }

    public void SelectBtn(string color)
    {
        if (color == "RedYellow")
        {
            if (colorList[0].name == "ImageRed(Clone)")
            {
                colorList.RemoveAt(0);

                int rand = Random.Range(0, imageList.Count);
                GameObject obj = MonoBehaviour.Instantiate(imageList[rand], parent);
                colorList.Add(obj);

                DeleteChilds(0);
                for (int i = 0; i < 10; ++i)
                    colorList[i].transform.position = new Vector3(0, (-4f + i) - (0.1f * i), 0);

                ++combo;
                totalScore += score;
            }
            else if (colorList[0].name == "ImageYellow(Clone)")
            {
                colorList.RemoveAt(0);

                int rand = Random.Range(0, imageList.Count);
                GameObject obj = MonoBehaviour.Instantiate(imageList[rand], parent);
                colorList.Add(obj);

                DeleteChilds(0);
                for (int i = 0; i < 10; ++i)
                    colorList[i].transform.position = new Vector3(0, (-4f + i) - (0.1f * i), 0);

                ++combo;
                totalScore += score;
            }
            else
            {
                combo = 0;
                totalScore -= 30;
                wrongImage.SetActive(true);
                Invoke("wrongImageInvoke",0.5f);
            }

        }
        else if (color == "BlueGreen")
        {
            if (colorList[0].name == "ImageGreen(Clone)")
            {
                colorList.RemoveAt(0);

                int rand = Random.Range(0, imageList.Count);
                GameObject obj = MonoBehaviour.Instantiate(imageList[rand], parent);
                colorList.Add(obj);

                DeleteChilds(0);
                for (int i = 0; i < 10; ++i)
                    colorList[i].transform.position = new Vector3(0, (-4f + i) - (0.1f * i), 0);

                ++combo;
                totalScore += score;

            }
            else if (colorList[0].name == "ImageBlue(Clone)")
            {
                colorList.RemoveAt(0);

                int rand = Random.Range(0, imageList.Count);
                GameObject obj = MonoBehaviour.Instantiate(imageList[rand], parent);
                colorList.Add(obj);

                DeleteChilds(0);
                for (int i = 0; i < 10; ++i)
                    colorList[i].transform.position = new Vector3(0, (-4f + i) - (0.1f * i), 0);

                ++combo;
                totalScore += score;
            }
            else
            {
                combo = 0;
                totalScore -= 30;
                wrongImage.SetActive(true);
                Invoke("wrongImageInvoke", 0.5f);
            }

        }
        if (totalScore < 0)
            totalScore = 0;


        scoreText.text = totalScore.ToString() + " Score";
        comboText.text = combo.ToString() + " Combo";

        gameOverCombo.text = scoreText.text;
        gameOverScore.text = comboText.text;
    }

    void CreateColor()
    {
        for (int i = 0; i < 10; ++i)
        {
            int rand = Random.Range(0, imageList.Count);
            GameObject obj = MonoBehaviour.Instantiate(imageList[rand], parent);
            colorList.Add(obj);
            //obj.name = "clone";

            //obj.transform.position 
            colorList[i].transform.position = new Vector3(0, (-4f + i) - (0.1f * i), 0);
        }
    }

    public void DeleteChilds(int num)
    {
        Destroy(parent.GetChild(num).gameObject);
    }

    public void Restart()
    {
        combo = 0;
        score = 10;
        totalScore = 0;
        startTime = 4f;
        gameTime = 20f;
        gameOverUI.SetActive(false);
        startTimer.gameObject.SetActive(true);
        bGameOver = true;
        gameStart = true;
        scoreText.text = totalScore.ToString() + " Score";
        comboText.text = combo.ToString() + " Combo";
        JsManager.gameStart();
    }

    public void ReturnMain()
    {
        combo = 0;
        score = 10;
        totalScore = 0;
        startTime = 4f;
        gameTime = 20f;
        gameOverUI.SetActive(false);
        startTimer.gameObject.SetActive(true);
        bGameOver = true;
        gameStart = true;
        scoreText.text = totalScore.ToString() + " Score";
        comboText.text = combo.ToString() + " Combo";
    }

    void wrongImageInvoke()
    {
        wrongImage.SetActive(false);
    }
}
