using UnityEngine;

public class Debugginger : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Animation>().Play("Idle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
