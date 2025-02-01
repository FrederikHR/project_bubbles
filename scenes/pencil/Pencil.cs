using Godot;
using System;

public partial class Pencil : Obstacle
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();

        //Connect to Area2D's body_entered signal
        Area2D area2D = GetNode<Area2D>("Area2D");
        area2D.BodyEntered += OnArea2DBodyEntered;
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

    private void OnArea2DBodyEntered(Node body)
    {
        if (body.IsInGroup("player"))
        {
            Player player = body as Player;
            player.Die();
        }
    }
}
