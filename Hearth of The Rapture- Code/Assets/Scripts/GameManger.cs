using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManger : MonoBehaviour {
	public static GameManger instance;
	private string playerDataPath; //The path where we store Player Data. 

	public GameObject player;
	private PlayerStats stats;

	private void Awake () {
		if(!instance){
			instance = this;
		}else{
			Destroy(gameObject);
		}
		
		playerDataPath = Application.persistentDataPath + "/playerData.dat";
	    stats = player.GetComponent<PlayerStats>();

		Debug.Log("Remember to delete the save file for playerdata from your device as not to corrupt data during the dev process. You can find the file at: " + 
        playerDataPath + " love, Gadnuuk");
	}

	

	public void SavePlayerData(){
        //This will save the players stats from this script to an external file found at the persistant data path. Th
        //The file created be almost unreadable to most people but nit entirely. 
        //We can change the location of the Save and Load functions later. 
        BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(playerDataPath);
        PlayerData data = new PlayerData(stats.MaxHealth, stats.CurrentHealth, stats.Stamina, stats.Fatigue, stats.MentalHealth, stats.Hunger, stats.Thirst);

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
			
			stats.LoadStats(data);
        }
    }
}
