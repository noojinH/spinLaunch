using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scatterPlot : MonoBehaviour
{
    public GameObject success;
    public GameObject fail;
    [SerializeField]
    private float[] rnv;

    void Start()
    {
        rnv = new float[4];
        rnv[1] = PlayerPrefs.GetFloat("maxRPMvar" + null);
        rnv[2] = PlayerPrefs.GetFloat("maxTimeVar" + null);
        rnv[3] = PlayerPrefs.GetFloat("delayVar" + null);
        rnv[4] = PlayerPrefs.GetFloat("massVar" + null);
        rnv[5] = PlayerPrefs.GetFloat("record" + null);
    }
}
