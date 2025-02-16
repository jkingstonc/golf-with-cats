using Godot;

public partial class BallController : RigidBody2D
{	

	private static float DECELERATION_RATE = 0.98f;
	private Vector2 velocity = Vector2.Zero;
	private Vector2 acceleration = Vector2.Zero;
	public bool Moving = false;

	public override void _PhysicsProcess(double delta)
	{
		if(velocity==Vector2.Zero){
			Moving = false;
		}else{
			Moving = true;
		}
		velocity = velocity + acceleration * (float)delta;
		velocity = velocity * DECELERATION_RATE;
		if(velocity.Length() < .005){
			velocity = Vector2.Zero;
		}
		var col = MoveAndCollide(velocity);
		acceleration = Vector2.Zero;
		if(col!=null){
			GD.Print(col.GetCollider() is HoleController);
			if(col.GetCollider() is HoleController){
				GetNode<GameManager>("/root/Root/GameManager").NextLevel();
				GlobalPosition = Vector2.Zero;
				velocity = Vector2.Zero;
				acceleration = Vector2.Zero;
			}else{
				velocity = velocity.Bounce(col.GetNormal());
			}
		}
	}

	public void ApplyForce(Vector2 force){
		acceleration=force;
	}
}
