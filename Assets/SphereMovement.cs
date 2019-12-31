using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SphereMovement : MonoBehaviour
{
    Rigidbody spherePhysic = null;
    public Vector3 jump;
    public Material sphereColor;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        jump = new Vector3(0.0f, 5.0f, 1.0f);

        //Add a random color to the sphere at the begin of the game
        sphereColor.SetColor("_Color", new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
        gameObject.GetComponent<MeshRenderer>().material = sphereColor;
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void OnCollisionExit()
    {
        isGrounded = false;
    }

    void OnCollisionEnter(Collision col)
    {
        //Detect the collision with the final plane
        //If collision is detected restart scene
        if(col.gameObject.name == "Plane")
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if(col.gameObject.tag == "accelerator")
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0,0, 200f));
        }
        else if (col.gameObject.tag == "decelerator")
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 400f, -200f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Start the game when the key e is press
        if (Input.GetKeyDown(KeyCode.E))
        { 
            spherePhysic = gameObject.AddComponent<Rigidbody>();
        }

        //Add jump force to the sphere if space is press
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            if(spherePhysic != null)
            {
                spherePhysic.AddForce(jump, ForceMode.Impulse);
                isGrounded = false;
            }
        }

        //Restart the game if the ball fall from the board
        if(this.transform.position.y < -10)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
