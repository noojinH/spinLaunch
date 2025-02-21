using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawner : MonoBehaviour
{
    public GameObject pointLightPrefab; // 포인트 라이트 프리팹
    [SerializeField]
    private float equal;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // 랜덤한 위치 생성
            Vector3 randomPos = transform.position + new Vector3(Random.Range(-1f * equal, equal), Random.Range(-1f * equal, equal), Random.Range(-1f * equal, equal));

            // 포인트 라이트 생성
            //GameObject newLight = Instantiate(pointLightPrefab, transform.position, Quaternion.identity);
            GameObject newLight = Instantiate(pointLightPrefab, randomPos, Quaternion.identity);
            
            // 생성된 포인트 라이트를 큐브의 자식 오브젝트로 만듭니다.
            newLight.transform.parent = transform;
        }
    }
}
