using Godot;
using System;

public partial class BaseScene : Node
{
    [Export]
    public Godot.Collections.Array<PackedScene> levels;

    [Export]
    public Godot.Collections.Array<float> levelTimes;

    private Node _currentLevel = null;
    private int _currentLevelIndex = 0;

    private StartScreen _startScreen;
    private TransitionScene _transitionScene;

    private Timer survivalTimer;
    private bool _isTransitioningToNextLevel = false;

    private ProgressHud _progressHUD;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        _startScreen = GetNode<StartScreen>("StartScreen");
        _startScreen.StartGame += OnStartGame;

        survivalTimer = GetNode<Timer>("SurvivalTimer");
        survivalTimer.Timeout += OnSurvivalTimerTimeout;

        // Check if the number of levels and level times match
        if (levels.Count != levelTimes.Count)
        {
            GD.PrintErr("Number of levels and level times do not match");
        }

        _progressHUD = GetNode<ProgressHud>("BaseCanvasLayer/ProgressHUD");
        _progressHUD.Hide();
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        // Update progress HUD based on ratio of survival timer and level time
        if (_currentLevel != null)
        {
            double progress = survivalTimer.TimeLeft / survivalTimer.WaitTime;
            _progressHUD.UpdateProgress((float)progress * 100.0f);
        }
    }

    public void OnStartGame()
    {
        var level = levels[0].Instantiate();
        _currentLevel = level;

        //_player = level.GetNode<Player>("Player");
        //_player.Died += OnPlayerDied;

        AddChild(level);

        //get transition scene from group
        _transitionScene = GetTree().GetNodesInGroup("transition")[0] as TransitionScene;
        //subscribe to transition scene's IrisClose event
        _transitionScene.IrisCloseSignal += OnIrisCloseSignal;

        survivalTimer.WaitTime = levelTimes[0];
        survivalTimer.Start();

        //show progress HUD
        _progressHUD.Show();
    }

    public void OnSurvivalTimerTimeout()
    {
        _isTransitioningToNextLevel = true;
        _transitionScene.IrisClose();
    }

    public void OnIrisCloseSignal()
    {
        //Check if we are transitioning to next level
        if (_isTransitioningToNextLevel)
        {
            _isTransitioningToNextLevel = false;

            //Player survived the level
            _currentLevel.QueueFree();

            _currentLevelIndex++;
            if (_currentLevelIndex >= levels.Count)
            {
                GetTree().Quit();
            }
            var newLevel = levels[_currentLevelIndex].Instantiate();
            _currentLevel = newLevel;

            AddChild(newLevel);

            //get transition scene from group (note: might be multiple transition scenes in the group, add last one)
            _transitionScene =
                GetTree().GetNodesInGroup("transition")[
                    GetTree().GetNodesInGroup("transition").Count - 1
                ] as TransitionScene;
            //subscribe to transition scene's IrisClose event
            _transitionScene.IrisCloseSignal += OnIrisCloseSignal;

            //restart timer with new time
            survivalTimer.WaitTime = levelTimes[_currentLevelIndex];
            survivalTimer.Start();
        }
        else
        {
            //Player gets queued free by obstacle/enemy

            _currentLevel.QueueFree();

            //Start the same level again
            var level = levels[_currentLevelIndex].Instantiate();
            _currentLevel = level;

            AddChild(level);

            //get transition scene from group (note: might be multiple transition scenes in the group, add last one)
            _transitionScene =
                GetTree().GetNodesInGroup("transition")[
                    GetTree().GetNodesInGroup("transition").Count - 1
                ] as TransitionScene;
            //subscribe to transition scene's IrisClose event
            _transitionScene.IrisCloseSignal += OnIrisCloseSignal;
        }
    }
}
