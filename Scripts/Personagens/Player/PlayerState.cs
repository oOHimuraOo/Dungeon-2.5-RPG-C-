using System;
using Godot;

public abstract partial class PlayerState : CharacterState
{
    public override void _Ready()
    {
        base._Ready();

        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    private void HandleZeroHealth()
    {
        GameEvents.RaiseEndGame();
        characterNode.StateMachineNode.SwitchState<PlayerDeathState>();
    }
}