﻿using System.Collections;
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
        [Range(0.01f, 100.0f)]
        public float range = 20.0f;
        [Range(0.01f, 100.0f)]
        public float projectileSpeed = 1.0f;

        public TowerType type = TowerType.Single;
    }

    public List<Stats> levels;

    int level;

    // Use this for initialization
    void Start() {
        level = 0;
        CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
        if (colliders[0].radius > colliders[1].radius)
        {
            colliders[0].radius = GetStats().range;
        }
        else
        {
            colliders[1].radius = GetStats().range;
        }
    }


    public Stats GetStats()
    {
        return levels[level];
    }
}