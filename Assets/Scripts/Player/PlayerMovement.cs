using Enemy;

using Game.Achivement;

using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;

        [SerializeField]
        private float speed;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        private void Start()
        {
            AchivementSystem.Instance.Achivements.Kills[EnemyType.Mole].OnChangeEvent += (value) => print(value);
        }

        private void FixedUpdate()
        {
            Move();
        }

        private void Move()
        {
            float y = Input.GetAxisRaw("Vertical");
            float x = Input.GetAxisRaw("Horizontal");

            rb.velocity = new Vector2(x * speed, y * speed);

            if (y == 0 && x == 0)
            {
                animator.SetFloat("Speed", 0);
            }
            else
            {
                animator.SetFloat("X", x);
                animator.SetFloat("Y", y);
                animator.SetFloat("Speed", 1);
            }
        }
    }
}
