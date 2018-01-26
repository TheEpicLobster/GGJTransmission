using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {
    public PathCreator path;
    [Range(0.01f, 20.0f)]
    public float speed = 5.0f;
    int index = 0;
    Vector3 target;
    Vector3 heading;
    float distance;

    // Use this for initialization
    void Start () {
        heading = transform.forward;
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
        PointToDestination();
        Move();
	}

    bool TargetReached()
    {
        if (Vector3.Distance(transform.position, target) < .1f)
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
            Vector3 target3D = new Vector3(target2D.x, target2D.y, transform.position.z);

            distance = (target3D - target).magnitude;
            heading = transform.forward;
            target = target3D;
            return true;
        }
        return false;
    }

    void PointToDestination()
    {

        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);

    }

    void Move()
    {
        float step = speed * Time.fixedDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, step); 
    }
}
