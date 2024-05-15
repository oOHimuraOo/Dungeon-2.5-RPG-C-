using System;
using Godot;

[GlobalClass]
public partial class RewardResouce : Resource
{
    [Export] public Texture2D SpriteTexture{ get; private set; }
    [Export] public String Description{ get; private set; }
    [Export] public Stat TargetStat{ get; private set; }
    [Export(PropertyHint.Range, "1,100,1")] public float amount { get; private set; }
}