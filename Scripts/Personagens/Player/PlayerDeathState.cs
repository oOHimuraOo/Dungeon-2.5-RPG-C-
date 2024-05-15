using Godot;
using System;

public partial class PlayerDeathState : PlayerState
{
    protected override void enterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DYING);

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimFinished;
    }

    private void HandleAnimFinished(StringName animName)
    {
        characterNode.QueueFree();
    }
}
