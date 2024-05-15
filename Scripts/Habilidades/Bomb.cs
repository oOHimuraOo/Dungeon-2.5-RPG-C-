using Godot;
using System;

public partial class Bomb : Habilidades
{  
    protected override void HandleAnimationFinished(StringName animName)
    {
        if (animName == GameConstants.ANIM_EXPAND)
        {
            AnimPlayerNode.Play(GameConstants.ANIM_EXPLOSION);
        }
        else if (animName == GameConstants.ANIM_EXPLOSION)
        {
            this.QueueFree();
        }
    }

}
