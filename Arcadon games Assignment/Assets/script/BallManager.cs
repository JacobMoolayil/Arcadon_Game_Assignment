using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    Vector2 MousePos;
    
    [Tooltip("position from where balls needs to be shooted")]
    Vector2 firePoint;

    [Header("Ball Pooling")]
    public GameObject prefab; 
    public int poolSize = 10;
    private Queue<GameObject> pool;

    public TextMeshPro ballcount;
 

    bool canShoot;

    private void Start()
    {
        canShoot = true;

        firePoint = transform.position;

        #region Setting balls in pool

        pool = new Queue<GameObject>();

        
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

        #endregion
    }

    private void Update()
    {
        MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       

        if (Input.GetButtonUp("Fire1") && IsMouseOnScreen() && canShoot)
        {
            StartCoroutine(Shoot());
        }


        ballcount.text = pool.Count.ToString();
      
    }

    
    //Checking if mouse is inside the current screen resolution
    bool IsMouseOnScreen()
    {

        Vector2 MousePos = Input.mousePosition;
        if (MousePos.x < Screen.currentResolution.width &&
            MousePos.y < Screen.currentResolution.height &&
            MousePos.x >= 0 &&
            MousePos.y >= 0)
        
            return true;
        
        else
            return false;
    }

    public GameObject GetObject(Vector3 position, Quaternion rotation)
    {
        if (pool.Count == 0)
        {
            Debug.Log("no balls left");
            return null;
        }

        GameObject pooledObject = pool.Dequeue();
        pooledObject.SetActive(true);
        pooledObject.transform.position = position;
        pooledObject.transform.rotation = rotation;

        return pooledObject;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        Vector2 direction = MousePos - firePoint;
        direction.Normalize();
        int count = pool.Count; 

        for (int i = 0; i < count; i++)
        {
            GameObject bullet = GetObject(firePoint, Quaternion.identity);
            
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f; 

            yield return new WaitForSeconds(0.2f);

        }
        yield return new WaitUntil(() => count == pool.Count);  
        canShoot = true;
    }


}
