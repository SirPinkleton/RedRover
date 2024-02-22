using Godot;
using System;
using static global_handler;

public partial class S8 : Node2D
{
	[Export]
	AudioStream Roar;
	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;

		if (handlerNode.currentMonsterState == MonsterStates.NotPresent)
		{
			//uncomment when testing:
			//handlerNode.currentTVStatic.Stream = handlerNode.whitenoise; 
			//handlerNode.currentTVStatic.Play();
			GD.Print($"Changing static to new volume: -25");
			handlerNode.currentTVStatic.VolumeDb = -25f;
				
			//results in a slew of scenes being loaded. Should involve a final step loading S10 (or the tutorial?)
			handlerNode.currentAmbientNoises.Stop();
			GetNode<TextureRect>("BackgroundImage").Texture = null;
			handlerNode.ProcessChangeScene("MonsterIntro");

			//unload this scene to allow intro to play
			QueueFree();
		}
		else
		{
			//if we're not loading for the first time, adjust audio to new values
			//ambient noises were silenced in intro, set them to start again
			if (!handlerNode.currentAmbientNoises.Playing)
			{
				GD.Print("playing creaking again");
				handlerNode.currentAmbientNoises.Stream = handlerNode.creaking;
				handlerNode.currentAmbientNoises.Play();
			}
			if (handlerNode.currentTVChannelNoises.VolumeDb == 0)
			{
				GD.Print("Reducing TV volume");
				//TV audio at max volume, set to sound more distant
				handlerNode.currentTVChannelNoises.VolumeDb -= 10;
				
				GD.Print($"Changed TV to new volume: {handlerNode.currentTVChannelNoises.VolumeDb}");
			}
			if (handlerNode.currentTVChannelNoises.VolumeDb == -20)
			{
				GD.Print("Reducing TV volume");
				//TV audio at max volume, set to sound more distant
				handlerNode.currentTVChannelNoises.VolumeDb += 10;
				GD.Print($"Changed TV to new volume: {handlerNode.currentTVChannelNoises.VolumeDb}");
			}
		}



		if (handlerNode.currentMonsterState == MonsterStates.Irritated)
		{
			//set monster expectant
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-expectant.png");
			//roar after a half second, briefly
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Stream = Roar;
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-roar.png");
			await ToSignal(GetTree().CreateTimer(0.3f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-expectant.png");

		}
		if (handlerNode.currentMonsterState == MonsterStates.Appeased || handlerNode.currentMonsterState == MonsterStates.BecameHappy)
		{
			GD.Print("monster is happy :]");
			//set texture
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-watching.png");
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override async void _Process(double delta)
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		if (handlerNode.currentMonsterState == MonsterStates.BecameAngry)
		{
			//set monster expectant
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-expectant.png");
			//roar after a half second, briefly
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Stream = Roar;
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-roar.png");
			await ToSignal(GetTree().CreateTimer(0.3f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-expectant.png");
		}
	}
}
