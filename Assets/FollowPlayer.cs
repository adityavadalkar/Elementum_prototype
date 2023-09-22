using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{   public float followSpeed =  2f; 
    public Transform target; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x,0, -20f); 
        transform.position = Vector3.Slerp(transform.position, newPos,followSpeed*Time.deltaTime); 
    }
}
