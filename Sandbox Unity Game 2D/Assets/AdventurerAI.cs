﻿using UnityEngine;
using System.Collections;

public class AdventurerAI : CreatureBase
{
	public int moveSpeed;
	public int rotationSpeed;
	
	void Start() 
	{
		maxHealth = health;
		/*health = 10;
		armor = 1;
		armorDivisor = 1;
		damage = 1;
		isDead = false;
		team = "Adventurer";
		hitTimer = 0;
		hitCooldown = 50;*/
	}
	
	void Update() 
	{   
		if (health < (maxHealth/3)) {
			if ((target != null && target.tag != "entrance") || (target == null)) {
				target = GameObject.FindGameObjectWithTag("entrance");
				Debug.Log("Running!");
			}
		}
		if (target != null) 
		{
			Vector3 dir = target.transform.position - transform.position;
			// Only needed if objects don't share 'z' value.
			dir.z = 0.0f;
			/*if (dir != Vector3.zero) 
				transform.rotation = Quaternion.Slerp ( transform.rotation, 
				                                       Quaternion.FromToRotation (Vector3.right, dir), 
				                                       rotationSpeed * Time.deltaTime);*/
			
			Vector3 old_scale = transform.localScale;
			if (dir.x > 0 && old_scale.x > 0) {
				transform.localScale = new Vector3(old_scale.x * -1, old_scale.y, old_scale.z);
			} else if (dir.x < 0 && old_scale.x < 0) {
				transform.localScale = new Vector3(old_scale.x * -1, old_scale.y, old_scale.z);
			}
			//Move Towards Target
			transform.position += (target.transform.position - transform.position).normalized 
				* moveSpeed * Time.deltaTime;
		} else {
			target = GameObject.FindGameObjectWithTag("ant");
		}
		GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		if (hitTimer > 0) {
			hitTimer--;
		}
		Vector3 pos = Camera.main.WorldToViewportPoint(this.transform.position);
		pos.x = Mathf.Clamp(pos.x,0f,1f);
		pos.y = Mathf.Clamp(pos.y,0f,1f);
		transform.position = Camera.main.ViewportToWorldPoint(pos);
	}

	void OnCollisionEnter2D(Collision2D collision) {
		//Debug.Log("hey!");
		if (collision.gameObject.tag == "entrance" && health < (maxHealth/3)) {
			Debug.Log("Ran!");
			Destroy(this.gameObject);
		}
	}
}