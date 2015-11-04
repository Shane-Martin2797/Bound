using UnityEngine;
using System.Collections;

public class DamageSpell : Spell
{
	public Ball damageBall;
	public Transform spawnPoint;
	public float damage = 20f;
	bool hasInst = false;
	
	
	public override void ReleaseCast ()
	{
		if (canCast) {
			Ball ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as Ball;
			ball.SetDamage (damage);
			CooldownValues ();
		}
		ResetValues ();
		ResetBallSize ();
	}
	
	public override void Shoot ()
	{
		if (damageBall != null) {
			Ball ball = Instantiate (damageBall, spawnPoint.transform.position, transform.rotation) as Ball;
			ball.SetDamage (damage);
			CooldownValues ();
			ResetValues ();
		} 
	}
	
	GameObject ball;
	float normalX;
	public override void Charge ()
	{
		if (player.castTime > 0) {
			if (!hasInst) {
				ball = Instantiate (damageBall.gameObject) as GameObject;
				normalX = ball.transform.localScale.x;
				ball.transform.position = spawnPoint.transform.position;
				ball.transform.localScale = Vector3.zero;
				Destroy (ball.GetComponent<Collider2D> ());
				Destroy (ball.GetComponent<Ball> ());
				hasInst = true;
			}
			if (hasInst) {
				ball.transform.position = spawnPoint.transform.position;
				IncreaseBallSize ();
			}
		}
	}
	
	void IncreaseBallSize ()
	{
		if (ball != null) {
			ball.transform.localScale += new Vector3 (Time.deltaTime, Time.deltaTime, Time.deltaTime) / player.castTime * normalX;
			if (ball.transform.localScale.x >= normalX) {
				ResetBallSize ();
			}
		}
	}
	void ResetBallSize ()
	{
		ball.transform.localScale = Vector3.zero;
	}
	
	public override void ResetValues ()
	{
		castTime = player.castTime;
		CastTimeModifier *= CastTimeModifier;
		canCast = false;
	}

}
