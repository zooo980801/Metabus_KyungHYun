using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class TopDownMeleeWeaponHandler : TopDownWeaponHandler
    {
        [Header("Melee Attack Info")]
        public Vector2 collideBoxSize = Vector2.one;

        protected override void Start()
        {
            base.Start();
            collideBoxSize = collideBoxSize * WeaponSize;
        }

        public override void Attack()
        {
            base.Attack();

            RaycastHit2D hit = Physics2D.BoxCast(transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, collideBoxSize, 0, Vector2.zero, 0, target);

            if (hit.collider != null)
            {

                TopDownResourceController resourceController = hit.collider.GetComponent<TopDownResourceController>();
                if (resourceController != null)
                {
                    resourceController.ChangeHealth(-Power);
                    if (IsOnKnockback)
                    {
                        TopDownBaseController controller = hit.collider.GetComponent<TopDownBaseController>();
                        if (controller != null)
                        {
                            controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                        }
                    }
                }
            }
        }

        public override void Rotate(bool isLeft)
        {
            if (isLeft)
                transform.eulerAngles = new Vector3(0, 180, 0);
            else
                transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}