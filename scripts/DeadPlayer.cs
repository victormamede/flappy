using Godot;
using System;

public partial class DeadPlayer : RigidBody2D
{
    [Export] public float EffectVelocity = 100f;
    [Export] public float RotateVelocity = 30f;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        RandomNumberGenerator random = new();

        var velocity = Vector2.Right.Rotated(-random.Randf() * Mathf.Pi) * EffectVelocity;
        LinearVelocity += velocity;
        var angVelocity = (random.Randf() - 0.5f) * RotateVelocity;
        AngularVelocity += angVelocity;
    }
}
