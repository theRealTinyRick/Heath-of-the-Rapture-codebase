using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {
    //I set the below variable as properties so that they are readonly out side of this class. If you want to alter a value please do so with one of the below methods.
    //We dont want these values altered all over the game.
    private float maxHealth = 100;
    public float MaxHealth{
        get{return maxHealth;}
    }

	private float currentHealth = 0;
    public float CurrentHealth{
        get{return currentHealth;}
    }
	private float stamina = 100;
    public float Stamina{
        get{return stamina;}
    }

    //every property below this line will be representing a percentage
	private float fatigue = 100;
    public float Fatigue{
        get{return fatigue;}
    }

	private float mentalHealth = 100;
    public float MentalHealth{
        get{return mentalHealth;}
    }

	private float hunger = 100;
    public float Hunger{
        get {return hunger;}
    }
	private float thirst = 100;
    public float Thirst{
        get{return thirst;}
    }

    //public string[] inventory;we can use this to dynamically create the player inventory at when the game loads. If we go that direction.
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Image fatigueSlider;
    [SerializeField] private Image hungerSlider;
    [SerializeField] private Image thirstSlider;
    [SerializeField] private Image mentalHealthSlider;

    private bool playerMayUseStamina = true;
    private float stasisTime = 1;

    private void Start(){

        currentHealth = maxHealth;
    }

    private void Update(){
        SetPlayerStatUI();
        DecreasePlayerState();


        //Use the below code to test the stamina system. 
        if(Input.GetMouseButton(0) && playerMayUseStamina){
            UseStamina();
        }

        if(!Input.GetMouseButton(0) || !playerMayUseStamina){
            IncreaseStamina();
        }
    }

    public void DealDamage(float dmg){
        currentHealth -= dmg;
        if(currentHealth < 0){
            currentHealth = 0;
        }
    }

    public void Heal(float heamAmt){
        currentHealth += heamAmt;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    public void HealOverTime(){
        float healOverTimeScale = 5;
        currentHealth = Mathf.MoveTowards(currentHealth, maxHealth, healOverTimeScale * Time.deltaTime);
    }

    public void UseStamina(){
        float rateOfStaminaLoss = 20;
        stamina = Mathf.MoveTowards(stamina, 0, rateOfStaminaLoss * Time.deltaTime);
        
        if(stamina <= 0){
            fatigue -= 10.0f;
            if(fatigue < 0){
                fatigue = 0;
            }

            thirst -= 25.0f;
            if(thirst < 0){
                thirst = 0; 
            }
            StartCoroutine(Stasis());
        }
    }

    private void IncreaseStamina(){
        float rateOfStaminaGain = 30;
        stamina = Mathf.MoveTowards(stamina, 100, rateOfStaminaGain * Time.deltaTime);
    }

    private IEnumerator Stasis(){
        //this function will be called to tell the player that he has become to tired and must rest for a few seconds before using stamina again. *See UseStamina
        playerMayUseStamina = false;
        yield return new WaitForSeconds(stasisTime);
        float time = Time.time;
        playerMayUseStamina = true;
        yield return null;
    }
    
    public void DecreasePlayerState(){
        float stateDecreaseSpeed = 1;
        fatigue = Mathf.MoveTowards(fatigue, 0, stateDecreaseSpeed * Time.deltaTime);
        hunger = Mathf.MoveTowards(hunger, 0, stateDecreaseSpeed * Time.deltaTime);
        thirst = Mathf.MoveTowards(thirst, 0, stateDecreaseSpeed * Time.deltaTime);
        mentalHealth = Mathf.MoveTowards(mentalHealth, 0, stateDecreaseSpeed * Time.deltaTime);
    }

    public void SetPlayerStatUI(){
        healthSlider.maxValue = maxHealth;
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, .3f);

        staminaSlider.maxValue = 100;
        staminaSlider.value = Mathf.Lerp(staminaSlider.value, stamina, .3f);

        fatigueSlider.fillAmount = fatigue * .01f;
        thirstSlider.fillAmount = thirst * .01f;
        hungerSlider.fillAmount = hunger * .01f;
        mentalHealthSlider.fillAmount = mentalHealth * .01f;
    }

    public void LoadStats(PlayerData data){
        maxHealth = data.maxHealth;
        currentHealth = data.currentHealth;
        stamina = data.stamina;
        fatigue = data.fatigue;
        mentalHealth = data.mentalHealth;
        hunger = data.hunger;
        thirst = data.thirst;
    }
}
