using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class PlayerData{

	public float maxHealth = 0;
	public float currentHealth = 0;
	public float stamina = 0;
	public float fatigue = 0;
	public float mentalHealth = 0;
	public float hunger = 0;
	public float thirst = 0;

	//public string[] inventory; add this to the constructor later in case we want to store that player inventory as an array of strings in the same external file. 

	public PlayerData(float maxHealth, float currentHealth, float stamina, float fatigue, float mentalHealth, float hunger, float thirst){
		this.maxHealth = maxHealth;
		this.currentHealth = currentHealth; 
		this.stamina = stamina;
		this.fatigue = fatigue;
		this.mentalHealth = mentalHealth;
		this.mentalHealth = mentalHealth;
		this.hunger = hunger;
		this.thirst = thirst;
	}
}
