using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadM : MonoBehaviour
{

    [SerializeField] private int size = 25;
    [SerializeField] private Color color = Color.red;
    //public string mainScene;
    [SerializeField]
    private int progressPercentage = -1; 
    string text;
    void Start(){
        StartCoroutine(loadM_Coroutine());
    }

    IEnumerator loadM_Coroutine(){
        
        yield return new WaitForEndOfFrame();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(0);
        while(!asyncLoad.isDone){
            float progress = asyncLoad.progress;
            progressPercentage = Mathf.RoundToInt(progress * 100f);
            yield return null;
        }
    }

     private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(30, 30, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;
        if(progressPercentage != -1){
            text = progressPercentage.ToString();
        }
        /*float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);*/

        GUI.Label(rect, text, style);
    }
}
