using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Cainos.PixelArtTopDown_Basic
{
    public class TopDownCharacterController : MonoBehaviour
    {
        public float speed;

        private Animator animator;

        bool grounded;
        private Rigidbody2D rb2d;
        public float addGrav;

        private SpriteRenderer sprite;
        public int sortingOrder = 0;


        void Start()
        {
            animator = GetComponent<Animator>();
            rb2d = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            sprite.sortingOrder = sortingOrder;

        }
        

        private void Update()
        {

            Vector2 dir = Vector2.zero;
            if (Input.GetKey(KeyCode.A))
            {
                dir.x = -1;
                animator.SetInteger("Direction", 3);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                dir.x = 1;
                animator.SetInteger("Direction", 2);
            }

            if (Input.GetKey(KeyCode.W))
            {
                dir.y = 1;
                animator.SetInteger("Direction", 1);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir.y = -1;
                animator.SetInteger("Direction", 0);
            }


            dir.Normalize();
                animator.SetBool("IsMoving", dir.magnitude > 0);

            GetComponent<Rigidbody2D>().linearVelocity = speed * dir;



         
        }



        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground"))
            {
                grounded = true;
                StartCoroutine(EnableRigidbody2D(0.5F));
            }


            IEnumerator EnableRigidbody2D(float waitTime)
            {
                yield return new WaitForSeconds(waitTime);
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.linearVelocity = Vector2.down;
                sprite.sortingOrder = 0;



                rb2d.gravityScale += addGrav;
                GetComponent<BoxCollider2D>().enabled = false;
                StartCoroutine(EnableBox(2.0F));

            }

            IEnumerator EnableBox(float waitTime)
            {
                yield return new WaitForSeconds(waitTime);
                GetComponent<BoxCollider2D>().enabled = true;
                rb2d.gravityScale -= addGrav;
                sprite.sortingOrder = 2;
            }
        }
    }
}
 