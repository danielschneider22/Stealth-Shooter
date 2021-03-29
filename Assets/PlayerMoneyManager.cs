using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMoneyManager : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyText;
    public GameObject moneyChangeText;

    public void changeMoney(int changeAmount)
    {
        money += changeAmount;
        moneyText.text = money.ToString();
        moneyChangeText.GetComponent<TextMeshProUGUI>().text = "+" + changeAmount.ToString();
        moneyChangeText.SetActive(true);
    }
}
