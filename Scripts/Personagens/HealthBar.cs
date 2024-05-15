using Godot;
using System;

public abstract partial class HealthBar : Control
{
    [Export] protected ProgressBar HealthBarNode;
    public Character Enemy;
    protected float HealthMax;
    protected float Health;
    protected float HealthPecentage = 100;

    public ProgressBar GetProgressBar()
    {
        ProgressBar Retorno = FindChild("HealthControler") as ProgressBar;
        return Retorno;
    }
}