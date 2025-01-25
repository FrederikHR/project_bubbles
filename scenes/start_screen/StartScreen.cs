using Godot;
using System;

public partial class StartScreen : Control
{
    private Button _startButton;
    private Button _quitButton;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _startButton = GetNode<Button>("VBoxContainer/StartButton");
        _quitButton = GetNode<Button>("VBoxContainer/QuitButton");

        _startButton.Pressed += OnstartButtonPressed;
        _quitButton.Pressed += OnQuitButtonPressed;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta) { }

    private void OnstartButtonPressed()
    {
        //Hide the start screen
        Visible = false;
    }

    private void OnQuitButtonPressed()
    {
        GetTree().Quit();
    }
}
