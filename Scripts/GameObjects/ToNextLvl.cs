using Godot;
using System;

public class ToNextLvl : Area2D
{
    [Export] public string NextLevel;
    public override void _Ready()
    {
        Connect("body_entered", this, nameof(OnBodyEntered));
    }
    public void OnBodyEntered(Node body)
    {
        if (body is Player)
        {
            GetTree().ChangeScene(NextLevel);
        }
    }
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
