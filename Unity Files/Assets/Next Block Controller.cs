using UnityEngine;

public class NextBlockController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Block>().SetNodes(new Matrix3x3(1, 0, 0, 1, 1, 1, 0, 0, 1));
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(1, 1, 1));
    }

    public void SetMatrix(Matrix3x3 matrix)
    {
        gameObject.GetComponent<Block>().SetNodes(matrix);
    }
}
