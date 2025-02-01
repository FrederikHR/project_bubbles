using Godot;
using System;

public partial class TransitionScene : Control
{
    [Signal]
    public delegate void IrisCloseSignalEventHandler();

    [Export(PropertyHint.Range, "0.0,1.5")]
    public float irisValue = 1.5f;

    private AnimationPlayer _animationPlayer;
    private ShaderMaterial _shaderMaterial;

    private Player _player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _shaderMaterial = (ShaderMaterial)GetNode<ColorRect>("IrisTransition").Material;
        _shaderMaterial.SetShaderParameter("radius", irisValue);
        IrisOpen();

        //get player
        _player = GetNode<Player>("../../Player");
        _player.Died += OnPlayerDied;

        //get aspect ratio for material
        Vector2I windowSize = DisplayServer.WindowGetSize();
        _shaderMaterial.SetShaderParameter("screen_size", windowSize);
    }

    public async void IrisClose()
    {
        _animationPlayer.Play("IrisClose");
        await ToSignal(_animationPlayer, "animation_finished");
        EmitSignal(SignalName.IrisCloseSignal);
    }

    public async void IrisOpen()
    {
        _animationPlayer.Play("IrisOpen");
        await ToSignal(_animationPlayer, "animation_finished");
    }

    public void OnPlayerDied()
    {
        GD.Print("Player died");
        IrisClose();
    }
}
