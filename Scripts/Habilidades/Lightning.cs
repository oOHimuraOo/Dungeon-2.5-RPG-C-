using Godot;
using System;

public partial class Lightning : Habilidades
{
    protected override void HandleAnimationFinished(StringName animName)
    {
        this.QueueFree();
    }
}
