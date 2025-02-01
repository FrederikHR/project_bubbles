using Godot;
using System;

public partial class Spikes : Obstacle
{
    private Area2D _area2D;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        _area2D = GetNode<Area2D>("Area2D");
        _area2D.BodyEntered += OnArea2DBodyEntered;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        move(delta);
    }

    private void _on_launch_timer_timeout()
    {
        GD.Print("Timer ended");
        Launch = true;
    }

    public void move(double delta)
    {
        Vector2 velocity = Velocity;
        if (Launch)
        {
            switch (direction)
            {
                case Direction.RIGHT:
                    velocity.X += speed * (float)delta;
                    break;
                case Direction.LEFT:
                    velocity.X -= speed * (float)delta;
                    break;
                case Direction.UP:
                    velocity.Y -= speed * (float)delta;
                    break;
                case Direction.DOWN:
                    velocity.Y += speed * (float)delta;
                    break;
            }
            Velocity = velocity;

            MoveAndSlide();
        }
    }

    private void OnArea2DBodyEntered(Node body)
    {
        if (body.IsInGroup("player"))
        {
            Player player = body as Player;
            player.Die();
        }
    }
}
