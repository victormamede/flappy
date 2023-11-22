using Godot;
using System;

[Tool]
public partial class TreeBuilder : Node2D
{
    [Export] public Texture2D Texture;
    [Export] public int TopFrame = 0;
    [Export] public int FrameFrom = 0;
    [Export] public int FrameTo = 0;
    [Export] public int HFrames = 1;
    [Export] public int VFrames = 1;
    [Export] public int Copies = 7;
    [Export] public float VDirection = 1f;

    public override void _Ready()
    {
        Generate();
    }

    private void Generate()
    {
        RandomNumberGenerator random = new();

        int height = Texture.GetHeight();

        Sprite2D topSprite = new()
        {
            Texture = Texture,
            Hframes = HFrames,
            Vframes = VFrames,
            Frame = TopFrame
        };

        AddChild(topSprite);

        for (int i = 0; i < Copies; i++)
        {
            Sprite2D sprite = new()
            {
                Texture = Texture,
                Hframes = HFrames,
                Vframes = VFrames,
                Frame = random.RandiRange(FrameFrom, FrameTo),
                Position = new(0f, (i + 1) * height * VDirection),
            };

            AddChild(sprite);
        }
    }
}
