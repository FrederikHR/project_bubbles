using Godot;
using System;

public partial class Spikes : CharacterBody2D
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
            if (Direction == "RIGHT")
            {
                velocity.X += speed;
            }
            else if (Direction == "LEFT")
            {
                velocity.X -= speed;
            }
            else if (Direction == "UP")
            {
                velocity.Y -= speed;
            }
            else if (Direction == "DOWN")
            {
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
