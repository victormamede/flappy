using Godot;
using System;

namespace Flappy
{
    public partial class Camera : Camera2D
    {
        [Export] public bool Shaking;
        [Export(PropertyHint.Range, "0,5,or_greater")] public float ShakeTime = 1f;

        private float shakeTimeCounter = 0f;
        private RandomNumberGenerator _random = new();

        public override void _Process(double delta)
        {
            if (Shaking)
            {
                Offset = new(
                    _random.RandfRange(-5, 5f),
                    0f
                );
                shakeTimeCounter += (float)delta;
                if (shakeTimeCounter >= ShakeTime) Shaking = false;
            }
            else
            {
                Offset = new();
                shakeTimeCounter = 0f;
            }
        }
    }
}