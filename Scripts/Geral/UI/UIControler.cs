using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
public partial class UIControler : Control
{
    private Dictionary<ContainerType, UIContainer> containers;
    private bool CanPause = false;

    public override void _Ready()
    {
        containers = GetChildren().Where((element) => element is UIContainer).Cast<UIContainer>().ToDictionary((element) => element.container);

        containers[ContainerType.Start].Visible = true;

        containers[ContainerType.Start].TextureButtonNode.Pressed += HandleStartPressed;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;
        containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardPressed;
        
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnPlayerVictory += HandlePlayerVictory;
        GameEvents.OnReward += HandleReward;
        
    }
    public override void _Input(InputEvent @event)
    {
        if (!CanPause)
        {
            return;
        }

        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }
        
        containers[ContainerType.Stats].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandlePausePressed()
    {
        CanPause = true;
        containers[ContainerType.Pause].Visible = false;
        GetTree().Paused = false;
        containers[ContainerType.Stats].Visible = true;
    }
    private void HandlePlayerVictory()
    {
        CanPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
        GetTree().CreateTimer(5).Timeout += () => GetTree().ReloadCurrentScene();
    }


    private void HandleEndGame()
    {
        CanPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
        GetTree().CreateTimer(5).Timeout += () => GetTree().ReloadCurrentScene();
    }


    private void HandleStartPressed()
    {
        CanPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        GameEvents.RaiseStartGame();
    }

    private void HandleReward(RewardResouce resouce)
    {
        CanPause = false;
        GetTree().Paused = true;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Reward].Visible = true;

        containers[ContainerType.Reward].TextureNode.Texture = resouce.SpriteTexture;
        containers[ContainerType.Reward].LabelNode.Text = resouce.Description;
    }
    private void HandleRewardPressed()
    {
        CanPause = true;
        GetTree().Paused = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Reward].Visible = false;
    }
}
