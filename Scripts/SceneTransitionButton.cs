using Godot;
using System;

public partial class SceneTransitionButton : TextureButton
{
	[Signal]
	public delegate void ChangeSceneEventHandler(string nameOfScene);
	[Export]
	public string NameOfSceneToLoad;

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

	private void SignalSceneChange()
	{
		GD.Print($"I, {Name}, have been pressed");
		EmitSignal(SignalName.ChangeScene, NameOfSceneToLoad);
		//unload current scene
		var topNodeForThisScene = GetParent().GetParent();
		//GD.Print($"'s name: {topNodeForThisScene.Name}");
		topNodeForThisScene.QueueFree();
	}
}
