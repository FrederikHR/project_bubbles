using Godot;
using System;

public partial class Pencil : CharacterBody2D
{
    [Export]
    private string Direction;

    [Export]
    private float WaitTime;

    [Export]
    private bool Launch = false;

    private int speed = 10;
    private Timer timer;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //spikes = GetNode<Sprite2D>("./Spikes");
        timer = GetNode<Timer>("./LaunchTimer");
        timer.WaitTime = WaitTime;
        timer.Start();
        GD.Print("WaitTime: ", timer.WaitTime);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        Vector2 velocity = Velocity;
        if (Launch)
        {
            if (Direction == "RIGHT_UP_DIAGONAL")
            {
                velocity.X += speed;
                velocity.Y -= speed;
            }
            else if (Direction == "RIGHT_DOWN_DIAGONAL")
            {
                velocity.X -= speed;
                velocity.Y += speed;
            }
            else if (Direction == "LEFT_UP_DIAGONAL")
            {
                velocity.X -= speed;
                velocity.Y -= speed;
                
            }
            else if (Direction == "LEFT_DOWN_DIAGONAL")
            {
                velocity.X += speed;
                velocity.Y += speed;
            }
            ;
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
