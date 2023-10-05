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

        if (Application.isEditor) {
            return;
        }

        Ray ray = new Ray();
        RaycastHit hit = new RaycastHit();
        GameObject target = null;

        if (!Application.isMobilePlatform && Input.GetMouseButtonDown(0)) {
            ray = this.mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            if (Physics.Raycast(ray, out hit)) {
                target = hit.collider.gameObject;
            }
        }
        else if (Application.isMobilePlatform && Input.touches[0].phase == TouchPhase.Began) {
            ray = this.mainCamera.ScreenPointToRay(Input.touches[0].position);
            if (Physics.Raycast(ray, out hit)) {
                target = hit.collider.gameObject;
            }
        }

        if (target != null) {
            CSS3DIframe iframe = target.GetComponent<CSS3DIframe>();

            if (iframe != null) {
                WebClickHandler.ClickIframe(iframe.id);
            }
            else if (target.name == "Next Button") {
                WebClickHandler.ClickNextButton(hit.collider.gameObject.GetComponentInParent<CSS3DIframe>().id);
            }
            else if (target.name == "Previous Button") {
                WebClickHandler.ClickPreviousButton(hit.collider.gameObject.GetComponentInParent<CSS3DIframe>().id);
            }
        }
    }
}
