using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyManager : MonoBehaviour
{
    #region Variables

    private IngredientManager ingredientManager;

    [Header("Bank Account")]
    [Tooltip("Amount of money in the bank account.")]
    public int money = 0;
    [SerializeField, Tooltip("UI Text object used for AddMoney animation which is played when item is sold.")]
    private Text moneyText;

    [Header("Status Text Animation")]
    [SerializeField, Tooltip("Text which appears when there are not enough resources.")]
    private Text statusText;
    // Animation of the status text
    private Animation statusTextAnimation;

    [Header("Make & Sell")]
    [SerializeField, Tooltip("UI Text object displaying glass of lemonade price.")]
    private Text glassMoneyText;
    [SerializeField, Tooltip("UI Text object displaying jug of lemonade price.")]
    private Text jugMoneyText;
    [SerializeField, Tooltip("UI Text object displaying bucket of lemonade price.")]
    private Text bucketMoneyText;

    // Animations which play when player sells corresponding item
    private Animation sellGlassAnimation;
    private Animation sellJugAnimation;
    private Animation sellBucketAnimation;

    [Header("Subtract Resources Animation")]
    [SerializeField, Tooltip("UI Text object which is displayed when limes are subtracted.")]
    private Text limeAnimatedText;
    [SerializeField, Tooltip("UI Text object which is displayed when ice is subtracted.")]
    private Text iceAnimatedText;
    [SerializeField, Tooltip("UI Text object which is displayed when sugar is subtracted.")]
    private Text sugarAnimatedText;

    // Animations which play when player sells any item
    private Animation limeSubtractAnimation;
    private Animation iceSubtractAnimation;
    private Animation sugarSubtractAnimation;

    #endregion

    #region Default Methods
    private void Start()
    {
        // get a reference to the ingredient manager
        ingredientManager = FindObjectOfType<IngredientManager>();

        // find corresponding animations
        statusTextAnimation = statusText.GetComponent<Animation>();

        sellGlassAnimation = glassMoneyText.GetComponent<Animation>();
        sellJugAnimation = jugMoneyText.GetComponent<Animation>();
        sellBucketAnimation = bucketMoneyText.GetComponent<Animation>();

        limeSubtractAnimation = limeAnimatedText.GetComponent<Animation>();
        iceSubtractAnimation = iceAnimatedText.GetComponent<Animation>();
        sugarSubtractAnimation = sugarAnimatedText.GetComponent<Animation>();
    }

    private void Update()
    {
        // update amount of money in the bank account
        moneyText.text = "Bank Account: $" + money;
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Subtract resources, add $1, update bank account and play sellGlass animation.
    /// </summary>
    public void SellGlass()
    {
        SellItem(5, 5, 5, 1, glassMoneyText, sellGlassAnimation);
    }

    /// <summary>
    /// Subtract resources, add $15, update bank account and play sellJug animation.
    /// </summary>
    public void SellJug()
    {
        SellItem(50, 50, 50, 15, jugMoneyText, sellJugAnimation);
    }

    /// <summary>
    /// Subtract resources, add $100, update bank account and play sellBucket animation.
    /// </summary>
    public void SellBucket()
    {
        SellItem(200, 200, 200, 100, bucketMoneyText, sellBucketAnimation);
    }

    /// <summary>
    /// Sell item if there are enough resources (subtract resources and add money to bank account, play subtractResources and addMoney animations).
    /// If there are not enough money, play NotEnoughResources animation.
    /// </summary>
    /// <param name="lime">How many limes to subtract.</param>
    /// <param name="ice">How many ice cubes to subtract.</param>
    /// <param name="sugar">How many sugar cubes to subtract.</param>
    /// <param name="_money">How much money to add to the bank account.</param>
    /// <param name="animatedMoneyText">UI Text element which is animated when item was sold.</param>
    /// <param name="animation">Animation which is played when item was sold.</param>
    private void SellItem(int lime, int ice, int sugar, int _money, Text animatedMoneyText, Animation animation)
    {
        // if there are enough resources, sell the lemonade
        if (ingredientManager.limeCounter - lime >= 0 && ingredientManager.iceCounter - ice >= 0 && ingredientManager.sugarCounter - sugar >= 0)
        {
            // subtract resources
            ingredientManager.limeCounter -= lime;
            ingredientManager.iceCounter -= ice;
            ingredientManager.sugarCounter -= sugar;

            // text for subtract animation
            limeAnimatedText.text = "-" + lime;
            iceAnimatedText.text = "-" + ice;
            sugarAnimatedText.text = "-" + sugar;

            // play subtract animation
            IngredientManager.PlayAnimation(limeSubtractAnimation, "SubtractResources");
            IngredientManager.PlayAnimation(iceSubtractAnimation, "SubtractResources");
            IngredientManager.PlayAnimation(sugarSubtractAnimation, "SubtractResources");

            // create and play addMoney animation 
            animatedMoneyText.text = "+$" + _money;
            IngredientManager.PlayAnimation(animation, "AddMoney");

            //add money to the bank account
            money += _money;
        }
        else if (ingredientManager.limeCounter - lime < 0 || ingredientManager.sugarCounter - sugar < 0 || ingredientManager.iceCounter - ice < 0)
        {
            // display that there are not enough resources
            statusText.text = "Not Enough Resources";
            IngredientManager.PlayAnimation(statusTextAnimation, "NotEnoughResources");
        }
    }
    #endregion
}
