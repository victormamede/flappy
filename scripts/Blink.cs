using Godot;
using System;

public partial class Blink : Label
{
    [Export] public float Rate = 1f;
    private float _time = 0f;

    public override void _Process(double delta)
    {
        _time += (float)delta;

        var color = Modulate;
        color.A = (Mathf.Sin(_time * Rate) + 1f) / 2f;

        Modulate = color;
    }
}
