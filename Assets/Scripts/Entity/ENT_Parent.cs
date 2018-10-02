using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENT_Parent : MonoBehaviour
{


    //self rigidbody
    public Rigidbody self;

    //physics
    public bool grounded = false;
    public float yvel;

    //Control
    public Vector2[] Sticks = new Vector2[2];
    public bool[] buttons = new bool[8];
    //jump
    //fly
    //cancel
    //input
    //?
    //?
    //?
    //?
    public float[] triggers = new float[2];
    //?
    //shoot

    public bool playercontrolled;

    //Movement
    public bool flying;
    public bool walking;


    Vector3 prevVel;
    Vector3 Vel;

    public float groundspeed;
    public float airspeed;

    public float jump_height;
    public float hover_power;
    public float speedlerp;
    public float grav = .8f;

    //physics
    public string touching;
    



    public void Initialize()
    {
        self = GetComponent<Rigidbody>();
    }

    public void ENT_input()
    {

        if (playercontrolled)
        {
            PlayerCode();
        }
        else
        {

        }

    }

    public void PlayerCode()
    {
        Vector3 Camfn = Camera.current.transform.forward.normalized;
        Vector3 CamRn = Camera.current.transform.right.normalized;
        Vector3 stik;
        stik = Camfn * Input.GetAxis("Vertical");
        stik += CamRn * Input.GetAxis("Horizontal");

        Sticks[1] = new Vector2(-Input.GetAxis("Vert2"), -Input.GetAxis("Horiz2"));

        // Stick = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        buttons[0] = Input.GetButton("Jump");
        buttons[1] = Input.GetButton("Fly");
        buttons[2] = Input.GetButton("Cancel");
        buttons[3] = Input.GetButton("Collect");
        triggers[1] = Input.GetAxis("Shoot");
    }
    //grounded
    void OnCollisionStay(Collision Col)
    {
        grounded = true;
        touching = Col.collider.tag;
    }
    void OnCollisionExit(Collision Col)
    {
        grounded = false;
        touching = "nothing";
    }
        //inventory
        //flowers
        //lifetime
        //pollen
        //planting
        //water
        //nemies

    public void Walk()
    {
        prevVel = Vel;
        float setspeed = groundspeed;
        speedlerp = .8f;
        {
            if (self.velocity.magnitude > .01)
            {
                self.transform.rotation = Quaternion.LookRotation(new Vector3(Vel.x, 0, Vel.z));
            }
        }
        


            if (grounded)
            {
                //set yvel to slope junk
                yvel = 0;
                if (buttons[0])
                {
                    yvel = jump_height;
                }
            }
            else
            {
                speedlerp = .025f;
                yvel -= grav * Time.deltaTime * 10;
                if (buttons[0])
                {

                    yvel += hover_power;
                  
                    setspeed = airspeed;
                }
            }
            //set velocity numbers
            Vel = (Stick * setspeed);
            if (Vel.magnitude < .001 && !(Vel.magnitude == 0))
            {
                Vel = new Vector3(0, 0, 0);
            }
            Vel = Vector3.Lerp(prevVel, Vel, speedlerp);
            Vel.y = yvel;
            if (self.velocity.magnitude > flightspeed / 2)
            {
                self.velocity = Vector3.Lerp(self.velocity, self.velocity = Vel * Time.deltaTime * 10, .01f);
            }
            else
            {
                self.velocity = Vel * Time.deltaTime * 10;
            }

            if (buttons[1])
            {
                mode = "flight";
                Rotation = Vel;
                //self.useGravity = false;
            }
        }
    }



