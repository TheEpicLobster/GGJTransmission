using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemies : MonoBehaviour {

    public GameObject bullet;

    GameObject enemy;
    EnemyTracker tracker;
    TowerStats stats;

    int countSinceLast = 0;
    int fireGap = 0;
    private void Start()
    {
        tracker = GetComponent<EnemyTracker>();
        stats = GetComponent<TowerStats>();
        fireGap = (int)(1.0f / (Time.fixedDeltaTime * stats.GetStats().fireRate));
    }

    // Update is called once per frame
    void FixedUpdate () {
        TrackEnemy();

        countSinceLast++;
        if (countSinceLast > fireGap && enemy != null)
        {
            countSinceLast = 0;
            ShootTarget();
        }

    }

    void TrackEnemy()
    {
        enemy = tracker.GetLockedEnemy();

        // No enemies in range
        if (enemy == null)
        {
            return;
        }

        Vector3 face = enemy.transform.position;
        Vector3 target = new Vector3(face.x, face.y, transform.position.z);
        Vector3 vectorToTarget = target - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 1);
    }

    void ShootTarget()
    {
        Vector3 dir = transform.up;

        GameObject newBullet = Instantiate(bullet, transform.position, Quaternion.identity);

        newBullet.GetComponent<BulletController>().Shoot(dir, transform.position, stats.GetStats());
    }
}
