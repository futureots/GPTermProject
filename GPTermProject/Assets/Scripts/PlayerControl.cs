using System.Collections;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputSystem_Actions _inputActions;
    public GameObject cupCake;

    public Transform rHand;
    public Animator anim;

    public float speed;
    public float rotateSpeed;
    Vector3 direction;
    public float power;
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        anim = GetComponentInChildren<Animator>();
    }

    bool isThrowing = false;
    private void Start()
    {
        _inputActions.Player.Attack.performed += value =>
        {
            Debug.Log("Throw");
            if (!isThrowing)
            {
                StartCoroutine(ThrowCoroutine());
            }
        };
        _inputActions.Player.Move.started += value => anim.SetBool("IsRun", true);
        _inputActions.Player.Move.performed += value =>
        {
            var vec = value.ReadValue<Vector2>();
            direction.x = vec.x;
            direction.z = vec.y;
            direction.Normalize();
        };
        _inputActions.Player.Move.canceled += value =>
        {
            anim.SetBool("IsRun", false);
            direction = Vector3.zero;
        };
        _inputActions.Player.Look.performed += value =>
        {
            var vec = value.ReadValue<Vector2>();
            transform.Rotate(Vector3.up * vec.x * rotateSpeed * 0.1f);
        };
    }



    IEnumerator ThrowCoroutine()
    {
        anim.SetTrigger("Throw");
        
        //isThrowing = true;  
        yield return new WaitForSeconds(0.65f);
        var cake = Instantiate(cupCake, rHand.position, rHand.rotation);
        var rigidbody = cake.GetComponent<Rigidbody>();
        rigidbody.AddForce((transform.forward + Vector3.up).normalized * power, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        //isThrowing = false;
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        Debug.Log(direction);
        anim.SetFloat("deltaX", direction.x);
        anim.SetFloat("deltaZ", direction.z);
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
