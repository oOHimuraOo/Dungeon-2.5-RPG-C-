using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemyDeathState : EnemyState
{
    protected override void enterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DYING);

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimFinished;
    }
    private void HandleAnimFinished(StringName animName)
    {
        characterNode.QueueFree();
        GameEvents.RaiseEnemyDied();
    }
}
