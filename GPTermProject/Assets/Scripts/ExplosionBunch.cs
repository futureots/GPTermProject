using UnityEngine;
using System.Collections.Generic;

public class ExplosionBunch : MonoBehaviour
{
    public int size;
    public int damage;
    ParticleSystem particle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        List<ParticleCollisionEvent> collisions = new();
        var eventSystem = particle.GetCollisionEvents(other, collisions);
        for(int i = 0; i < eventSystem; i++)
        {
            var pos = collisions[i].intersection;
            var list = Physics.OverlapSphere(pos, size * 1.5f);
            //Debug.Log(other.name);
            foreach (var item in list)
            {
                var enemy = item.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.Hit(damage);
                }
            }
        }
        
    }
}
