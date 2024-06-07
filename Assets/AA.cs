using UnityEngine;
using TMPro; // Using TextMeshPro namespace
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DoubleClickHandler : MonoBehaviour
{
    public GameObject cube;
    public GameObject cylinder;
    public GameObject rocket;
    public GameObject canvas;
    public TMP_InputField maxRPMInputField;
    public TMP_InputField maxTimeInputField;
    public TMP_InputField massInputField;
    public TMP_InputField delayInputField;
    public Button doneButton;
    public Button cancelButton;

    private Randomizer cubeRandomizer;
    private DrillSimulation cylinderDrillSimulation;
    private Rigidbody rocketRigidbody;
    private Attacher rocketAttacher;
    [SerializeField]
    private TextMeshProUGUI tmp0;

     private float lastClickTime = 0f;
    private float doubleClickThreshold = 0.3f; // Adjust the threshold as needed

    void Start()
    {
        cylinderDrillSimulation = cylinder.GetComponent<DrillSimulation>();
        rocketRigidbody = rocket.GetComponent<Rigidbody>();
        rocketAttacher = rocket.GetComponent<Attacher>();
        cubeRandomizer = cube.GetComponent<Randomizer>();

        if(PlayerPrefs.GetInt("rand") == 1){
            cubeRandomizer.enabled = false;

        float maxRPM = PlayerPrefs.GetFloat("maxRPMvar");
        float maxTime = PlayerPrefs.GetFloat("maxTimevar");
        float mass = PlayerPrefs.GetFloat("massVar");
        float delay = PlayerPrefs.GetFloat("delayVar");

        cylinderDrillSimulation.maxRPM = maxRPM;
        cylinderDrillSimulation.maxRPMTime = maxTime;

        // Update Rocket Rigidbody mass
        rocketRigidbody.mass = mass;

        // Update Attacher script
        rocketAttacher.delay = delay;

        tmp0.text =  maxRPM.ToString() + "RPM";
         tmp0.text +=  " in " + maxTime.ToString() +"s";
         tmp0.text += "\n" + mass.ToString() + "kg";
          tmp0.text += " \nlaunch: " + delay.ToString()+"seconds";
        }

        doneButton.onClick.AddListener(OnDoneButtonClick);
        cancelButton.onClick.AddListener(OnCancelButtonClick);
        canvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastClickTime < doubleClickThreshold)
            {
                Time.timeScale = 0f;
                cubeRandomizer.enabled = false;
                PlayerPrefs.SetInt("rand", 1);
                canvas.SetActive(true);
            }
            lastClickTime = Time.time;
        }
    }

    void OnDoneButtonClick()
    {
        // Update values from input fields
        if(maxRPMInputField.text == null || maxTimeInputField.text == null || massInputField.text == null || delayInputField.text == null){
            Debug.Log("null");
        }
        else{
        float maxRPM = float.Parse(maxRPMInputField.text);
        float maxTime = float.Parse(maxTimeInputField.text);
        float mass = float.Parse(massInputField.text);
        float delay = float.Parse(delayInputField.text);

        // Update DrillSimulation script
        cylinderDrillSimulation.maxRPM = maxRPM;
        cylinderDrillSimulation.maxRPMTime = maxTime;

        // Update Rocket Rigidbody mass
        rocketRigidbody.mass = mass;

        // Update Attacher script
        rocketAttacher.delay = delay;

        // Save to PlayerPrefs
        PlayerPrefs.SetFloat("maxRPMvar", maxRPM);
        PlayerPrefs.SetFloat("maxTimevar", maxTime);
        PlayerPrefs.SetFloat("massVar", mass);
        PlayerPrefs.SetFloat("delayVar", delay);

        // Restore time and hide canvas
        Time.timeScale = 1f;
        canvas.SetActive(false);

        tmp0.text =  maxRPM.ToString() + "RPM";
         tmp0.text +=  " in " + maxTime.ToString() +"s";
         tmp0.text += "\n" + mass.ToString() + "kg";
          tmp0.text += " \nlaunch: " + delay.ToString()+"seconds";

          SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    }

    void OnCancelButtonClick()
    {
        cubeRandomizer.enabled = true;
        Time.timeScale = 1f;
        canvas.SetActive(false);
        PlayerPrefs.SetInt("rand", 0);
        if(rocketAttacher.launch == 0){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
