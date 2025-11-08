using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyMovement : MonoBehaviour
{
	[SerializeField] private float _speed;

	private Rigidbody2D _rigidbody;
	private PlayerAwarenessController _playerAwarenessController;
	private Vector2 _targetDirection;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();
		_playerAwarenessController = GetComponent<PlayerAwarenessController>();
	}

	private void FixedUpdate()
	{
		UpdateTargetDirection();
		SetVelocity();
	}

	private void UpdateTargetDirection()
	{
		if (_playerAwarenessController.AwareOfPlayer)
		{
			_targetDirection = _playerAwarenessController.DirectionToPlayer.normalized;
		}
		else
		{
			_targetDirection = Vector2.zero;
		}
	}

	private void SetVelocity()
	{
		if (_targetDirection == Vector2.zero)
			_rigidbody.velocity = Vector2.zero;
		else
			_rigidbody.velocity = _targetDirection * _speed;
	}
}
