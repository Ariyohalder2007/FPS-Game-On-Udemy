using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ObjectPoolingManager : MonoBehaviour
    {
        // Start is called before the first frame update
        public static ObjectPoolingManager Instance { get; private set; }
        public GameObject bulletPrefab;
        public int bulletAmount = 20;
        private List<GameObject> _bulletList;

        void Awake()
        {
            Instance = this;
            _bulletList = new List<GameObject>(bulletAmount);
            for (int i = 0; i<bulletAmount; i++)
            {
                GameObject prefabInstance=Instantiate(bulletPrefab, transform, true);
                prefabInstance.SetActive(false);
                _bulletList.Add(prefabInstance);
            }
        }

        public GameObject GetBullet()
        {
            foreach (var bullet in _bulletList)    
            {
                if (!bullet.activeInHierarchy)
                {
                    bullet.SetActive(true);
                    return bullet;
                }
            }
            GameObject prefabInstance=Instantiate(bulletPrefab, transform, true);
            _bulletList.Add(prefabInstance);
            return prefabInstance;
        }
    }
}
