using Godot;
using System;

public partial class Obstacle : CharacterBody2D
{
    [Export]
    protected float WaitTime;

    [Export]
    protected bool Launch = false;

    protected int speed = 10;
    protected Timer timer;

    protected enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        RIGHT_UP_DIAGONAL,
        LEFT_UP_DIAGONAL,
        RIGHT_DOWN_DIAGONAL,
        LEFT_DOWN_DIAGONAL
    }

    [Export]
    protected Direction direction { get; set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = GetNode<Timer>("LaunchTimer");
        timer.WaitTime = WaitTime;
        timer.Start();
        GD.Print("WaitTime: ", timer.WaitTime);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    public virtual void move() { }
}
