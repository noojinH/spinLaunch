using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

[System.Serializable]
public class PointData
{
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
    private GameObject pointObject;
    [SerializeField]
    List<PointData> rnv = new List<PointData>();
    [SerializeField]
    private int up;
    [SerializeField]
    private float scaleFactor = 0.1f;  // 축소 비율

    public void Load()
    {
        rnv.Clear();
        for (int _ = 1; _ < PlayerPrefs.GetInt("attempt") + 1; _++)
        {
            float x0 = PlayerPrefs.GetFloat("maxRPMvar" + _.ToString());
            float y0 = PlayerPrefs.GetFloat("maxTimevar" + _.ToString());
            float z0 = PlayerPrefs.GetFloat("delayVar" + _.ToString());
            float add1 = PlayerPrefs.GetFloat("massVar" + _.ToString());
            float add2 = PlayerPrefs.GetFloat("record" + _.ToString());

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
        whens();
    }

    public void whens()
    {
        Load();
        DisplayPoints();
        success.SetActive(false);
        fail.SetActive(false);
    }

    void DisplayPoints()
    {
        Dictionary<Vector3, (PointData, int)> pointDictionary = new Dictionary<Vector3, (PointData, int)>();

        foreach (PointData point in rnv)
        {
            Vector3 position = new Vector3(point.x0 * scaleFactor, point.y0 * scaleFactor, point.z0 * scaleFactor);

            if (pointDictionary.ContainsKey(position))
            {
                var existingPoint = pointDictionary[position];
                pointDictionary[position] = (existingPoint.Item1, existingPoint.Item2 + 1);
            }
            else
            {
                pointDictionary.Add(position, (point, 1));
            }
        }

        foreach (var entry in pointDictionary)
        {
            Vector3 position = entry.Key;
            PointData point = entry.Value.Item1;
            int count = entry.Value.Item2;

            if (point.add2 > up || point.add2 == up)
            {
                pointObject = Instantiate(success, position, Quaternion.identity);
            }
            else
            {
                pointObject = Instantiate(fail, position, Quaternion.identity);
            }

            TextMeshPro[] textComponents = pointObject.GetComponentsInChildren<TextMeshPro>();
            if (textComponents.Length >= 2)
            {
                TextMeshPro text0 = textComponents[0];
                TextMeshPro text1 = textComponents[1];
                TextMeshPro text3 = textComponents[2];

                text0.text = point.add1.ToString() + "kg";
                text1.text = point.add2.ToString() + "m";

                if (count > 1)
                {
                    text3.text += " x" + count.ToString();
                }
                else
                {
                    text3.text = " ";
                }
            }
            else
            {
                Debug.LogError("Expected at least 2 TextMeshPro components in the prefab.");
            }
        }
    }

    public void back()
    {
        StartCoroutine(toHome());
    }

    IEnumerator toHome()
    {
        yield return new WaitForEndOfFrame();
        SceneManager.LoadScene(0);
    }
}