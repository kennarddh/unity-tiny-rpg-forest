using System;

using UnityEngine;

namespace Player
{
    public class PlayerShoot : MonoBehaviour
    {
        float lastX = 0, lastY = 1;

        [SerializeField]
        GameObject arrowPrefab;

        private long lastShootTime;

        private Animator anim;

        [SerializeField]
        Transform shootPoint;

        private void Awake()
        {
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                lastX = Input.GetAxisRaw("Horizontal");
                lastY = Input.GetAxisRaw("Vertical");
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() < lastShootTime + 500) return;

            lastShootTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

            double angle = Math.Atan2(lastY, lastX) * (180 / Math.PI);

            anim.SetTrigger("Shoot");

            Instantiate(arrowPrefab, shootPoint.position, Quaternion.Euler(0, 0, (float)angle - 90));
        }
    }
}
