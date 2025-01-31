using Godot;
using System;

public partial class Pencil : Obstacle
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector2 velocity = Velocity;
        if (Launch)
        {
            switch (direction)
            {
                case Direction.RIGHT_UP_DIAGONAL:

                    velocity.X += speed * (float)delta;
                    velocity.Y -= speed * (float)delta;
                    break;

                case Direction.RIGHT_DOWN_DIAGONAL:

                    velocity.X -= speed * (float)delta;
                    velocity.Y += speed * (float)delta;
                    break;

                case Direction.LEFT_UP_DIAGONAL:

                    velocity.X -= speed * (float)delta;
                    velocity.Y -= speed * (float)delta;
                    break;

                case Direction.LEFT_DOWN_DIAGONAL:

                    velocity.X += speed * (float)delta;
                    velocity.Y += speed * (float)delta;
                    break;
            }
            Velocity = velocity;

            MoveAndSlide();
        }
    }

    private void _on_launch_timer_timeout()
    {
        GD.Print("Timer ended");
        Launch = true;
    }
}
