using System.Collections;

using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject ExplosionEffect;

    public float gravityScale;
    Rigidbody _rigidbody;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        StartCoroutine(ExplosionCoroutine());
    }
    private void FixedUpdate()
    {
        _rigidbody.AddForce(Physics.gravity*gravityScale, ForceMode.Acceleration);
    }
    IEnumerator ExplosionCoroutine()
    {
        yield return new WaitForSeconds(3f);
        var effect = Instantiate(ExplosionEffect,transform.position,transform.rotation);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
