using System;
using System.Collections.Generic;
using System.Numerics;

public class Matrix3x3
{
    List<float> numbers;
    public Matrix3x3(float num_01, float num_02, float num_03, float num_04, float num_05, float num_06, float num_07, float num_08, float num_09)
    {
        numbers = new List<float>();
        numbers.Add(num_01);
        numbers.Add(num_02);
        numbers.Add(num_03);
        numbers.Add(num_04);
        numbers.Add(num_05);
        numbers.Add(num_06);
        numbers.Add(num_07);
        numbers.Add(num_08);
        numbers.Add(num_09);
    }

    // Allows the object to be indexable for the numbers list
    public float this[int index]
    {
        get => numbers[index];
        set => numbers.Insert(index, value);
    }

    public float this[int index, int index2]
    {
        get => numbers[index * 3 + index2];
        set => numbers.Insert(index * 3 + index2, value);
    }

    public void Add(Matrix3x3 secondMatrix)
    {
        // Add corresponding values together
        for (int i = 0; i < 9; i++)
        {
            numbers[i] = numbers[i] + secondMatrix[i];
        }
    }

    public void Multiply(Matrix3x3 secondMatrix)
    {
        // Find dot product of row by column for each spot in new matrix
        Matrix3x3 temp = new Matrix3x3(0, 0, 0, 0, 0, 0, 0, 0, 0);
        for (int i = 0; i < 9; i++)
        {
            int row, col;
            row = Math.DivRem(i, 3, out col);
            temp[i] = numbers[row * 3] * secondMatrix[col] + numbers[row * 3 + 1] * secondMatrix[col + 3] + numbers[row * 3 + 2] * secondMatrix[col + 6];
        }

        for (int i = 0; i < 9; i++) numbers[i] = temp[i];

    }

    public void ScalarMultiply(int number)
    {
        // Multiply each value by scalar
        for (int i = 0; i < 9; i++)
        {
            numbers[i] = numbers[i] * number;
        }
    }

    public void Transpose()
    {
        // Swap rows and columns 
        (numbers[1], numbers[3]) = (numbers[3], numbers[1]);
        (numbers[6], numbers[2]) = (numbers[2], numbers[6]);
        (numbers[7], numbers[5]) = (numbers[5], numbers[7]);
    }

    public void Reflect(string axis)
    {
        if (axis == "x")
        {
            List<float> temp = new List<float>();
            temp.Add(this[2]); temp.Add(this[1]); temp.Add(this[0]);
            temp.Add(this[5]); temp.Add(this[4]); temp.Add(this[3]);
            temp.Add(this[8]); temp.Add(this[7]); temp.Add(this[6]);
            numbers = temp;
        }
        

        else if (axis == "y")
        {
            List<float> temp = new List<float>();
            temp.Add(this[6]); temp.Add(this[7]); temp.Add(this[8]);
            temp.Add(this[3]); temp.Add(this[4]); temp.Add(this[5]);
            temp.Add(this[0]); temp.Add(this[1]); temp.Add(this[2]);
            numbers = temp;
        }

    }

    public void Rotate(int dir)
    {
        if (dir == 1)
        {
            List<float> temp = new List<float>();
            temp.Add(this[6]); temp.Add(this[3]); temp.Add(this[0]);
            temp.Add(this[7]); temp.Add(this[4]); temp.Add(this[1]);
            temp.Add(this[8]); temp.Add(this[5]); temp.Add(this[2]);
            numbers = temp;
        }
        else if (dir == -1)
        {
            List<float> temp = new List<float>();
            temp.Add(this[2]); temp.Add(this[5]); temp.Add(this[8]);
            temp.Add(this[1]); temp.Add(this[4]); temp.Add(this[7]);
            temp.Add(this[0]); temp.Add(this[3]); temp.Add(this[6]);
            numbers = temp;
        }
    }

    public override string ToString()
    {
        return $"{this[0]}, {this[1]}, {this[2]}, {this[3]}, {this[4]}, {this[5]}, {this[6]}, {this[7]}, {this[8]}";
    }
}
