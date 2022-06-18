using UnityEngine;
using PathCreation;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {
    [SerializeField] private GameObject cylinderPrefab;
    [SerializeField] private PathCreator pathCreator;
    private Button _dummyButton;
    private bool _generateCream = false, isGameCompleted = false;
    private Color _currentSelectColor = Color.red;
    private int _currentIndex;

    private void Start() {
        _currentIndex = pathCreator.path.NumPoints - 1;
    }

    private void Update() {
        if (!Input.GetMouseButton(0) || isGameCompleted || !_generateCream) return;
        var cylinder = Instantiate(cylinderPrefab, transform);
        cylinder.transform.GetChild(0).GetComponent<MeshRenderer>().materials[0].color = _currentSelectColor;
        cylinder.transform.position = pathCreator.path.GetPoint(_currentIndex);
        cylinder.transform.rotation = Quaternion.LookRotation(pathCreator.path.GetTangent(_currentIndex));
        _currentIndex--;

        if (_currentIndex == 0) {
            isGameCompleted = true;
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