using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectBaka
{
    public class PatrolPoints : MonoBehaviour
    {
        [SerializeField] Transform[] points_;
        public Transform[] Points { get { return points_; } }
    }
}