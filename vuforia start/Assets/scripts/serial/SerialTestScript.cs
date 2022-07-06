using UnityEngine;
using System.IO.Ports;

/*
 * An example script to send and receive messages to/from a Microbit 
 * The microbit hex file is included in this directory
 */

public class SerialTestScript : MonoBehaviour
{

    SerialPort sp;
    //the bullet prefab to fire
    public GameObject projectile;

    //the angle the canon is aiming at
    int aimAngle;


    // Use this for initialization
    void Start()
    {

        string the_com = "";

        //get all serial ports available
        foreach (string mysps in SerialPort.GetPortNames())
        {
            Debug.Log(mysps);
            //if the port contains usb, it's the one we are looking for
            if (mysps.Contains("usb")) { the_com = mysps; break; }
        }
        //make a new serial port
        sp = new SerialPort(the_com, 115200);

        if (!sp.IsOpen)
        {
            Debug.Log("Opening " + the_com + ", baud 115200");
            sp.Open();

            if (sp.IsOpen) { Debug.Log("Open"); }

        }
    }

    // Update is called once per frame
    void Update()
    {
        //check if serial message came in
        handleRx();
    }

    void handleRx()
    {
        //check if the port is open and if there is anything to read
        if (sp.IsOpen && sp.BytesToRead > 0)
        {
            //read what is available
            string input = sp.ReadExisting();

            //log received data
            Debug.Log("Received: " + input);

            /*
             * Possible messages:
             * 
             * heading #degrees from 0-360
             * buttonA #1 or 0
             * 
             */

            //get the separate strings in an array
            string[] lines = input.Split();

            //if length is 0, return
            if (lines.Length == 0) return;
            //read the first argument
            string Arg1 = lines[0].Trim();

            //if length < 2 (no 2 arguments, return
            if (lines.Length < 2) return;
            //read second argument
            string Arg2 = lines[1].Trim();

            //if it is a heading, rotate the game object to that angle
            if (Arg1 == "heading")
            {
                aimAngle = int.Parse(Arg2);
                //Debug.Log("heading=" + aimAngle);

                if (aimAngle >= 0)
                {
                    this.gameObject.transform.rotation = Quaternion.Euler(0, aimAngle, 0);
                }

            }
            //if it is a button pressed, fire a bullet
            else if (Arg1 == "buttonA")
            {
                //Vector3 aimVector= Vector3.
                GameObject bullet = Instantiate(projectile, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
                Debug.Log("fireheading=" + aimAngle);
                //bullet.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left * 1000);
            }

        }
    }

    //if the mouse is clicked on the gameobject, send a message to the microbit
    private void OnMouseDown()
    {
        Debug.Log("Clicked ");
        sp.WriteLine("9");
    }

}