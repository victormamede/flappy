using Godot;
using System;

namespace Flappy
{
    public partial class PlayerSprite : Sprite2D
    {
        [Export] public float RotationSpeed = 50f;
        public float TargetRotation = 0f;

        public override void _Process(double delta)
        {
            Rotation += (TargetRotation - Rotation) * RotationSpeed * (float)delta;
        }
    }
}