using Godot;
using System;

public partial class InimigosContainer : Node3D
{
    public override void _Ready()
    {
        int TotalEnemies = GetChildCount();
        GameEvents.RaiseNewEnemyCount(TotalEnemies);
    }

}
