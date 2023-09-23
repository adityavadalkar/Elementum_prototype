using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{    
   
    public float moveSpeed = 10f; 
    public float jumpForce = 10f; 
    public float thrust = 10f; 
    private Rigidbody2D rgbd; 
   

 
    //private bool isFacingRight = true; 
    // Start is called before the first frame update
    void Start()
    {   
        rgbd = GetComponent<Rigidbody2D>(); 
 

    }

    // Update is called once per frame
    void Update()
    {   float moveInput = Input.GetAxisRaw("Horizontal"); 
        transform.position += new Vector3(moveInput, 0, 0)*moveSpeed*Time.deltaTime; 
        


        if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rgbd.velocity.y)<0.001f) {
            Jump();  
            rgbd.AddTorque(5.0f * -(moveInput)); 
            //rgbd.AddTorque(5.0f * -(jumpForce)); 
            
        }

        

        
        
        
        

   
    }


 
    void Jump(){
        rgbd.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); 
    }


   

}
