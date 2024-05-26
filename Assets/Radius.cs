using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Radius : MonoBehaviour
{
    [SerializeField] private float equal = 1;
    [SerializeField] private float initial = 5.79f;
    [SerializeField] private GameObject child;
    [SerializeField] private Transform childT;

    void Start(){
        child.GetComponent<HingeJoint>().connectedBody = null;
        equal = Random.Range(0.1f, 10f);
        transform.localScale = new Vector3(transform.localScale.x,transform.localScale.y, initial * equal);
        
        childT.position = new Vector3(childT.position.x, childT.position.y, transform.position.z - transform.localScale.z);
        child.GetComponent<HingeJoint>().connectedBody = gameObject.GetComponent<Rigidbody>();
    }

    void Update(){
        //transform.localScale.z
    }
}
