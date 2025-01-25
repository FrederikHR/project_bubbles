using Godot;
using System;

public partial class TransitionScene : Control
{
    private AnimationPlayer _animationPlayer;
    private ShaderMaterial _shaderMaterial;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _shaderMaterial = (ShaderMaterial)GetNode<ColorRect>("IrisTransition").Material;
    }

    public async void IrisClose()
    {
        _animationPlayer.Play("IrisClose");
        await ToSignal(_animationPlayer, "animation_finished");
    }

    public async void IrisOpen()
    {
        _animationPlayer.Play("IrisOpen");
        await ToSignal(_animationPlayer, "animation_finished");
    }
}
