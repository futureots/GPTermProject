using UnityEngine;

public class AttackState : IState
{
    Enemy enemy;
    public void Enter(Enemy _enemy)
    {
        enemy = _enemy;
        //enemy.transform.LookAt(enemy.targetTr);
        //enemy.rigidbody.isKinematic = true;
        enemy.anim.SetBool("Attack",true);
        
    }

    public void Exit()
    {
        enemy.anim.SetBool("Attack", false);
        //enemy.rigidbody.isKinematic = false;
    }

    public void Update(Enemy _enemy)
    {
        _enemy.transform.LookAt(_enemy.targetTr);
    }
}
