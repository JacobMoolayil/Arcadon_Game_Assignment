using UnityEngine;
using TMPro;

public class PointBlockManager : MonoBehaviour
{

    public TextMeshPro points;

    [Header("set point to start with")]
    [SerializeField] int intialPoints;

    private void Start()
    {
        points.text = intialPoints.ToString();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            int temp = int.Parse(points.text);
            temp--;
            
            if(temp < 1)
                Destroy(gameObject);
           
            points.text = temp.ToString();  
        }

    }

}
