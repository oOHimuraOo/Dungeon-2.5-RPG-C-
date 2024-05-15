using Godot;
using System;
using System.Runtime.CompilerServices;

public partial class PlayerMoveState : PlayerState
{
    
    [Export(PropertyHint.Range, "0,10,0.1")]private float MaximunSpeed = 5;
    public override void _PhysicsProcess(double delta)
    {
        if (characterNode.direction == Vector2.Zero){
            characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
            return;
        }

        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);
        characterNode.Velocity *= MaximunSpeed;

        characterNode.MoveAndSlide();

        characterNode.Flip();
    }

    protected override void enterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_MOVE);
    }

    public override void _Input(InputEvent @event)
    {
        if (Input.IsActionJustPressed(GameConstants.INPUT_DASH)) {
            characterNode.StateMachineNode.SwitchState<PlayerDashState>();
        }
        if (Input.IsActionJustPressed(GameConstants.INPUT_ATTACK))
        {
            characterNode.StateMachineNode.SwitchState<PlayerAttackState>();
        }
    }
}

