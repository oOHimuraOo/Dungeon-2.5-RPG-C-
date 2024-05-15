using System;
using Godot;

public abstract partial class EnemyState : CharacterState
{
    protected Vector3 Destination;
    public override void _Ready()
    {
        base._Ready();
        characterNode.GetStatResource(Stat.Health).OnZero += HandleZeroHealth;
    }

    protected Vector3 GetPointGlobalPosition(int index) 
    {
        Vector3 LocalPos = characterNode.PathNode.Curve.GetPointPosition(index);
        Vector3 GlobalPos = characterNode.PathNode.GlobalPosition;
        return LocalPos + GlobalPos;
    }

    protected void Move(float speed = 1.0f)
    {
        characterNode.AgentNode.GetNextPathPosition();
        characterNode.Velocity = characterNode.GlobalPosition.DirectionTo(Destination) * speed;
        characterNode.MoveAndSlide();
        characterNode.Flip();
    }
    protected void HandleChaseAreaBodyEntered(Node3D Body)
    {
        if (Body is Player){
            characterNode.StateMachineNode.SwitchState<EnemyChaseState>();
        }
    }
    private void HandleZeroHealth()
    {
        characterNode.StateMachineNode.SwitchState<EnemyDeathState>();
    }
}