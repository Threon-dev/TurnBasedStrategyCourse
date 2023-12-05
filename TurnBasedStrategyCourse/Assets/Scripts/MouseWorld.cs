using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    private static MouseWorld _instance;
    [SerializeField] private LayerMask mousePLanePlayerMask;

    private void Awake()
    {
        _instance = this;
    }

    public static Vector3 GetPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputManager.Instance.GetMouseScreenPosition()); 
        Physics.Raycast(ray, out RaycastHit raycastHit,float.MaxValue,_instance.mousePLanePlayerMask);
        return raycastHit.point;
    }
}
