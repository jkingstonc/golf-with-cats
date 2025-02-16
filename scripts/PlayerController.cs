using Godot;

public partial class PlayerController : Node2D
{
	private enum SwingState {
		IDLE,
		SWINGING,
		BALL_MOVING
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
				GetNode<Line2D>("Line2D").AddPoint(GetGlobalMousePosition());
				GetNode<Line2D>("Line2D").AddPoint(GetGlobalMousePosition());
				GD.Print("left");
				swingState = SwingState.SWINGING;
				swingStartingPos = GetGlobalMousePosition();
			}
		}else if(swingState==SwingState.SWINGING){
			GetNode<Line2D>("Line2D").SetPointPosition(1, GetGlobalMousePosition());
			if(Input.IsActionJustReleased("swing")){
			GetNode<Line2D>("Line2D").ClearPoints();
				var diff = GetGlobalMousePosition()-swingStartingPos;
				GetNode<BallController>("/root/Root/Ball").ApplyForce(-diff);
				swingState = SwingState.BALL_MOVING;
			}
		}else if(swingState==SwingState.BALL_MOVING){
			if(!GetNode<BallController>("/root/Root/Ball").Moving){
				swingState = SwingState.IDLE;
			}
		}
	}
}
