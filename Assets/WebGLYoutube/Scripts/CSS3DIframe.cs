using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;

public class CSS3DIframe : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void CreateIframe(string id, string videoIdListJSON, string listType, string listId,
        bool autoplay, bool loop, bool shuffle, float width, float height, 
        float xPosition, float yPosition, float zPosition, float yRotation);

    public string id = "";
    public List<string> videoIdList;
    public bool autoplay = false;
    public bool loop = false;
    public bool shuffle = false;
    public enum ListType { playlist, userUploads };
    public ListType listType;
    public string listId = "";

    public TextMeshPro titleText;
    public GameObject nextButton;
    public GameObject previousButton;

    private void Start() {
        this.nextButton.SetActive(this.hasPlaylist());
        this.previousButton.SetActive(this.hasPlaylist());
#if UNITY_WEBGL && ! UNITY_EDITOR
        CreateIframe(this.id, JsonConvert.SerializeObject(this.videoIdList), 
            this.listType == ListType.playlist ? "playList" : "user_uploads", this.listId,
            this.autoplay, this.loop, this.shuffle, this.transform.localScale.x, this.transform.localScale.y,
            this.transform.position.x, this.transform.position.y, this.transform.position.z, this.transform.eulerAngles.y);
#endif
    }

    private bool hasPlaylist() {
        return this.videoIdList.Count > 1 || this.listId.Length > 0;
    }


    public void SetTitle(string title) {
        if (this.titleText != null) {
            this.titleText.text = title;
        }
    }
}
