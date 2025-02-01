using Godot;
using System;

public partial class Rocket : Obstacle
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        var area2D = GetNode<Area2D>("Area2D");
        area2D.BodyEntered += OnArea2DBodyEntered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        move(delta);
        move(delta);
    }

    public void _on_launch_timer_timeout()
    {
        Launch = true;
    }

    public void move(double delta)
    {
        Vector2 velocity = Velocity;

        if (Launch)
        {
            velocity.Y -= 100 * (float)delta;
        }
        Velocity = velocity;
        MoveAndSlide();
    }

    private void OnArea2DBodyEntered(Node body)
    {
        if (body.IsInGroup("player"))
        {
            Player player = (Player)body;
            player.Die();
        }
    }
}
