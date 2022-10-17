using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class MNG_CARDGAME : MonoBehaviour
{
    public static MNG_CARDGAME instance;

    public List<GameObject> Field4List = new List<GameObject>();
    public List<GameObject> Field6List = new List<GameObject>();
    public List<GameObject> Field8List = new List<GameObject>();
    public List<GameObject> Field12List = new List<GameObject>();
    
    public Sprite normalImage;

    public TMP_Text startTimer;
    public TMP_Text gameTimer;
    public TMP_Text gameCombo;
    public TMP_Text gameScore;

    public TMP_Text gameOverCombo;
    public TMP_Text gameOverScore;

    public int combo = 0;
    public float comboScore = 1.1f;
    public float totalScore = 0f;
    public int score = 10;

    public List<Sprite> numSpriteList = new List<Sprite>();
    public static bool gameStart = false;
    public bool start1 = true, start2 = true, start3 = true, start4 = true;

    public List<GameObject> clickList = new List<GameObject>();

    private float startTime = 4f;
    private float gameTime = 30f;

    public GameObject field4;
    public GameObject field6;
    public GameObject field8;
    public GameObject field12;

    public GameObject gameOverUI;
    public GameObject wrongImage;

    public bool once = true;
    public bool bGameOver = true;
    void Awake()
    {
        MNG_CARDGAME.instance = this;
    }
    void Start()
    {
        Card4Setting();
        Card6Setting();
        Card8Setting();
        Card12Setting();
    }

    void Update()
    {
        if (gameStart)
        {
            if (Mathf.Floor(startTime) <= 0)
            {
                startTimer.gameObject.SetActive(false);
                if (once)
                {
                    Card4Reverse();
                    once = false;
                }

                if (Mathf.Floor(gameTime) <= 0)
                {
                    if (bGameOver)
                    {
                        gameOverUI.SetActive(true);
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
        gameOverCombo.text = combo.ToString() + " Combo";
        gameOverScore.text = totalScore.ToString() + " Score";
        if (combo >= 30)
        {
            gameCombo.color = new Color32(0, 0, 255, 255);
        }
        else if (combo >= 20)
        {
            gameCombo.color = new Color32(0, 0, 160, 255);
        }
        else if (combo >= 10)
        {
            gameCombo.color = new Color32(0, 0, 80, 255);
        }
        else if (combo == 0)
        {
            gameCombo.color = new Color32(0, 0, 0, 255);
        }
    }
    void Card4Setting()
    {
        int rand1 = Random.Range(0, 11);
        int rand2 = Random.Range(0, 11);
        while (rand2 == rand1)
        {
            rand2 = Random.Range(0, 11);
            if (rand2 != rand1)
                break;
        }

        List<int> numList = new List<int>() { 1,2,3,4};

        for (int i = 0; i < 2; ++i)
        {
            int randList = Random.Range(0, numList.Count);
            
            GameObject.Find("Field4Image"+ numList[randList]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand1];
            GameObject.Find("Field4Image" + numList[randList]).gameObject.GetComponent<ClickCard>().num = rand1;
            numList.RemoveAt(randList);
        }
        for (int i = 0; i < 2; ++i)
        {
            int randList = Random.Range(0, numList.Count);

            GameObject.Find("Field4Image" + numList[randList]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand2];
            GameObject.Find("Field4Image" + numList[randList]).gameObject.GetComponent<ClickCard>().num = rand2;
            numList.RemoveAt(randList);
        }

    }
    void Card6Setting()
    {
        List<int> randList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        int tmp = Random.Range(0, randList.Count);
        int rand1 = randList[tmp]-1;
        randList.RemoveAt(tmp);

        tmp = Random.Range(0, randList.Count);
        int rand2 = randList[tmp]-1;
        randList.RemoveAt(tmp);

        tmp = Random.Range(0, randList.Count);
        int rand3 = randList[tmp]-1;
        randList.RemoveAt(tmp);
        
        List<int> numList = new List<int>() { 1, 2, 3, 4, 5, 6 };
        for (int i = 0; i < 2; ++i)
        {
            int rand = Random.Range(0, numList.Count);
            
            GameObject.Find("CardGame").transform.Find("Field_6").transform.Find("Field6Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand1];
            GameObject.Find("CardGame").transform.Find("Field_6").transform.Find("Field6Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = rand1;
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            int rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_6").transform.Find("Field6Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand2];
            GameObject.Find("CardGame").transform.Find("Field_6").transform.Find("Field6Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = rand2;
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            int rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_6").transform.Find("Field6Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand3];
            GameObject.Find("CardGame").transform.Find("Field_6").transform.Find("Field6Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = rand3;
            numList.RemoveAt(rand);
        }
    }
    void Card8Setting()
    {
        List<int> randList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        int tmp = Random.Range(0, randList.Count);
        int rand1 = randList[tmp] - 1;
        randList.RemoveAt(tmp);

        tmp = Random.Range(0, randList.Count);
        int rand2 = randList[tmp] - 1;
        randList.RemoveAt(tmp);

        tmp = Random.Range(0, randList.Count);
        int rand3 = randList[tmp] - 1;
        randList.RemoveAt(tmp);

        tmp = Random.Range(0, randList.Count);
        int rand4 = randList[tmp] - 1;
        randList.RemoveAt(tmp);

        List<int> numList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };

        for (int i = 0; i < 2; ++i)
        {
            int rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_8").transform.Find("Field8Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand1];
            GameObject.Find("CardGame").transform.Find("Field_8").transform.Find("Field8Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = rand1;
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            int rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_8").transform.Find("Field8Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand2];
            GameObject.Find("CardGame").transform.Find("Field_8").transform.Find("Field8Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = rand2;
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            int rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_8").transform.Find("Field8Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand3];
            GameObject.Find("CardGame").transform.Find("Field_8").transform.Find("Field8Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = rand3;
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            int rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_8").transform.Find("Field8Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[rand4];
            GameObject.Find("CardGame").transform.Find("Field_8").transform.Find("Field8Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = rand4;
            numList.RemoveAt(rand);
        }
    }

    public void Card12Setting()
    {
        List<int> randList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        List<int> num = new List<int>();
        for (int i = 0; i < 6; ++i)
        {
            int tmp = Random.Range(0, randList.Count);
            int rand1 = randList[tmp] - 1;
            num.Add(rand1);
            randList.RemoveAt(tmp);
        }

        List<int> numList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };

        int rand;

        for (int i = 0; i < 2; ++i)
        {
            rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[num[0]];
            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = num[0];
            numList.RemoveAt(rand);

        }
        for (int i = 0; i < 2; ++i)
        {
            rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[num[1]];
            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = num[1];
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[num[2]];
            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = num[2];
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[num[3]];
            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = num[3];
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[num[4]];
            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = num[4];
            numList.RemoveAt(rand);
        }
        for (int i = 0; i < 2; ++i)
        {
            rand = Random.Range(0, numList.Count);

            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<Image>().sprite = numSpriteList[num[5]];
            GameObject.Find("CardGame").transform.Find("Field_12").transform.Find("Field12Image" + numList[rand]).gameObject.GetComponent<ClickCard>().num = num[5];
            numList.RemoveAt(rand);
        }
    }

    void Card4Reverse()
    {
        for (int i = 0; i < Field4List.Count; ++i)
        {
            Field4List[i].GetComponent<Image>().sprite = normalImage;
        }
    }

    void test()
    {
        once = true;
    }

    public void WrongCard()
    {
        for (int i = 0; i < Field4List.Count; ++i)
        {
            Field4List[i].GetComponent<Button>().enabled = false;
        }
        for (int i = 0; i < Field6List.Count; ++i)
        {
            Field6List[i].GetComponent<Button>().enabled = false;
        }
        for (int i = 0; i < Field8List.Count; ++i)
        {
            Field8List[i].GetComponent<Button>().enabled = false;
        }
        for (int i = 0; i < Field12List.Count; ++i)
        {
            Field12List[i].GetComponent<Button>().enabled = false;
        }
    }
    public void WrongCardEnable()
    {
        for (int i = 0; i < Field4List.Count; ++i)
        {
            Field4List[i].GetComponent<Button>().enabled = true;
        }
        for (int i = 0; i < Field6List.Count; ++i)
        {
            Field6List[i].GetComponent<Button>().enabled = true;
        }
        for (int i = 0; i < Field8List.Count; ++i)
        {
            Field8List[i].GetComponent<Button>().enabled = true;
        }
        for (int i = 0; i < Field12List.Count; ++i)
        {
            Field12List[i].GetComponent<Button>().enabled = true;
        }
    }

    public void Restart()
    {
        combo = 0;
        totalScore = 0;
        comboScore = 1.1f;
        score = 10;
        field12.SetActive(false);
        field8.SetActive(false);
        field6.SetActive(false);
        field4.SetActive(true);
        for (int i = 0; i < Field4List.Count; ++i)
            Field4List[i].GetComponent<Image>().color = new Color(255, 255, 255);

        Card4Setting();
        Card6Setting();
        Card8Setting();
        Card12Setting();
        gameTime = 30f;
        startTime = 4f;
        gameOverUI.SetActive(false);
        bGameOver = true;
        once = true;
        startTimer.gameObject.SetActive(true);
        start1 = true;
        start2 = true;
        start3 = true;
        start4 = true;
        ClickCard.cnt = 0;
        gameScore.text = "0";
        gameCombo.text = "0";
    }

    public void ReturnMain()
    {
        combo = 0;
        totalScore = 0;
        comboScore = 1.1f;
        score = 10;
        field12.SetActive(false);
        field8.SetActive(false);
        field6.SetActive(false);
        field4.SetActive(true);
        for (int i = 0; i < Field4List.Count; ++i)
            Field4List[i].GetComponent<Image>().color = new Color(255, 255, 255);

        Card4Setting();
        Card6Setting();
        Card8Setting();
        Card12Setting();
        gameTime = 30f;
        startTime = 4f;
        gameOverUI.SetActive(false);
        bGameOver = true;
        once = true;
        startTimer.gameObject.SetActive(true);
        start1 = true;
        start2 = true;
        start3 = true;
        start4 = true;
        ClickCard.cnt = 0;
        gameScore.text = "0";
        gameCombo.text = "0";
    }
}
