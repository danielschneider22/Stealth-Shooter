using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;
    public GameObject moneyChangeText;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void changeMoney(int changeAmount)
    {
        money += changeAmount;
        moneyText.text = money.ToString();
        moneyChangeText.GetComponent<TextMeshProUGUI>().text = "+" + changeAmount.ToString();
        moneyChangeText.SetActive(true);
        audioManager.Play("CoinCollect", 0);
    }
}
