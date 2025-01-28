using Godot;
using System;

public partial class BaseScene : Node
{
    [Export]
    public Godot.Collections.Array<PackedScene> levels;

    private Node _currentLevel = null;
    private int _currentLevelIndex = 0;

    private StartScreen _startScreen;
    private Player _player;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _startScreen = GetNode<StartScreen>("StartScreen");
        _startScreen.StartGame += OnStartGame;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_cancel"))
        {
            //reset the game
            GetTree().ReloadCurrentScene();
        }
    }

    public void OnStartGame()
    {
        var level = levels[0].Instantiate();
        _currentLevel = level;

        _player = level.GetNode<Player>("Player");
        _player.Died += OnPlayerDied;

        AddChild(level);
    }

    public void OnPlayerDied()
    {
        //Player gets queued free by obstacle/enemy

        _currentLevel.QueueFree();

        //Start the same level again
        var level = levels[_currentLevelIndex].Instantiate();
        _currentLevel = level;

        _player = level.GetNode<Player>("Player");
        _player.Died += OnPlayerDied;

        AddChild(level);
    }
}
