using Godot;
using System;

public partial class PlayerAttackState : PlayerState
{
    [Export] private Timer ResetCounter;
    [Export] private PackedScene LightningScene;
    private int ComboCounter = 1;
    private int MaxCombo = 2;
    public override void _Ready()
    {
        base._Ready();

        ResetCounter.Timeout += () => ComboCounter = 1;
    }
    protected override void enterState()
    {
        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_ATTACK + ComboCounter, -1, 2.5f);

        characterNode.AnimPlayerNode.AnimationFinished += HandleAnimationFinished;
        characterNode.Hitbox.BodyEntered += HandleBodyEntered;
    }
    protected override void exitState()
    {
        characterNode.AnimPlayerNode.AnimationFinished -= HandleAnimationFinished;
        ResetCounter.Start();
        characterNode.Hitbox.BodyEntered -= HandleBodyEntered;
    }

    private void HandleBodyEntered(Node3D body)
    {
        if (ComboCounter != MaxCombo)
        {
            return;
        }

        Node3D Lightning = LightningScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(Lightning);
        Lightning.GlobalPosition = body.GlobalPosition;
    }


    private void HandleAnimationFinished(StringName animName)
    {
        ComboCounter = Mathf.Wrap(ComboCounter + 1, 1, 3);
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }

    private void PerformHit()
    {
        Vector3 NewPosition = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        float DistanceMultiplayer = 0.75f;
        characterNode.Hitbox.Position = NewPosition * DistanceMultiplayer;
        characterNode.ToggleHitbox(false);
    }

    private void HideHit()
    {
        characterNode.Hitbox.Position = Vector3.Zero;
        characterNode.ToggleHitbox(true);
    }
}
