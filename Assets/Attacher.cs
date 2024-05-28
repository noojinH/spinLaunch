using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private float temp0;
    private float prev0;
    private bool isOnGround = false;

    void Start()
    {
        if (camera0 == null){
            Debug.Log("rocket: Untargeted cineMachine.");
        }
        if (ruler0 == null){
            Debug.Log("rocket: Untargeted indicator.");
        } else {
            ruler0.SetActive(false);
        }
        
        rocketRb = GetComponent<Rigidbody>();
        rocketRb.drag = 0.1f; // 공기 저항 추가
        previousPosition = transform.position;

        Invoke("StoreVelocity", delay);
        Invoke("Launch", delay);
        Invoke("fp0", delay - 0.45f);
    }

    void Update()
    {
        Vector3 currentPosition = transform.position;
        velocity = (currentPosition - previousPosition) / Time.deltaTime;
        previousPosition = currentPosition;

        if (target != null){
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
        //Debug.Log("V: " + velocity.magnitude);

        if (launch == 1){
            if(transform.position.y - initY > temp0)
            temp0 = (transform.position.y - initY);
            ruler0.GetComponent<TextMeshProUGUI>().text = temp0.ToString() + "m";
            prev0 = temp0;
        }

        if (Input.GetKeyDown(KeyCode.L)){
            //StoreVelocity();
            fp0();
            Launch();
            launch = 1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // 바닥 태그를 가진 오브젝트와 충돌했는지 확인
        if (collision.gameObject.CompareTag("Respawn") && !isOnGround)
        {
            isOnGround = true;
            PlayerPrefs.SetFloat("record" + PlayerPrefs.GetInt("attempt").ToString(), temp0);
            Debug.Log("record: " + PlayerPrefs.GetFloat("record" + PlayerPrefs.GetInt("attempt"))+ "m");
            StartCoroutine(SwitchSceneAfterDelay(5f));
        }
    }


    void StoreVelocity()
    {
        storedVelocity = velocity * 0.1f; // 속도를 현실적으로 조정
        Debug.Log("stored V: " + storedVelocity);
    }

    public void DetachFromParent()
    {
        target = null;
        rocketRb.velocity = storedVelocity;
        ruler0.SetActive(true);
    }

    public void Launch()
    {
        if (launch != 1){
            launch = 1;
            initY = transform.position.y;
            DetachFromParent();
        }
    }

    IEnumerator SwitchSceneAfterDelay(float fi1)
    {
        yield return new WaitForSeconds(fi1);
        SceneManager.LoadScene(1);
    }

    void fp0()
    {
        if (launch != 1){
            camera0.GetComponent<cineMachine>().fixFp();
        }
    }
}
