using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class PatrolPoints : MonoBehaviour
    {
        [SerializeField] Transform[] local_points_;
        private Vector3[] points_ = new Vector3[3];
        public Vector3[] Points { get { return points_; } }

        public void RecalculatePoints()
        {
            for(int iterator = 0; iterator < local_points_.Length; ++iterator)
            {
                points_[iterator] = local_points_[iterator].localPosition + transform.position;
            }
        }

        private void Start()
        {
            points_ = new Vector3[local_points_.Length];
            RecalculatePoints();
        }
    }
}