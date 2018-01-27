using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerStats : MonoBehaviour {
    public enum TowerType
    {
        Single,
        Multi,
        Splash
    }

    [System.Serializable]
    public class Stats
    {
        [Range(0, 999)]
        public int damage = 0;
        [Range(0.01f, 24.0f)]
        public float fireRate = 0.01f;
        [Range(0.01f, 1000.0f)]
        public float range = 20.0f;
        [Range(0.01f, 1000.0f)]
        public float projectileSpeed = 1.0f;
        [Range(0, 10000)]
        public int price = 100;

        public TowerType type = TowerType.Single;
    }

    public List<Stats> levels;

    int level;

    // Use this for initialization
    void Start() {
        level = 0;
        GetComponent<CircleCollider2D>().radius = GetStats().range;
        
    }


    public Stats GetStats()
    {
        return levels[level];
    }
}
