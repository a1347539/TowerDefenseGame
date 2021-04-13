using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower_3_impactParticle_online : MonoBehaviour
{
    private ParticleSystem thisParticleSystem;

    ParticleSystem.CollisionModule col;

    private float damage;

    private void Start()
    {
        thisParticleSystem = GetComponent<ParticleSystem>();
        col = thisParticleSystem.collision;
        damage = transform.parent.GetComponent<tower_3_proj_online>().Parent_tower.damage;
    }

    private void OnParticleCollision(GameObject collision)
    {
        col.enabled = false;
        collision.GetComponent<Enemy_Online>().OnCollision(damage / 2);
    }
}
