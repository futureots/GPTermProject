using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHitable
{

    public Transform targetTr;
    public NavMeshAgent agent;
    public Animator anim;
    public int hp;
    public int power;
    

    public IState state;

    private void Start()
    {
        //이동 상태
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(targetTr.position);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Debug.Log((transform.position - targetTr.position).magnitude);
    }
    public void Hit(int damage)
    {
        hp -= damage;
        if(hp < 0)
        {
            //사망 이벤트 발생

            Dead();
        }
        else
        {
            //피격 이벤트 발생
        }
    }


    public void Dead()
    {
        //사망 모션 출력
        agent.isStopped = true;

        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cake")
        {
            //도착 및 공격 시작
            agent.isStopped = true;
            Debug.Log("Cake!!!");
        }
    }
}
