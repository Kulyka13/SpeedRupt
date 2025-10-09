using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private GameObject[] wayPoints;
	private int nextWayPoint = 1;
	private Rigidbody2D rb;

	private void Awake()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		Vector2 target = wayPoints[nextWayPoint].transform.position;
		Vector2 newPos = Vector2.MoveTowards(rb.position, target, moveSpeed * Time.fixedDeltaTime);
		rb.MovePosition(newPos);

		if (Vector2.Distance(rb.position, target) < 0.2f)
		{
			TakeTurn();
		}
	}
	private void TakeTurn()
	{
		Vector3 currRot = transform.eulerAngles;
		currRot.z += wayPoints[nextWayPoint].transform.eulerAngles.z;
		transform.eulerAngles = currRot;
		ChooseNextWayPoint();
	}
	private void ChooseNextWayPoint()
	{
		nextWayPoint++;

		if(nextWayPoint == wayPoints.Length)
		{
			nextWayPoint = 0;
		}
	}
}
