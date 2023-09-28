using System.Runtime.InteropServices;
using UnityEngine;

public class WebCameraSync : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void SyncCameraTransform(float x, float y, float z, float rw, float rx, float ry, float rz);


    private Camera mainCamera;

    private void Start() {
        this.mainCamera = Camera.main;
    }

    private void Update() {
#if UNITY_WEBGL && ! UNITY_EDITOR
        WebCameraSync.SyncCameraTransform(this.mainCamera.transform.position.x, this.mainCamera.transform.position.y, this.mainCamera.transform.position.z,
            this.mainCamera.transform.rotation.w, this.mainCamera.transform.rotation.x, this.mainCamera.transform.rotation.y, this.mainCamera.transform.rotation.z);
#endif
    }
}
