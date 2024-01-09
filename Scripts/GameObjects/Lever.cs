using Godot;
using System;

public class Lever : Node2D
{
    [Export] public bool LeverState { get => _leverState; set { _leverState = value; OnLeverStateChanged(); } }
    private bool _leverState;

    private int _currentFrame;
    private Sprite LeverSprite;

    public event Action LeverStateChangedEvent;
    private Interactable Interactable;
    private void OnLeverStateChanged()
    {
        _currentFrame = Mathf.Wrap(_currentFrame + 1, 0, LeverSprite.Hframes - 1);
        this.LeverSprite.Frame = _currentFrame;

        this.LeverStateChangedEvent?.Invoke();
    }

    public override void _Ready()
    {
        this.LeverSprite = base.GetNode<Sprite>("Sprite");

        this.Interactable = base.GetNode<Interactable>("Interactable");
        this.Interactable.Interacted += OnLeverStateChanged;
    }
}
