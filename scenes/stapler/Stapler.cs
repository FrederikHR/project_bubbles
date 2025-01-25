using Godot;
using System;

public partial class Stapler : Node2D
{
    [Export]
    public float staplerTopRotation = 0.0f;

    [Export(PropertyHint.Enum, "0,90,180,270")]
    public int testEnum = 0;


    private Sprite2D staplerTopSprite;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        staplerTopSprite = GetNode<Sprite2D>("StaplerTop");
        RotateStaplerTop(staplerTopRotation);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            FireStaple();
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

        newStaple.Speed = 500;

        GetParent().AddChild(newStaple);
        Vector2 offset = new Vector2(50, 5).Rotated(Mathf.DegToRad(staplerTopRotation));
        newStaple.GlobalPosition = staplerTopSprite.GlobalPosition + offset;
        newStaple.RotationDegrees = staplerTopRotation;
    }
}
