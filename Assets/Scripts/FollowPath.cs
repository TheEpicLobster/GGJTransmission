using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    Transform transform;
    int index = 0;
    Vector2 target;

    // Use this for initialization
    void Start () {
        transform = gameObject.GetComponent(typeof(Transform));
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (TargetReached())
        {
            NextTarget();
        }
        Move();
	}

    bool TargetReached()
    {
        if ((transform.position.x * transform.position.x + transform.position.y * transform.position.y) - target.sqrMagnitude <= 1)
        {
            return true;
        }
        return false;
    }

    void NextTarget()
    {

    }

    void Move()
    {
        
    }
}
