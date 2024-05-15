using Godot;
using System;

public partial class EnemyReturnState : EnemyState
{
    public override void _Ready()
    {
        base._Ready();

        Destination = GetPointGlobalPosition(0);
    }
    protected override void enterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);

        characterNode.AgentNode.TargetPosition = Destination;
        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
        characterNode.AttackAreaNode.BodyEntered += HandleAttackAreaBodyEntered;
    }

    protected override void exitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
        characterNode.AttackAreaNode.BodyEntered -= HandleAttackAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.AgentNode.IsNavigationFinished()){
            characterNode.StateMachineNode.SwitchState<EnemyPatrolState>();
            return;
        }

        RandomNumberGenerator rng = new();
        float speed = rng.RandfRange(1.0f, 2.0f);
        Move(speed);
    }

    private void HandleAttackAreaBodyEntered(Node3D body)
    {
        characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
    }
}
