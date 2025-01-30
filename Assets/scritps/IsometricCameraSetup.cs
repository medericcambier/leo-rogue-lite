using UnityEngine;

[ExecuteInEditMode] 
public class IsometricCameraSetup : MonoBehaviour
{
    [Header("Position Offset")]
    public Vector3 offset = new Vector3(10f, 10f, -10f); 

    [Header("Rotation")]
    public float rotationX = 30f; 
    public float rotationY = 45f; 
    [Header("Camera Settings")]
    public float orthographicSize = 5f; 

    void SetupCamera()
    {
        
        Camera camera = GetComponent<Camera>();
        if (camera == null)
        {
            Debug.LogError("Ce script doit être attaché à un objet ayant une caméra !");
            return;
        }

        camera.orthographic = true; 
        camera.orthographicSize = orthographicSize;

        
        transform.position = offset;

       
        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);
    }

    
    void Start()
    {
        SetupCamera();
    }

#if UNITY_EDITOR
    
    void Update()
    {
        if (!Application.isPlaying)
        {
            SetupCamera();
        }
    }
#endif
}
