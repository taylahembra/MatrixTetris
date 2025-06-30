using System.Collections.Generic;
using UnityEngine;

public class BlockCreation : MonoBehaviour
{
    public CameraMovement cameraManager;
    public float sensitivity;
    public GameObject blockPrefab;
    [HideInInspector] public GameObject curBlock = null;
    [HideInInspector] public Matrix3x3 nextBlock = null;
    public NextBlockController nextBlock3D;
    UI UIController;


    void Start()
    {
        // Get UI object to update score
        UIController = GameObject.FindGameObjectWithTag("UIController").GetComponent<UI>();

        // First block. Disable colliders and make not affected by gravity
        curBlock = Instantiate(blockPrefab, cameraManager.touch, Quaternion.identity);
        curBlock.GetComponent<Block>().SetNodes(GenerateShape());
        curBlock.GetComponent<Rigidbody>().useGravity = false;
        curBlock.GetComponent<Block>().DisableCollider();
        curBlock.GetComponent<Block>().SetColor();

        // Generate next
        nextBlock = GenerateShape();
        nextBlock3D.SetMatrix(nextBlock);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInput();
        UIController.UpdateScore();
    }

    void FixedUpdate()
    {
        if (curBlock != null) curBlock.transform.position = cameraManager.touch;
    }

    private void PlayerInput()
    {
        // Place block by left clicking
        if (Input.GetMouseButtonDown(0) && cameraManager.allowMove) PlaceAndCreateBlock();

        // Increase or decrease distance with scroll wheel
        if (Input.mouseScrollDelta.y != 0)
        {
            cameraManager.reach += Input.mouseScrollDelta.y * Time.deltaTime * sensitivity;
        }
    }

    private void PlaceAndCreateBlock()
    {
        // Make cur block affected by gravity and enable colliders
        curBlock.GetComponent<Rigidbody>().useGravity = true;
        curBlock.GetComponent<Block>().EnableCollider();
        curBlock.GetComponent<Block>().placed = true;
        UIController.IncreaseScore();

        // Get new cur block. Disable colliders and make not affected by gravity
        curBlock = Instantiate(blockPrefab, cameraManager.touch, Quaternion.identity);
        curBlock.GetComponent<Block>().SetNodes(nextBlock);
        curBlock.GetComponent<Rigidbody>().useGravity = false;
        curBlock.GetComponent<Block>().DisableCollider();
        curBlock.GetComponent<Block>().SetColor();

        // Set next block
        nextBlock = GenerateShape();
        nextBlock3D.SetMatrix(nextBlock);
    }

    public void NewNext()
    {
        Debug.Log("New next");
        nextBlock = GenerateShape();
        nextBlock3D.SetMatrix(nextBlock);
    }

    private Matrix3x3 GenerateShape()
    {
        // Pick between block shapes then place them randomly on the 3 by 3 area
        List<string> list = new List<string>();
        list.Add("i"); list.Add("o"); list.Add("O"); list.Add("t"); list.Add("s"); list.Add("z"); list.Add("j"); list.Add("l");
        string choice = list[Random.Range(0, list.Count)];
        Matrix3x3 matrix = new Matrix3x3(0, 0, 0, 0, 0, 0, 0, 0, 0);

        // I
        if (choice == "i")
        {
            // Choice between horizontal or vertical
            if (Random.Range(0, 2) == 0)
            {
                // Horizontal
                matrix[1, 0] = 1;
                matrix[1, 1] = 1;
                matrix[1, 2] = 1;
            }
            else
            {
                // Vertical
                matrix[0, 1] = 1;
                matrix[1, 1] = 1;
                matrix[2, 1] = 1;
            }
        }

        // o
        else if (choice == "o")
        {
            matrix[1, 1] = 1;
        }

        // O
        else if (choice == "O")
        {
            matrix[0, 0] = 1;
            matrix[0, 1] = 1;
            matrix[1, 0] = 1;
            matrix[1, 1] = 1;
        }

        // t
        else if (choice == "t")
        {
            matrix[0, 0] = 1;
            matrix[0, 1] = 1;
            matrix[0, 1] = 1;
            matrix[1, 1] = 1;
        }

        // s
        else if (choice == "s")
        {
            matrix[0, 1] = 1;
            matrix[0, 2] = 1;
            matrix[1, 0] = 1;
            matrix[1, 1] = 1;
        }

        // z
        else if (choice == "z")
        {
            matrix[0, 0] = 1;
            matrix[0, 1] = 1;
            matrix[1, 1] = 1;
            matrix[1, 2] = 1;
        }

        // j
        else if (choice == "j")
        {
            matrix[0, 2] = 1;
            matrix[1, 0] = 1;
            matrix[1, 0] = 1;
            matrix[1, 0] = 1;
        }

        // l
        else if (choice == "l")
        {
            matrix[0, 0] = 1;
            matrix[1, 0] = 1;
            matrix[1, 0] = 1;
            matrix[1, 0] = 1;
        }
        return matrix;
    }
}
