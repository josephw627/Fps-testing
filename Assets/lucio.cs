using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lucio : MonoBehaviour
{
    public GameObject eyeballs;
    public float mouseSentivity = 100;
    private float vAngle = 0;
    public float moveSpeed;
    public float jumpPower;
    public Vector3 speed;
    public float horiz;
    public float vert;
    public float prevHorizontal;
    public float prevVertical;
    private Vector3 prevPosition;
    private Vector3 inputDirection;
    public Vector3 prevDirection;
    private Vector3 prevSpeed;
    int jumps = 2;
    bool floating;

    private void OnCollisionEnter(Collision collision)
    {
        //if(collision.gameObject.tag == "Floor" && collision.transform.position.y < this.transform.position.y)
        //{
            floating = false;
             jumps = 2;
        //}
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            floating = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        float h = mouseSentivity * Input.GetAxis("Mouse X");
        float v = mouseSentivity * Input.GetAxis("Mouse Y");
        vAngle += -v;
        vAngle = Mathf.Clamp(vAngle, -90, 90);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + h, 0);
        eyeballs.transform.rotation = Quaternion.Euler(vAngle, transform.rotation.eulerAngles.y, 0);

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        inputDirection = new Vector3(horizontal, 0, vertical);
        inputDirection = inputDirection.normalized;
        horiz = horizontal;
        vert = vertical;
        if (prevPosition != null)
        {
            speed = prevPosition - transform.position;
            prevPosition = transform.position;
        }

        if (floating)
        {
            if (horizontal != 0 || vertical != 0)
            {
                transform.position += (((prevDirection) * moveSpeed * Time.deltaTime * 1000)); //+(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime * 500));


                transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime * 500);
                prevDirection += transform.TransformDirection((new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime * 50));
                //if (Mathf.Abs(horizontal + prevDirection.x) < 1)
                //{
                //    prevDirection += transform.TransformDirection((new Vector3(horizontal, 0, 0) * moveSpeed * Time.deltaTime * 50));
                //}
                //if (Mathf.Abs(vertical + prevDirection.z) < 1)
                //{
                //    prevDirection += transform.TransformDirection(new Vector3(0, 0, vertical) * moveSpeed * Time.deltaTime * 50);
                //    //prevDirection += transform.TransformDirection((new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime * 50));
                //}
            }
            else
            {
                transform.position += prevDirection * moveSpeed * Time.deltaTime * 1000;
                //transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime * 500);
            }

            //transform.position += transform.TransformDirection(prevDirection * moveSpeed * Time.deltaTime * 1000);
        }
        else
        {
            transform.Translate((inputDirection) * moveSpeed * Time.deltaTime * 1000);

            //transform.Translate(new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime * 1000);
            prevHorizontal = horizontal;
            prevVertical = vertical;
            prevDirection = transform.TransformDirection(new Vector3(prevHorizontal, 0, prevVertical));
            prevDirection = prevDirection.normalized;
            prevSpeed = prevDirection;
        }
        


        if (Input.GetKeyDown("space") && jumps > 0)
        {
            if(jumps > 1)
            {
                Rigidbody rigid = this.GetComponent<Rigidbody>();
                if (rigid.velocity.y < 0)
                {
                    rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
                }
                rigid.AddForce(transform.up * jumpPower);
            }
            else
            {
                Rigidbody rigid = this.GetComponent<Rigidbody>();
                if(rigid.velocity.y < 0)
                {
                    rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
                }
                rigid.AddForce(transform.up * jumpPower);
            }
            --jumps;
        }
    }

}
