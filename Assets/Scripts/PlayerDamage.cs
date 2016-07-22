using UnityEngine;
using System.Collections;

public class PlayerDamage {

	public float player_health = 100;
	public float damage_multiplier = 1f;
	public LayerMask mask; //= -1;


	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.LayerMask == mask)
			PlayerTakeDamage ((int)(other.relativeVelocity.magnitude * damage_multiplier));
	}

	public void PlayerTakeDamage(int damage){
		player_health -= damage;
	}




}
