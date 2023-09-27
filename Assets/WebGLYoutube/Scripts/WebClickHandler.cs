using System.Runtime.InteropServices;
using UnityEngine;


public class WebClickHandler : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void ClickIframe(string id);

    [DllImport("__Internal")]
    private static extern void ClickNextButton(string id);

    [DllImport("__Internal")]
    private static extern void ClickPreviousButton(string id);


    private Camera mainCamera;

    private void Start() {
        this.mainCamera = Camera.main;
    }

    private void Update() {
#if UNITY_WEBGL && !UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = this.mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                CSS3DIframe iframe = hit.collider.gameObject.GetComponent<CSS3DIframe>();
                if (iframe != null) {
                    ClickIframe(iframe.id);
                }
                else if (hit.collider.gameObject.name == "Next Button") {
                    ClickNextButton(hit.collider.gameObject.GetComponentInParent<CSS3DIframe>().id);
                }
                else if (hit.collider.gameObject.name == "Previous Button") {
                    ClickPreviousButton(hit.collider.gameObject.GetComponentInParent<CSS3DIframe>().id);
                }
            }
        }
#endif
    }
}