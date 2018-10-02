using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : ENT_Parent
{

    // Use this for initialization
    void Start()
    {
        Initialize();
        offset = gameObject.transform.position - CamSphere.transform.position;
        gun = GetComponent<Emitter>();
    }
    //camera JUNK
    public GameObject CamSphere;
    Vector3 offset;
    
    //physics



    public float hover_speed;

    public float flightspeed;
    public float fturn;
    public float pitch;
    float setspeed;


    public string mode = "hover";
    public float Camspeed;
    //Collecting
    public float tank_size;
    GameObject currently_touching;
    public float honey;
    public float water;
    public float nectar;

    float col_hon;
    float col_water;
    float col_nect;

    int score = 0;

    Vector3 prevVel;
    Vector3 Rotation;
    Vector3 Vel;
    Quaternion saved_rotation;
    //UI
    public Texture2D empty;
    public Texture2D hone;
    public Texture2D nect;
    public Texture2D wat;
    Emitter gun;

    public ParticleSystem basicf;
    public ParticleSystem fastf;
    public ParticleSystem burst;
    bool wasflying;
   

    void Collection()
    {

        if (Input.GetButton("Collect"))
        {
            if ((honey + nectar + water < tank_size))
            {
                if (touching == "Honey")
                {
                    honey += 5 * Time.deltaTime;
                    currently_touching.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
                }
                if (touching == "Water")
                {
                    water += 5 * Time.deltaTime;
                    currently_touching.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
                }
                if (touching == "Nectar")
                {
                    nectar += 5 * Time.deltaTime;
                    currently_touching.transform.localScale -= new Vector3(1, 1, 1) * Time.deltaTime;
                }
            }
            if (touching == "Input")
            {
                Debug.Log("inputting");
                if (nectar > 0)
                {
                    nectar -= (3 * Time.deltaTime);
                    col_nect += (3 * Time.deltaTime);
                }
                if (honey > 0)
                {
                    honey -= 3 * Time.deltaTime;
                    col_hon += (3 * Time.deltaTime);

                }
                if (water > 0)
                {
                    water -= 3 * Time.deltaTime;
                    col_water += (3 * Time.deltaTime);
                }
            }

        }

    }

    void Move()
    {
        prevVel = Vel;
        setspeed = ground_speed;
        speedlerp = .8f;
        if (mode == "flight")
        {
            //self.transform.rotation = Quaternion.LookRotation(Rotation);
        }
        else
        {
            if (self.velocity.magnitude > .01)
            {
                self.transform.rotation = Quaternion.LookRotation(new Vector3(Vel.x, 0, Vel.z));
            }
        }
        if (mode == "hover")
        {


            if (grounded)
            {
                yvel = 0;
                if (jump)
                {
                    yvel = jump_height;
                }
            }
            else
            {
                speedlerp = .025f;
                yvel -= grav * Time.deltaTime * 10;
                if (jump)
                {
                    if (yvel < hover_speed * 2)
                    {
                        yvel += hover_speed;
                    }
                    setspeed = air_speed;
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
        if (mode == "flight")
        {
            if (buttons[1])
            {
                self.velocity = Vector3.Lerp(self.velocity, self.transform.forward * flightspeed, .1f);

            }
            else
            {

            }
            self.transform.RotateAround(transform.position, new Vector3(0, 1, 0), Input.GetAxis("Horizontal") * fturn);
            self.transform.RotateAround(transform.position, transform.right, Input.GetAxis("Vertical") * pitch);

            if (buttons[2])
            {
                walking = true;
            }
        }
    }
    void Camera_JUNK()
    {
        CamSphere.transform.position = gameObject.transform.position + offset;
        if (mode == "hover")
        {
            CamSphere.transform.eulerAngles += new Vector3(Sticks[1].y, Sticks[1].x, 0) * Camspeed * Time.deltaTime;
            saved_rotation = CamSphere.transform.rotation;
        }
        else
        {

            Vector3 v = transform.rotation.eulerAngles;
            CamSphere.transform.rotation = Quaternion.Euler(v.x, self.transform.rotation.eulerAngles.y, v.z);
            // CamSphere.transform.rotation = self.transform.rotation;
        }
    }
    void OnGUI()
    {

        GUI.DrawTexture(new Rect(10, 10, (tank_size * 10), 20), empty);
        GUI.DrawTexture(new Rect(10, 10, (honey * 10), 20), hone);
        GUI.DrawTexture(new Rect(10 + (honey * 10), 10, (water * 10), 20), wat);
        GUI.DrawTexture(new Rect(10 + ((honey + water) * 10), 10, (nectar * 10), 20), nect);

        //collected num 
        GUI.DrawTexture(new Rect(Screen.width - 20, Screen.height - tank_size * 20, 10, tank_size * 15), empty);
        GUI.DrawTexture(new Rect(Screen.width - 10, Screen.height - tank_size * 20, 10, tank_size * 15), empty);
        GUI.DrawTexture(new Rect(Screen.width - 30, Screen.height - tank_size * 20, 10, tank_size * 15), empty);
        GUI.DrawTexture(new Rect(Screen.width - 20, Screen.height - tank_size * 20, 10, col_water * 15), wat);
        GUI.DrawTexture(new Rect(Screen.width - 10, Screen.height - tank_size * 20, 10, col_nect * 15), nect);
        GUI.DrawTexture(new Rect(Screen.width - 30, Screen.height - tank_size * 20, 10, col_hon * 15), hone);
        GUI.Label(new Rect(Screen.width - 100, Screen.height - 300, 600, 30), "Score: " + score);
        if (col_hon > tank_size)
        {
            score += 1;
            col_hon = 0;
        }
        if (col_nect > tank_size)
        {
            score += 1;
            col_nect = 0;
        }
        if (col_water > tank_size)
        {
            score += 1;
            col_water = 0;
        }

    }
    void Particles()
    {
        if ((!wasflying) && fly)
        {
            //burst.Burst();
        }
        if (mode == "flight")
        {
            basicf.Play();
            if (!wasflying)
            {
                wasflying = true;
            }
        }
        else
        {
            basicf.Pause();
            wasflying = false;
        }
        if (fly)
        {
            fastf.Play();
        }
        else
        {
            fastf.Pause();
        }

    }
    void Shoot()
    {
        if ((shoot > .1f) && (water > 0))
        {

            gun.emitting = true;
            gun.timer = 1 - shoot;
            water -= Time.deltaTime * shoot;

        }
        else
        {
            gun.emitting = false;
        }


    }

    // Update is called once per frame
    void Update()
    {
        
        Move();
        Shoot();
        Collection();
        Camera_JUNK();

    }
    void FixedUpdate()
    {
        Particles();
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
