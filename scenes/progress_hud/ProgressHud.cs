using Godot;
using System;

public partial class ProgressHud : Control
{
    private ProgressBar _progressBar;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _progressBar = GetNode<ProgressBar>("MarginContainer/ProgressBar");
    }

    public void UpdateProgress(float value)
    {
        _progressBar.Value = value;
    }
}
