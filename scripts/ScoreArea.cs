using Flappy;
using Godot;
using System;

public partial class ScoreArea : Area2D
{
    [Export] public int Points = 1;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is IScorer scorer)
        {
            scorer.Score(Points);
        }
    }
}
