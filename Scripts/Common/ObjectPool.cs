using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public Transform[] throwObjectSpawnPoints;
    //public GameObject rock;
    //public GameObject cannonball;
    public GameObject poolItem;
    public string tag;
    public int maxPool = 5;
    public int idx = 0;

    private List<GameObject> objectPool = new List<GameObject>();
    private WaitForSeconds ws = new WaitForSeconds(0.1f);

    private void OnEnable()
    {
        StartCoroutine(SpawnPoolObjects());
    }

    private IEnumerator SpawnPoolObjects()
    {
        yield return ws;
        CreatePooling();
        throwObjectSpawnPoints = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        //while (!isEndGame)
        while (true)
        {
            yield return ws;
            int poolItemCount = GameObject.FindGameObjectsWithTag(tag).Length;
            if (poolItemCount < maxPool)
            {
                for (int i = 0; i < objectPool.Count; i++)
                {
                    if (objectPool[i].activeSelf == false)
                    {
                        objectPool[i].tag = tag;
                        objectPool[i].gameObject.GetComponent<MeshCollider>().isTrigger = false;
                        objectPool[i].transform.position = throwObjectSpawnPoints[i + 1].position;
                        objectPool[i].transform.rotation = throwObjectSpawnPoints[i + 1].rotation;
                        objectPool[i].SetActive(true);
                        objectPool[i].transform.SetParent(GameObject.Find("Object Pool").transform);
                    }
                }
            }
        }

    }

    public void CreatePooling()
    {
        if (objectPool != null)
            objectPool.Clear();
        GameObject objPools = new GameObject("Object Pool");
        for (int i = 0; i < maxPool; i++)
        {
            GameObject obj = Instantiate<GameObject>(poolItem, objPools.transform);
            obj.name = tag + i.ToString("00");
            obj.SetActive(false);
            objectPool.Add(obj);
        }

    }
}
