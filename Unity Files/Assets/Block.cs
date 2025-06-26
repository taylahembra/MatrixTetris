using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Matrix3x3 matrix;
    public List<GameObject> Nodes;

    void Start()
    {
        // for (int i = 0; i < Nodes.Count; i++)
        // {
        //     Nodes[i].SetActive(false);
        // }
        //SetNodes(new Matrix3x3(2, 0, 0, 1, 2, 3, 0, 0, 2));
    }

    public GameObject this[int row, int col, int layer]
    {
        // Row Col Layer
        get => Nodes[3 * row + col + 9 * layer];
    }

    void Update()
    {
        //     if (Input.GetKeyDown(KeyCode.Tab))
        //     {
        //         matrix.Add(new Matrix3x3(2, 0, 0, 0, 2, 0, 0, 0, 2));
        //         SetNodes(matrix);
        //     }
    }

    public void SetNodes(Matrix3x3 newmatrix)
    {
        matrix = newmatrix;
        //Reset Nodes
        for (int i = 0; i < 3; i++)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                for (int iii = 0; iii < 3; iii++) this[i, ii, iii].SetActive(false);
            }
        }

        // // Set to given matrix
        for (int i = 0; i < 3; i++)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                for (int iii = 0; iii < 3; iii++)
                {
                    if (iii + 1 <= (int)matrix[i * 3 + ii])
                    {
                        // Add the block
                        this[i, ii, iii].SetActive(true);
                    }
                }
            }
        }
    }

    public void DisableCollider()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                for (int iii = 0; iii < 3; iii++) this[i, ii, iii].GetComponent<BoxCollider>().enabled = false;
            }
        }
    }

    public void EnableCollider()
    {
        for (int i = 0; i < 3; i++)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                for (int iii = 0; iii < 3; iii++)
                {
                    this[i, ii, iii].GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }

    public void Transpose()
    {
        matrix.Transpose();
        SetNodes(matrix);
    }

    public void ScalarMultiply(int num)
    {
        matrix.ScalarMultiply(num);
        SetNodes(matrix);
    }

    public void Multiply(Matrix3x3 newmatrix)
    {
        matrix.Multiply(newmatrix);
        SetNodes(matrix);
    }

    public void Add(Matrix3x3 newmatrix)
    {
        matrix.Add(newmatrix);
        SetNodes(matrix);
    }

    public void Rotate(int dir)
    {
        matrix.Rotate(dir);
        SetNodes(matrix);
    }

    public void Reflect(string axis)
    {
        matrix.Reflect(axis);
        SetNodes(matrix);
        
    }

    public void SetColor()
    {
        Color color = new Color(Random.Range(0F, 1F), Random.Range(0, 1F), Random.Range(0, 1F));
        for (int i = 0; i < Nodes.Count; i++)
        {
            Nodes[i].GetComponent<MeshRenderer>().material.SetColor("_Color", color);
        }
    }
}