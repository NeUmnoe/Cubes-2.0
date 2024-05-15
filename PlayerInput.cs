using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LayerMask _interactionLayers;

    private const int _leftMouseButton = 0;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _interactionLayers))
            {
                Splitter splitter = hit.collider.GetComponent<Splitter>();

                if (splitter != null)
                {
                    splitter.SplitCube();
                }
            }
        }
    }
}