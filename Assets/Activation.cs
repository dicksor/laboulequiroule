using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Activation : MonoBehaviour
{
    public GameObject boardBrick;
    public GameObject boardObstacle;

    public Material brickColor;
    public Material acceleratorColor;
    public Material deceleratorColor;

    const int BOARD_WIDTH = 7;
    const int BOARD_HEIGHT = 50;

    /// <summary>
    /// Return true if the random is in the proba, return false if the random is not in the proba
    /// </summary>
    /// <param name="proba">proba beetween 0 and 100</param>
    /// <returns>true if random in proba, otherwise 0</returns>
    private bool RandomFromProba(double proba)
    {
        int rand = Random.Range(0, 100);
        return (rand < proba) ? true : false;
    }

    private Tuple<GameObject, GameObject> CreateBoard()
    {
        GameObject lastLeftObject = null;
        GameObject lastRigthObject = null;

        for (int i = 0; i < BOARD_WIDTH; i++)
        {
            for (int j = 0; j < BOARD_HEIGHT; j++)
            {
                GameObject go = null;

                //int aleaNumber = UnityEngine.Random.Range(0, 7);

                //Create board brick
                if (go == null && RandomFromProba(65))
                {
                    go = Instantiate(boardBrick, new Vector3(i, 0, j), Quaternion.identity);
                    go.transform.parent = this.transform;
                    go.GetComponent<MeshRenderer>().material = brickColor;

                }
                //Create board obstacle
                else if (go == null && RandomFromProba(50))
                {
                    go = Instantiate(boardObstacle, new Vector3(i, 0.4f, j), Quaternion.identity);
                    go.transform.parent = this.transform;
                }

                else if (go == null && RandomFromProba(10))
                {
                    go = Instantiate(boardBrick, new Vector3(i, 0, j), Quaternion.identity);
                    go.gameObject.tag = "accelerator";
                    go.transform.parent = this.transform;
                    go.GetComponent<MeshRenderer>().material = acceleratorColor;
                }

                else if (go == null && RandomFromProba(10))
                {
                    go = Instantiate(boardBrick, new Vector3(i, 0, j), Quaternion.identity);
                    go.gameObject.tag = "decelerator";
                    go.transform.parent = this.transform;
                    go.GetComponent<MeshRenderer>().material = deceleratorColor;
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
                        go.GetComponent<MeshRenderer>().material = brickColor;
                    }

                    //get the the last right and left object
                    if (i == BOARD_WIDTH - 1)
                    {
                        lastLeftObject = go;
                    }
                    else if (i == 0)
                    {
                        lastRigthObject = go;
                    }
                }
            }
        }
        return new Tuple<GameObject, GameObject>(lastLeftObject, lastRigthObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Tuple<GameObject, GameObject> tupleLastBoardItem = CreateBoard();

        //Get the plane and replace it to the end of the board
        Transform endPlane = gameObject.transform.GetChild(0);
        endPlane.position = (tupleLastBoardItem.Item1.transform.position + tupleLastBoardItem.Item2.transform.position) / 2;
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
