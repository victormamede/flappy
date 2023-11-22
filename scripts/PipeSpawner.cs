using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Flappy
{
    [Tool]
    public partial class PipeSpawner : Marker2D
    {
        [Export] public PackedScene Pipe;
        [Export] public Node PipesContainer;
        [Export] public int MaxPipes = 1;

        [Export(PropertyHint.Range, "0,500,or_greater")] public float MaxY { get => maxY; set { maxY = value; QueueRedraw(); } }

        private float maxY = 20f;
        private RandomNumberGenerator _random;
        private Timer _timer;
        private Queue<Node> _pipes = new();


        public override void _Ready()
        {
            if (Engine.IsEditorHint()) return;

            _random = new();
            _timer = GetNode<Timer>("Timer");
            _timer.Timeout += Spawn;
        }

        public void StartSpawning()
        {
            _timer.Start();
        }
        public void StopSpawning()
        {

            _timer.Stop();
        }

        public override void _Draw()
        {
            if (Engine.IsEditorHint())
            {
                DrawLine(new(-GizmoExtents, MaxY), new(GizmoExtents, MaxY), new(0, 1, 0));
            }
        }
        public void Spawn()
        {
            if (Engine.IsEditorHint()) return;

            while (_pipes.Count > MaxPipes)
            {
                var firstPipe = _pipes.Dequeue();
                firstPipe.QueueFree();
            }

            Node2D pipe = Pipe.Instantiate<Node2D>();
            pipe.Position = GlobalPosition + Vector2.Down * _random.Randf() * maxY;
            _pipes.Enqueue(pipe);
            PipesContainer.AddChild(pipe);

        }
    }
}