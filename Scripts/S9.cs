using Godot;
using System;

public partial class S9 : Node2D
{
	[Export]
	AudioStream screech;
	[Export]
	AudioStream chew;
	[Export]
	AudioStream thud;
	public override async void _Ready()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;

		if (handlerNode.currentMonsterState == global_handler.MonsterStates.Dead)
		{
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-dead.png");
		}
		else if (handlerNode.isCarryingBowl && handlerNode.isCarryingBadFood && handlerNode.isCarryingSludge)
		{
			//first, stop timers
			handlerNode.timeSinceMonsterWasAngered.Stop();
			handlerNode.timeSinceMonsterWasAppeased.Stop();

			//stop music
			handlerNode.currentTenseMusic.Stop();

			//disable buttons
			handlerNode.DisableAllButtons();

			//if player is holding something, then play the killing cutscene
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-looking-at-player.png");
			//roar after a half second, briefly
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Stream = screech;
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-monster-roaring.png");
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-looking-at-player.png");
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);

			//remove bowl and stuff from the canvas
			handlerNode.HideAcquirables();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-looking-at-bowl.png");
			await ToSignal(GetTree().CreateTimer(3f), SceneTreeTimer.SignalName.Timeout);
			
			handlerNode.currentAmbientNoises.Stream = screech;
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-1.png");
			await ToSignal(GetTree().CreateTimer(.3f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-2.png");
			await ToSignal(GetTree().CreateTimer(.3f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Stop();
			handlerNode.currentAmbientNoises.Stream = chew;
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-3.png");
			await ToSignal(GetTree().CreateTimer(.2f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-4.png");
			await ToSignal(GetTree().CreateTimer(.2f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-3.png");
			await ToSignal(GetTree().CreateTimer(.2f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-4.png");
			await ToSignal(GetTree().CreateTimer(.2f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-3.png");
			await ToSignal(GetTree().CreateTimer(.2f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-4.png");
			await ToSignal(GetTree().CreateTimer(.2f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-3.png");
			await ToSignal(GetTree().CreateTimer(.2f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-eatingbowl-4.png");
			await ToSignal(GetTree().CreateTimer(.2f), SceneTreeTimer.SignalName.Timeout);

			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-finished-eating.png");
			await ToSignal(GetTree().CreateTimer(5f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-dead.png");
			handlerNode.currentAmbientNoises.Stream = thud;
			handlerNode.currentAmbientNoises.Play();
			handlerNode.DisplayComment("IT WORKED!!!! I'M SAFE!!! Now, to get out of here.");
			await ToSignal(GetTree().CreateTimer(2f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentMonsterState = global_handler.MonsterStates.Dead;

			handlerNode.ProcessChangeScene("S9");
			QueueFree();
		}
		else
		{
			//otherwise return to S8
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-looking-at-player.png");
			//roar after a half second, briefly
			await ToSignal(GetTree().CreateTimer(0.5f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.currentAmbientNoises.Stream = screech;
			handlerNode.currentAmbientNoises.Play();
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-monster-roaring.png");
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
			GetNode<TextureRect>("BackgroundImage").Texture = (Texture2D)ResourceLoader.Load("PNGs/S9-looking-at-player.png");
			await ToSignal(GetTree().CreateTimer(1f), SceneTreeTimer.SignalName.Timeout);
			handlerNode.ProcessChangeScene("S8");
			QueueFree();
		}
	}

}
