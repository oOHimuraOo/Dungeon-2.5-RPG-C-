using Godot;
using System;
using System.Linq;

public partial class StateMachine : Node
{
    [ExportGroup("STATE MACHINE")]
    [Export] public Node CurrentState;
    [Export] private CharacterState[] states;

    public override void _Ready()
    {
        CurrentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }

    public void SwitchState<T>(){
        CharacterState NewState = states.Where((state) => state is T).FirstOrDefault();

        if (NewState == null)
        {
            return;
        }

        if (CurrentState is T)
        {
            return;
        }

        if (!NewState.CanTrasition())
        {
            return;
        }

        CurrentState.Notification(GameConstants.NOTIFICATION_EXIT_STATE);
        CurrentState = NewState;
        CurrentState.Notification(GameConstants.NOTIFICATION_ENTER_STATE);
    }
}
