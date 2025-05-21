using UnityEditor;

using UnityEngine;

using static UnityEngine.GraphicsBuffer;

public class MoveState : IState
{
    Enemy enemy;
    public void Enter(Enemy _enemy)
    {
        enemy = _enemy;
        enemy.anim.SetBool("Move", true);
        enemy.agent.SetDestination(_enemy.targetTr.position);
    }

    public void Exit()
    {
        enemy.anim.SetBool("Move", false);
        enemy.agent.isStopped = true;
    }

    public void Update()
    {
        
    }
}
