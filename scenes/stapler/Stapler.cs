using Godot;
using System;

public partial class Stapler : Node2D
{
    [Export]
    public float staplerTopRotation = 0.0f;

    [Export]
    public Player playerReference;

    [Export]
    public float stapleSpeed = 500.0f;


    private Sprite2D staplerTopSprite;

    private Timer firingTimer;

    private Timer startTimer;
    private bool canFire = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        staplerTopSprite = GetNode<Sprite2D>("StaplerTop");
        RotateStaplerTop(staplerTopRotation);

        firingTimer = GetNode<Timer>("FiringTimer");
        firingTimer.Timeout += FireStaple;

        startTimer = GetNode<Timer>("StartTimer");
        startTimer.Timeout += OnStartTimerTimeout;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (canFire && playerReference != null)
        {
            float targetAngle = CalculateAngle(playerReference.GlobalPosition);
            staplerTopRotation = Mathf.RadToDeg(
                Mathf.LerpAngle(
                    Mathf.DegToRad(staplerTopRotation),
                    Mathf.DegToRad(targetAngle),
                    (float)delta * 10.0f
                )
            );
            RotateStaplerTop(staplerTopRotation);
        }
    }

    public void RotateStaplerTop(float angle)
    {
        staplerTopRotation = angle;
        staplerTopSprite.RotationDegrees = staplerTopRotation;
    }

    public void FireStaple()
    {
        Staple newStaple = GD.Load<PackedScene>("res://scenes/stapler/staple.tscn")
            .Instantiate<Staple>();

        newStaple.Speed = stapleSpeed;

        GetParent().AddChild(newStaple);
        Vector2 offset = new Vector2(50, 0).Rotated(Mathf.DegToRad(staplerTopRotation));
        newStaple.GlobalPosition = staplerTopSprite.GlobalPosition + offset;
        newStaple.RotationDegrees = staplerTopRotation;
    }

    public float CalculateAngle(Vector2 point)
    {
        // Get the stapler's firing position after rotation
        Vector2 firePosition =
            staplerTopSprite.GlobalPosition
            + new Vector2(50, 0).Rotated(Mathf.DegToRad(staplerTopRotation));

        // Calculate direction vector from firing position to target
        Vector2 fireDirection = (point - firePosition).Normalized();

        // Get the correct angle and adjust for Godot's coordinate system
        float angle = Mathf.Atan2(fireDirection.Y, fireDirection.X);

        // Convert to degrees and adjust for stapler alignment
        return Mathf.RadToDeg(angle) - 90; // Subtracting 90 because we assume 0° is right-facing
    }

    public void OnStartTimerTimeout()
    {
        canFire = true;
        firingTimer.Start();
    }
}
