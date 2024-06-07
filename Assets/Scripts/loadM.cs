using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TableC
{
    public float euler;
    public float xRPM;
    public float xTime;
    public float aTime;
    public float mass;
    public float height;
    public float inY;
    public float inZ;
    public int att;

    public TableC(int meta, float a, float b, float c, float d, float e, float f, float g, float h)
    {
        this.att = meta;
        this.xRPM = a;
        this.xTime = b;
        this.aTime = c;
        this.mass = d;
        this.height = e;
        this.inY = f;
        this.inZ = g;
        this.euler = h;

        
    }
}

public class loadM : MonoBehaviour

{

    [SerializeField] private int size = 25;
    [SerializeField] private Color color = Color.red;
    //public string mainScene;
    [SerializeField]
    private int progressPercentage = -1; 
    [SerializeField]
    public List<TableC> rnv0 = new List<TableC>();
    string text;
    void Start(){
        StartCoroutine(eCSV());
    }

    IEnumerator eCSV(){
    rnv0.Clear();
        for (int _ = 1; _ < PlayerPrefs.GetInt("rep") + 1; _++)
        {
            int att = _;
            float xRPM = PlayerPrefs.GetFloat("maxRPMvar" + _.ToString());
            float xTime = PlayerPrefs.GetFloat("maxTimevar" + _.ToString());
            float aTime = PlayerPrefs.GetFloat("delayVar" + _.ToString());
            float mass = PlayerPrefs.GetFloat("massVar" + _.ToString());
            float height = PlayerPrefs.GetFloat("record" + _.ToString());
            float inZ = PlayerPrefs.GetFloat("lauy" + _.ToString());
            float inY = PlayerPrefs.GetFloat("lauz" + _.ToString());
            float euler = PlayerPrefs.GetFloat("angle" + _.ToString());

            TableC table = new TableC(att, xRPM, xTime, aTime, mass, height, inY, inZ, euler);
            rnv0.Add(table);
        }
        //yield return new WaitForSeconds(999);
        yield return null;
        StartCoroutine(loadM_Coroutine());
    }

    IEnumerator loadM_Coroutine(){
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
        else{
            text = "Exporting..";
        }

        GUI.Label(rect, text, style);
    }
}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;*/