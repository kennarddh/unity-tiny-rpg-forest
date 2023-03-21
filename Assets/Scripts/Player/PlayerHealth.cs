using System;

using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField]
        private Image[] hearts;

        [SerializeField]
        private Sprite emptyHeartSprite;

        private int health, prevHealth;

        private long lastHitTime;

        public int Health
        {
            get => health;

            set
            {
                prevHealth = health;

                health = value;

                for (int i = 1; i <= prevHealth - health; i++)
                {
                    hearts[prevHealth - i].sprite = emptyHeartSprite;
                }
            }
        }

        private void Awake()
        {
            health = hearts.Length;

            prevHealth = health;
        }

        private void Dead()
        {
            print("Dead");
        }

        private void Damage(int damage)
        {
            Health = Health - damage <= 0 ? 0 : Health - damage;

            if (Health <= 0)
            {
                Dead();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() < lastHitTime + 500) return;

            lastHitTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            if (collision.gameObject.CompareTag("Mole"))
            {
                Damage(1);
            }
        }
    }
}
