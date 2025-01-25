using Godot;
using System;

public partial class Obstacle : CharacterBody2D
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
        timer = GetNode<Timer>("./LaunchTimer");
        timer.WaitTime = WaitTime;
        timer.Start();
        GD.Print("WaitTime: ", timer.WaitTime);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public virtual void move() { }
}
