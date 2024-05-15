using Godot;
using System;

public abstract partial class Habilidades : Node3D
{
    [Export] protected AnimationPlayer AnimPlayerNode;
    [Export(PropertyHint.Range, "1,20,0.5")] public float Damage {get; private set;} = 10;

    public override void _Ready()
    {
        AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
    }

    protected virtual void HandleAnimationFinished(StringName animName)
    {
        return;
    }
}