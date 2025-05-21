using UnityEngine;

public class Cake : MonoBehaviour
{
    public int hp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            hp -= 10;
            Debug.Log(hp);
        }
    }
}
