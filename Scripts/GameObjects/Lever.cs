using Godot;
using System;

public class Lever : Node2D
{
    [Export] public bool LeverState { get => _leverState; set { _leverState = value; OnLeverStateChanged(); } }
    [Export] public bool IsInteractable;

    private bool _leverState;
    private int _currentFrame;
    private Sprite LeverSprite;

    public event Action<int> LeverStateChangedEvent;
    private Interactable Interactable;
    private ShaderMaterial ShaderMaterial = GD.Load<ShaderMaterial>("res://Scripts/Shared/OutlineShader.tres");
    private void OnLeverStateChanged()
    {
        _currentFrame = Mathf.Wrap(_currentFrame, 0, LeverSprite.Hframes);
        this.LeverSprite.Frame = _currentFrame;
        this.LeverStateChangedEvent?.Invoke(_currentFrame);
        _currentFrame++;

        
    }

    public override void _Ready()
    {
        this.LeverSprite = base.GetNode<Sprite>("Sprite");

        this.Interactable = base.GetNode<Interactable>("Interactable");
        this.Interactable.Interacted += OnLeverStateChanged;
        this.Interactable.PlayerEntered += OnPlayerEntered;
        this.Interactable.PlayerExited += OnPlayerExited;
    }

    private void OnPlayerEntered()
    {
        if (IsInteractable)
        {
            this.LeverSprite.Material = ShaderMaterial;
        }
    }

    private void OnPlayerExited()
    {
        this.LeverSprite.Material = null;
    }
}
