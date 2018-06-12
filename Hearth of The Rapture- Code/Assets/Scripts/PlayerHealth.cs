using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
	
	private float timeSinceLastHit = 0;
	private float autoHealTime = 5; 

	private PlayerStats stats;

	void Start () {
		stats = GetComponent<PlayerStats>();
	}
	
	void Update () {
		AutoHeal();
	}

	private void AutoHeal(){
		if(Time.time - timeSinceLastHit > autoHealTime){
			stats.HealOverTime();
			Debug.Log(stats.CurrentHealth);
		}
	}

	private void CalculateDamage(float dmg){
		//find how we are going to actually damage the player
		//apply resistances and what not once we know what they are
		//I added the input here to easily test, Im done with it so you can delete it if you want. 
		// if(Input.GetMouseButtonDown(0)){
			stats.DealDamage(dmg);
			timeSinceLastHit = Time.time;
		// }
	}
}
