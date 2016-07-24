using UnityEngine;
using System.Collections;

public class PlayerDamage : MonoBehaviour {

	public float player_health = 100;
	public float damage_multiplier = 1f;
	public LayerMask mask = -1;


	void OnCollisionEnter (Collision other)
	{
		
		if (mask.DoesContainLayer(other.gameObject.layer))
			PlayerTakeDamage ((int)(other.relativeVelocity.magnitude * damage_multiplier));
	}

	public void PlayerTakeDamage(int damage){
		player_health -= damage;
		Debug.Log (player_health);
	}
}

public static class LayerMaskUtils {

	public static bool DoesContainLayer(this LayerMask mask, int layer)
	{
		int m_mask = mask.value;
		int l_mask = 1 << (layer);
		int combined_layer = m_mask & l_mask;

		if (combined_layer != 0) {
			Debug.Log ("Returning True");
			return true;
		} else {
			Debug.Log ("Returning False");
			return false;
		}
	}

}