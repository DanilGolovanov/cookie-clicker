using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    /// <summary>
    /// Make upgrade button interactable when player has enough money to buy it.
    /// </summary>
    public static void MakeBtnInteractibleWhenEnoughMoney(CurrencyManager _currencyManager, Button btn, int cost)
    {
        if (_currencyManager.money - cost >= 0)
        {
            btn.interactable = true;
        }
        else
        {
            btn.interactable = false;
        }
    }

    /// <summary>
    /// Determine and return the cost ingredient which is used in the method.
    /// </summary>
    /// <param name="ingredient">Name of the used ingredient.</param>
    /// <returns>Cost of the used ingredient.</returns>
    public static int DetermineCost(string ingredient, int limeUpgradeCost, int iceUpgradeCost, int sugarUpgradeCost, int ultraUpgradeCost)
    {
        int cost = 0;

        switch (ingredient)
        {
            case "Lime":
                cost = limeUpgradeCost;
                break;
            case "Ice":
                cost = iceUpgradeCost;
                break;
            case "Sugar":
                cost = sugarUpgradeCost;
                break;
            case "Ultra":
                cost = ultraUpgradeCost;
                break;
        }

        return cost;
    }
}
