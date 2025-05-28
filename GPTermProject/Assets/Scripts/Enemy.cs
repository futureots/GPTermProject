using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitable
{

    public Transform targetTr;
    public NavMeshAgent agent;
    public Animator anim;
    public int hp;
    int curhp;
    public int power;
    public int enemyNum;
    new public Collider collider;
    public Collider punch;
    new public Rigidbody rigidbody;

    public IState state;
    public void SetState(IState state)
    {
        this.state?.Exit();
        this.state = state;
        state.Enter(this);
    }

    private void Awake()
    {
        //이동 상태
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        
    }

    public void Init()
    {
        curhp = hp;
        gameObject.SetActive(true);
        agent.isStopped = false;
        collider.enabled = true;
        SetState(new MoveState());
        if (agent.hasPath)
            agent?.ResetPath();
        agent.SetDestination(targetTr.position);
        StartCoroutine(SetRigidbody());
    }

    
    private void Update()
    {
        var distance = (transform.position - targetTr.position).magnitude;
        //Debug.Log(distance);
        
        
        
    }
    IEnumerator SetRigidbody()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            rigidbody.linearVelocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
    public void Hit(int damage)
    {
        curhp -= damage;
        Debug.Log("Hit : "+ curhp);
        if(curhp <= 0)
        {
            //사망 이벤트 발생
            collider.enabled = false;
            Dead();
        }
        else
        {
            anim.SetTrigger("Hit");
            //피격 이벤트 발생
        }
    }


    public void Dead()
    {
        
        StopAllCoroutines();
        agent.isStopped = true;
        

        GameManager.Instance.point += hp;

        GameManager.Instance.enemyPool[enemyNum].Release(this);
        // 사망 모션 출력
        SetState(new DeadState());
        StartCoroutine(SetDisable());
        
    }
    IEnumerator SetDisable()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cake")
        {
            //도착 및 공격 시작
            SetState(new AttackState());
            
            //Debug.Log("Cake!!!");
        }
    }

}
