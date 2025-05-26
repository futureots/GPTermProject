using UnityEngine;

public class DeadState : IState
{
    public void Enter(Enemy _enemy)
    {

        _enemy.anim.SetTrigger("Die");
        
    }

    public void Exit()
    {
        
    }

    public void Update(Enemy _enemy)
    {
       
    }
}
