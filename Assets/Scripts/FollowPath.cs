using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {
    public PathCreator path;
    [Range(1, 100)]
    public float speed;
    int index = 0;
    Vector3 target;

    // Use this for initialization
    void Start () {
        target = path.points[0];
	}

    // Update is called once per frame
    void FixedUpdate() {
        if (TargetReached())
        {
            if (!NextTarget())
            {
                // TODO Lose health
                Destroy(gameObject);
            }
        }
        Move();
	}

    bool TargetReached()
    {
        Debug.Log(target);
        if (Vector3.Distance(transform.position, target) < .5f)
        {
            return true;
        }
        return false;
    }

    bool NextTarget()
    {
        index++;
        if (index < path.points.Count)
        {
            Vector2 target2D = path.points[index];
            target = new Vector3(target2D.x, target2D.y, transform.position.z);
            return true;
        }
        return false;
    }

    void Move()
    {
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step); 
    }
}
