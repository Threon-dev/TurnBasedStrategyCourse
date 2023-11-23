using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour
{
    [SerializeField] private TextMeshPro textMesh;
    private GridObject _gridObject;

    public void SetGridObject(GridObject gridObject)
    {
        _gridObject = gridObject;
    }

    private void Update()
    {
        textMesh.text = _gridObject.ToString();
    }
}
