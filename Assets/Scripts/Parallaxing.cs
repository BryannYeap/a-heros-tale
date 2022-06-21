using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    
    public Transform[] backgrounds;         // List of all elements that will be parallaxed
    private float[] parallaxScales;         // Proportion of the camera's movement to move the backgrounds by
    public float smoothing = 1f;            // How smooth the parallax is going to be (Make sure > 0)

    private Transform cam;                  // Reference to the main camera's transform
    private Vector3 previousCamPosition;    // Store the position of the camera in the previous frame

    // Called before Start()
    private void Awake()
    {
        cam = Camera.main.transform;
    }

    // Start is called before the first frame update
    void Start()
    {
        // The previous frame has the current frame's camera position
        previousCamPosition = cam.position;

        // Assiging corresponding parallax scales;
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // Parallax is the opposite of the camera movement because previous frame multiplied by the scale
            float parallax = (previousCamPosition.x - cam.position.x) * parallaxScales[i];

            // Set a target x position which is the current position plus the parallax
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;

            // Create a target position which is the background's current position with its target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

            // Fade between current position and target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.fixedDeltaTime);
        }

        // Set the previous cam position to the camera's position at the end of the frame
        previousCamPosition = cam.position;
    }
}
