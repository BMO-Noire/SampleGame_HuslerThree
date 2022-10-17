using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickCard : MonoBehaviour
{
    public int num;
    static public int cnt = 0;

    public void OnClick()
    {
        //gameObject.GetComponent<Image>().color = new Color(255, 255, 0);
        MNG_CARDGAME.instance.clickList.Add(gameObject);
        MNG_CARDGAME.instance.clickList[0].GetComponent<Button>().enabled = false;

        gameObject.GetComponent<Image>().sprite = MNG_CARDGAME.instance.numSpriteList[num];

        if (MNG_CARDGAME.instance.clickList.Count == 2)
        {
            if (MNG_CARDGAME.instance.clickList[0].GetComponent<ClickCard>().num == MNG_CARDGAME.instance.clickList[1].GetComponent<ClickCard>().num)
            {
                ++cnt;
                ++MNG_CARDGAME.instance.combo;
                if (MNG_CARDGAME.instance.combo > 10)
                {
                    MNG_CARDGAME.instance.comboScore = 1.3f;
                }
                else if (MNG_CARDGAME.instance.combo > 20)
                {
                    MNG_CARDGAME.instance.comboScore = 1.6f;
                }
                else if (MNG_CARDGAME.instance.combo > 30)
                {
                    MNG_CARDGAME.instance.comboScore = 2.0f;
                }
                MNG_CARDGAME.instance.totalScore += MNG_CARDGAME.instance.score * MNG_CARDGAME.instance.comboScore;
                MNG_CARDGAME.instance.gameScore.text = ((int)MNG_CARDGAME.instance.totalScore).ToString() + " Score";
                MNG_CARDGAME.instance.gameCombo.text = MNG_CARDGAME.instance.combo.ToString() + " Combo";
                //MNG_CARDGAME.instance.gameOverScore.text = ((int)MNG_CARDGAME.instance.totalScore).ToString();
                //MNG_CARDGAME.instance.gameOverScore.text = MNG_CARDGAME.instance.combo.ToString();
                MNG_CARDGAME.instance.clickList[0].GetComponent<Button>().enabled = true;
                MNG_CARDGAME.instance.clickList.Clear();
            }
            else
            {
                WrongAction();
                Invoke("test", 0.5f);
                //MNG_CARDGAME.instance.clickList[0].GetComponent<Image>().color = new Color(255, 255, 255);
                //MNG_CARDGAME.instance.clickList[1].GetComponent<Image>().color = new Color(255, 255, 255);
                MNG_CARDGAME.instance.combo = 0;
                MNG_CARDGAME.instance.gameCombo.text = MNG_CARDGAME.instance.combo.ToString() + " Combo";
                //MNG_CARDGAME.instance.gameOverScore.text = MNG_CARDGAME.instance.combo.ToString();

                MNG_CARDGAME.instance.clickList[0].GetComponent<Button>().enabled = true;
            }
            
        }

        if (cnt == 2)
        {
            if (MNG_CARDGAME.instance.start1)
            {
                Invoke("Card6Reverse", 0.8f);
                MNG_CARDGAME.instance.start1 = false;
            }

            MNG_CARDGAME.instance.field4.SetActive(false);
            MNG_CARDGAME.instance.field6.SetActive(true);

            MNG_CARDGAME.instance.score = 20;
        }
        else if (cnt == 5)
        {
            if (MNG_CARDGAME.instance.start2)
            {
                Invoke("Card8Reverse", 0.6f);
                MNG_CARDGAME.instance.start2 = false;
            }

            MNG_CARDGAME.instance.field6.SetActive(false);
            MNG_CARDGAME.instance.field8.SetActive(true);

            MNG_CARDGAME.instance.score = 30;
        }
        else if (cnt == 9)
        {
            if (MNG_CARDGAME.instance.start3)
            {
                Invoke("Card12Reverse", 0.6f);
                MNG_CARDGAME.instance.start3 = false;
            }

            MNG_CARDGAME.instance.field8.SetActive(false);
            MNG_CARDGAME.instance.field12.SetActive(true);

            MNG_CARDGAME.instance.score = 50;
        }
        else if (cnt == 15)
        {
            if (MNG_CARDGAME.instance.start4)
            {
                Invoke("Card12Reverse", 0.6f);
                MNG_CARDGAME.instance.start4 = false;
                MNG_CARDGAME.instance.Card12Setting();
            }
            MNG_CARDGAME.instance.score = 50;
        }
        else if (cnt == 20)
        {
            MNG_CARDGAME.instance.start4 = true;
            cnt = 14;
        }

    }

    void test()
    {
        WrongActionTrue();
        MNG_CARDGAME.instance.clickList[0].GetComponent<Image>().sprite = MNG_CARDGAME.instance.normalImage;
        MNG_CARDGAME.instance.clickList[1].GetComponent<Image>().sprite = MNG_CARDGAME.instance.normalImage;
        MNG_CARDGAME.instance.clickList.Clear();

        //MNG_CARDGAME.instance.once = true;
    }

    public void Card6Reverse()
    {
        for (int i = 0; i < MNG_CARDGAME.instance.Field6List.Count; ++i)
        {
            MNG_CARDGAME.instance.Field6List[i].GetComponent<Image>().sprite = MNG_CARDGAME.instance.normalImage;
        }
    }

    public void Card8Reverse()
    {
        for (int i = 0; i < MNG_CARDGAME.instance.Field8List.Count; ++i)
        {
            MNG_CARDGAME.instance.Field8List[i].GetComponent<Image>().sprite = MNG_CARDGAME.instance.normalImage;
        }
    }

    public void Card12Reverse()
    {
        for (int i = 0; i < MNG_CARDGAME.instance.Field12List.Count; ++i)
        {
            MNG_CARDGAME.instance.Field12List[i].GetComponent<Image>().sprite = MNG_CARDGAME.instance.normalImage;
        }
    }

    void WrongAction()
    {
        MNG_CARDGAME.instance.WrongCard();
        MNG_CARDGAME.instance.wrongImage.SetActive(true);
    }
    void WrongActionTrue()
    {
        MNG_CARDGAME.instance.WrongCardEnable();
        MNG_CARDGAME.instance.wrongImage.SetActive(false);
    }
}
