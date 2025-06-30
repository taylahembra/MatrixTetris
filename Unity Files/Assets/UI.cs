using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// UI
// Controls the UI for the main ui camera, not the death ui camera

public class UI : MonoBehaviour
{
    public Button transpose;
    public Button scalar;
    public Button multiply;
    public Button add;
    public Button transformMat;
    public Slider scalarSlider;
    public TMP_Text scalarText;
    public BlockCreation blockManager;
    public List<TMP_InputField> matrix = new List<TMP_InputField>();
    public TMP_Text scoreText;
    public int score = 0;

    public void Transpose()
    {
        blockManager.curBlock.GetComponent<Block>().Transpose();
    }

    public void onSliderChange()
    {
        scalarText.text = scalarSlider.value.ToString();
    }

    public void ScalarMultiply()
    {
        blockManager.curBlock.GetComponent<Block>().ScalarMultiply((int)scalarSlider.value);
    }

    public void Multiply()
    {
        blockManager.curBlock.GetComponent<Block>().Multiply(blockManager.nextBlock);
        blockManager.NewNext();
    }

    public void Add()
    {
        blockManager.curBlock.GetComponent<Block>().Add(blockManager.nextBlock);
        blockManager.NewNext();
    }

    public void Transform()
    {
        Matrix3x3 transformMatrix = new Matrix3x3(float.Parse(matrix[0].text), float.Parse(matrix[1].text), float.Parse(matrix[2].text), float.Parse(matrix[3].text),
        float.Parse(matrix[4].text), float.Parse(matrix[5].text), float.Parse(matrix[6].text), float.Parse(matrix[7].text), float.Parse(matrix[8].text));
        Debug.Log(transformMatrix.ToString());
        blockManager.curBlock.GetComponent<Block>().Multiply(transformMatrix);
    }

    public void RotateLeft()
    {
        blockManager.curBlock.GetComponent<Block>().Rotate(1);
    }
    public void RotateRight()
    {
        blockManager.curBlock.GetComponent<Block>().Rotate(-1);
    }

    public void ReflectX()
    {
        blockManager.curBlock.GetComponent<Block>().Reflect("x");
    }

    public void ReflectY()
    {
        blockManager.curBlock.GetComponent<Block>().Reflect("y");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void UpdateScore()
    {
        scoreText.text = $"Score: {score}";
    }

    public void IncreaseScore()
    {
        score++;
    }
}
