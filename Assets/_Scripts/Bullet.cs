using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	// PUBLIC VAL. INSTANCE
	public int speed = 10;
	public float lifeTime = 1;
	public int power = 1;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
		Destroy (gameObject, lifeTime);
	}
}