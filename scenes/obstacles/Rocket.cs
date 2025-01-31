using Godot;
using System;

public partial class Rocket : Obstacle
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        move(delta);
    }
    public void _on_launch_timer_timeout(){
        Launch = true;
    }
    public override void move(){
                Vector2 velocity = Velocity;

        if (Launch) {
            velocity.Y -= 10;
        }
        Velocity = velocity;
                    MoveAndSlide();

    }
    
    
}
