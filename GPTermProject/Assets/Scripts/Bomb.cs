using System.Collections;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionEffect;

    public float explosionTime;
    private void Start()
    {
        StartCoroutine(ExplosionCoroutine());   
    }

    IEnumerator ExplosionCoroutine()
    {
        yield return new WaitForSeconds(explosionTime);
        var effect = Instantiate(ExplosionEffect,transform.position,Quaternion.identity);

        var list = Physics.OverlapSphere(transform.position, 7.5f);

        foreach (var item in list)
        {
            var enemy = item.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.Hit(100);
            }
        }

        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
