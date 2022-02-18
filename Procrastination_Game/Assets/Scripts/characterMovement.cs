using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMovement : MonoBehaviour
{
    //movement varaibles
    public float characterSpeed;
    public Rigidbody2D myRB;
    private Vector2 characterMove;
    public Animator myAnim;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //get vertical and horizontal input
        characterMove.x = Input.GetAxisRaw("Horizontal");
        characterMove.y = Input.GetAxisRaw("Vertical");

        myAnim.SetFloat("Horizontal", characterMove.x);
        myAnim.SetFloat("Vertical", characterMove.y);
        myAnim.SetFloat("characterSpeed", characterMove.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        myRB.MovePosition(myRB.position + characterMove * characterSpeed * Time.fixedDeltaTime);
    }
}
