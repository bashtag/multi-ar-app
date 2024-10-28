using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class SetVideoScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public Renderer screenRenderer;

    void Start()
    {
        videoPlayer.renderMode = VideoRenderMode.APIOnly;
        videoPlayer.prepareCompleted += OnVideoPrepared;
        videoPlayer.Prepare();
    }

    void OnVideoPrepared(VideoPlayer vp)
    {
        // (Element 1 - VideoMaterial)
        screenRenderer.materials[1].mainTexture = videoPlayer.texture;
    }
}
