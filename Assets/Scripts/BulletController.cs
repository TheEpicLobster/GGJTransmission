﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    
    Vector3 step;
    TowerStats.Stats stats;
    Vector3 origin;

    int targetLock = 0;

    bool inFlight = false;    
	
	void FixedUpdate () {
        if (!inFlight)
        {
            return;
        }
        Vector3 newPos = transform.position + step * Time.fixedDeltaTime;
        if ((newPos - origin).sqrMagnitude > stats.range * stats.range)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = newPos;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Virus")
        {
            
            if (System.Threading.Interlocked.CompareExchange(ref targetLock, 1, 0) == 0)
            {
                other.gameObject.GetComponent<VirusStats>().TakeDamage(stats.damage);
                Destroy(gameObject);
            }
        }
    }

    public void Shoot( Vector3 _dir, Vector3 _origin, TowerStats.Stats _stats)
    {

        step = _dir.normalized * _stats.projectileSpeed;
        stats = _stats;
        origin = _origin;

        inFlight = true;
    }
}
