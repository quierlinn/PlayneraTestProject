using UnityEngine;

public class ItemsMover : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    private bool isDragging;
    [SerializeField] private float offset = 0.5f;
    [SerializeField] private CameraMovement cameraMovement;
    private void OnMouseDown()
    {
        isDragging = true;
        rigidBody.isKinematic = true;
        cameraMovement.enabled = false;
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;
        Vector3 mousePosition = cameraMovement.gameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rigidBody.isKinematic = false;
        cameraMovement.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.GetComponent<Shelf>()) return;
        transform.position = new Vector3(transform.position.x, collision.collider.transform.position.y + offset, 0);
        
        rigidBody.isKinematic = true;
    }

    private void Update()
    {
        var screenPos = cameraMovement.gameObject.GetComponent<Camera>().WorldToViewportPoint(transform.position);
        if (!(screenPos.y > 1)) return;
        isDragging = false;
        rigidBody.isKinematic = true;

        var newY = cameraMovement.gameObject.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        transform.position = new Vector3(transform.position.x, newY - 0.1f, 0);
    }
}