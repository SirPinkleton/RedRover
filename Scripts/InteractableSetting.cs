using Godot;
using System;

public partial class InteractableSetting : TextureButton
{
	int currentVisibility = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		handlerNode.ButtonVisibilitySetting = currentVisibility;
		TextureNormal = ResourceLoader.Load<Texture2D>($@"PNGs\visibility-setting-{currentVisibility}.png");
		
	}

	private void ChangeVisibility()
	{
		GD.Print("changing visibility");
		var handlerNode = GetTree().CurrentScene as global_handler;
		GD.Print($"was {currentVisibility}");
		currentVisibility++;
		currentVisibility = currentVisibility % 3;
		GD.Print($"is now {currentVisibility}");
		handlerNode.ButtonVisibilitySetting = currentVisibility;
		TextureNormal = ResourceLoader.Load<Texture2D>($@"PNGs\visibility-setting-{currentVisibility}.png");

	}
}
