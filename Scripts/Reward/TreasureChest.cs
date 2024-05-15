using Godot;
using System;

public partial class TreasureChest : StaticBody3D
{
    [Export] private Area3D DetectionAreaNode;
    [Export] private Sprite3D IconNode;
    [Export] private RewardResouce Reward;
    public override void _Ready()
    {
        DetectionAreaNode.BodyEntered += (body) => IconNode.Visible = true;
        DetectionAreaNode.BodyExited += (body) => IconNode.Visible = false;
    }

    public override void _Input(InputEvent @event)
    {
        if (!DetectionAreaNode.Monitoring || !DetectionAreaNode.HasOverlappingBodies() || !Input.IsActionJustPressed(GameConstants.INPUT_INTERACT))
        {
            return;
        }
        
        DetectionAreaNode.Monitoring = false;

        GameEvents.RaiseReward(Reward);
    }
}
