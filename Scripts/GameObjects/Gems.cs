using Godot;
using System;

public class Gems : Node2D
{
	[Export] public Color Color;
	public override void _Ready()
	{
		
	}

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }

	public enum Color 
	{
		Blue, Yellow, Red Purple
	}
}
