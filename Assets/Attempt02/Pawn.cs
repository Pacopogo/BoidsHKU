using UnityEngine;

namespace Attempt02
{

    /// <summary>
    /// this is the individual boid/pawn to move through the scene
    /// </summary>
    public class Pawn : MonoBehaviour
    {
        public void MoveToDir(Vector3 target)
        {
            Vector3 dir = target - transform.position;

            if (dir.magnitude > 0.5f)
            {
                transform.position += dir.normalized * (5 + dir.magnitude) * Time.fixedDeltaTime;            
            }
        }
    }
}
