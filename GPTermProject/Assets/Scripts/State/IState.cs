using UnityEngine;

public interface IState
{
    public void Enter(Enemy enemy);
    public void Update();
    public void Exit();
}
