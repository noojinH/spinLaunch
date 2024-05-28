using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[System.Serializable]
public class PointData{
    public float x0;
    public float y0;
    public float z0;
    public float add1;
    public float add2;

    public PointData(float x, float y, float z, float ad1, float ad2)
    {
        this.x0 = x;
        this.y0 = y;
        this.z0 = z;
        this.add1 = ad1;
        this.add2 = ad2;
    }
}

public class scatterPlot : MonoBehaviour
{
    public GameObject success;
    public GameObject fail;
    [SerializeField]
    List<PointData> rnv = new List<PointData>();

    public void Load(){
        rnv.Clear();
        for(int _ = 1; _ < PlayerPrefs.GetInt("attempt") + 1; _++){
        float x0 = PlayerPrefs.GetFloat("maxRPMvar" + _.ToString());
        float y0 = PlayerPrefs.GetFloat("maxTimevar" + _.ToString());
        float z0 = PlayerPrefs.GetFloat("delayVar" +  _.ToString());
        float add1 = PlayerPrefs.GetFloat("massVar" +  _.ToString());
        float add2 = PlayerPrefs.GetFloat("record" +  _.ToString());

        PointData newPoint = new PointData(x0, y0, z0, add1, add2);
        rnv.Add(newPoint);
    }

        for (int i = 0; i < rnv.Count; i++)
        {
            PlayerPrefs.SetFloat("point" + i + "_x", rnv[i].x0);
            PlayerPrefs.SetFloat("point" + i + "_y", rnv[i].y0);
            PlayerPrefs.SetFloat("point" + i + "_z", rnv[i].z0);
            PlayerPrefs.SetFloat("point" + i + "_ad1", rnv[i].add1);
            PlayerPrefs.SetFloat("point" + i + "_ad2", rnv[i].add2);
        }
        PlayerPrefs.SetInt("pointCount", rnv.Count);
    }

    void Start()
    {
       int pointCount = PlayerPrefs.GetInt("pointCount", 0);
       
       for (int i = 0; i < pointCount; i++)
        {
            float x0 = PlayerPrefs.GetFloat("point" + i + "_x");
            float y0 = PlayerPrefs.GetFloat("point" + i + "_y");
            float z0 = PlayerPrefs.GetFloat("point" + i + "_z");
            float add1 = PlayerPrefs.GetFloat("point" + i + "_ad1");
            float add2 = PlayerPrefs.GetFloat("point" + i + "_ad2");

            PointData loadedPoint = new PointData(x0, y0, z0, add1, add2);
            rnv.Add(loadedPoint);
    }
}

public void back(){
    StartCoroutine(toHome());
}

IEnumerator toHome(){
    yield return new WaitForEndOfFrame();
    SceneManager.LoadScene(0);
}
}