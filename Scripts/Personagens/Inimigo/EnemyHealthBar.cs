using Godot;
using System;
using System.Reflection.Metadata;

public partial class EnemyHealthBar : HealthBar
{
    public void LifeLoaded()
    {
        Enemy = GetOwner<Character>();
        HealthMax = Enemy.GetStatResource(Stat.Health).StatValue;
        Health = HealthMax;
        HealthBarNode.MaxValue = HealthMax;
        HealthBarNode.Value = Health;
    }
    public void HandleEnemyHitted()
    {
        Health = Enemy.GetStatResource(Stat.Health).StatValue;
        HealthBarNode.Value = Health;
    }
}
