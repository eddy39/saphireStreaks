using Godot;
using System;

public class Lever : Node2D
{
    [Export] public bool LeverState { get => _leverState; set { _leverState = value; OnLeverStateChanged(); } }
    private bool _leverState;
    public event Action LeverStateChangedEvent;
    private Interactable Interactable;
    private void OnLeverStateChanged()
    {
        this.LeverStateChangedEvent?.Invoke();
    }

    public override void _Ready()
    {
        this.Interactable = base.GetNode<Interactable>("Interactable");
    }
}
