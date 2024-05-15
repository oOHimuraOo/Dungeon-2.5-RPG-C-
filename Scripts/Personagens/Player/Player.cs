using Godot;
using System;

public partial class Player : Character
{
    public override void _Ready()
    {
        base._Ready();
        GameEvents.OnReward += HandleRewards;
    }

    public override void _Input(InputEvent @event)
    {
        direction = Input.GetVector(
            GameConstants.INPUT_LEFT, GameConstants.INPUT_RIGHT, GameConstants.INPUT_UP, GameConstants.INPUT_DOWN
        );
    }

    private void HandleRewards(RewardResouce resouce)
    {
        StatResource TargetStat = GetStatResource(resouce.TargetStat);
        TargetStat.StatValue += resouce.amount;
    }
}
