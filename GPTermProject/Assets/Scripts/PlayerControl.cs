using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.Rendering.DebugUI.Table;

public class PlayerControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputSystem_Actions _inputActions;
    public GameObject cupCake;
    public float power;

    Vector3 direction;
    public float speed;
    public float rotateSpeed;

    public GameObject cameraObj;
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }
    void Start()
    {
        _inputActions.Player.Attack.performed += value =>
        {
            Debug.Log("Throw");
            var cake = Instantiate(cupCake,transform.position+ cameraObj.transform.forward,transform.rotation);
            var rigidbody = cake.GetComponent<Rigidbody>();
            rigidbody.AddForce(cameraObj.transform.forward*power, ForceMode.Impulse);
        };
        _inputActions.Player.Move.performed += value =>
        {
            Vector2 vec = value.ReadValue<Vector2>();
            Debug.Log(vec);
            direction = new Vector3(vec.x, 0, vec.y);
        };
        _inputActions.Player.Move.canceled += value => direction = Vector3.zero;
        
    }

    // Update is called once per frame
    void Update()
    {
        SetCamera();

        var mouseX = Input.GetAxis("Mouse X");
        var rot = transform.rotation.eulerAngles;
        rot.y += mouseX * rotateSpeed * 100 * Time.deltaTime;

        transform.rotation = Quaternion.Euler(rot);

        transform.Translate(direction.normalized * speed * Time.deltaTime);

    }

    void SetCamera()
    {
        var mouseY = Input.GetAxis("Mouse Y");
        var rot = cameraObj.transform.rotation.eulerAngles;
        rot.x -= mouseY * rotateSpeed * 100 * Time.deltaTime;
        cameraObj.transform.rotation = Quaternion.Euler(rot);
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
