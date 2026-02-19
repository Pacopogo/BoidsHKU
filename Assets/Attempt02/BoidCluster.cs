using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Attempt02
{
    /// <summary>
    /// This script will control the cluster of Boids/Pawns in the scene to give them a grouped direction
    /// and make them stay close
    /// Bassiclly this is one big hive mind
    /// </summary>
    public class BoidCluster : MonoBehaviour
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private GameObject target;

        [SerializeField] private int spawnAmount = 5;
        public List<Pawn> pawns = new List<Pawn>();

        [SerializeField] private Vector3 centerCluster = Vector3.zero;
        [SerializeField] private float debugCenterRadius = 5;

        [ContextMenu("Spawn Pawns")]
        public void CreatePawns()
        {
            GameObject obj;
            for (int i = 0; i < spawnAmount; i++)
            {
                obj = Instantiate(prefab);
                pawns.Add(obj.GetComponent<Pawn>());

                obj.transform.SetParent(transform, false);

                float rndX = Random.Range(-15, 15);
                float rndY = Random.Range(-15, 15);
                obj.transform.localPosition = new Vector3(rndX, rndY, 0);
            }
        }

        [ContextMenu("Clear Pawns")]
        public void DeleteAllPawns()
        {
            foreach (Pawn pawn in pawns)
            {
                Destroy(pawn.gameObject);
            }
            pawns.Clear();
        }

        private void FixedUpdate()
        {
            UpdatePawns();
        }

        private void UpdatePawns()
        {
            Vector3 intededDir = Vector3.zero;
            GameObject obj = target;
            centerCluster = Vector3.zero;

            foreach (Pawn pawn in pawns)
            {
                pawn.MoveToDir(obj.transform.position);

                intededDir = obj.transform.position;

                Debug.DrawLine(pawn.gameObject.transform.position, intededDir, Color.green);
                obj = pawn.gameObject;

                Recenter(pawn.transform.position);
            }

            centerCluster /= pawns.Count;
        }

        private void Recenter(Vector3 pawnPos)
        {
            centerCluster += pawnPos;
        }

        private void MoveCenterTowardsTarget()
        {
            Vector3 Dir = target.transform.position - centerCluster;
            centerCluster += Dir * 6 * Time.fixedDeltaTime;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(centerCluster, debugCenterRadius);
        }
    }
}