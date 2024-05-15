using System;
using Godot;

public abstract partial class CharacterState : Node
{
    protected Character characterNode;
    public Func<bool> CanTrasition = () => true;
    public override void _Ready()
    {
        characterNode = GetOwner<Character>();
        SetProcess(false);
        SetPhysicsProcess(false);
        SetProcessInput(false);
    }

    public override void _Notification(int what)
    {
        base._Notification(what);

        if (what == GameConstants.NOTIFICATION_ENTER_STATE){
            enterState();

            SetProcess(true);
            SetPhysicsProcess(true);
            SetProcessInput(true);
        }
        else if (what == GameConstants.NOTIFICATION_EXIT_STATE){
            SetProcess(false);
            SetPhysicsProcess(false);
            SetProcessInput(false);
            
            exitState();
        }
    }

    protected virtual void enterState(){}

    protected virtual void exitState(){}
}