using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class X2PerClickUpgrade : MonoBehaviour
{
    #region Variables

    private CurrencyManager currencyManager;
    private IngredientManager ingredientManager;
    private OnePlusPerClickUpgrade perClickUpgrade;

    // upgrade costs
    private int limeUpgradeCost = 1;
    private int iceUpgradeCost = 1;
    private int sugarUpgradeCost = 1;
    private int ultraUpgradeCost = 5;

    [Header("Price Tags")]
    [SerializeField, Tooltip("Text object of the 'x2 limes per click' upgrade price tag.")]
    private Text limePriceTag;
    [SerializeField, Tooltip("Text object of the 'x2 ice per click' upgrade price tag.")]
    private Text icePriceTag;
    [SerializeField, Tooltip("Text object of the 'x2 sugar per click' upgrade price tag.")]
    private Text sugarPriceTag;
    [SerializeField, Tooltip("Text object of the 'x2 ultra per click' upgrade price tag.")]
    private Text ultraPriceTag;

    [Header("Upgrade Buttons")]
    [SerializeField, Tooltip("Button object which is used to buy 'x2 limes per click' upgrade.")]
    private Button limeUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy 'x2 ice per click' upgrade.")]
    private Button iceUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy 'x2 sugar per click' upgrade.")]
    private Button sugarUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy 'x2 ultra per click' upgrade.")]
    private Button ultraUpgradeBtn;

    // indicate if the upgrade is active
    private bool limeIsActive;
    private bool iceIsActive;
    private bool sugarIsActive;
    private bool ultraIsActive;

    // timers
    private float limeTimer = 0;
    private float iceTimer = 0;
    private float sugarTimer = 0;
    private float ultraTimer = 0;

    [Header("X2 Texts")]
    [SerializeField, Tooltip("Green 'X2' text object.")]
    private Text limeX2Text;
    [SerializeField, Tooltip("Blue 'X2' text object.")]
    private Text iceX2Text;
    [SerializeField, Tooltip("White 'X2' text object.")]
    private Text sugarX2Text;
    [SerializeField, Tooltip("Pink 'X2' text object.")]
    private Text ultraX2Text;

    [Header("Timer Texts")]
    [SerializeField, Tooltip("Text object which displays 'x2 limes per click' upgrade timer.")]
    private Text limeTimerText;
    [SerializeField, Tooltip("Text object which displays 'x2 ice per click' upgrade timer.")]
    private Text iceTimerText;
    [SerializeField, Tooltip("Text object which displays 'x2 sugar per click' upgrade timer.")]
    private Text sugarTimerText;
    [SerializeField, Tooltip("Text object which displays 'x2 ultra per click' upgrade timer.")]
    private Text ultraTimerText;

    #endregion

    #region Default Methods
    private void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        ingredientManager = FindObjectOfType<IngredientManager>();
        perClickUpgrade = FindObjectOfType<OnePlusPerClickUpgrade>();

        // setup initial values on the price tags
        limePriceTag.text = "$" + limeUpgradeCost;
        icePriceTag.text = "$" + iceUpgradeCost;
        sugarPriceTag.text = "$" + sugarUpgradeCost;
        ultraPriceTag.text = "$" + ultraUpgradeCost;
    }

    private void Update()
    {
        ManageButtonsInteractibility(currencyManager);

        // apply double points upgrade when necessary
        ExecuteLimeDoublePoints();
        ExecuteIceDoublePoints();
        ExecuteSugarDoublePoints();
        ExecuteUltraDoublePoints();

        // update upgrade costs 
        limeUpgradeCost = perClickUpgrade.limeUpgradeCost / 3;
        limePriceTag.text = "$" + limeUpgradeCost;

        iceUpgradeCost = perClickUpgrade.iceUpgradeCost / 3;
        icePriceTag.text = "$" + iceUpgradeCost;

        sugarUpgradeCost = perClickUpgrade.sugarUpgradeCost / 3;
        sugarPriceTag.text = "$" + sugarUpgradeCost;

        ultraUpgradeCost = perClickUpgrade.ultraUpgradeCost / 3;
        ultraPriceTag.text = "$" + ultraUpgradeCost;
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Subtract money, activate double points upgrade and activate timer in the play area.
    /// </summary>
    /// <param name="ingredient">Name of the ingredient.</param>
    public void ApplyDoublePointsUpgrade(string ingredient)
    {
        int cost = UpgradeManager.DetermineCost(ingredient, limeUpgradeCost, iceUpgradeCost, sugarUpgradeCost, ultraUpgradeCost);

        // withdraw money
        currencyManager.money -= cost;
        // determine which upgrade was chosen
        switch (ingredient)
        {
            case "Lime":
                // apply upgrade
                limeIsActive = true;
                limeTimer = 30;
                ingredientManager.limeValueToAdd *= 2;

                limeX2Text.gameObject.SetActive(true);
                limeTimerText.gameObject.SetActive(true);
                break;
            case "Ice":
                // apply upgrade
                iceIsActive = true;
                iceTimer = 30;
                ingredientManager.iceValueToAdd *= 2;

                iceX2Text.gameObject.SetActive(true);
                iceTimerText.gameObject.SetActive(true);
                break;
            case "Sugar":
                // apply upgrade
                sugarIsActive = true;
                sugarTimer = 30;
                ingredientManager.sugarValueToAdd *= 2;

                sugarX2Text.gameObject.SetActive(true);
                sugarTimerText.gameObject.SetActive(true);
                break;
            case "Ultra":
                // apply upgrade
                ultraIsActive = true;
                ultraTimer = 30;
                ingredientManager.limeValueToAdd *= 2;
                ingredientManager.iceValueToAdd *= 2;
                ingredientManager.sugarValueToAdd *= 2;

                ultraX2Text.gameObject.SetActive(true);
                ultraTimerText.gameObject.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// In case when lime upgrade was activated ,
    /// if timer is greater than 0 update timer text and the timer itself,
    /// else deactivate the upgrade.
    /// </summary>
    private void ExecuteLimeDoublePoints()
    {
        if (limeIsActive)
        {
            if (limeTimer > 0)
            {
                limeTimer -= Time.deltaTime;
                limeTimerText.text = (int)limeTimer + "s";
            }
            else
            {
                ingredientManager.limeValueToAdd = ingredientManager.storeLimeValueToAdd;
                limeIsActive = false;

                limeTimerText.gameObject.SetActive(false);
                limeX2Text.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// In case when ice upgrade was activated ,
    /// if timer is greater than 0 update timer text and the timer itself,
    /// else deactivate the upgrade.
    /// </summary>
    private void ExecuteIceDoublePoints()
    {
        if (iceIsActive)
        {
            if (iceTimer > 0)
            {
                iceTimer -= Time.deltaTime;
                iceTimerText.text = (int)iceTimer + "s";
            }
            else
            {
                ingredientManager.iceValueToAdd = ingredientManager.storeIceValueToAdd;
                iceIsActive = false;

                iceTimerText.gameObject.SetActive(false);
                iceX2Text.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// In case when sugar upgrade was activated ,
    /// if timer is greater than 0 update timer text and the timer itself,
    /// else deactivate the upgrade.
    /// </summary>
    private void ExecuteSugarDoublePoints()
    {
        if (sugarIsActive)
        {
            if (sugarTimer > 0)
            {
                sugarTimer -= Time.deltaTime;
                sugarTimerText.text = (int)sugarTimer + "s";
            }
            else
            {
                ingredientManager.sugarValueToAdd = ingredientManager.storeSugarValueToAdd;
                sugarIsActive = false;

                sugarTimerText.gameObject.SetActive(false);
                sugarX2Text.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// In case when ultra upgrade was activated ,
    /// if timer is greater than 0 update timer text and the timer itself,
    /// else deactivate the upgrade.
    /// </summary>
    private void ExecuteUltraDoublePoints()
    {
        if (ultraIsActive)
        {
            if (ultraTimer > 0)
            {
                ultraTimer -= Time.deltaTime;
                ultraTimerText.text = (int)ultraTimer + "s";
            }
            else
            {
                ingredientManager.limeValueToAdd = ingredientManager.storeLimeValueToAdd;
                ingredientManager.iceValueToAdd = ingredientManager.storeIceValueToAdd;
                ingredientManager.sugarValueToAdd = ingredientManager.storeSugarValueToAdd;

                ultraIsActive = false;

                ultraTimerText.gameObject.SetActive(false);
                ultraX2Text.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Make upgrade buttons interactable when player has enough money to buy them 4
    /// and deactivate corresponding button in case if player have activated the upgrade.
    /// </summary>
    /// <param name="_currencyManager"></param>
    private void ManageButtonsInteractibility(CurrencyManager _currencyManager)
    {
        UpgradeManager.MakeBtnInteractibleWhenEnoughMoney(_currencyManager, limeUpgradeBtn, limeUpgradeCost);
        UpgradeManager.MakeBtnInteractibleWhenEnoughMoney(_currencyManager, iceUpgradeBtn, iceUpgradeCost);
        UpgradeManager.MakeBtnInteractibleWhenEnoughMoney(_currencyManager, sugarUpgradeBtn, sugarUpgradeCost);
        UpgradeManager.MakeBtnInteractibleWhenEnoughMoney(_currencyManager, ultraUpgradeBtn, ultraUpgradeCost);

        // check if the x2 points upgrade was already applied
        // if it was applied disable the button
        if (ingredientManager.limeValueToAdd != ingredientManager.storeLimeValueToAdd)
        {
            limeUpgradeBtn.interactable = false;
        }
        if (ingredientManager.iceValueToAdd != ingredientManager.storeIceValueToAdd)
        {
            iceUpgradeBtn.interactable = false;
        }
        if (ingredientManager.sugarValueToAdd != ingredientManager.storeSugarValueToAdd)
        {
            sugarUpgradeBtn.interactable = false;
        }
        if (ingredientManager.limeValueToAdd != ingredientManager.storeLimeValueToAdd
                    && ingredientManager.limeValueToAdd != ingredientManager.storeLimeValueToAdd * 2)
        {
            ultraUpgradeBtn.interactable = false;
        }
    }
    #endregion
}