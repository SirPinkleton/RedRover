using Godot;
using System;

public partial class AcquiringButton : TextureButton
{
	[Signal]
	public delegate void MakeCommentEventHandler(string nameOfScene);
	[Export]
	public string CommentWhenClicked = "Huh...";
	[Export]
	public string whatToCollect = "";

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

	private void SendCommentMessage()
	{
		GD.Print($"I, {Name}, have been pressed");
		EmitSignal(SignalName.MakeComment, CommentWhenClicked);
	}

	private void AcquireItem()
	{
		var handlerNode = GetTree().CurrentScene as global_handler;
		if (whatToCollect == "bowl" && !handlerNode.isCarryingBowl)
		{
			EmitSignal(SignalName.MakeComment, "A bowl, hm...");
			handlerNode.ShowBowl();
			GetNode<TextureRect>("../../BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/S15-nobowl.png");
		}
		else if (whatToCollect == "bowl" && handlerNode.isCarryingBowl)
		{
			EmitSignal(SignalName.MakeComment, "Already got the bowl.");
		}
		else if (whatToCollect == "food" && handlerNode.isCarryingBowl && !handlerNode.isCarryingBadFood && !handlerNode.isCarryingSludge)
		{
			EmitSignal(SignalName.MakeComment, "I think this is palatable to that... thing. But it needs to be deadly...");
			handlerNode.ShowFood();
			GetNode<TextureRect>("../../BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/S13-nofood.png");
		}
		else if (whatToCollect == "food" && handlerNode.isCarryingBowl && !handlerNode.isCarryingBadFood && handlerNode.isCarryingSludge)
		{
			EmitSignal(SignalName.MakeComment, "Ok, this should work.");
			handlerNode.ShowFood();
			GetNode<TextureRect>("../../BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/S13-nofood.png");
		}
		else if (whatToCollect == "food" && handlerNode.isCarryingBadFood)
		{
			EmitSignal(SignalName.MakeComment, "Already got the food.");
		}
		else if (whatToCollect == "food" && !handlerNode.isCarryingBowl)
		{
			EmitSignal(SignalName.MakeComment, "I need something to carry this, because I'm not touching it.");
		}
		else if (whatToCollect == "goo" && handlerNode.isCarryingBowl && handlerNode.isCarryingSludge)
		{
			EmitSignal(SignalName.MakeComment, "Already got the sludge.");
		}
		else if (whatToCollect == "goo" && handlerNode.isCarryingBowl && !handlerNode.isCarryingSludge && !handlerNode.isCarryingBadFood)
		{
			EmitSignal(SignalName.MakeComment, "I think this ought to kill that thing, but only if it looks enough like food...");
			handlerNode.ShowGoo();
			GetNode<TextureRect>("../../BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/S14-touched.png");
		}
		else if (whatToCollect == "goo" && handlerNode.isCarryingBowl && !handlerNode.isCarryingSludge && handlerNode.isCarryingBadFood)
		{
			EmitSignal(SignalName.MakeComment, "I think this should work. I hope it eats this if I give it to them...");
			handlerNode.ShowGoo();
			GetNode<TextureRect>("../../BackgroundImage").Texture = (Texture2D)ResourceLoader.Load($"PNGs/S14-touched.png");
		}
		else if (whatToCollect == "goo" && !handlerNode.isCarryingBowl)
		{
			EmitSignal(SignalName.MakeComment, "I need something to carry this, because I'm not touching it.");
		}
	}
}