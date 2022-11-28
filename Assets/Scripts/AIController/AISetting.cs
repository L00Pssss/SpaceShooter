using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{

    public class AISetting : MonoBehaviour
    {
        [SerializeField] AIPointPatrol aIPointPatrol;

        [SerializeField] private List<Transform> m_targets;
    }
}