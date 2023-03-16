using System;

using UnityEngine;

public class MoleMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] paths;

    [SerializeField]
    private float speed, chaseRadius;

    [SerializeField]
    private LayerMask chaseLayerMask;

    private Animator animator;

    [SerializeField]
    int targetCount = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.SetFloat("Speed", speed);
    }

    private void Update()
    {
        Collider2D player = Physics2D.OverlapCircle(transform.position, chaseRadius, chaseLayerMask);

        if (player)
        {
            // Chase
            LookAt(player.transform.position);

            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                speed * Time.deltaTime
            );
        }
        else
        {
            Vector2 target = paths[targetCount].position;

            LookAt(target);

            transform.position = Vector2.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

            if (target.x == transform.position.x && target.y == transform.position.y)
            {
                targetCount += 1;

                if (targetCount == paths.Length)
                {
                    targetCount = 0;
                }
            }
        }

    }

    void LookAt(Vector2 targetPosition)
    {
        Vector2 startPosition = transform.position;

        Vector2 lookPos = targetPosition - startPosition;

        float rad = Mathf.Atan2(lookPos.y, lookPos.x);

        float angleX = (float)Math.Cos(rad);
        float angleY = (float)Math.Sin(rad);

        animator.SetFloat("X", angleX);
        animator.SetFloat("Y", angleY);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}