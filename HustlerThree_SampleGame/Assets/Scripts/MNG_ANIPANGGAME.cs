using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MNG_ANIPANGGAME : MonoBehaviour
{
    public static MNG_ANIPANGGAME instance;
    public const int LEFT = 4;
    public const int RIGHT = 6;
    public const int UP = 8;
    public const int DOWN = 2;
    public const int POS_PRESET = -4;
    public const int FIELD_X = 9;
    public const int FIELD_Y = 14;

    public List<Sprite> img;
    public List<Sprite> img2;

    public TMP_Text startTimer;
    public TMP_Text gameTimer;
    public TMP_Text gameCombo;
    public TMP_Text gameScore;

    public TMP_Text gameOverCombo;
    public TMP_Text gameOverScore;

    private GameObject[,] m_Field = new GameObject[FIELD_X, FIELD_Y];
    public GameObject[,] Field {
        get {return m_Field;}
    }

    private bool m_initChecker = false;
    private int m_score = 0;
    private int m_combo = 0;

    public static bool gameStart = false;
    public bool once = true;
    private float startTime = 4f;
    private float gameTime = 40f;
    public bool bGameOver = true;
    public GameObject gameOverUI;
    void Awake()
    {
        MNG_ANIPANGGAME.instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        JsManager.onGameLoaded();
        JsManager.gameStart();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            if (Mathf.Floor(startTime) <= 0)
            {
                startTimer.gameObject.SetActive(false);

                if (once)
                {
                    once = false;
                    initialize();
                    for (int i = 0; i < FIELD_X; i++)
                    {
                        for (int j = 0; j < FIELD_Y; j++)
                        {
                            if (m_Field[i, j] != null)
                                CheckAllDirection(m_Field[i, j]);
                        }
                    }
                    m_initChecker = true;
                    m_score = 0;
                }
                if (Mathf.Floor(gameTime) <= 0)
                {
                    if (bGameOver)
                    {
                        StopAllCoroutines();
                        foreach (ObjectScript child in this.gameObject.GetComponentsInChildren<ObjectScript>())
                        {
                            Destroy(child.transform.gameObject);
                        }
                        for (int i = 0; i < FIELD_X; i++)
                        {
                            for (int j = 0; j < FIELD_Y; j++)
                            {
                                m_Field[i, j] = null;
                            }
                        }
                        gameOverUI.SetActive(true);
                        JsManager.gameEnd(m_score.ToString());
                        bGameOver = false;
                    }

                }
                else
                {
                    if (m_initChecker)
                    {
                        m_initChecker = false;
                        StartCoroutine(AllCheck());
                        m_initChecker = false;
                    }
                    gameTime -= Time.deltaTime;
                    gameTimer.text = Mathf.Floor(gameTime).ToString() + "s";
                }
            }
            else
            {
                startTime -= Time.deltaTime;
                startTimer.text = Mathf.Floor(startTime).ToString();
            }
            gameScore.text = m_score.ToString();
            gameCombo.text = m_combo.ToString();
        }
        gameOverCombo.text = m_combo.ToString() + " Combo";
        gameOverScore.text = m_score.ToString() + " Score";

    }

    private void initialize() {
        int tmpSpriteType = -1;

        foreach (ObjectScript child in this.gameObject.GetComponentsInChildren<ObjectScript>())
        {
            int childPosX = child.posX;
            int childPosY = child.posY;
            GameObject tmpObj = m_Field[childPosX - POS_PRESET, childPosY - POS_PRESET];
            if (tmpObj != child.transform.gameObject)
            {
                GameObject tmp = m_Field[childPosX - POS_PRESET, childPosY - POS_PRESET];
                m_Field[childPosX - POS_PRESET, childPosY - POS_PRESET] = null;
                Destroy(tmp);
                m_Field[childPosX - POS_PRESET, childPosY - POS_PRESET] = child.transform.gameObject;
            }
        }
        for (int i = 0; i < FIELD_X; i++)
        {
            for (int j = 0; j < FIELD_Y; j++)
            {
                if (m_Field[i, j] != null)
                {
                    continue;
                }
                tmpSpriteType = UnityEngine.Random.Range(0, img.Count);
                m_Field[i, j] = new GameObject("img" + i.ToString() + j.ToString());

                m_Field[i, j].AddComponent<SpriteRenderer>().sprite = img[tmpSpriteType];

                m_Field[i, j].AddComponent<ObjectScript>().spriteType = tmpSpriteType;
                m_Field[i, j].GetComponent<ObjectScript>().posX = i + POS_PRESET;
                m_Field[i, j].GetComponent<ObjectScript>().posY = j + POS_PRESET;

                m_Field[i, j].AddComponent<BoxCollider2D>();
                m_Field[i, j].GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);

                m_Field[i, j].GetComponent<Transform>().parent = this.gameObject.transform;

                //m_Field[i, j].GetComponent<Transform>().SetPositionAndRotation(new Vector3(i + POS_PRESET, j + POS_PRESET), new Quaternion());
                m_Field[i, j].GetComponent<Transform>().localPosition = new Vector3(i + POS_PRESET/2, j + POS_PRESET/2);
                m_Field[i, j].GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
            }
        }
        m_initChecker = true;
    }

    IEnumerator AllCheck()
    {

        m_initChecker = false;
        //if (m_initChecker)
        //{
            yield return new WaitForSeconds(0.3f);
        //}
        //else
        //{
        //    yield return new WaitForSeconds(0);
        //}
        for (int i = 0; i < FIELD_X; i++)
        {
            for (int j = 0; j < FIELD_Y; j++)
            {
                if (m_Field[i, j] != null)
                    CheckAllDirection(m_Field[i, j]);
            }
        }

    }
    public void CheckAllDirection(GameObject targetObj)
    {
        m_initChecker = false;
        int leftFlag1, rightFlag1, upFlag1, downFlag1;
        leftFlag1 = LeftCheck(targetObj);
        rightFlag1 = RightCheck(targetObj);
        upFlag1 = UpCheck(targetObj);
        downFlag1 = DownCheck(targetObj);

        bool flag1 = false;
        if (leftFlag1 != 0 || rightFlag1 != 0 || upFlag1 != 0 || downFlag1 != 0)
        {
            int flagPosX = targetObj.GetComponent<ObjectScript>().posX - POS_PRESET;
            int flagPosY = targetObj.GetComponent<ObjectScript>().posY - POS_PRESET;
            if (leftFlag1 + rightFlag1 >= 2)
            {
                m_score += leftFlag1 + rightFlag1;
                // 가로 제거
                flag1 = true;
                for (int i = leftFlag1; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX - i, flagPosY];
                    m_Field[flagPosX - i, flagPosY] = null;
                    Destroy(tmp);
                }
                for (int i = rightFlag1; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX + i, flagPosY];
                    m_Field[flagPosX + i, flagPosY] = null;
                    Destroy(tmp);
                }
            }
            if (upFlag1 + downFlag1 >= 2)
            {
                m_score += upFlag1 + downFlag1;
                // 세로 제거
                flag1 = true;
                for (int i = upFlag1; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX, flagPosY + i];
                    m_Field[flagPosX, flagPosY + i] = null;
                    Destroy(tmp);
                }
                for (int i = downFlag1; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX, flagPosY - i];
                    m_Field[flagPosX, flagPosY - i] = null;
                    Destroy(tmp);
                }
            }
            if (targetObj != null)
            {
                int targetSpecialType = targetObj.GetComponent<ObjectScript>().specialType;
                if (targetSpecialType == 1)
                {
                    for (int i = 0; i < FIELD_Y; i++)
                    {
                        GameObject tmp = m_Field[targetObj.GetComponent<ObjectScript>().posX - POS_PRESET, i];
                        if (tmp == null) continue;
                        m_Field[targetObj.GetComponent<ObjectScript>().posX - POS_PRESET, i] = null;
                        Destroy(tmp);
                    }
                    m_combo++;
                    m_score += 5;
                    m_score += m_combo * 2;
                }
                else if (targetSpecialType == 2)
                {
                    for (int i = 0; i < FIELD_X; i++)
                    {
                        GameObject tmp = m_Field[i, targetObj.GetComponent<ObjectScript>().posY - POS_PRESET];
                        if (tmp == null) continue;
                        m_Field[i, targetObj.GetComponent<ObjectScript>().posY - POS_PRESET] = null;
                        Destroy(tmp);
                    }
                    m_combo++;
                    m_score += 5;
                    m_score += m_combo * 2;
                }
            }
            if (flag1)
            {
                GameObject specialObj = null;
                m_combo++;
                m_score++;
                m_score += m_combo * 2;
                if (leftFlag1 + rightFlag1 >= 3)
                {
                    int tmpSpriteType = m_Field[flagPosX, flagPosY].GetComponent<ObjectScript>().spriteType;
                    specialObj = new GameObject("img" + flagPosX.ToString() + flagPosY.ToString());
                    specialObj.AddComponent<SpriteRenderer>().sprite = img2[tmpSpriteType * 2 + 1];

                    specialObj.AddComponent<ObjectScript>().spriteType = tmpSpriteType;
                    specialObj.GetComponent<ObjectScript>().specialType = 1;
                    specialObj.GetComponent<ObjectScript>().posX = flagPosX + POS_PRESET;
                    specialObj.GetComponent<ObjectScript>().posY = flagPosY + POS_PRESET;

                    specialObj.AddComponent<BoxCollider2D>();
                    specialObj.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);

                    specialObj.GetComponent<Transform>().parent = this.gameObject.transform;
                    specialObj.GetComponent<Transform>().localPosition = new Vector3(flagPosX + POS_PRESET / 2, flagPosY + POS_PRESET / 2);
                    specialObj.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                }
                else if (upFlag1 + downFlag1 >= 3)
                {
                    int tmpSpriteType = m_Field[flagPosX, flagPosY].GetComponent<ObjectScript>().spriteType;
                    specialObj = new GameObject("img" + flagPosX.ToString() + flagPosY.ToString());
                    specialObj.AddComponent<SpriteRenderer>().sprite = img2[tmpSpriteType * 2];

                    specialObj.AddComponent<ObjectScript>().spriteType = tmpSpriteType;
                    specialObj.GetComponent<ObjectScript>().specialType = 2;
                    specialObj.GetComponent<ObjectScript>().posX = flagPosX + POS_PRESET;
                    specialObj.GetComponent<ObjectScript>().posY = flagPosY + POS_PRESET;

                    specialObj.AddComponent<BoxCollider2D>();
                    specialObj.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);

                    specialObj.GetComponent<Transform>().parent = this.gameObject.transform;
                    specialObj.GetComponent<Transform>().localPosition = new Vector3(flagPosX + POS_PRESET / 2, flagPosY + POS_PRESET / 2);
                    specialObj.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                }
                GameObject tmp = m_Field[flagPosX, flagPosY];
                m_Field[flagPosX, flagPosY] = null;
                Destroy(tmp);
                m_Field[flagPosX, flagPosY] = specialObj;
            }
        }

        
        Gravity();
    }
    public void MatchCheck(GameObject targetObj, int checkDirection)
    {
        m_initChecker = false;
        int targetPosX = targetObj.GetComponent<ObjectScript>().posX - POS_PRESET;
        int targetPosY = targetObj.GetComponent<ObjectScript>().posY - POS_PRESET;
        GameObject tmpObj;
        switch (checkDirection)
        {
            case LEFT:
                tmpObj = m_Field[targetPosX - 1, targetPosY];
                SwapObject(targetObj, tmpObj);
                break;
            case RIGHT:
                tmpObj = m_Field[targetPosX + 1, targetPosY];
                SwapObject(targetObj, tmpObj);
                break;
            case UP:
                tmpObj = m_Field[targetPosX, targetPosY + 1];
                SwapObject(targetObj, tmpObj);
                break;
            case DOWN:
                tmpObj = m_Field[targetPosX, targetPosY - 1];
                SwapObject(targetObj, tmpObj);
                break;
            default:
                tmpObj = targetObj;
                break;
        }
        int targetSpecialType = targetObj.GetComponent<ObjectScript>().specialType;
        int tmpSpecialType = tmpObj.GetComponent<ObjectScript>().specialType;
        int tmpPosX = tmpObj.GetComponent<ObjectScript>().posX - POS_PRESET;
        int tmpPosY = tmpObj.GetComponent<ObjectScript>().posY - POS_PRESET;
        m_initChecker = false;
        int leftFlag1, rightFlag1, upFlag1, downFlag1, leftFlag2, rightFlag2, upFlag2, downFlag2;
        leftFlag1 = LeftCheck(targetObj);
        leftFlag2 = LeftCheck(tmpObj);

        rightFlag1 = RightCheck(targetObj);
        rightFlag2 = RightCheck(tmpObj);

        upFlag1 = UpCheck(targetObj);
        upFlag2 = UpCheck(tmpObj);

        downFlag1 = DownCheck(targetObj);
        downFlag2 = DownCheck(tmpObj);

        bool flag1 = false, flag2 = false;
        if (leftFlag1 != 0 || rightFlag1 != 0 || upFlag1 != 0 || downFlag1 != 0)
        {
            int flagPosX = targetObj.GetComponent<ObjectScript>().posX - POS_PRESET;
            int flagPosY = targetObj.GetComponent<ObjectScript>().posY - POS_PRESET;
            if (leftFlag1 + rightFlag1 >= 2)
            {
                m_score += leftFlag1 + rightFlag1;
                // 가로 제거
                flag1 = true;
                for (int i = leftFlag1; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX - i, flagPosY];
                    m_Field[flagPosX - i, flagPosY] = null;
                    Destroy(tmp);
                }
                for (int i = rightFlag1; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX + i, flagPosY];
                    m_Field[flagPosX + i, flagPosY] = null;
                    Destroy(tmp);
                }
            }
            if (upFlag1 + downFlag1 >= 2)
            {
                m_score += upFlag1 + downFlag1;
                // 세로 제거
                flag1 = true;
                for (int i = upFlag1; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX, flagPosY + i];
                    m_Field[flagPosX, flagPosY + i] = null;
                    Destroy(tmp);
                }
                for (int i = downFlag1; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX, flagPosY - i];
                    m_Field[flagPosX, flagPosY - i] = null;
                    Destroy(tmp);
                }
            }
            if (flag1)
            {
                GameObject specialObj = null;
                m_combo++;
                m_score++;
                m_score += m_combo * 2;
                if (leftFlag1 + rightFlag1 >= 3)
                {
                    int tmpSpriteType = m_Field[flagPosX, flagPosY].GetComponent<ObjectScript>().spriteType;
                    specialObj = new GameObject("img" + flagPosX.ToString() + flagPosY.ToString());
                    specialObj.AddComponent<SpriteRenderer>().sprite = img2[tmpSpriteType * 2 + 1];

                    specialObj.AddComponent<ObjectScript>().spriteType = tmpSpriteType;
                    specialObj.GetComponent<ObjectScript>().specialType = 1;
                    specialObj.GetComponent<ObjectScript>().posX = flagPosX + POS_PRESET;
                    specialObj.GetComponent<ObjectScript>().posY = flagPosY + POS_PRESET;

                    specialObj.AddComponent<BoxCollider2D>();
                    specialObj.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);

                    specialObj.GetComponent<Transform>().parent = this.gameObject.transform;
                    specialObj.GetComponent<Transform>().localPosition = new Vector3(flagPosX + POS_PRESET / 2, flagPosY + POS_PRESET / 2);
                    specialObj.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                }
                else if (upFlag1 + downFlag1 >= 3)
                {
                    int tmpSpriteType = m_Field[flagPosX, flagPosY].GetComponent<ObjectScript>().spriteType;
                    specialObj = new GameObject("img" + flagPosX.ToString() + flagPosY.ToString());
                    specialObj.AddComponent<SpriteRenderer>().sprite = img2[tmpSpriteType * 2];

                    specialObj.AddComponent<ObjectScript>().spriteType = tmpSpriteType;
                    specialObj.GetComponent<ObjectScript>().specialType = 2;
                    specialObj.GetComponent<ObjectScript>().posX = flagPosX + POS_PRESET;
                    specialObj.GetComponent<ObjectScript>().posY = flagPosY + POS_PRESET;

                    specialObj.AddComponent<BoxCollider2D>();
                    specialObj.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);

                    specialObj.GetComponent<Transform>().parent = this.gameObject.transform;
                    specialObj.GetComponent<Transform>().localPosition = new Vector3(flagPosX + POS_PRESET / 2, flagPosY + POS_PRESET / 2);
                    specialObj.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                }
                GameObject tmp = m_Field[flagPosX, flagPosY];
                m_Field[flagPosX, flagPosY] = null;
                Destroy(tmp);
                m_Field[flagPosX, flagPosY] = specialObj;
            }
        }
        if (leftFlag2 != 0 || rightFlag2 != 0 || upFlag2 != 0 || downFlag2 != 0)
        {
            int flagPosX = tmpObj.GetComponent<ObjectScript>().posX - POS_PRESET;
            int flagPosY = tmpObj.GetComponent<ObjectScript>().posY - POS_PRESET;
            if (leftFlag2 + rightFlag2 >= 2)
            {
                m_score += leftFlag2 + rightFlag2;
                // 가로 제거
                flag2 = true;
                for (int i = leftFlag2; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX - i, flagPosY];
                    m_Field[flagPosX - i, flagPosY] = null;
                    Destroy(tmp);
                }
                for (int i = rightFlag2; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX + i, flagPosY];
                    m_Field[flagPosX + i, flagPosY] = null;
                    Destroy(tmp);
                }
            }
            if (upFlag2 + downFlag2 >= 2)
            {
                m_score += upFlag2 + rightFlag2;
                // 세로 제거
                flag2 = true;
                for (int i = upFlag2; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX, flagPosY + i];
                    m_Field[flagPosX, flagPosY + i] = null;
                    Destroy(tmp);
                }
                for (int i = downFlag2; i > 0; i--)
                {
                    GameObject tmp = m_Field[flagPosX, flagPosY - i];
                    m_Field[flagPosX, flagPosY - i] = null;
                    Destroy(tmp);
                }
            }
            if (flag2)
            {
                GameObject specialObj = null;
                m_combo++;
                m_score++;
                m_score += m_combo * 2;
                if (leftFlag2 + rightFlag2 >= 3)
                {
                    int tmpSpriteType = m_Field[flagPosX, flagPosY].GetComponent<ObjectScript>().spriteType;
                    specialObj = new GameObject("img" + flagPosX.ToString() + flagPosY.ToString());
                    specialObj.AddComponent<SpriteRenderer>().sprite = img2[tmpSpriteType * 2 + 1];

                    specialObj.AddComponent<ObjectScript>().spriteType = tmpSpriteType;
                    specialObj.GetComponent<ObjectScript>().specialType = 1;
                    specialObj.GetComponent<ObjectScript>().posX = flagPosX + POS_PRESET;
                    specialObj.GetComponent<ObjectScript>().posY = flagPosY + POS_PRESET;

                    specialObj.AddComponent<BoxCollider2D>();
                    specialObj.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);

                    specialObj.GetComponent<Transform>().parent = this.gameObject.transform;
                    specialObj.GetComponent<Transform>().localPosition = new Vector3(flagPosX + POS_PRESET / 2, flagPosY + POS_PRESET / 2);
                    specialObj.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                }
                else if (upFlag2 + downFlag2 >= 3)
                {
                    int tmpSpriteType = m_Field[flagPosX, flagPosY].GetComponent<ObjectScript>().spriteType;
                    specialObj = new GameObject("img" + flagPosX.ToString() + flagPosY.ToString());
                    specialObj.AddComponent<SpriteRenderer>().sprite = img2[tmpSpriteType * 2];

                    specialObj.AddComponent<ObjectScript>().spriteType = tmpSpriteType;
                    specialObj.GetComponent<ObjectScript>().specialType = 2;
                    specialObj.GetComponent<ObjectScript>().posX = flagPosX + POS_PRESET;
                    specialObj.GetComponent<ObjectScript>().posY = flagPosY + POS_PRESET;

                    specialObj.AddComponent<BoxCollider2D>();
                    specialObj.GetComponent<BoxCollider2D>().size = new Vector2(0.9f, 0.9f);

                    specialObj.GetComponent<Transform>().parent = this.gameObject.transform;
                    specialObj.GetComponent<Transform>().localPosition = new Vector3(flagPosX + POS_PRESET / 2, flagPosY + POS_PRESET / 2);
                    specialObj.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
                }
                GameObject tmp = m_Field[flagPosX, flagPosY];
                m_Field[flagPosX, flagPosY] = null;
                Destroy(tmp);
                m_Field[flagPosX, flagPosY] = specialObj;
            }
        }

        if (targetSpecialType == 1)
        {
            for (int i = 0; i < FIELD_Y; i++)
            {
                GameObject tmp = m_Field[targetPosX, i];
                if (tmp == null) continue;
                m_Field[targetPosX, i] = null;
                Destroy(tmp);
            }
            m_combo++;
            m_score += 5;
            m_score += m_combo * 2;
            flag1 = flag2 = true;
        }
        else if (targetSpecialType == 2)
        {
            for (int i = 0; i < FIELD_X; i++)
            {
                GameObject tmp = m_Field[i, targetPosY];
                if (tmp == null) continue;
                m_Field[i, targetPosY] = null;
                Destroy(tmp);
            }
            m_combo++;
            m_score += 5;
            m_score += m_combo * 2;
            flag1 = flag2 = true;
        }
        if (targetSpecialType == 1)
        {
            for (int i = 0; i < FIELD_Y; i++)
            {
                GameObject tmp = m_Field[tmpPosX, i];
                if (tmp == null) continue;
                m_Field[tmpPosX, i] = null;
                Destroy(tmp);
            }
            m_combo++;
            m_score += 5;
            m_score += m_combo * 2;
            flag1 = flag2 = true;
        }
        else if (targetSpecialType == 2)
        {
            for (int i = 0; i < FIELD_X; i++)
            {
                GameObject tmp = m_Field[i, tmpPosY];
                if (tmp == null) continue;
                m_Field[i, tmpPosY] = null;
                Destroy(tmp);
            }
            m_combo++;
            m_score += 5;
            m_score += m_combo * 2;
            flag1 = flag2 = true;
        }


        if (!flag1 && !flag2)
        {
            //StartCoroutine(SwapCoroutine(tmpObj, targetObj));
            SwapObject(tmpObj, targetObj);
        }
        Gravity();
    }
    void SwapObject(GameObject obj1, GameObject obj2)
    {
        m_initChecker = false;
        if (obj1 == null || obj2 == null) return;
        GameObject tmp = obj1;
        Vector3 tmpPos = obj1.transform.position;
        int obj1X = obj1.GetComponent<ObjectScript>().posX;
        int obj1Y = obj1.GetComponent<ObjectScript>().posY;
        int obj2X = obj2.GetComponent<ObjectScript>().posX;
        int obj2Y = obj2.GetComponent<ObjectScript>().posY;
        
        obj1.transform.position = obj2.transform.position;
        obj1.GetComponent<ObjectScript>().posX = obj2X;
        obj1.GetComponent<ObjectScript>().posY = obj2Y;

        obj2.transform.position = tmpPos;
        obj2.GetComponent<ObjectScript>().posX = obj1X;
        obj2.GetComponent<ObjectScript>().posY = obj1Y;

        m_Field[obj1X - POS_PRESET, obj1Y - POS_PRESET] = obj2;
        m_Field[obj2X - POS_PRESET, obj2Y - POS_PRESET] = tmp;

        tmp = null;
    }
    IEnumerator SwapCoroutine(GameObject obj1, GameObject obj2)
    {
        yield return new WaitForSeconds(0.1f);
        SwapObject(obj1, obj2);
        m_combo = 0;
    }

    void Gravity()
    {
        m_initChecker = false;
        GameObject tmp = null;
        for (int i = 0; i < FIELD_X; i++)
        {
            for (int j = 0; j < FIELD_Y; j++)
            {
                if (m_Field[i, j] == null)
                {
                    for (int k = j; k < FIELD_Y; k++)
                    {
                        tmp = m_Field[i, k];
                        if (tmp != null)
                        {
                            m_Field[i, k] = null;
                            break;
                        }
                    }
                    if (tmp != null)
                    {
                        // 한칸 내리기
                        tmp.GetComponent<ObjectScript>().posX = i + POS_PRESET;
                        tmp.GetComponent<ObjectScript>().posY = j + POS_PRESET;
                        tmp.transform.localPosition = new Vector3(i + POS_PRESET / 2, j + POS_PRESET / 2, 0);
                        m_Field[i, j] = tmp;
                    }
                }
            }
        }
        StartCoroutine(CreateObj());
        //initialize();
    }

    IEnumerator CreateObj()
    {
        yield return new WaitForSeconds(0.3f);
        m_initChecker = false;
        initialize();
    }


    bool Check(GameObject targetObj, GameObject checkObj) {
        if (targetObj == null || checkObj == null) return false;
        return (checkObj.GetComponent<ObjectScript>().spriteType == targetObj.GetComponent<ObjectScript>().spriteType) ? true : false;
    }
    int LeftCheck(GameObject targetObj) {
        int result = 0;
        int targetPosX = targetObj.GetComponent<ObjectScript>().posX-POS_PRESET;
        int targetPosY = targetObj.GetComponent<ObjectScript>().posY-POS_PRESET;

        if (targetPosX <= 0) return result;

        if (Check(targetObj, m_Field[targetPosX-1, targetPosY])) {
            result += 1;
            if (targetPosX <= 1) return result;
            result += Check(targetObj, m_Field[targetPosX-2, targetPosY]) ? 1 : 0 ;
        }
        return result;
    }
    int RightCheck(GameObject targetObj)
    {
        int result = 0;
        int targetPosX = targetObj.GetComponent<ObjectScript>().posX-POS_PRESET;
        int targetPosY = targetObj.GetComponent<ObjectScript>().posY-POS_PRESET;
        
        if (targetPosX >= FIELD_X-1) return result;

        if (Check(targetObj, m_Field[targetPosX+1, targetPosY])) {
            result += 1;
            if (targetPosX >= FIELD_X-2) return result;
            result += Check(targetObj, m_Field[targetPosX+2, targetPosY]) ? 1 : 0 ;
        }
        return result;
    }
    int UpCheck(GameObject targetObj)
    {
        int result = 0;
        int targetPosX = targetObj.GetComponent<ObjectScript>().posX-POS_PRESET;
        int targetPosY = targetObj.GetComponent<ObjectScript>().posY-POS_PRESET;

        if (targetPosY >= FIELD_Y-1) return result;

        if (Check(targetObj, m_Field[targetPosX, targetPosY+1])) {
            result += 1;
            if (targetPosY >= FIELD_Y-2) return result;
            result += Check(targetObj, m_Field[targetPosX, targetPosY+2]) ? 1 : 0 ;
        }
        return result;
    }
    int DownCheck(GameObject targetObj)
    {
        int result = 0;
        int targetPosX = targetObj.GetComponent<ObjectScript>().posX-POS_PRESET;
        int targetPosY = targetObj.GetComponent<ObjectScript>().posY-POS_PRESET;

        if (targetPosY <= 0) return result;

        if (Check(targetObj, m_Field[targetPosX, targetPosY-1])) {
            result += 1;
            if (targetPosY <= 1) return result;
            result += Check(targetObj, m_Field[targetPosX, targetPosY-2]) ? 1 : 0 ;
        }
        return result;
    }
    public void Restart()
    {
        once = true;
        m_initChecker = true;
        m_combo = 0;
        m_score = 0;
        gameTime = 40f;
        startTime = 4f;
        gameOverUI.SetActive(false);
        bGameOver = true;
        startTimer.gameObject.SetActive(true);
        gameScore.text = "0";
        gameCombo.text = "0";
        foreach (ObjectScript child in this.gameObject.GetComponentsInChildren<ObjectScript>())
        {
            Destroy(child.transform.gameObject);
        }
        for (int i = 0; i < FIELD_X; i++)
        {
            for (int j = 0; j < FIELD_Y; j++)
            {
                m_Field[i, j] = null;
            }
        }
        initialize();
        for (int i = 0; i < FIELD_X; i++)
        {
            for (int j = 0; j < FIELD_Y; j++)
            {
                if (m_Field[i, j] != null)
                    CheckAllDirection(m_Field[i, j]);
            }
        }
        m_score = 0;
        JsManager.gameStart();
    }
    public void ReturnMain()
    {
        once = true;
        m_initChecker = true;
        m_combo = 0;
        m_score = 0;
        gameTime = 40f;
        startTime = 4f;
        gameOverUI.SetActive(false);
        bGameOver = true;
        startTimer.gameObject.SetActive(true);
        gameScore.text = "0";
        gameCombo.text = "0";
        foreach (ObjectScript child in this.gameObject.GetComponentsInChildren<ObjectScript>())
        {
            Destroy(child.transform.gameObject);
        }
        for (int i = 0; i < FIELD_X; i++)
        {
            for (int j = 0; j < FIELD_Y; j++)
            {
                m_Field[i, j] = null;
            }
        }
    }
}
