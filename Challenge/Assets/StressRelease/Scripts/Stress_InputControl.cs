using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stress_InputControl : MonoBehaviour
{
    InputField textInput;
    RectTransform textInput_Rect;
    Vector2 offset = new Vector2(2.0f, 0.0f);

    public bool move = true;

    // void OnGUI(){ 
    //     string[] selString = System.Enum.GetNames( typeof(IMECompositionMode) );
	// 	Input.imeCompositionMode = (IMECompositionMode)GUILayout.SelectionGrid( (int)Input.imeCompositionMode,selString,selString.Length );
	// }

    void Start() {
        textInput = GetComponent<InputField>();
        textInput_Rect = GetComponent<RectTransform>();
    }

    public void moveX(float x){
        textInput_Rect.anchoredPosition = new Vector2(x, textInput_Rect.anchoredPosition.y);
    }

    public string getText(){
        string tv_text = textInput.text;
        // textInput.text = ""; # TODO: remove text after shoot?
        // Debug.Log("TEXT LEN: " + tv_text.Length);
        return tv_text;
    }

    public void disableMove(){
        move = false;
    }

    public void enableMove(){
        move = true;
    }
}
