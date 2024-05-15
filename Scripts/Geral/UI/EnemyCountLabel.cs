using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemyCountLabel : Label
{
    public int EnemyCount;
    public override void _Ready()
    {
        GameEvents.OnNewEnemyCount += HandleNewEnemyCount;
        GameEvents.OnEnemyDied += HandleEnemyDied;
    }

    private void HandleEnemyDied()
    {
        EnemyCount -= 1;
        Text = EnemyCount.ToString();
        if (EnemyCount == 0)
        {
            GameEvents.RaisePlayerVictory();
        }
    }

    private void HandleNewEnemyCount(int obj)
    {
        EnemyCount = obj;
        Text = EnemyCount.ToString();
    }



}
