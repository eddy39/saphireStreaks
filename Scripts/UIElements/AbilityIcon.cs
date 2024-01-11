using Godot;
using System;

public class AbilityIcon : TextureRect
{
    /* [Export] */
    public Gem.Color AbilityColor;
    // Nodes
    public TextureRect GemTexture;
	private Label Label;
    public override void _Ready()
    {
		this.Label = GetNode<Label>("Label");
		this.Label.Visible = false;
		/* GameState.GemsUpdatedNotifier += (_) => {
			this.Label.Text = GameState.GemCount[AbilityColor].ToString();
		}; */

        // Get Gem
        GemTexture = GetNode<TextureRect>("Gem");
        // Load Texture based on color
		switch (AbilityColor)
		{
			case Gem.Color.Blue:
				GemTexture.Texture = ResourceLoader.Load<Texture>("res://Assets/UI/Blue_Gem.png");
				break;
			case Gem.Color.Yellow:
				GemTexture.Texture = ResourceLoader.Load<Texture>("res://Assets/UI/Yellow_Gem.png");
				break;
			case Gem.Color.Red:
				GemTexture.Texture = ResourceLoader.Load<Texture>("res://Assets/UI/Red_Gem.png");
				break;
			case Gem.Color.Purple:
				GemTexture.Texture = ResourceLoader.Load<Texture>("res://Assets/UI/Purple_Gem.png");
				break;
		}
    }
	
//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
