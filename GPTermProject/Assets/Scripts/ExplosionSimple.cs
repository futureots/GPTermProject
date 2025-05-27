using UnityEngine;

public class ExplosionSimple : MonoBehaviour
{
    public int size;
    public int damage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        var list = Physics.OverlapSphere(transform.position, size*1.5f);

        foreach (var item in list)
        {
            var enemy = item.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.Hit(damage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
