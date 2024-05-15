using Godot;
using System;
using System.Linq;

public partial class EnemyAttackState : EnemyState
{
    [Export] private Timer Cooldown;
    private Vector3 TargetHitboxPosition;
    private Node3D target;
    
    protected override void enterState()
    {
        target = characterNode.AttackAreaNode.GetOverlappingBodies().First();
        TargetHitboxPosition = target.GlobalPosition;

        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimFinished;
        Cooldown.Timeout += HandleCooldown;
    }
    protected override void exitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimFinished;
        Cooldown.Timeout -= HandleCooldown;
    }

    private void HandleCooldown()
    {
        target = characterNode.AttackAreaNode.GetOverlappingBodies().FirstOrDefault();

        if (target == null)
        {
            Node3D ChaseTarget = characterNode.ChaseAreaNode.GetOverlappingBodies().FirstOrDefault();
            if (ChaseTarget != null)
            {
                characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
                return;
            }
            characterNode.StateMachineNode.SwitchState<EnemyIdleState>();
            return;
        }
        
        characterNode.SpriteNode.FlipH = characterNode.GlobalPosition.DirectionTo(target.GlobalPosition).X < 0;
        TargetHitboxPosition = target.GlobalPosition;
        
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK);
    }


    private void HandleAnimFinished(StringName animName)
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_IDLE);
        Cooldown.Start();
    }

    private void PerformHit()
    {
        characterNode.Hitbox.GlobalPosition = TargetHitboxPosition;
        characterNode.ToggleHitbox(false);
    }

    private void HideHit()
    {
        characterNode.Hitbox.Position = Vector3.Zero;
        characterNode.ToggleHitbox(true);
    }
}
