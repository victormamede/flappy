using Godot;
using System;

namespace Flappy
{
    public partial class Player : CharacterBody2D, IScorer
    {
        [Signal] public delegate void StartedEventHandler();
        [Signal] public delegate void DiedEventHandler();
        [Signal] public delegate void PointsScoredEventHandler(int points);

        [Export] public float Gravity = 100f;
        [Export] public float JumpStrength = 100f;
        [Export] public float RotationAmount = 2f;
        [Export] public float Speed = 20f;
        [Export] public float SpeedInc = 0.1f;
        [Export] public PackedScene DeadBird;
        [Export] public PackedScene Explosion;

        private PlayerSprite _sprite;
        private AudioStreamPlayer _flapSound;
        private AudioStreamPlayer _scoreSound;
        private bool _shouldJump = false;
        private bool _isIdle = true;
        private Vector2 _initialPosition;

        public override void _Ready()
        {
            _sprite = GetNode<PlayerSprite>("Sprite");
            _flapSound = GetNode<AudioStreamPlayer>("FlapSound");
            _scoreSound = GetNode<AudioStreamPlayer>("ScoreSound");
            _initialPosition = Position;
        }

        public override void _UnhandledInput(InputEvent @event)
        {
            if (@event.IsActionPressed("jump"))
            {
                _shouldJump = true;

                if (_isIdle)
                {
                    EmitSignal(SignalName.Started);
                    _isIdle = false;
                }
            }
        }

        public override void _Process(double delta)
        {
            if (!_isIdle)
            {
                Speed += (float)delta * SpeedInc;
            }
            _sprite.TargetRotation = Mathf.Atan2(Velocity.Y * RotationAmount / 256f, 1f);
        }

        public override void _PhysicsProcess(double delta)
        {

            Vector2 velocity = Velocity;

            if (_isIdle)
            {
                velocity.Y = Mathf.Sin(Time.GetTicksMsec() / 400f) * 50f;
            }
            else
            {

                if (!IsOnFloor())
                {
                    velocity.Y += Gravity * (float)delta;
                }


                if (_shouldJump)
                {
                    velocity.Y = -JumpStrength;
                    _flapSound.Play();
                    _shouldJump = false;
                }

            }

            velocity.X = Speed;

            Velocity = velocity;
            bool collided = MoveAndSlide();

            if (collided)
            {
                for (int i = 0; i < GetSlideCollisionCount(); i++)
                {
                    KinematicCollision2D collision = GetSlideCollision(i);
                    if (collision.GetNormal().Dot(Vector2.Left) > 0.5)
                    {
                        var explosionEffect = Explosion.Instantiate<GpuParticles2D>();
                        explosionEffect.Position = collision.GetPosition();
                        GetParent().AddChild(explosionEffect);
                        explosionEffect.Emitting = true;

                        Die();
                        break;
                    }
                }
            }
        }
        public void Die()
        {
            var deadBirdNode = DeadBird.Instantiate<RigidBody2D>();
            deadBirdNode.Position = Position;
            deadBirdNode.Rotation = _sprite.Rotation;
            deadBirdNode.LinearVelocity = Velocity;
            Visible = false;

            GetParent().AddChild(deadBirdNode);
            GetParent().MoveChild(deadBirdNode, GetIndex());

            var camera2d = GetViewport().GetCamera2D();
            if (camera2d != null && camera2d is Camera camera)
            {
                camera.Shaking = true;
            }

            EmitSignal(SignalName.Died);

            QueueFree();
        }

        public void Score(int points)
        {
            _scoreSound.Play();
            EmitSignal(SignalName.PointsScored, points);
        }
    }

}