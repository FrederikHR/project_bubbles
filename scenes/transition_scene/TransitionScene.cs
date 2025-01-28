using Godot;
using System;

public partial class TransitionScene : Control
{
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

        //get player from "player" group
        _player = GetTree().GetNodesInGroup("player")[0] as Player;
        //subscribe to player's Died event
        _player.Died += OnPlayerDied;
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

    public void OnPlayerDied()
    {
        IrisClose();
    }
}
