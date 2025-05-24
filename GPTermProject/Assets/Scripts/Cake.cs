using Unity.VisualScripting;
using UnityEngine;

public class Cake : MonoBehaviour
{
    public int maxHp =1000;
    public int curHp {  get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Punch"))
        {
            DecreaseHp(1);
        }
    }
    public void DecreaseHp(int i)
    {
        curHp -= i;
        if (curHp <= 0)
        {
            GameManager.Instance.GameOver();
            gameObject.SetActive(false);
        }
    }
}
