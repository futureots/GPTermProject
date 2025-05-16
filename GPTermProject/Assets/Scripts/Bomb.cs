using System.Collections;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionEffect;
    private void Start()
    {
        StartCoroutine(ExplosionCoroutine());   
    }

    IEnumerator ExplosionCoroutine()
    {
        yield return new WaitForSeconds(3f);
        var effect = Instantiate(ExplosionEffect,transform.position,transform.rotation);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
