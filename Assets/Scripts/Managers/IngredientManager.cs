using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientManager : MonoBehaviour
{
    #region Variables

    [Header("Ingredient Counters")]
    [Tooltip("Number of limes collected.")]
    public int limeCounter;
    [Tooltip("Number of ice cubes collected.")]
    public int iceCounter;
    [Tooltip("Number of sugar cubes collected.")]
    public int sugarCounter;

    [Header("Counter Holders")]
    [SerializeField, Tooltip("UI Text object which displays number of collected limes.")]
    private Text limeCounterText;
    [SerializeField, Tooltip("UI Text object which displays number of collected ice cubes.")]
    private Text iceCounterText;
    [SerializeField, Tooltip("UI Text object which displays number of collected sugar cubes.")]
    private Text sugarCounterText;

    [Header("Animated 'Add' Text")]
    [SerializeField, Tooltip("UI Text object which is displayed when the lime button is pressed.")]
    private Text limeAddText;
    [SerializeField, Tooltip("UI Text object which is displayed when the ice button is pressed.")]
    private Text iceAddText;
    [SerializeField, Tooltip("UI Text object which is displayed when the sugar button is pressed.")]
    private Text sugarAddText;

    // Animations which play when player presses corresponding ingedient button in the play area
    private Animation limeAddAnimation;
    private Animation iceAddAnimation;
    private Animation sugarAddAnimation;

    [Header("Value to Add")]
    [Tooltip("How many limes are added per one click.")]
    public int limeValueToAdd;
    [Tooltip("How many ice cubes are added per one click.")]
    public int iceValueToAdd;
    [Tooltip("How many sugar cubes are added per one click.")]
    public int sugarValueToAdd;

    [Header("Store 'Value to Add'")]
    [Tooltip("Variable which stores default value of lime 'value to add' variable.")]
    public int storeLimeValueToAdd;
    [Tooltip("Variable which stores default value of ice 'value to add' variable.")]
    public int storeIceValueToAdd;
    [Tooltip("Variable which stores default value of sugar 'value to add' variable.")]
    public int storeSugarValueToAdd;

    #endregion

    #region Default Methods
    private void Start()
    {
        // get corresponding animation objects
        limeAddAnimation = limeAddText.GetComponent<Animation>();
        iceAddAnimation = iceAddText.GetComponent<Animation>();
        sugarAddAnimation = sugarAddText.GetComponent<Animation>();

        // store initial values to 'store' variables
        storeLimeValueToAdd = limeValueToAdd;
        storeIceValueToAdd = iceValueToAdd;
        storeSugarValueToAdd = sugarValueToAdd;
    }

    private void Update()
    {        
        UpdateCounter(limeCounterText, limeCounter);
        UpdateCounter(iceCounterText, iceCounter);
        UpdateCounter(sugarCounterText, sugarCounter);
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Update lime counter variable and play limeAdd animation when player clicks the lime button.
    /// </summary>
    public void ClickLime()
    {
        limeCounter += limeValueToAdd;
        limeAddText.text = "+" + limeValueToAdd;
        PlayAnimation(limeAddAnimation, "AddIngredient");
    }

    /// <summary>
    /// Update ice counter variable and play iceAdd animation when player clicks the ice button.
    /// </summary>
    public void ClickIce()
    {
        iceCounter += iceValueToAdd;
        iceAddText.text = "+" + iceValueToAdd;
        PlayAnimation(iceAddAnimation, "AddIngredient");
    }

    /// <summary>
    /// Update sugar counter variable and play sugarAdd animation when player clicks the sugar button.
    /// </summary>
    public void ClickSugar()
    {
        sugarCounter += sugarValueToAdd;
        sugarAddText.text = "+" + sugarValueToAdd;
        PlayAnimation(sugarAddAnimation, "AddIngredient");
    }
    
    /// <summary>
    /// Plays animation and restarts it if the same animation was invoked again and the previous one wasn't finished.
    /// </summary>
    /// <param name="animation">Animation object.</param>
    /// <param name="animationName">Animation name as it is called in Unity.</param>
    public static void PlayAnimation(Animation animation, string animationName)
    {
        if (animation.isPlaying)
        {
            animation.Stop(animationName);
            animation.Play(animationName);
        }
        else
        {
            animation.Play(animationName);
        }
    }

    /// <summary>
    /// Update UI text element using updated counter value.
    /// </summary>
    /// <param name="counterText">UI Text element where the amount of corresponding collected ingredients is displayed.</param>
    /// <param name="value">Counter variable.</param>
    private void UpdateCounter(Text counterText, int value)
    {
        counterText.text = "x" + value;
    }
    #endregion
}
