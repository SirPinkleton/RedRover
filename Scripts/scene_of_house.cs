using Godot;
using System;

public partial class scene_of_house : Node2D
{
	[Signal]
	public delegate void ChangeSceneEventHandler(string nameOfScene);
	[Export]
	public string NameOfLeftSceneToLoad = "";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// tell global handler what direction TV/monster sounds are coming from, or change ambience
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//handle if scene transitions are clicked
	private void LoadSceneFromLeftClick(string sceneToLoad)
	{
		EmitSignal("ChangeScene", sceneToLoad);
	}
}
