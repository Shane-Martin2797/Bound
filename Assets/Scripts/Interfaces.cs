using UnityEngine;
using System.Collections;
	
public interface ICollisionEnter2D
{
	void OnCollisionEnter2D (Collision2D collision);
}
public interface ICollisionStay2D
{
	void OnCollisionStay2D (Collision2D collision);
}
public interface ICollisionExit2D
{
	void OnCollisionExit2D (Collision2D collision);
}
public interface ITriggerEnter2D
{
	void OnTriggerEnter2D (Collider2D collider);
}
public interface ITriggerStay2D
{
	void OnTriggerStay2D (Collider2D collider);
}
public interface ITriggerExit2D
{
	void OnTriggerExit2D (Collider2D collider);
}
public interface CollisionBehaviour
{
	void OnCollisionEnter2D (Collision2D collision);
	void OnCollisionStay2D (Collision2D collision);
	void OnCollisionExit2D (Collision2D collision);
}
public interface TriggerBehaviour
{
	void OnTriggerEnter2D (Collider2D collider);
	void OnTriggerStay2D (Collider2D collider);
	void OnTriggerExit2D (Collider2D collider);
}
public class CollisionTriggerBehaviour : MonoBehaviour,
TriggerBehaviour,
CollisionBehaviour
{
	public void OnCollisionEnter2D (Collision2D collision)
	{
		NotifyEnter (collision.gameObject);
	}
	public void OnTriggerEnter2D (Collider2D collider)
	{
		NotifyEnter (collider.gameObject);
	}
	
	public void OnCollisionExit2D (Collision2D collision)
	{
		NotifyExit (collision.gameObject);
	}
	public void OnTriggerExit2D (Collider2D collider)
	{
		NotifyExit (collider.gameObject);
	}
	
	public void OnCollisionStay2D (Collision2D collision)
	{
		NotifyStay (collision.gameObject);
	}
	public void OnTriggerStay2D (Collider2D collider)
	{
		NotifyStay (collider.gameObject);
	}
		
	public virtual void NotifyEnter (GameObject gameObj)
	{
	}
	public virtual void NotifyExit (GameObject gameObj)
	{
	}
	public virtual void NotifyStay (GameObject gameObj)
	{
	}
}
