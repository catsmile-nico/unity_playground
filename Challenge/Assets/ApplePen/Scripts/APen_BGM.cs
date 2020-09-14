using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APen_BGM : MonoBehaviour
{
    AudioSource audioSource;
    Button button;

    [SerializeField] Sprite speakerImage1 = null;
    [SerializeField] Sprite speakerImage2 = null;

    private int playback = 1;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();

        if (PlayerPrefs.HasKey("BGM")) {
            playback = PlayerPrefs.GetInt("BGM");
            if (playback != 1)
                button.image.sprite = speakerImage2;
        }
    }

    private void Update() {
        if (playback != 1)
            audioSource.Pause();
    }

    public void playbackControl(){
        if (playback == 1){
            //audioSource.Pause(); this line is in update to account for pause on scene restart
            button.image.sprite = speakerImage2;
            playback = 0;
        } else {
            audioSource.UnPause(); 
            button.image.sprite = speakerImage1;
            playback = 1;
        }
        PlayerPrefs.SetInt("BGM",playback);
    }
}
