using Godot;
using System;

public partial class PlayerIdleState : PlayerState
{
    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction != Vector2.Zero){
            characterNode.StateMachineNode.SwitchState<PlayerMoveState>();
        }
    }

    protected override void enterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) 
        {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}
