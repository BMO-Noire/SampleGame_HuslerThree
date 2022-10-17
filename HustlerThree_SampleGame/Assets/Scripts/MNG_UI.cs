using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MNG_UI : MonoBehaviour
{
    [SerializeField] private GameObject mainBackGround;
    [SerializeField] private GameObject contactBackGround;
    [SerializeField] private GameObject cardGameStartUIBackGround;
    [SerializeField] private GameObject cardGameRuleBackGround;
    [SerializeField] private GameObject cardGame;
    [SerializeField] private GameObject colorGame;
    [SerializeField] private GameObject colorGameStartUIBackGround;
    [SerializeField] private GameObject colorGameRuleBackGround;
    [SerializeField] private GameObject anipangGame;
    [SerializeField] private GameObject anipangGameStartUIBackGround;
    [SerializeField] private GameObject anipangGameRuleBackGround;
    [SerializeField] private GameObject hammerGame;
    [SerializeField] private GameObject hammerGameStartUIBackGround;
    [SerializeField] private GameObject hammerGameRuleBackGround;

    public void OnMain()
    {
        mainBackGround.SetActive(true);
        contactBackGround.SetActive(false);
        cardGameStartUIBackGround.SetActive(false);
        colorGameStartUIBackGround.SetActive(false);
    }
    public void OnContactBtn()
    {
        mainBackGround.SetActive(false);
        contactBackGround.SetActive(true);
    }
    public void ContactBackBtn()
    {
        mainBackGround.SetActive(true);
        contactBackGround.SetActive(false);
    }
    public void OnCardGame()
    {
        mainBackGround.SetActive(false);
        cardGameStartUIBackGround.SetActive(true);
    }

    public void CardGameRuleBtn()
    {
        cardGameStartUIBackGround.SetActive(false);
        cardGameRuleBackGround.gameObject.SetActive(true);
    }
    public void CardGameRuleBackBtn()
    {
        cardGameStartUIBackGround.SetActive(true);
        cardGameRuleBackGround.gameObject.SetActive(false);
    }
    public void CardGameStartBtn()
    {
        cardGameStartUIBackGround.gameObject.SetActive(false);
        cardGameRuleBackGround.gameObject.SetActive(false);
        cardGame.SetActive(true);
        MNG_CARDGAME.gameStart = true;
    }

    public void ColorGameStartBtn()
    {
        colorGameStartUIBackGround.gameObject.SetActive(false);
        colorGameRuleBackGround.gameObject.SetActive(false);
        colorGame.SetActive(true);
        MNG_COLORGAME.gameStart = true;
    }

    public void ColorGameRuleBackBtn()
    {
        colorGameStartUIBackGround.SetActive(true);
        colorGameRuleBackGround.gameObject.SetActive(false);
    }

    public void ColorGameRuleBtn()
    {
        colorGameStartUIBackGround.SetActive(false);
        colorGameRuleBackGround.gameObject.SetActive(true);
    }

    public void onColorGame()
    {
        mainBackGround.SetActive(false);
        colorGameStartUIBackGround.SetActive(true);
    }

    public void CardtoMain()
    {
        MNG_CARDGAME.instance.ReturnMain();
        mainBackGround.SetActive(true);
        cardGame.SetActive(false);
    }

    public void ColortoMain()
    {
        MNG_COLORGAME.instance.ReturnMain();
        mainBackGround.SetActive(true);
        colorGame.SetActive(false);
    }

    public void OnAnipangGame()
    {
        mainBackGround.SetActive(false);
        anipangGameStartUIBackGround.SetActive(true);
    }

    public void AnipangRuleBtn()
    {
        anipangGameStartUIBackGround.SetActive(false);
        anipangGameRuleBackGround.gameObject.SetActive(true);
    }
    public void AnipangRuleBackBtn()
    {
        anipangGameStartUIBackGround.SetActive(true);
        anipangGameRuleBackGround.gameObject.SetActive(false);
    }
    public void AnipangStartBtn()
    {
        anipangGameStartUIBackGround.gameObject.SetActive(false);
        anipangGameRuleBackGround.gameObject.SetActive(false);
        anipangGame.SetActive(true);
        MNG_ANIPANGGAME.gameStart = true;
    }
    public void AnipangGameMain()
    {
        MNG_ANIPANGGAME.instance.ReturnMain();
        mainBackGround.SetActive(true);
        anipangGame.SetActive(false);
    }
    public void AnipangtoMain()
    {
        MNG_ANIPANGGAME.instance.ReturnMain();
        mainBackGround.SetActive(true);
        anipangGame.SetActive(false);
    }

    public void OnHammerGame()
    {
        SceneManager.LoadScene(1);
    }
    public void HammerGameRuleBtn()
    {
        hammerGameStartUIBackGround.SetActive(false);
        hammerGameRuleBackGround.gameObject.SetActive(true);
    }
    public void HammerGameBackBtn()
    {
        hammerGameStartUIBackGround.SetActive(true);
        hammerGameRuleBackGround.gameObject.SetActive(false);
    }
    public void HammerGameStartBtn()
    {
        hammerGameStartUIBackGround.gameObject.SetActive(false);
        hammerGameRuleBackGround.gameObject.SetActive(false);
        hammerGame.SetActive(true);
        MNG_HAMMERGAME.instance.gameStart = true;
    }
    public void HammerGameMain()
    {
        SceneManager.LoadScene(0);
    }
    public void HammerGametoMain()
    {
        SceneManager.LoadScene(0);
    }
}
