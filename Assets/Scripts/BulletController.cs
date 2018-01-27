using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    Vector3 step;
    TowerStats.Stats stats;
    Vector3 origin;

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
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Virus")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
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
