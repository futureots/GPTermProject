using UnityEngine;

public interface IState
{
    public void Enter(Enemy _enemy);
    public void Update(Enemy _enemy);
    public void Exit();
}
