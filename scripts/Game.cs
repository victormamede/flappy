using Flappy;
using Godot;
using System;

public partial class Game : Node2D
{
    private bool _hasEnded = false;
    private Player _player;
    private PipeSpawner _spawner;
    private Menu _menu;
    private int _score = 0;

    public override void _Ready()
    {
        _player = GetNode<Player>("Player");
        _spawner = GetNode<PipeSpawner>("XFollow/PipeSpawner");
        _menu = GetNode<Menu>("Menu");

        var bgMusic = GetNode<AudioStreamPlayer>("/root/BgMusic");

        _player.Died += () =>
        {
            bgMusic.Stop();
            _menu.ShowEnd();
            _spawner.StopSpawning();
            _hasEnded = true;
        };
        _player.Started += () =>
        {
            _spawner.StartSpawning();
            _menu.HideStart();
        };
        _player.PointsScored += (int points) =>
        {
            _score += points;
            _menu.SetScore(_score);
        };

        bgMusic.Play();
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (_hasEnded && @event.IsActionPressed("jump"))
        {
            GetTree().ReloadCurrentScene();
        }
    }
}
