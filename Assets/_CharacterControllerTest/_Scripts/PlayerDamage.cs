using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

	public float player_health = 100;
	public float damage_multiplier = 1f;
	public LayerMask mask = -1;


	void OnCollisionEnter (Collision other)
	{
		if (other.gameObject.layer == mask)
			PlayerTakeDamage ((int)(other.relativeVelocity.magnitude * damage_multiplier));
	}

	public void PlayerTakeDamage(int damage){
		player_health -= damage;
		Debug.Log (player_health);
	}




}
