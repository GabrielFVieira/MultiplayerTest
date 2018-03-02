using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    private int dmg = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        GameObject hit = col.gameObject;
        Health health = hit.GetComponent<Health>();

        if(health != null)
        {
            health.TakeDamage(dmg);
        }

        Destroy(gameObject);
    }
}
