using Godot;
using System;

public partial class MonsterIntro : Node2D
{
	[Export]
	AudioStream Bump;
	[Export]
	AudioStream Crash;
	[Export]
	AudioStream Stomp;
	[Export]
	AudioStream Grunt;
	[Export]
	AudioStream Roar;
	public override void _Ready()
	{
		PlayMonsterIntroCutscene();
	}

	public async void PlayMonsterIntroCutscene()
	{
		// get global handler, will be using it a lot
		var handlerNode = GetTree().CurrentScene as global_handler;

		//set scene to first image
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-0.png");

		//after 3 seconds make bump noise
		await ToSignal(GetTree().CreateTimer(3.0f), SceneTreeTimer.SignalName.Timeout);
		handlerNode.currentAmbientNoises.Stream = Bump;
		handlerNode.currentAmbientNoises.Play();
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);

		//show next scene
		GD.Print("Changing background to next break-in scene, 1");
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-1.png");
		
		//after 2 seconds, make bump noise again
		await ToSignal(GetTree().CreateTimer(2.0f), SceneTreeTimer.SignalName.Timeout);
		handlerNode.currentAmbientNoises.Play();
		//show damage to door for a flash, indicating it's breaking down
		GD.Print("Changing background to next break-in scene, 2");
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-2.png");
		await ToSignal(GetTree().CreateTimer(0.4f), SceneTreeTimer.SignalName.Timeout);
		GD.Print("Changing background to next break-in scene, 3");
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-3.png");
		
		//wait 2 more seconds, then crash the door down
		await ToSignal(GetTree().CreateTimer(2.0f), SceneTreeTimer.SignalName.Timeout);
		handlerNode.currentTenseMusic.Stream = Crash;
		handlerNode.currentAmbientNoises.Play();
		handlerNode.currentTenseMusic.Play();
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-4.png");
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-5.png");
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-6.png");
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-7.png");
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-8.png");
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-9.png");
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-10.png");
		await ToSignal(GetTree().CreateTimer(0.2f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-11.png");
		//delay, then lower arm
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-11-1.png");
		//after another second, step forward
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		handlerNode.currentAmbientNoises.Stream = Stomp;
		handlerNode.currentAmbientNoises.Play();
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-11-2.png");
		//after another second, step forward
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		handlerNode.currentAmbientNoises.Play();
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-11-3.png");
		// after a second and a half, monster notices TV
		await ToSignal(GetTree().CreateTimer(1.5f), SceneTreeTimer.SignalName.Timeout);
		handlerNode.currentAmbientNoises.Stream = Grunt;
		handlerNode.currentAmbientNoises.Play();
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-11-4.png");
		//show next scene for a second
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-12.png");
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		//next step towards couch
		handlerNode.currentAmbientNoises.Stream = Stomp;
		handlerNode.currentAmbientNoises.Play();
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-13.png");
		//another second, then sit on couch
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);
		handlerNode.currentAmbientNoises.Play();
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/break-in-14.png");
		await ToSignal(GetTree().CreateTimer(1.0f), SceneTreeTimer.SignalName.Timeout);

		//set monster expectant
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-expectant.png");
		//roar after a half second, briefly
		await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
		handlerNode.currentAmbientNoises.Stream = Roar;
		handlerNode.currentAmbientNoises.Play();
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-roar.png");
		await ToSignal(GetTree().CreateTimer(0.3f), SceneTreeTimer.SignalName.Timeout);
		GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S8-monster-expectant.png");
		await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
		
		handlerNode.DisplayComment("I think it wants me to... change the channel?", 4f);
		await ToSignal(GetTree().CreateTimer(4f), SceneTreeTimer.SignalName.Timeout);

		//load S8 again
		handlerNode.ProcessChangeScene("S10");
		//unload this scene to allow S8 to play
		QueueFree();
	}
}
