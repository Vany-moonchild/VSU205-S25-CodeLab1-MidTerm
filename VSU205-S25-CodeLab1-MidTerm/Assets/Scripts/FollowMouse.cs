using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    private Camera mainCamera;
    
    [SerializeField]
    private float maxSpeed = 10f;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        
        
    }

    // Update is called once per frame
    void Update()
    {

            FollowMousePositionDelayed(maxSpeed);
    }

    private void FollowMousePosition()
    {
        transform.position = GetWorldMousePositionFromMouse();
    }

    private void FollowMousePositionDelayed(float maxSpeed)
    {
        transform.position = Vector2.MoveTowards(transform.position, GetWorldMousePositionFromMouse(), maxSpeed * Time.deltaTime);
    }

    private Vector2 GetWorldMousePositionFromMouse()
    {
        return mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }


    
}
