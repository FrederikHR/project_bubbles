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

    private Sprite2D _sprite;
    private AnimatedSprite2D _animatedSprite;

    public override void _Ready()
    {
        WindSprite = GetNode<Sprite2D>("TempWindSprite");

        _sprite = GetNode<Sprite2D>("Sprite");
        _animatedSprite = GetNode<AnimatedSprite2D>("Animations");
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
            WindMovement(direction);
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            WindMovement(Vector2.Zero);
        }

        Velocity = velocity;
        MoveAndSlide();

        // Check for collisions with walls
        CollisionHandling();
    }

    private void WindMovement(Vector2 windDirection)
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
            WindSprite.Position = -windDirection * 200;

            // Rotate the wind sprite to face the wind direction.
            WindSprite.Rotation = windDirection.Angle();
        }
    }

    public void CollisionHandling()
    {
        if (GetSlideCollisionCount() == 0)
        {
            _sprite.Show();
            _animatedSprite.Hide();

            // Reset the animation sprite position
            _animatedSprite.Position = Vector2.Zero;
        }
        else
        {
            for (int i = 0; i < GetSlideCollisionCount(); i++)
            {
                KinematicCollision2D collision = GetSlideCollision(i);
                if (collision.GetCollider() is StaticBody2D)
                {
                    //CHeck which side the collision is on
                    Vector2 collisionNormal = collision.GetNormal();
                    if (
                        Mathf.Abs(collisionNormal.X) > Mathf.Abs(collisionNormal.Y)
                        && collisionNormal.Length() > 0.1
                    )
                    {
                        if (collisionNormal.X > 0)
                        {
                            // We hit the left wall
                            _sprite.Hide();
                            _animatedSprite.Show();
                            _animatedSprite.Play("rightToLeft");

                            //offset the animation sprite to the right
                            _animatedSprite.Position = new Vector2(50, 0);
                        }
                        else if (collisionNormal.X < 0)
                        {
                            // We hit the right wall
                            _sprite.Hide();
                            _animatedSprite.Show();
                            _animatedSprite.Play("leftToRight");
                        }
                    }
                    else
                    {
                        if (collisionNormal.Y > 0)
                        {
                            // We hit the ceiling
                            _sprite.Hide();
                            _animatedSprite.Show();
                            _animatedSprite.Play("bottomToTop");

                            //offset the animation sprite to the bottom
                            //_animatedSprite.Position = new Vector2(0, 70);
                        }
                        else if (collisionNormal.Y < 0)
                        {
                            // We hit the floor
                            _sprite.Hide();
                            _animatedSprite.Show();
                            _animatedSprite.Play("topToBottom");

                            //offset the animation sprite to the top
                            _animatedSprite.Position = new Vector2(0, -40);
                        }
                    }
                }
            }
        }
    }

    public void Die()
    {
        EmitSignal(SignalName.Died);
        CallDeferred("queue_free");
    }
}
