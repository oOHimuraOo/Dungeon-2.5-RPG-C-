using Godot;
using System;

public partial class StatLabel : Label
{
    [Export] private StatResource StatResource;

    public override void _Ready()
    {
        StatResource.OnUpdate += HandleUpdate;
        Text = StatResource.StatValue.ToString();
    }

    private void HandleUpdate()
    {
        Text = StatResource.StatValue.ToString();
    }
}
