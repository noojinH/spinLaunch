using UnityEngine;

public class cineMachine : MonoBehaviour
{
    [SerializeField]
    private GameObject tp0;
    [SerializeField]
    private GameObject fp1;
    [SerializeField]
    private bool toggle = false;

    void Start(){
        if (tp0 == null)
            {
                Debug.LogError("tp0 is not assigned.");
            }
            if (fp1 == null)
            {
                Debug.LogError("fp1 is not assigned.");
            }
    }

    void Update()
    {
            if (Input.GetKeyDown(KeyCode.M))
            {
                if (toggle)
                {
                    tp0.SetActive(true);
                    fp1.SetActive(false);
                }
                else
                {
                    fp1.SetActive(true);
                    tp0.SetActive(false);
                }
                toggle = !toggle; // toggle 값을 전환
            }
        }

        public void fixFp(){
            {
            toggle = true;
            tp0.SetActive(false);
            fp1.SetActive(true);
            }
        }
}
