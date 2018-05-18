using UnityEngine;

public class BackCameraScript : MonoBehaviour {

    public Camera mainCamera;
    public Camera curCamera;

    void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            curCamera.tag = "MainCamera";
            mainCamera.tag = "Untagged";
            curCamera.enabled = true;
            mainCamera.enabled = false;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            mainCamera.tag = "MainCamera";
            curCamera.tag = "Untagged";
            mainCamera.enabled = true;
            curCamera.enabled = false;
        }
    }
}
