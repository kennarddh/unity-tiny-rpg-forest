using UnityEngine;

namespace Arrow
{
    public class ArrowBehaviour : MonoBehaviour
    {
        [SerializeField]
        float speed, lifeTime = 5;

        private void Start()
        {
            Destroy(gameObject, lifeTime);
        }

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * Vector2.up);
        }
    }
}