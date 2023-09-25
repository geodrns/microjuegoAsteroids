using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private int poolSize = 10;
    [SerializeField] private List<GameObject> bulletList;

    private static BulletPool instance;
    public static BulletPool Instance {  get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void Start()
    {
        AddBulletsToPool(poolSize);
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletList.Add(bullet);
            bullet.transform.parent = transform;
        }
    }

    public GameObject RequestBullet()
    {
        for (int i = 0;i < bulletList.Count; i++)
        {
            if (!bulletList[i].activeSelf)//si elemento esta inactivo
            {
                bulletList[i].SetActive(true);//lo activo
                return bulletList[i];//lo devuelvo a quien lo haya solicitado
            }
        }
        return null;//si no encuentro ninguna inactiva
    }
}
