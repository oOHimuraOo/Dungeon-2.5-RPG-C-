using Godot;
using System;
using System.Reflection.Metadata;

public partial class PlayerDashState : PlayerState
{
    [Export] private Timer DashTimerNode;
    [Export] private Timer CooldownTimer;
    [Export] private PackedScene BombScene;
    [Export(PropertyHint.Range, "0,20,0.1")] private float Speed = 10;
    public override void _Ready()
    {
        base._Ready();
        CanTrasition = () => CooldownTimer.IsStopped();
    }
    protected override void enterState()
    {
        DashTimerNode.Timeout += HandleDashTimeout;

        characterNode.AnimPlayerNode.Play(GameConstants.ANIM_DASH);
        characterNode.Velocity = new(characterNode.direction.X, 0, characterNode.direction.Y);

        if (characterNode.Velocity == Vector3.Zero){
            characterNode.Velocity = characterNode.SpriteNode.FlipH ? Vector3.Left : Vector3.Right;
        }

        characterNode.Velocity *= Speed;
        DashTimerNode.Start();

        Node3D bomb = BombScene.Instantiate<Node3D>();
        GetTree().CurrentScene.AddChild(bomb);
        bomb.GlobalPosition = characterNode.GlobalPosition;
    }

    protected override void exitState()
    {
        DashTimerNode.Timeout -= HandleDashTimeout;

    }

    public override void _PhysicsProcess(double delta)
    {
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }

    private void HandleDashTimeout(){
        CooldownTimer.Start();

        characterNode.Velocity = Vector3.Zero;
        characterNode.StateMachineNode.SwitchState<PlayerIdleState>();
    }
}
