using Godot;
using System;

public partial class XFollow : Node2D
{
    [Export] public Node2D Target;

    private float _delta;

    public override void _Ready()
    {
        _delta = Position.X - Target.GlobalPosition.X;
        Target.TreeExited += () => Target = null;
    }
    public override void _Process(double delta)
    {
        if (Target != null)
        {
            Position = new Vector2(Target.GlobalPosition.X + _delta, Position.Y);
        }
    }
}
