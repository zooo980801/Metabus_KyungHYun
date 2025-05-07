using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class TopDownProjectileController : MonoBehaviour
    {
        [SerializeField] private LayerMask levelCollisionLayer;

        private TopDownRangeWeaponHandler topDownrangeWeaponHandler;

        private float currentDuration;
        private Vector2 direction;
        private bool isReady;
        private Transform pivot;

        private Rigidbody2D _rigidbody;
        private SpriteRenderer spriteRenderer;

        public bool fxOnDestory = true;

        private TopDownProjectileManager topDownprojectileManager;

        private void Awake()
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _rigidbody = GetComponent<Rigidbody2D>();
            pivot = transform.GetChild(0);
        }

        private void Update()
        {
            if (!isReady)
            {
                return;
            }

            currentDuration += Time.deltaTime;

            if (currentDuration > topDownrangeWeaponHandler.Duration)
            {
                DestroyProjectile(transform.position, false);
            }

            _rigidbody.velocity = direction * topDownrangeWeaponHandler.Speed;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (((1 << collision.gameObject.layer) & levelCollisionLayer.value) != 0)
            {
                DestroyProjectile(collision.ClosestPoint(transform.position) - direction * .2f, fxOnDestory);
            }
            else if (((1 << collision.gameObject.layer) & topDownrangeWeaponHandler.target.value) != 0)
            {
                TopDownResourceController resourceController = collision.GetComponent<TopDownResourceController>();
                resourceController.ChangeHealth(-topDownrangeWeaponHandler.Power);
                DestroyProjectile(collision.ClosestPoint(transform.position), fxOnDestory);
            }
        }

        public void Init(Vector2 direction, TopDownRangeWeaponHandler weaponHandler, TopDownProjectileManager projectileManager)
        {
            this.topDownprojectileManager = projectileManager;

            topDownrangeWeaponHandler = weaponHandler;

            this.direction = direction;
            currentDuration = 0;
            transform.localScale = Vector3.one * weaponHandler.BulletSize;
            spriteRenderer.color = weaponHandler.ProjectileColor;

            transform.right = this.direction;

            if (this.direction.x < 0)
                pivot.localRotation = Quaternion.Euler(180, 0, 0);
            else
                pivot.localRotation = Quaternion.Euler(0, 0, 0);

            isReady = true;
        }

        private void DestroyProjectile(Vector3 position, bool createFx)
        {
            if (createFx)
            {
                topDownprojectileManager.CreateImpactParticlesAtPostion(position, topDownrangeWeaponHandler);
            }

            Destroy(this.gameObject);
        }
    }

}