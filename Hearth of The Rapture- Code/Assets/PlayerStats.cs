using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

    private string playerDataPath; //The path where we store Player Data. 

    //I set the below variable as properties so that they are readonly out side of this class. If you want to alter a value please do so with one of the below methods.
    //We dont want these values altered all over the game.
    private float maxHealth = 0;
    public float MaxHealth{
        get{return maxHealth;}
    }

	private float currentHealth = 0;
    public float CurrentHealth{
        get{return currentHealth;}
    }

	private float stamina = 0;
    public float Stamina{
        get{return stamina;}
    }

	private float fatigue = 0;
    public float Fatigue{
        get{return fatigue;}
    }

	private float mentalHealth = 0;
    public float MentalHealth{
        get{return mentalHealth;}
    }

	private float hunger = 0;
    public float Hunger{
        get {return hunger;}
    }
	private float thirst = 0;
    public float Thirst{
        get{return thirst;}
    }

	//public string[] inventory;we can use this to dynamically create the player inventory at when the game loads. If we go that direction.
    [SerializeField] Slider healthSlider;
    [SerializeField] Slider staminaSlider;

    private void Start(){
        playerDataPath = Application.persistentDataPath + "/playerData.dat";

        Debug.Log("Remember to delete the save file for playerdata from your device as not to corrupt data during the dev process. You can find the file at: " + 
        playerDataPath + " love, Gadnuuk");

    }

    public void DealDamage(float dmg){
        currentHealth -= dmg;
        if(currentHealth < 0){
            currentHealth = 0;
        }
    }

    public void HealDamage(float heamAmt){
        currentHealth += heamAmt;
        if(currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
    }

    public void SetPlayerStatUI(){

    }

	public void SavePlayerData(){
        //This will save the players stats from this script to an external file found at the persistant data path. Th
        //The file created be almost unreadable to most people but nit entirely. 
        //We can change the location of the Save and Load functions later. 
        BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(playerDataPath);
        PlayerData data = new PlayerData(maxHealth, currentHealth, stamina, fatigue, mentalHealth, hunger, thirst);

        bf.Serialize(file, data);
        file.Close();
    }

    public void LoadPlayerData(){
        //get the file where we stored the player data, open it, then cast it as a PlayeData object. Then use the data to load the Player Stats in this script
        if(File.Exists(playerDataPath)){
            BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(playerDataPath, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            maxHealth = data.maxHealth;
            currentHealth = data.currentHealth;
            stamina = data.stamina;
            fatigue = data.fatigue;
            mentalHealth = data.mentalHealth;
            hunger = data.hunger;
            thirst = data.thirst;
        }
    }
}
