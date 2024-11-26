using Godot;
using System;

public partial class Inimigo : Character
{
	[Export] private EnemyHealthBar LifeBarNode; 
	public override void _Ready()
	{
		base._Ready();
		LifeBarNode.LifeLoaded();
		Hurtbox.AreaEntered += HandleAreaEntered;
	}

	protected override void HandleHurtBoxAreaEntered(Area3D area)
	{
		base.HandleHurtBoxAreaEntered(area);
		LifeBarNode.HandleEnemyHitted();
	}


	private void HandleAreaEntered(Area3D area)
	{
		if (area is not IHitbox hitbox)
		{
			return;
		}
		if (hitbox.CanStun() && GetStatResource(Stat.Health).StatValue != 0)
		{
			StateMachineNode.SwitchState<EnemyStunState>();
		}
	}
}
