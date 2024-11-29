using UnityEngine;

public class returnball : MonoBehaviour
{
    public BallManager ballManager;

    private void OnCollisionEnter2D(Collision2D collision)
    {
   
        if(collision.gameObject.layer == LayerMask.NameToLayer("Balls"))
        {
            ballManager.ReturnObject(collision.gameObject);
        }
    }
}
