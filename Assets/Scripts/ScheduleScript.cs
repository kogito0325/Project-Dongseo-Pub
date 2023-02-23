using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ScheduleScript : MonoBehaviour
{
    public Image[] loveGazes;
    public Text[] loveAmount;
    public Text moneyTxt;

    public int loveSummer;
    public int loveFall;
    public int loveWinter;

    public int money;
    private void Awake()
    {
        loveSummer = GameManager.Instance.loveSummer;
        loveFall = GameManager.Instance.loveFall;
        loveWinter = GameManager.Instance.loveWinter;
        UpdateLoveGazes();
        UpdateLoves();
        UpdateMoney();
    }

    private void UpdateLoveGazes()
    {
        loveGazes[0].fillAmount = (float)loveSummer / 100;
        loveGazes[1].fillAmount = (float)loveFall / 100;
        loveGazes[2].fillAmount = (float)loveWinter / 100;
    }

    private void UpdateLoves()
    {
        loveAmount[0].text = "������\n" + loveSummer.ToString() + "%";
        loveAmount[1].text = "������\n" + loveFall.ToString() + "%";
        loveAmount[2].text = "�Ѽ���\n" + loveWinter.ToString() + "%";
    }

    private void UpdateMoney()
    {
        money = GameManager.Instance.money;
        moneyTxt.text = money.ToString() + "��";
    }
}
