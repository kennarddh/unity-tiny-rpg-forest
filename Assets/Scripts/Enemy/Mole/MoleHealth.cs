using Game.Achivement;

using UnityEngine;
using UnityEngine.UI;

namespace Enemy.Mole
{
    public class MoleHealth : MonoBehaviour
    {
        [SerializeField]
        Slider slider;

        [SerializeField]
        private float health;

        private float maxHealth;

        public float Health
        {
            get => health;

            set
            {
                health = value;

                slider.value = health / maxHealth;

                if (health == 0)
                {
                    AchivementSystem.Instance.Achivements.Kills[EnemyType.Mole].Set(prev => prev + 1);

                    Destroy(gameObject);
                }
            }
        }

        private void Awake()
        {
            maxHealth = health;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Arrow"))
            {
                Health -= 1;
            }
        }
    }
}
