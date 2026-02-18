using System.Collections.Generic;
using UnityEngine;

namespace FirstPrototype
{
    public class Boid : MonoBehaviour
    {
        [SerializeField] private LayerMask boidMask;
        [SerializeField] private Transform target;
        private float speed;
        [SerializeField] private float checkRadius = 5;
        [SerializeField] List<Transform> boidTrans = new List<Transform>();


        private void Start()
        {
            speed = Random.Range(16, 16);
        }
        private void FixedUpdate()
        {
            MoveToTarget();

            CheckForCloseBoids();
            CheckNeigbours();
            
            OffScreenCheck();
        }
        private void MoveToTarget()
        {
            if (Vector3.Distance(transform.position, target.position) < 5)
                return;

            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            Debug.DrawLine(transform.position, target.position, Color.blue);
        }

        private void OffScreenCheck()
        {
            Camera cam = Camera.main;
            Vector3 pos = cam.WorldToScreenPoint(transform.position);

            if (!Screen.safeArea.Contains(pos))
            {
                //offset by 80% (0.8f)
                transform.position = -transform.position * 0.8f;
            }
        }

        private void CheckForCloseBoids()
        {
            if (Physics2D.OverlapCircle(transform.position, checkRadius, boidMask))
            {
                boidTrans.Clear();

                Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, checkRadius, boidMask);

                foreach (var item in hit)
                {
                    if (item == item.gameObject)
                        continue;

                    boidTrans.Add(item.transform);
                }
            }
        }

        private void CheckNeigbours()
        {
            foreach (var boid in boidTrans)
            {
                Color color = Color.white;
                Vector3 dist = (boid.position - transform.position);
                if (dist.magnitude < 3)
                {
                    color = Color.red;
                    transform.position -= dist.normalized * 6 * Time.fixedDeltaTime;
                    Debug.DrawLine(transform.position, boid.position, color);
                    break;
                }
                else if (dist.magnitude > 10)
                {
                    color = Color.yellow;
                    transform.position += dist.normalized * 6 * Time.fixedDeltaTime;
                }

                Debug.DrawLine(transform.position, boid.position, color);
            }
        }

        private void OnDrawGizmos()
        {
            //Gizmos.DrawWireSphere(transform.position, checkRadius);
        }
    }
}
