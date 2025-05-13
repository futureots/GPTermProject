using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputSystem_Actions _inputActions;
    public GameObject cupCake;
    public float power;

    Vector3 direction;
    public float speed;
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }
    void Start()
    {
        _inputActions.Player.Attack.performed += value =>
        {
            Debug.Log("Throw");
            var cake = Instantiate(cupCake,transform.position+ Vector3.forward,transform.rotation);
            var rigidbody = cake.GetComponent<Rigidbody>();
            rigidbody.AddForce(transform.forward*power, ForceMode.Impulse);
        };

        
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        

        transform.Translate(direction * speed * Time.deltaTime);

    }
    private void OnEnable()
    {
        _inputActions.Enable();
    }
    private void OnDisable()
    {
        _inputActions.Disable();
    }
}
