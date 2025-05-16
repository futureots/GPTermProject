using UnityEngine;
using UnityEngine.InputSystem;

public class CakeThrower : MonoBehaviour
{
    public GameObject cupCake;
    public float power;
    private void Start()
    {
        PlayerControl._inputActions.Player.Attack.performed += value =>
        {
            Debug.Log("Throw");
            var cake = Instantiate(cupCake, transform.position, transform.rotation);
            var rigidbody = cake.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward * power, ForceMode.Impulse);
        };
    }
}
