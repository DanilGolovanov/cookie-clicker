using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnePlusPerClickUpgrade : MonoBehaviour
{
    #region Variables

    private CurrencyManager currencyManager;
    private IngredientManager ingredientManager;

    [Header("+1 Upgrade Costs")]
    [Tooltip("Cost of the '+1 lime per click' upgrade.")]
    public int limeUpgradeCost = 3;
    [Tooltip("Cost of the '+1 ice per click' upgrade.")]
    public int iceUpgradeCost = 3;
    [Tooltip("Cost of the '+1 sugar per click' upgrade.")]
    public int sugarUpgradeCost = 3;
    [Tooltip("Cost of the '+1 ultra per click' upgrade.")]
    public int ultraUpgradeCost = 15;

    [Header("Price Tags")]
    [SerializeField, Tooltip("Text object of the '+1 lime per click' upgrade price tag.")]
    private Text limePriceTag;
    [SerializeField, Tooltip("Text object of the '+1 ice per click' upgrade price tag.")]
    private Text icePriceTag;
    [SerializeField, Tooltip("Text object of the '+1 sugar per click' upgrade price tag.")]
    private Text sugarPriceTag;
    [SerializeField, Tooltip("Text object of the '+1 ultra per click' upgrade price tag.")]
    private Text ultraPriceTag;

    // number by which the cost of the next upgrade is multiplied when player buys the previous one
    private int costMultiplier = 2;

    [Header("Upgrade Buttons")]
    [SerializeField, Tooltip("Button object which is used to buy '+1 lime per click' upgrade.")]
    private Button limeUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy '+1 ice per click' upgrade.")]
    private Button iceUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy '+1 sugar per click' upgrade.")]
    private Button sugarUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy '+1 ultra per click' upgrade.")]
    private Button ultraUpgradeBtn;

    [Header("Tip Panel Texts")]
    [SerializeField, Tooltip("Text object from the '+1 lime per click' upgrade tip panel.")]
    private Text limeTipPanelText;
    [SerializeField, Tooltip("Text object from the '+1 ice per click' upgrade tip panel.")]
    private Text iceTipPanelText;
    [SerializeField, Tooltip("Text object from the '+1 sugar per click' upgrade tip panel.")]
    private Text sugarTipPanelText;

    #endregion

    #region Default Methods
    private void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        ingredientManager = FindObjectOfType<IngredientManager>();

        // setup initial values on the price tags
        limePriceTag.text = "$" + limeUpgradeCost;
        icePriceTag.text = "$" + iceUpgradeCost;
        sugarPriceTag.text = "$" + sugarUpgradeCost;
        ultraPriceTag.text = "$" + ultraUpgradeCost;
}

    private void Update()
    {
        ManageButtonsInteractibility(currencyManager);
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Subtract money, increase 'valueToAdd' variable of corresponding ingredient and increase cost of the next upgrade.
    /// </summary>
    /// <param name="ingredient">Name of the ingredient.</param>
    public void PlusOnePerClickUpgrade(string ingredient)
    {
        int cost = UpgradeManager.DetermineCost(ingredient, limeUpgradeCost, iceUpgradeCost, sugarUpgradeCost, ultraUpgradeCost);

        // withdraw money
        currencyManager.money -= cost;
        // determine which upgrade was chosen
        switch (ingredient)
        {
            case "Lime":
                // apply upgrade
                ingredientManager.limeValueToAdd++;
                ingredientManager.storeLimeValueToAdd++;
                // increase the upgrade cost
                limeUpgradeCost *= costMultiplier;
                limePriceTag.text = "$" + limeUpgradeCost;
                // update description
                limeTipPanelText.text = $"Get {ingredientManager.storeLimeValueToAdd + 1} limes per click.";
                break;
            case "Ice":
                // apply upgrade
                ingredientManager.iceValueToAdd++;
                ingredientManager.storeIceValueToAdd++;
                // increase the upgrade cost
                iceUpgradeCost *= costMultiplier;
                icePriceTag.text = "$" + iceUpgradeCost;
                // update description
                iceTipPanelText.text = $"Get {ingredientManager.storeIceValueToAdd + 1} ice cubes per click.";
                break;
            case "Sugar":
                // apply upgrade
                ingredientManager.sugarValueToAdd++;
                ingredientManager.storeSugarValueToAdd++;
                // increase the upgrade cost
                sugarUpgradeCost *= costMultiplier;
                sugarPriceTag.text = "$" + sugarUpgradeCost;
                // update description
                sugarTipPanelText.text = $"Get {ingredientManager.storeSugarValueToAdd + 1} sugars per click.";
                break;
            case "Ultra":
                // apply upgrade
                ingredientManager.limeValueToAdd++;
                ingredientManager.iceValueToAdd++;
                ingredientManager.sugarValueToAdd++;

                ingredientManager.storeLimeValueToAdd++;
                ingredientManager.storeIceValueToAdd++;
                ingredientManager.storeSugarValueToAdd++;
                // increase the upgrade cost
                ultraUpgradeCost *= costMultiplier;
                ultraPriceTag.text = "$" + ultraUpgradeCost;
                break;
        }
    }

    /// <summary>
    /// Make upgrade buttons interactable when player has enough money to buy them.
    /// </summary>
    private void ManageButtonsInteractibility(CurrencyManager _currencyManager)
    {
        UpgradeManager.MakeBtnInteractibleWhenEnoughMoney(_currencyManager, limeUpgradeBtn, limeUpgradeCost);
        UpgradeManager.MakeBtnInteractibleWhenEnoughMoney(_currencyManager, iceUpgradeBtn, iceUpgradeCost);
        UpgradeManager.MakeBtnInteractibleWhenEnoughMoney(_currencyManager, sugarUpgradeBtn, sugarUpgradeCost);
        UpgradeManager.MakeBtnInteractibleWhenEnoughMoney(_currencyManager, ultraUpgradeBtn, ultraUpgradeCost);
    }
    #endregion
}
