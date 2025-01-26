using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Signal]
    public delegate void DiedEventHandler();

    [Export]
    public float Speed = 200.0f;

    [Export]
    public float GravityFactor = 0.5f;

    [Export]
    public float upSpeedFactor = 0.5f;

    private Sprite2D WindSprite;

    public override void _Ready()
    {
        WindSprite = GetNode<Sprite2D>("TempWindSprite");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GravityFactor * GetGravity() * (float)delta;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
        if (direction != Vector2.Zero)
        {
            velocity.X = direction.X * Speed;
            velocity.Y = direction.Y < 0 ? direction.Y * Speed * upSpeedFactor : velocity.Y;
            windMovement(direction);
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            windMovement(Vector2.Zero);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    private void windMovement(Vector2 windDirection)
    {
        //make sure windDirection.Y is negative or zero
        windDirection.Y = windDirection.Y > 0 ? 0 : windDirection.Y;

        //normalize the wind direction
        windDirection = windDirection.Normalized();

        //if windDirection is zero, hide the wind sprite.
        if (windDirection == Vector2.Zero)
        {
            WindSprite.Hide();
            return;
        }
        else
        {
            WindSprite.Show();

            // Move wind sprite offset by the wind direction.
            WindSprite.Position = -windDirection * 100;

            // Rotate the wind sprite to face the wind direction.
            WindSprite.Rotation = windDirection.Angle();
        }
    }

    public void Die()
    {
        EmitSignal(SignalName.Died);
        CallDeferred("queue_free");
    }
}
