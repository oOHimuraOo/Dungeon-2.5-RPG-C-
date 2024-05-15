using Godot;
using System;
using System.Linq;

public partial class EnemyIdleState : EnemyState
{
    protected override void enterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);

        characterNode.ChaseAreaNode.BodyEntered += HandleChaseAreaBodyEntered;
    }

    protected override void exitState()
    {
        characterNode.ChaseAreaNode.BodyEntered -= HandleChaseAreaBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
    {
        Node3D target = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
        
        if (characterNode.Scale.X != 1.0f){
            return;
        }

        if (target != null)
        {
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }

        characterNode.StateMachineNode.SwitchState<EnemyReturnState>();

        
    }
}
