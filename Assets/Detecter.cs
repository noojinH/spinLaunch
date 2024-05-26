using UnityEngine;

public class Detecter : MonoBehaviour
{
    [SerializeField] private GameObject rocket;
    [SerializeField] private int reached; 
    [SerializeField] Attacher attacher;
    void Start(){
        attacher = rocket.GetComponent<Attacher>();
    }

    private void OnTriggerEnter(Collider other)
    {
        reached = attacher.launch;
        if(reached == 1){
            rocket.GetComponent<Attacher>().DetachFromParent();
        }
        Debug.Log(reached);
    }
}