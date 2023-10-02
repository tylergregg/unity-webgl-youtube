using Newtonsoft.Json.Linq;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class YoutubeManager : MonoBehaviour {

    [DllImport("__Internal")]
    private static extern void SetupYoutube();

    private CSS3DIframe[] iframes;
    private int iframeReadyCount;

    private void Awake() {
        this.iframes = Object.FindObjectsByType<CSS3DIframe>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
    }


    private void IframeCreated() {
        this.iframeReadyCount++;
        if (this.iframeReadyCount == this.iframes.Length) {
            YoutubeManager.SetupYoutube();
        }
    }


    public void SetTitle(string titleJSON) {
        var array = JArray.Parse(titleJSON);
        string id = array[0].Value<string>();
        CSS3DIframe iframe = this.iframes.First(iframe => iframe.id == id);
        iframe.SetTitle(array[1].Value<string>());
    }
}
