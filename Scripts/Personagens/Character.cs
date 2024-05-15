using System;
using Godot;
using System.Linq;

public abstract partial class Character : CharacterBody3D
{
    [ExportCategory("Required Nodes")]
    [ExportGroup("Functional Systems")]
    [Export] public AnimationPlayer AnimPlayerNode { get; private set; }
    [Export] public Sprite3D SpriteNode{ get; private set; }
    [Export] public StateMachine StateMachineNode{ get; private set; }
    [Export] private StatResource[] Stats;
    [Export] private Timer HurtCooldown;
    
    [ExportGroup("Attack System")]
    [Export] public Area3D Hitbox{ get; private set; }
    [Export] public CollisionShape3D HitboxColisionShapeNode{ get; private set; }
    [Export] public Area3D Hurtbox{ get; private set; }
    [Export] public CollisionShape3D HurtboxColisionShapeNode{ get; private set; }

    [ExportCategory("IA Nodes (NPC ONLY)")]
    [Export] public Path3D PathNode{ get; private set; }
    [Export] public NavigationAgent3D AgentNode{ get; private set; }
    [Export] public Area3D ChaseAreaNode{ get; private set; }
    [Export] public Area3D AttackAreaNode{ get; private set; }
    

    public Vector2 direction = new();
    public ShaderMaterial shader;
    public override void _Ready()
    {
        shader = (ShaderMaterial)SpriteNode.MaterialOverlay;
        Hurtbox.AreaEntered += HandleHurtBoxAreaEntered;
        SpriteNode.TextureChanged += handleTextureChange;
        HurtCooldown.Timeout += HandleHurtCooldownTimeout;

    }

    private void handleTextureChange()
    {
        shader.SetShaderParameter("tex", SpriteNode.Texture);
    }

    protected virtual void HandleHurtBoxAreaEntered(Area3D area)
    {
        if (area is not IHitbox hitbox)
        {
            return;
        }

        StatResource Health = GetStatResource(Stat.Health);

        float damage = hitbox.GetDamage();
        Health.StatValue -= damage;
        shader.SetShaderParameter("active", true);
        HurtCooldown.Start();
        GameEvents.RaiseEnemyHitted();
    }

    public StatResource GetStatResource(Stat stat)
    {
        return Stats.Where((element) => stat == element.StatType).FirstOrDefault();
    }

    public void Flip(){
        bool isNotMovingHorizontally = Velocity.X == 0;

        if (isNotMovingHorizontally){
            return;
        }

        bool isMovingLeft = Velocity.X < 0;
        SpriteNode.FlipH = isMovingLeft;
    }

    public void ToggleHitbox(bool Flag)
    {
        HitboxColisionShapeNode.Disabled = Flag;
    }
    private void HandleHurtCooldownTimeout()
    {
        shader.SetShaderParameter("active", false);
    }
}