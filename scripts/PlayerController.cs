using Godot;

public partial class PlayerController : Node2D
{
	private enum SwingState {
		IDLE,
		SWINGING
	}

	private SwingState swingState;
	private Vector2 swingStartingPos;

	public override void _Ready()
	{
		swingState = SwingState.IDLE;
	}

	public override void _Process(double delta)
	{
		if(swingState==SwingState.IDLE){
			if(Input.IsActionJustPressed("swing")){
				GD.Print("left");
				swingState = SwingState.SWINGING;
				swingStartingPos = GetGlobalMousePosition();
			}
		}else if(swingState==SwingState.SWINGING){
			if(Input.IsActionJustReleased("swing")){
				var diff = GetGlobalMousePosition()-swingStartingPos;
				var dir = -diff.Normalized();
				var power = diff.Length();
				GD.Print($"dir = {dir} power = {power}");

				GetNode<BallController>("/root/Root/Ball").ApplyForce(-diff);

				swingState = SwingState.IDLE;
			}
		}
	}
}
