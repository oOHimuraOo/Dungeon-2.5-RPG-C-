using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemyStunState : EnemyState
{
    protected override void enterState()
    {
        base.enterState();
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_STUN);
        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimFinished;
    }

    protected override void exitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimFinished;
    }

    private void HandleAnimFinished(StringName animName)
    {
        if (characterNode.AttackAreaNode.HasOverlappingBodies())
        {
            characterNode.StateMachineNode.SwitchState<EnemyAttackState>();
        }
        else if (characterNode.ChaseAreaNode.HasOverlappingBodies())
        {
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }
        else 
        {
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
        }
        
    }

}
