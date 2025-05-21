using UnityEngine;

public class AttackState : IState
{
    Enemy enemy;
    public void Enter(Enemy _enemy)
    {
        enemy = _enemy;
        enemy.transform.LookAt(enemy.targetTr);
        enemy.anim.SetBool("Attack",true);
    }

    public void Exit()
    {
        enemy.anim.SetBool("Attack", false);
    }

    public void Update()
    {
        
    }
}
