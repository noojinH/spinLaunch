using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class conc : MonoBehaviour
{
    [SerializeField]
    private GameObject loader0;
    private List<TableC> rnv1;

    void Start()
    {
        rnv1 = loader0.GetComponent<loadM>().rnv0;
        StartCoroutine(ter());
    }

    IEnumerator ter(){
    string filePath = Application.persistentDataPath + "/Assets/Exported.csv";

        using(StreamWriter writer = new StreamWriter(filePath)){
            writer.WriteLine("Attempt,MaxRPM,MaxTaken,LaunchTimer, mass, record, initX, initY, angle");
            foreach (var field in rnv1){
                writer.WriteLine($"{field.att}, {field.xRPM},{field.xTime}, {field.aTime}, {field.mass}, {field.height}, {field.inY}, {field.inZ}, {field.euler}");
            }
        }
        
        Debug.Log("Data exported to " + filePath);
        yield return null;
    }
}
