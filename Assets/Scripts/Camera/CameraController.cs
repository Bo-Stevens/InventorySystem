using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    [SerializeField] GameObject following;
    [SerializeField] Vector2 cameraOffsetFromCenter;

    private void Awake()
    {
        Instance = this;
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
}
