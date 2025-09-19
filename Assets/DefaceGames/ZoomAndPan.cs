using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DefaceGames
{
    public class ZoomAndPan : MonoBehaviour
    {
        public float zoomSpeed = 0.1f;
        public float minZoom = 1f;
        public float maxZoom = 5f;
        public RectTransform layoutGroup;

        private void Update()
        {
            float newScale = transform.localScale.x;

#if UNITY_EDITOR || UNITY_STANDALONE
            // Zoom In/Out with mouse scroll in editor/PC
            float scrollValue = Input.GetAxis("Mouse ScrollWheel");
            newScale += scrollValue * zoomSpeed;
#endif

#if UNITY_ANDROID || UNITY_IOS
            // Pinch Zoom on Mobile
            if (Input.touchCount == 2)
            {
                Touch touch0 = Input.GetTouch(0);
                Touch touch1 = Input.GetTouch(1);

                Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
                Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

                float prevMagnitude = (touch0PrevPos - touch1PrevPos).magnitude;
                float currentMagnitude = (touch0.position - touch1.position).magnitude;

                float difference = currentMagnitude - prevMagnitude;

                newScale += difference * zoomSpeed * 0.01f; // scaled for sensitivity
            }
#endif

            // Clamp zoom
            newScale = Mathf.Clamp(newScale, minZoom, maxZoom);

            // Apply zoom if changed
            if (Mathf.Abs(newScale - transform.localScale.x) > 0.001f)
            {
                transform.localScale = new Vector3(newScale, newScale, 1f);
                LayoutRebuilder.ForceRebuildLayoutImmediate(layoutGroup);
            }
        }
    }
}
