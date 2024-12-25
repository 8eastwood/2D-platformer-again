using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrolBehavior : MonoBehaviour
{
    [SerializeField] private PlayerDetector _playerDetector;
    [SerializeField] private List<Waypoint> _waypoints;
    [SerializeField] private float _speed = 2f;

    private Quaternion _inverseRotation = Quaternion.Euler(0, 180, 0);
    private Rigidbody2D _rigidbody;
    private Transform _currentWaypointToGo;
    private Player _player;
    private float _distanceToWaypoint = 0.5f;
    private bool _isPlayerNear = false;
    private bool _isChaseActive = false;
    private int _waypointIndexA = 0;
    private int _waypointIndexB = 1;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentWaypointToGo = _waypoints[_waypointIndexA].transform;
    }

    private void OnEnable()
    {
        _playerDetector.PlayerEntered += Chase;
        _playerDetector.PlayerEscaped += Patrol;
    }

    private void FixedUpdate()
    {
        if (_isChaseActive & _player != null)
        {
            float directionChanger = -1f;
            Vector2 point = _player.transform.position - transform.position;

            if (_player.transform.position.x > transform.position.x)
            {
                _rigidbody.velocity = new Vector2(_speed, 0);
                transform.rotation = _inverseRotation;
            }
            else
            {
                _rigidbody.velocity = new Vector2(_speed * directionChanger, 0);
                transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            Patrol();

            float directionChanger = -1f;
            Vector2 point = _currentWaypointToGo.position - transform.position;

            if (_currentWaypointToGo == _waypoints[_waypointIndexB].transform)
            {
                _rigidbody.velocity = new Vector2(_speed, 0);
                transform.rotation = _inverseRotation; 
            }
            else
            {
                _rigidbody.velocity = new Vector2(_speed * directionChanger, 0);
                transform.rotation = Quaternion.identity;
            }

            if ((transform.position - _currentWaypointToGo.position).sqrMagnitude < _distanceToWaypoint * _distanceToWaypoint)
            {
                _currentWaypointToGo = _currentWaypointToGo == _waypoints[_waypointIndexB].transform ? _waypoints[_waypointIndexA].transform : _waypoints[_waypointIndexB].transform;
            }
        }
    }

    private void OnDisable()
    {
        _playerDetector.PlayerEntered -= Chase;
        _playerDetector.PlayerEscaped -= Patrol;
    }

    private void Chase(Player player)
    {
        _player = player;

        _isChaseActive = true;
    }

    private void Patrol()
    {
        _isChaseActive = false;
    }
}
