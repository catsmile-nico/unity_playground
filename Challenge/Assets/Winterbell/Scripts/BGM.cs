using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    AudioSource audioSource;
    Button button;

    [SerializeField] Sprite speakerImage1;
    [SerializeField] Sprite speakerImage2;

    private bool playback = true;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();
    }

    public void playbackControl(){
        if (playback){
            audioSource.Pause();
            button.image.sprite = speakerImage2;
            playback = false;
        } else{
            audioSource.UnPause();
            button.image.sprite = speakerImage1;
            playback = true;
        }
    }
}
