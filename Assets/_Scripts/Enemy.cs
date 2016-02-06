using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	// PUBLIC VAL. INSTANCE
	public int hp = 1;
	public int point = 10;

	// Base Component
	Spaceship spaceship;

	IEnumerator Start () {
		spaceship = GetComponent<Spaceship> ();
		Move (transform.up * -1);

		if (spaceship.canShot == false) {
			yield break;
		}
			
		// Loop the Shoot if enemy is alive.
		while (true) {
			for (int i = 0; i < transform.childCount; i++) {
				
				Transform shotPosition = transform.GetChild (i);
				spaceship.Shot (shotPosition);
			}

			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}

	// Manage the Enemy Movement
	public void Move (Vector2 direction) {
		GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
	}

	// Check the Collider2D
	void OnTriggerEnter2D (Collider2D c) {
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

        Debug.Log("Bullet Enemy Hit1");

        if (layerName != "Bullet (Player)") {
            Debug.Log("Bullet Enemy Hit2");
			return;
		}

		Transform beam1Transform = c.transform.parent;
		Bullet bullet =  beam1Transform.GetComponent<Bullet>();
		hp = hp - bullet.power;

		// Bullet doens't have hp. So, bullet trigger the collider, destroy the object ASAP.
		Destroy(c.gameObject);

		// Check the Destroy HP, if Enemy HP is lower than 0, enemy will be die.
		if(hp <= 0 ) {
			// Add Score.
			FindObjectOfType<Score>().AddPoint(point);
			spaceship.Explosion ();
			Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		// If it is GameOver, Destroy All Game Object for next game.
		if (FindObjectOfType<GameManager> ().IsGameOver ()) {
			Destroy (gameObject);
		}
	}
}
