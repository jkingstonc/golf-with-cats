using Godot;

public partial class BallController : RigidBody2D
{	

	private static float SPEED = 100;
	private static float DECELERATION_RATE = 0.98f;
	private Vector2 velocity = Vector2.Zero;
	private Vector2 acceleration = Vector2.Zero;

	public override void _PhysicsProcess(double delta)
	{
		velocity = velocity + acceleration * (float)delta;
		velocity = velocity * DECELERATION_RATE;
		if(velocity.Length() < .005){
			velocity = Vector2.Zero;
		}
		var col = MoveAndCollide(velocity);
		acceleration = Vector2.Zero;
		if(col!=null){
			velocity = velocity.Bounce(col.GetNormal());
		}
	}

	public void ApplyForce(Vector2 force){
		acceleration=force;
	}
}
