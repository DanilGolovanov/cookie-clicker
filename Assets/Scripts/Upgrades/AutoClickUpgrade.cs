using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoClickUpgrade : MonoBehaviour
{
    #region Variables

    private CurrencyManager currencyManager;

    // upgrade costs
    private int limeUpgradeCost = 5;
    private int iceUpgradeCost = 5;
    private int sugarUpgradeCost = 5;
    private int ultraUpgradeCost = 15;

    [Header("Price Tags")]
    [SerializeField, Tooltip("Text object of the 'lime auto click' upgrade price tag.")]
    private Text limePriceTag;
    [SerializeField, Tooltip("Text object of the 'ice auto click' upgrade price tag.")]
    private Text icePriceTag;
    [SerializeField, Tooltip("Text object of the 'sugar auto click' upgrade price tag.")]
    private Text sugarPriceTag;
    [SerializeField, Tooltip("Text object of the 'ultra auto click' upgrade price tag.")]
    private Text ultraPriceTag;

    private int costMultiplier = 3;

    [Header("Upgrade Buttons")]
    [SerializeField, Tooltip("Button object which is used to buy 'lime auto click' upgrade.")]
    private Button limeUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy 'ice auto click' upgrade.")]
    private Button iceUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy 'sugar auto click' upgrade.")]
    private Button sugarUpgradeBtn;
    [SerializeField, Tooltip("Button object which is used to buy 'ultra auto click' upgrade.")]
    private Button ultraUpgradeBtn;

    // timers
    private float limeTimer = -1;
    private float iceTimer = -1;
    private float sugarTimer = -1;
    private float ultraTimer = -1;
    
    // how many seconds to make auto click 
    private float limeAutoClickTime = 0;
    private float iceAutoClickTime = 0;
    private float sugarAutoClickTime = 0;
    private float ultraAutoClickTime = 0;

    [Header("Ingredient Buttons")]
    [SerializeField, Tooltip("Button which player click in order to collect limes.")]
    private Button limeBtn;
    [SerializeField, Tooltip("Button which player click in order to collect ice.")]
    private Button iceBtn;
    [SerializeField, Tooltip("Button which player click in order to collect sugar.")]
    private Button sugarBtn;

    [Header("Tip Panel Texts")]
    [SerializeField, Tooltip("Text object from the 'lime auto click' upgrade tip panel.")]
    private Text limeTipPanelText;
    [SerializeField, Tooltip("Text object from the 'ice auto click' upgrade tip panel.")]
    private Text iceTipPanelText;
    [SerializeField, Tooltip("Text object from the 'sugar auto click' upgrade tip panel.")]
    private Text sugarTipPanelText;
    [SerializeField, Tooltip("Text object from the 'ultra auto click' upgrade tip panel.")]
    private Text ultraTipPanelText;

    #endregion

    #region Default Methods
    private void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();

        // setup initial values on the price tags
        limePriceTag.text = "$" + limeUpgradeCost;
        icePriceTag.text = "$" + iceUpgradeCost;
        sugarPriceTag.text = "$" + sugarUpgradeCost;
        ultraPriceTag.text = "$" + ultraUpgradeCost;
    }

    private void Update()
    {
        ManageButtonsInteractibility(currencyManager);
        // make auto clicks where applicable
        limeTimer = GetIngredient(limeAutoClickTime, limeTimer, limeBtn);
        iceTimer = GetIngredient(iceAutoClickTime, iceTimer, iceBtn);
        sugarTimer = GetIngredient(sugarAutoClickTime, sugarTimer, sugarBtn);
        ultraTimer = GetIngredients(ultraAutoClickTime, ultraTimer);
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Subtract money, activate auto-click upgrade and update the cost of the next upgrade.
    /// </summary>
    /// <param name="ingredient">Name of the ingredient.</param>
    public void ApplyAutoClickUpgrade(string ingredient)
    {
        int cost = UpgradeManager.DetermineCost(ingredient, limeUpgradeCost, iceUpgradeCost, sugarUpgradeCost, ultraUpgradeCost);

        // withdraw money
        currencyManager.money -= cost;
        // determine which upgrade was chosen
        switch (ingredient)
        {
            case "Lime":
                // apply upgrade
                limeAutoClickTime = UpdateAutoClickTime(limeAutoClickTime);
                // increase the upgrade cost
                limeUpgradeCost *= costMultiplier;
                limePriceTag.text = "$" + limeUpgradeCost;
                // update description 
                limeTipPanelText.text = $"Auto-collect limes every {(limeAutoClickTime - 1).ToString("0.00")} seconds.";
                break;
            case "Ice":
                // apply upgrade
                iceAutoClickTime = UpdateAutoClickTime(iceAutoClickTime);
                // increase the upgrade cost
                iceUpgradeCost *= costMultiplier;
                icePriceTag.text = "$" + iceUpgradeCost;
                // update description 
                iceTipPanelText.text = $"Auto-collect ice every {(iceAutoClickTime - 1).ToString("0.00")} seconds.";
                break;
            case "Sugar":
                // apply upgrade
                sugarAutoClickTime = UpdateAutoClickTime(sugarAutoClickTime);
                // increase the upgrade cost
                sugarUpgradeCost *= costMultiplier;
                sugarPriceTag.text = "$" + sugarUpgradeCost;
                // update description 
                sugarTipPanelText.text = $"Auto-collect sugar every {(sugarAutoClickTime - 1).ToString("0.00")} seconds.";
                break;
            case "Ultra":
                // apply upgrade
                ultraAutoClickTime = UpdateAutoClickTime(ultraAutoClickTime);
                // increase the upgrade cost
                ultraUpgradeCost *= costMultiplier;
                ultraPriceTag.text = "$" + ultraUpgradeCost;
                // update description 
                ultraTipPanelText.text = $"Auto-collect everything every {(ultraAutoClickTime - 1).ToString("0.00")} seconds.";
                break;
        }
    }

    /// <summary>
    /// Update time necessary to make the auto click when player buys a new upgrade.
    /// </summary>
    /// <param name="autoClickTime">Time necessary to make the auto click.</param>
    /// <returns></returns>
    private float UpdateAutoClickTime(float autoClickTime)
    {
        if (autoClickTime == 0)
        {
            autoClickTime = 8;
        }
        else if (autoClickTime > 1)
        {
            autoClickTime--;
        }
        else if (autoClickTime <= 1 && autoClickTime >= 0.5)
        {
            autoClickTime -= 0.15f;
        }
        else if (autoClickTime < 0.5 && autoClickTime > 0)
        {
            autoClickTime /= 2;
        }

        return autoClickTime;
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

    /// <summary>
    /// Make the auto click and get the ingredient according to the autoClickTimer.
    /// </summary>
    /// <param name="clickTime">How long it takes to make the auto click.</param>
    /// <param name="timer">Time left before making the auto click.</param>
    /// <param name="ingredientButton">Which button to click.</param>
    /// <returns>Time left before making the auto click.</returns>
    private float GetIngredient(float clickTime, float timer, Button ingredientButton)
    {      
        // if upgrade was applied
        if (clickTime > 0)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                ingredientButton.onClick.Invoke();
                timer = clickTime;
            }
        }

        return timer;
    }

    /// <summary>
    /// Make the auto clicks and get all ingredients according to the autoClickTimer.
    /// </summary>
    /// <param name="clickTime">How long it takes to make the auto click.</param>
    /// <param name="timer">Time left before making the auto click.</param>
    /// <returns>Time left before making auto clicks.</returns>
    private float GetIngredients(float clickTime, float timer)
    {
        // if upgrade was applied
        if (clickTime > 0)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                limeBtn.onClick.Invoke();
                iceBtn.onClick.Invoke();
                sugarBtn.onClick.Invoke();
                timer = clickTime;
            }
        }

        return timer;
    }
    #endregion
}
