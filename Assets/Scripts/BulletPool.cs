using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool instance;

    private List<GameObject> pooledBullets = new List<GameObject>();
    private int amountToPool = 20;

    [SerializeField] private GameObject bulletPrefab;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            pooledBullets.Add(bullet);
        }
    }


    public GameObject GetPooledBullet()
    {
        for (int i = 0;i < pooledBullets.Count; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)//si elemento esta inactivo
            {
                return pooledBullets[i];//lo devuelvo a quien lo haya solicitado
            }
        }
        return null;//si no encuentro ninguna inactiva
    }
}
