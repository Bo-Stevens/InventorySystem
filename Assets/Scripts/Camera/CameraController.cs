using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    [SerializeField] GameObject following;
    [SerializeField] Vector2 cameraOffsetFromCenter;
    Camera cam;

    private void Awake()
    {
        Debug.Log("AWAKE!");
        Instance = this;
        cam = GetComponent<Camera>();
    }
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(following.transform.position.x + cameraOffsetFromCenter.x, following.transform.position.y + cameraOffsetFromCenter.y, transform.position.z);
    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(following.transform.position.x + cameraOffsetFromCenter.x, following.transform.position.y + cameraOffsetFromCenter.y, transform.position.z);

    }

    public Vector2 GetScreenSize()
    {
        return new Vector2(cam.aspect * cam.orthographicSize * 2, cam.orthographicSize * 2);
    }
}
