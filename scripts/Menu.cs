using Godot;
using System;

namespace Flappy
{
    public partial class Menu : CanvasLayer
    {

        public void HideStart()
        {
            Label startLabel = GetNode<Label>("%Start");
            Label pointsLabel = GetNode<Label>("%Points");
            startLabel.Visible = false;
            pointsLabel.Visible = true;
        }

        public void ShowEnd()
        {
            Label restartLabel = GetNode<Label>("%Restart");
            restartLabel.Visible = true;

        }

        internal void SetScore(int score)
        {
            Label pointsLabel = GetNode<Label>("%Points");
            pointsLabel.Text = score.ToString();
        }
    }

}