using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputSystem_Actions _inputActions;
    public List<GameObject> cupCakes;
    GameObject curCake;

    public Transform rHand;
    public Rigidbody rb;
    public Animator anim;

    public PlayerSetting data;
    public AudioSource audioSource;
    public WeaponUI ui;
    public float speed;
    float rotateSpeed;
    Vector3 direction;
    public float power;
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    bool isThrowing = false;
    private void Start()
    {
        rotateSpeed = data.RotateSpeed;
        audioSource.volume = data.BgmSound;
        curCake = cupCakes[0];
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

            var rot = Camera.main.transform.eulerAngles;
            rot.x = rot.x > 180f ? rot.x-360 : rot.x;
            rot.x -= vec.y * rotateSpeed * 10 * Time.deltaTime;
            rot.x = Mathf.Clamp(rot.x, -80, 80);

            rot.x = rot.x <0 ? rot.x+360 : rot.x;
            Camera.main.transform.eulerAngles = rot;

        };
        _inputActions.Player.Attack.performed += Throw;
        _inputActions.Player.Interact.performed += Roll;


        GameManager.Instance.OnGameOver.AddListener(() =>
        {
            _inputActions.Player.Attack.performed -= Throw;
            _inputActions.Player.Interact.performed -= Roll;
        });

        _inputActions.Player.Respawn.started += x =>
        {
            transform.position = GameManager.Instance.RespawnPoint.position;
            rb.linearVelocity = Vector3.zero;
        };

    }
    void Roll(InputAction.CallbackContext context)
    {
        if (!isThrowing)
        {
            StartCoroutine(RollCoroutine());
        }
    }
    void Throw(InputAction.CallbackContext context)
    {
        //Debug.Log("Throw");
        if (!isThrowing)
        {
            StartCoroutine(ThrowCoroutine());
        }
    }
    IEnumerator RollCoroutine()
    {
        var cake = Instantiate(curCake, rHand.position, rHand.rotation);
        var rigidbody = cake.GetComponent<Rigidbody>();
        rigidbody.AddForce(Camera.main.transform.forward * power * 0.1f, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
    }
    IEnumerator ThrowCoroutine()
    {
        //anim.SetTrigger("Throw");
        
        //isThrowing = true;  
        //yield return new WaitForSeconds(0.65f);
        var cake = Instantiate(curCake, rHand.position, rHand.rotation);
        var rigidbody = cake.GetComponent<Rigidbody>();
        rigidbody.AddForce((Camera.main.transform.forward+Vector3.up*0.5f).normalized * power, ForceMode.Impulse);
        yield return new WaitForSeconds(1.5f);
        //isThrowing = false;
    }
    // Update is called once per frame
    void Update()
    {
        var worldDir = transform.TransformDirection(direction);
        rb.linearVelocity = new Vector3(worldDir.x*speed, rb.linearVelocity.y, worldDir.z*speed);
        //transform.Translate(direction * Time.deltaTime * speed);
        //Debug.Log(direction);
        anim.SetFloat("deltaX", direction.x);
        anim.SetFloat("deltaZ", direction.z);

        for(int i = 0; i < cupCakes.Count; i++)
        {
            if(Input.GetKeyDown(KeyCode.Alpha0 + i + 1))
            {
                ui.ShowUI(i);
                curCake = cupCakes[i];
            }
        }
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
