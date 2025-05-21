using UnityEngine;

public interface IState
{
    public void Enter(Enemy _enemy);
    public void Update();
    public void Exit();
}
