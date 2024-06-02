using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class loadM : MonoBehaviour
{

    [SerializeField]
    private int progressPercentage = -1;
    [SerializeField] private int size = 25;
    [SerializeField] private Color color = Color.red;

    void Start(){
        StartCoroutine(loadM_Coroutine());
    }

    IEnumerator loadM_Coroutine(){
        yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        asyncLoad.allowSceneActivation = false;

        while(!asyncLoad.isDone){
            float time = Time.time;
            float progress = asyncLoad.progress;
            progressPercentage = Mathf.RoundToInt(progress * 100f);
            if(time > 0.5f){
                asyncLoad.allowSceneActivation = true;
            }
        }
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(30, 30, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;
        string text = progressPercentage.ToString();

        GUI.Label(rect, text, style);
    }
}
