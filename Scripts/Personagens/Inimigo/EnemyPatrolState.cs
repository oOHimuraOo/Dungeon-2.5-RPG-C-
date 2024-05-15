using Godot;
using System;

public partial class EnemyPatrolState : EnemyState
{
    [Export] private Timer IdleTimerNode;
    [Export(PropertyHint.Range, "0,20,0.1")] private float MaxIdleTime = 4;
    private int PointIndex;
    private Vector3 NextPosition;
    
    protected override void enterState()
    {
        PointIndex = 1;
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        
        Destination = GetPointGlobalPosition(PointIndex);
        characterNode.AgentNode.TargetPosition = Destination;

        characterNode.AgentNode.NavigationFinished += HandleNavigationFinished;
        IdleTimerNode.Timeout += HandleIdleTimerNode;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
    }
    protected override void exitState()
    {
        characterNode.AgentNode.NavigationFinished -= HandleNavigationFinished;
        IdleTimerNode.Timeout -= HandleIdleTimerNode;
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
    }
    public override void _PhysicsProcess(double delta)
    {
        if (!IdleTimerNode.IsStopped()){
            return;
        }

        RandomNumberGenerator rng = new();
        float speed = rng.RandfRange(1.0f, 2.0f);
        Move(speed);
    }

    private void HandleNavigationFinished()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
        
        RandomNumberGenerator rng = new();
        IdleTimerNode.WaitTime = rng.RandfRange(0, MaxIdleTime);

        IdleTimerNode.Start();

    }
    private void HandleIdleTimerNode()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        PointIndex = Mathf.Wrap(PointIndex + 1, 0, characterNode.PathNode.Curve.PointCount);

        Destination = GetPointGlobalPosition(PointIndex);
        characterNode.AgentNode.TargetPosition = Destination;        
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }
}
