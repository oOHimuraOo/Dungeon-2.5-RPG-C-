using Godot;
using System;
using System.Linq;

public partial class EnemyChaseState : EnemyState
{
    [Export] private Timer TimerChaseCalculation;
    private CharacterBody3D Target;
    protected override void enterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
        Target = characterNode.ChaseAreaNode.GetOverlappingBodies().First() as CharacterBody3D;
        TimerChaseCalculation.Start();
        
        characterNode.ChaseAreaNode.BodyExited += HandleChaseAreaBodyExited;
        TimerChaseCalculation.Timeout += HandleTimerChaseCalculation;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
    }
    protected override void exitState()
    {
        TimerChaseCalculation.Stop();
        characterNode.ChaseAreaNode.BodyExited -= HandleChaseAreaBodyExited;
        TimerChaseCalculation.Timeout -= HandleTimerChaseCalculation;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
    }

    protected private void HandleChaseAreaBodyExited(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
    }

    public override void _PhysicsProcess(double delta)
    {
        RandomNumberGenerator rng = new();
        float speed = 3.0f;
        Move(speed);
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }
    private void HandleTimerChaseCalculation()
    {
        Destination = Target.GlobalPosition;
        characterNode.AgentNode.TargetPosition = Destination;
    }
}
