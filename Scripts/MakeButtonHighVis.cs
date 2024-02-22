using Godot;
using System;

public partial class MakeButtonHighVis : TextureButton
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		//if looking at a menu screen, skip button defining
		if (handlerNode.GetNodeOrNull("Main_Menu") == null 
		&& handlerNode.GetNodeOrNull("MainMenuSettings") == null
		&& handlerNode.GetNodeOrNull("Credits") == null
		&& handlerNode.GetNodeOrNull("InGameSettings") == null)
		{
			//check if global_handler has visibility set to high, if so change Texture Normal
			string visibilityPngToUse = "noVisButton.png";
			switch (handlerNode.ButtonVisibilitySetting)
			{
				case 1:
					visibilityPngToUse = "lowVisButton.png";
					break;
				case 2:
					visibilityPngToUse = "highVisButton.png";
					break;
				default:
					break;
			}
			TextureNormal = ResourceLoader.Load<Texture2D>($@"PNGs\{visibilityPngToUse}");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
