using Godot;
using System;
using static global_handler;

public partial class S11Escape : Node2D
{
	[Export]
	public AudioStream chasedown;
	
	[Export]
	public AudioStream runningAway;
	
	[Export]
	public AudioStream aaarunningAwayTransforming;
	[Export]
	public AudioStream transformingTension;

	// Called when the node enters the scene tree for the first time.
	public override async void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		handlerNode.timeSinceMonsterWasAngered.Stop();
		handlerNode.timeSinceMonsterWasAppeased.Stop();

		try
		{
			handlerNode.currentTVStatic.Stop();
			handlerNode.currentTVChannelNoises.Stop();
			handlerNode.currentAmbientNoises.Stop();
			handlerNode.currentTenseMusic.Stop();
			handlerNode.currentMonsterNoises.Stop();
		}
		catch
		{

		}

		if (handlerNode.currentMonsterState != MonsterStates.Dead)
		{
			//play grunting noises from player
			handlerNode.currentAmbientNoises.Stop();
			handlerNode.currentAmbientNoises.Stream = runningAway;
			handlerNode.currentAmbientNoises.Play();
			//after a delay, play monster screech and stomping that gets louder and louder
			handlerNode.currentMonsterNoises.Stop();
			handlerNode.currentMonsterNoises.Stream = chasedown;
			handlerNode.currentMonsterNoises.Play();

			//show running away images
			GetNode<TextureRect>("ForegroundImage").Texture = null;
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);

			handlerNode.ProcessChangeScene("Death");
			GD.Print("ending death scene");
			QueueFree();

		}
		else
		{
			//play grunting noises from player
			handlerNode.currentAmbientNoises.Stop();
			handlerNode.currentAmbientNoises.Stream = runningAway;
			handlerNode.currentAmbientNoises.Play();

			//show running away images
			GetNode<TextureRect>("ForegroundImage").Texture = null;
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);

			//player starts transforming
			handlerNode.currentAmbientNoises.Stop();
			handlerNode.currentAmbientNoises.Stream = aaarunningAwayTransforming;
			handlerNode.currentAmbientNoises.Play();
			handlerNode.currentTenseMusic.Stop();
			handlerNode.currentTenseMusic.Stream = transformingTension;
			handlerNode.currentTenseMusic.Play();

			//more running away, but include growing red hair
			GetNode<TextureRect>("ForegroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/growing-hair-1.png");
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);

			GetNode<TextureRect>("ForegroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/growing-hair-2.png");
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);

			GetNode<TextureRect>("ForegroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/growing-hair-3.png");
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);

			GetNode<TextureRect>("ForegroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/growing-hair-4.png");
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-1.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/escape-2.png");
			await ToSignal(GetTree().CreateTimer(.4f), SceneTreeTimer.SignalName.Timeout);

			handlerNode.currentTVStatic.Stop();
			handlerNode.currentTVChannelNoises.Stop();
			handlerNode.currentAmbientNoises.Stop();
			handlerNode.currentTenseMusic.Stop();
			handlerNode.currentMonsterNoises.Stop();

			handlerNode.ProcessChangeScene("Main_Menu");
			QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
