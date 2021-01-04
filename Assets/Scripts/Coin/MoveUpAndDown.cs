using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
	private Rigidbody2D m_Rigidbody2D;
	public float heightTarget = 100;
	public float initOffset = 0;
	public enum Direction {up, down};
	public float duration = 1.0f;
	public Direction direction;
	private Vector2 topPos;
	private Vector2 bottomPos;
	private float startTime;
	private float? fakeStartTime;
	private float fakeStartTimeAmt;
	private Vector2 velocity = Vector2.zero;

	private void Start()
	{
		float heightTargetCoor = heightTarget / 100f;
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
		topPos = new Vector2(transform.position.x, transform.position.y - heightTargetCoor);
		bottomPos = new Vector2(transform.position.x, transform.position.y + heightTargetCoor);
		transform.position = new Vector2(transform.position.x, transform.position.y + (initOffset / 100));
		startTime = Time.time;
		fakeStartTimeAmt = ((heightTarget - Mathf.Abs(initOffset)) / heightTarget * duration);
		fakeStartTime = Time.time + fakeStartTimeAmt;
	}

	void toggleDirection()
    {
		direction = direction == Direction.up ? Direction.down : Direction.up;
    }
	float directionAsNum()
	{
		return  direction == Direction.up ? 1 : -1;
	}

	void FixedUpdate()
    {
		if(fakeStartTime != null && Time.time + fakeStartTimeAmt - startTime >= duration)
        {
			toggleDirection();
			startTime = Time.time;
			fakeStartTime = null;
		}
		else if (fakeStartTime == null && Time.time - startTime >= duration)
		{
			toggleDirection();
			startTime = Time.time;
			fakeStartTime = null;
		}
		float t = fakeStartTime != null ?
			(Time.time + fakeStartTimeAmt - startTime) / duration :
			(Time.time - startTime) / duration;
		transform.position = direction == Direction.up ?
			new Vector2(transform.position.x, Mathf.SmoothStep(topPos.y, bottomPos.y, t)) :
			new Vector2(transform.position.x, Mathf.SmoothStep(bottomPos.y, topPos.y, t));

    }
}
