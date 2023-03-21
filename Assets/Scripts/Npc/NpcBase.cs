using UnityEngine;

namespace Npc
{
    public abstract class NpcBase : MonoBehaviour
    {
        [SerializeField]
        private float interactRadius;

        [SerializeField]
        private LayerMask interactLayerMask;

        [SerializeField]
        private KeyCode interactKey;

        protected abstract void OnInteract();

        private void Update()
        {
            Collider2D player = Physics2D.OverlapCircle(transform.position, interactRadius, interactLayerMask);

            if (Input.GetKeyDown(interactKey) && player)
            {
                OnInteract();
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.white;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }
    }
}