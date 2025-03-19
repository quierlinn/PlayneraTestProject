using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.1f;
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private Vector3 lastTouchPosition;

    private void Start()
    {
        lastTouchPosition = Vector3.zero;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (Input.touchCount <= 0) return;
        var touch = Input.GetTouch(0);
        switch (touch.phase)
        {
            case TouchPhase.Began:
                lastTouchPosition = touch.position;
                break;
            case TouchPhase.Moved:
            {
                Vector3 currentTouchPosition = touch.position;

                var offset = Camera.main.ScreenToWorldPoint(currentTouchPosition) - Camera.main.ScreenToWorldPoint(lastTouchPosition);
                offset.z = 0;
                var newPosition = transform.position - offset;
                newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x, maxBounds.x);
                newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y, maxBounds.y);

                transform.position = newPosition;
                lastTouchPosition = currentTouchPosition;
                break;
            }
        }
    }
}