using Godot;
using System;

public partial class Spikes : Obstacle
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        move();
    }

    private void _on_launch_timer_timeout()
    {
        GD.Print("Timer ended");
        Launch = true;
    }

    public override void move()
    {
        Vector2 velocity = Velocity;
        if (Launch)
        {
            switch (direction) 
            {
                case Direction.RIGHT:
                    velocity.X += speed;
                    break;
                case Direction.LEFT:
                    velocity.X -= speed;
                    break;
                case Direction.UP:
                    velocity.Y -= speed;
                    break;
                case Direction.DOWN:
                    velocity.Y += speed;
                    break;
            }
            Velocity = velocity;

            MoveAndSlide();
        }
    }
}
