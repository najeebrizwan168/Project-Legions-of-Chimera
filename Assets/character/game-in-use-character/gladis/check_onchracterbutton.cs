using UnityEngine;

public class check_onchracterbutton : MonoBehaviour
{
    void Update()
    {
#if UNITY_EDITOR
        // Allow testing with mouse click in editor
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            CheckRay(ray);
        }
#else
        // Touch input for mobile (Android/iOS)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            CheckRay(ray);
        }
#endif
    }

    void CheckRay(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform)
            {
                working();
            }
        }
    }

    public void working()
    {
        Debug.Log("working");
    }
}
