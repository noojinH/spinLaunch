using UnityEngine;
using TMPro;

public class Randomizer : MonoBehaviour
{
    // 다른 오브젝트를 참조할 수 있는 변수
    [SerializeField]
    private GameObject cylinder0;
    [SerializeField]
    private GameObject rocket0;

    // 다른 오브젝트에 있는 스크립트의 참조
    private DrillSimulation targetScript0;
    [SerializeField]
    private TextMeshProUGUI tmp0;

    void Start(){
        if(tmp0 == null){
            Debug.Log("Untargeted tmp0.");
        }

        if (cylinder0 != null && rocket0 != null)
        {
            // targetObject에서 TargetScript를 가져옵니다.
            targetScript0 = cylinder0.GetComponent<DrillSimulation>();

            if (targetScript0 != null)
            {
                if(PlayerPrefs.GetInt("attempt") != null){
                PlayerPrefs.SetInt("attempt", PlayerPrefs.GetInt("attempt") + 1);
                }
                else{
                    PlayersPrefs.SetInt("attempt", 1);
                }
                targetScript0.maxRPM = Random.Range(300f, 3000f);
                tmp0.text =  targetScript0.maxRPM.ToString() + "RPM";
                PlayerPrefs.SetFloat("maxRPMvar", targetScript0.maxRPM);

                targetScript0.maxRPMTime = Random.Range(3f, 6f);
                tmp0.text +=  " in " + targetScript0.maxRPMTime.ToString() +"s";
                PlayerPrefs.SetFloat("maxTimevar", targetScript0.maxRPMTime);

                rocket0.GetComponent<Rigidbody>().mass = Random.Range(0.5f, 1.5f);
                tmp0.text += "\n" + rocket0.GetComponent<Rigidbody>().mass.ToString() + "kg";
                PlayerPrefs.SetFloat("massVar", rocket0.GetComponent<Rigidbody>().mass);

                rocket0.GetComponent<Attacher>().delay = Random.Range(1f, 7f);
                tmp0.text += " \nlaunch: " + rocket0.GetComponent<Attacher>().delay.ToString()+"seconds";
                PlayerPrefs.SetFloat("delayVar", rocket0.GetComponent<Attacher>().delay);


                //Debug.Log("Randomized value: " + targetScript.maxRPM);
            }
            else
            {
                //Debug.LogError("TargetScript not found on targetObject.");
            }
        }
        else
        {
           //Debug.LogError("TargetObject is not assigned.");
        }
    }
}
