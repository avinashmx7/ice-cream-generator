using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour {
    [SerializeField] private GameObject cylinderPrefab;
    [SerializeField] private PathCreator pathCreator;
    private Button _dummyButton;
    private Queue<CustomPathFollower> _cylinderPrefabs;
    private bool _generateCream = false, isGameCompleted = false;
    private Color _currentSelectColor = Color.red;

    private void Start() {
        _cylinderPrefabs = new Queue<CustomPathFollower>();
    }

    private void Update() {
        if (!Input.GetMouseButton(0) || isGameCompleted || !_generateCream) return;
        var cylinder = Instantiate(cylinderPrefab, transform);
        cylinder.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].color = _currentSelectColor;
        var customPathCreator = cylinder.GetComponent<CustomPathFollower>();
        customPathCreator.PathCreator = pathCreator;
        _cylinderPrefabs.Enqueue(customPathCreator);

        foreach (var pathFollower in _cylinderPrefabs) {
            pathFollower.ShiftPosition();
        }

        if (_cylinderPrefabs.TryPeek(out var customPath)) {
            isGameCompleted = customPath.IsPathCompleted();
        }
    }


    public void SetColor(string colorName) {
        _currentSelectColor = colorName switch {
            "Red" => Color.red,
            "Blue" => Color.blue,
            "Green" => Color.green,
            _ => Color.red
        };
        _generateCream = true;
    }
    
    public void StopCreamGenerate() {
        _generateCream = false;
    }

}