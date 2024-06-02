using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
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


public class Load0 : MonoBehaviour
{
    [SerializeField] private int size = 50;
    [SerializeField] private Color color = Color.red;

    [SerializeField]
    private GameObject floadM;
    
    [SerializeField]
    List<TableC> rnv0 = new List<TableC>();
    void Start()
    {
        floadM.SetActive(false);
        StartCoroutine(eCSV());
    }

    IEnumerator eCSV(){
        rnv0.Clear();
        for (int _ = 1; _ < PlayerPrefs.GetInt("rep") + 1; _++)
        {
            int att = PlayerPrefs.GetInt("rep");
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

        string filePath = Application.persistentDataPath + "/Exported.csv";

        using(StreamWriter writer = new StreamWriter(filePath)){
            writer.WriteLine("Attempt,MaxRPM,MaxTaken,LaunchTimer, mass, record, initX, initY, angle");
            foreach (var field in rnv0){
                writer.WriteLine($"{field.att}, {field.xRPM},{field.xTime}, {field.aTime}, {field.mass}, {field.height}, {field.inY}, {field.inZ}, {field.euler}");
            }
        }
        
        Debug.Log("Data exported to " + filePath);
        floadM.SetActive(true);
        yield return null;
    }

    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();

        Rect rect = new Rect(30, 30, Screen.width, Screen.height);
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = size;
        style.normal.textColor = color;
        string text = "Exporting..";
        /*float ms = deltaTime * 1000f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS ({1:0.0} ms)", fps, ms);*/

        GUI.Label(rect, text, style);
    }
}
