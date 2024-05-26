using UnityEngine;
using TMPro;

public class Attacher : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] public float delay = 3.0f;
    [SerializeField] public int launch = 0;
    private Rigidbody rocketRb;
    private Vector3 previousPosition;
    private Vector3 velocity;
    private Vector3 storedVelocity;
    [SerializeField]
    private GameObject camera0;
    [SerializeField]
    private GameObject ruler0;
    private float initY;

    void Start()
    {
        if(camera0 == null){
            Debug.Log("rocket: Untargeted cineMachine.");
        }
        if(ruler0 == null){
            Debug.Log("rocket: Untargeted indicator.");
        }
        else{
            ruler0.SetActive(false);
        }
        rocketRb = GetComponent<Rigidbody>();
        previousPosition = transform.position;
    
        Invoke("StoreVelocity", delay);
        Invoke("Launch", delay);
        Invoke("fp0", delay - 0.45f);
    }

    void Update(){
        Vector3 currentPosition = transform.position;
        
        velocity = (currentPosition - previousPosition) / Time.deltaTime;

        previousPosition = currentPosition;

        if (target != null)
        {
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        //Debug.Log("V: " + velocity.magnitude);

        if(launch == 1){
            ruler0.GetComponent<TextMeshProUGUI>().text = (transform.position.y).ToString() + "m";
        }

        if (Input.GetKeyDown(KeyCode.L)){
            StoreVelocity();
            Launch();
            launch = 1;
        }
    }

    void StoreVelocity(){
        // 현재 속도와 각속도를 저장합니다.
        storedVelocity = velocity;
        Debug.Log("stored V: " + storedVelocity);
    }

    public void DetachFromParent()
    {
        target = null;
        // 저장된 속도와 각속도를 로켓에 다시 적용합니다.
        rocketRb.velocity = storedVelocity;
        ruler0.SetActive(true);
    }

    public void Launch(){
        if(launch != 1){
        launch = 1;
        initY = transform.position.y;
        DetachFromParent();
        }
    }

    void fp0(){
        if(launch != 1){
        //camera0.GetComponent<cineMachine>().fixFp();
        }
    }
}
