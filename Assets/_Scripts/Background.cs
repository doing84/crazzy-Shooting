using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour
{
	// PUBLIC VAL. INSTANCE
	public float speed = 1.2f;

	// Update is called once per frame
	void Update ()
	{
		float y = Mathf.Repeat (Time.time * speed, 2);
		Vector2 offset = new Vector2 (0, y);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", offset);
	}
}