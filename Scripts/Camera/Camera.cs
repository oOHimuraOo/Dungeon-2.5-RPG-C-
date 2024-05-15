using Godot;
using System;

public partial class Camera : Godot.Camera3D
{
    [Export] private Node Target;
    [Export] private Vector3 PositionFromTarget;
    public override void _Ready()
    {

        GameEvents.OnStartGame += HandleStartGame;
        GameEvents.OnEndGame += HandleEndGame;
    }

    private void HandleEndGame()
    {
        Reparent(GetTree().CurrentScene);
    }


    private void HandleStartGame()
    {
        Reparent(Target);
        Position = PositionFromTarget;
    }
}
