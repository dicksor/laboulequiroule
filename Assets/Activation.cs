using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activation : MonoBehaviour
{
    public GameObject boardBrick;
    public GameObject boardObstacle;
    public Material blackColor;

    const int BOARD_WIDTH = 7;
    const int BOARD_HEIGHT = 50;

    // Start is called before the first frame update
    void Start()
    {
        GameObject lastLeftObject = null;
        GameObject lastRigthObject = null;

        for(int i = 0; i < BOARD_WIDTH; i++)
        {
            for(int j = 0; j < BOARD_HEIGHT; j++)
            {
                GameObject go = null;

                int aleaNumber = Random.Range(0, 6);

                //Create board brick
                if(aleaNumber == 0 || aleaNumber == 1 || aleaNumber == 2 || aleaNumber == 3)
                {
                    go = Instantiate(boardBrick, new Vector3(i, 0, j), Quaternion.identity);
                    go.transform.parent = this.transform;
                    go.GetComponent<MeshRenderer>().material = blackColor;

                }
                //Create board obstacle
                else if(aleaNumber == 4)
                {
                    go = Instantiate(boardObstacle, new Vector3(i, 0.4f, j), Quaternion.identity);
                    go.transform.parent = this.transform;
                }

                //at the last iteration we recover the 2 last blocks in the corners 
                //to be able to place a plane that will allow to detect the end of the game.
                if (j == BOARD_HEIGHT - 1)
                {
                    //Allows to force the creation of a game object to the last iteration
                    if (go == null)
                    {
                        go = Instantiate(boardBrick, new Vector3(i, 0, j), Quaternion.identity);
                        go.transform.parent = this.transform;
                        go.GetComponent<MeshRenderer>().material = blackColor;
                    }

                    //get the the last right and left object
                    if(i ==  BOARD_WIDTH - 1)
                    {
                        lastLeftObject = go;
                    }
                    else if(i == 0)
                    {
                        lastRigthObject = go;
                    }
                }
            }
        }

        //Get the plane and replace it to the end of the board
        Transform endPlane = gameObject.transform.GetChild(0);
        endPlane.position = (lastRigthObject.transform.position + lastLeftObject.transform.position) / 2;
        endPlane.localScale = new Vector3(BOARD_WIDTH / 10f, 1, BOARD_WIDTH / 10f);

        //Rotate the board at the end of the generation
        this.transform.Rotate(15, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Update board angle when arrow key is press
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Rotate(0.0f, 0.0f, 0.5f);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Rotate(0.0f, 0.0f, -0.5f);
        }
    }
}
