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

    private float maxStamina = 100;
    public float MaxStamina{
        get{return maxStamina;}
    }

	private float stamina = 0;
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
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider staminaSlider;
    [SerializeField] Image fatigueSlider;
    [SerializeField] Image hungerSlider;
    [SerializeField] Image thirstSlider;
    [SerializeField] Image mentalHealthSlider;

    private void Start(){

        currentHealth = maxHealth;
        stamina = maxStamina;
    }

    private void Update(){
        SetPlayerStatUI();
        DecreasePlayerState();
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

    public void SetPlayerStatUI(){
        healthSlider.maxValue = maxHealth;
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, .3f);

        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = Mathf.Lerp(staminaSlider.value, stamina, .3f);

        fatigueSlider.fillAmount = fatigue * 0.01f;//I do it this way because fatigue/hunger/and mental health values will decrease in percentage. Fillamount is clampled between 0 and 1.
        hungerSlider.fillAmount = hunger * 0.01f;
        thirstSlider.fillAmount = thirst * 0.01f;
        mentalHealthSlider.fillAmount = mentalHealth * 0.01f;
    }

    public void DecreasePlayerState(){
        float stateDecreaseSpeed = 1;
        fatigue = Mathf.MoveTowards(fatigue, 0, stateDecreaseSpeed * Time.deltaTime);
        hunger = Mathf.MoveTowards(hunger, 0, stateDecreaseSpeed * Time.deltaTime);
        thirst = Mathf.MoveTowards(thirst, 0, stateDecreaseSpeed * Time.deltaTime);
        mentalHealth = Mathf.MoveTowards(mentalHealth, 0, stateDecreaseSpeed * Time.deltaTime);
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
