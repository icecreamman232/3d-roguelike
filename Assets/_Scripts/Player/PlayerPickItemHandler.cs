using System;
using SGGames.Scripts.Managers;
using SGGames.Scripts.Pickables;
using UnityEngine;

namespace SGGames.Scripts.Players
{
    public class PlayerPickItemHandler : MonoBehaviour
    {
        [SerializeField] private LayerMask m_pickableMask;
        [SerializeField] private float m_pickRadius;
        
        private void OnTriggerEnter(Collider other)
        {
            if (LayerManager.IsInLayerMask(other.gameObject.layer, m_pickableMask))
            {
                var pickable = other.GetComponent<Pickable>();
                pickable.StartPick(this.transform.parent);
            }
        }
    }
}
