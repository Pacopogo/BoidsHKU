using System.Collections.Generic;
using UnityEngine;

public class BoidManager : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject prefab;
    [SerializeField] private int spawnAmount = 20;

    [SerializeField] private List<GameObject> boidList;

    [ContextMenu("Spawn Boids")]
    public void SpawnBoids()
    {
        GameObject obj;
        for (int i = 0; i < spawnAmount; i++)
        {
            obj = Instantiate(prefab);

            obj.transform.SetParent(transform, false);

            float rndX = Random.Range(-15, 15);
            float rndY = Random.Range(-15, 15);
            obj.transform.localPosition = new Vector3(rndX, rndY, 0);

            boidList.Add(obj);
        }

        foreach (GameObject item in boidList)
        {
            item.GetComponent<Boid>().boidList = boidList;
            item.GetComponent<Boid>().TargetTrans = target.transform;
        }
    }
    [ContextMenu("Clear Boids")]
    public void ClearBoids()
    {
        foreach (GameObject obj in boidList)
        {
            Destroy(obj);
        }
        boidList.Clear();
    }

    private void OnDrawGizmos()
    {
        if (boidList.Count > 0)
        {
            Vector3 center = Vector3.zero;
            foreach (GameObject obj in boidList)
            {
                center += obj.transform.position;
            }
            center /= boidList.Count;
            Gizmos.DrawWireCube(center, Vector3.one);
        }

    }
}
