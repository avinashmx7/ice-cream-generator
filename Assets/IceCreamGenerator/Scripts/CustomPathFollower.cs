using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class CustomPathFollower : MonoBehaviour {
    [SerializeField] private EndOfPathInstruction endOfPathInstruction;
    internal PathCreator PathCreator;
    public float speed = 5;
    private float _distanceTravelled;
    private int _currentIndex;
    private bool _isPathCompleted;
    private Vector3 _offSet = new Vector3(-90f, -90f, -90f);

    private void Start() {
        if (PathCreator != null) {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            PathCreator.pathUpdated += OnPathChanged;
        }
    }

    internal void ShiftPosition() {
        if (PathCreator == null || _isPathCompleted) return;
        transform.position = PathCreator.path.GetPoint(_currentIndex);
        transform.rotation = Quaternion.LookRotation(PathCreator.path.GetTangent(_currentIndex));

        if (++_currentIndex != PathCreator.path.NumPoints) return;
        Debug.Log("Path completed.");
        _isPathCompleted = true;
    }

    internal bool IsPathCompleted() {
        return _isPathCompleted;
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    private void OnPathChanged() {
        _distanceTravelled = PathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }
}