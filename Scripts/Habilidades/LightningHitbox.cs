using Godot;
using System;

public partial class LightningHitbox : Area3D, IHitbox
{
    public bool CanStun()
    {
        return true;
    }

    public float GetDamage() => GetOwner<Lightning>().Damage;
}
