using Godot;
using System;

public partial class Staple : Node2D
{
    public float Speed { get; set; } = 500.0f;

    public Stapler Stapler;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //Connect to Area2D's body_entered signal
        Area2D area2D = GetNode<Area2D>("Area2D");
        area2D.BodyEntered += OnArea2DBodyEntered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector2 velocity = new Vector2(0, Speed).Rotated(Mathf.DegToRad(RotationDegrees));
        GlobalPosition += velocity * (float)delta;

        if (GlobalPosition.X > GetViewportRect().Size.X)
        {
            QueueFree();
        }
    }

    private void OnArea2DBodyEntered(Node body)
    {
        //CHeck if body in group "player"
        if (body.IsInGroup("player"))
        {
            var player = body as Player;
            GD.Print("Player hit by staple");
            player.Die();
            Stapler.playerReference = null;
        }

        QueueFree();
    }
}
