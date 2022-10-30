using UnityEngine;

public interface IAnimation
{
    public void Initialize(Animator animator);
    public void Idle();
    public void Run();
    public void Attack();
    public void Aggressive();
    public void Neutral();
    public void Taking();
    public void Die();
}
