using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ObjectPoolingManager Instance { get; private set; }
    public GameObject bulletPrefab;
    public int bulletAmount = 20;
    private List<GameObject> bulletList;

    void Awake()
    {
        Instance = this;
        bulletList = new List<GameObject>(bulletAmount);
        for (int i = 0; i<bulletAmount; i++)
        {
            GameObject prefabInstance=Instantiate(bulletPrefab);
            prefabInstance.transform.SetParent(transform);
            prefabInstance.SetActive(false);
            bulletList.Add(prefabInstance);
        }
    }

    public GameObject GetBullet()
    {
        foreach (var bullet in bulletList)    
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }
        GameObject prefabInstance=Instantiate(bulletPrefab);
        prefabInstance.transform.SetParent(transform);
        bulletList.Add(prefabInstance);
        return prefabInstance;
    }
}
