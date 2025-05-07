using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class TopDownWeaponHandler : MonoBehaviour
    {
        [Header("Attack Info")]
        [SerializeField] private float delay = 1f;

        public float Delay { get => delay; set => delay = value; }

        [SerializeField] private float weaponSize = 1f;
        public float WeaponSize { get => weaponSize; set => weaponSize = value; }

        [SerializeField] private float power = 1f;
        public float Power { get => power; set => power = value; }

        [SerializeField] private float speed = 1f;
        public float Speed { get => speed; set => speed = value; }

        [SerializeField] private float attackRange = 10f;
        public float AttackRange { get => attackRange; set => attackRange = value; }


        public AudioClip attackSoundClip;

        public LayerMask target;

        [Header("Knock Back Info")]
        [SerializeField] private bool isOnKnockback = false;
        public bool IsOnKnockback { get => isOnKnockback; set => isOnKnockback = value; }

        [SerializeField] private float knockbackPower = 0.1f;
        public float KnockbackPower { get => knockbackPower; set => knockbackPower = value; }

        [SerializeField] private float knockbackTime = 0.5f;
        public float KnockbackTime { get => knockbackTime; set => knockbackTime = value; }

        private static readonly int IsAttack = Animator.StringToHash("IsAttack");

        public TopDownBaseController Controller { get; private set; }

        private Animator animator;
        private SpriteRenderer weaponRenderer;

        protected virtual void Awake()
        {
            Controller = GetComponentInParent<TopDownBaseController>();
            animator = GetComponentInChildren<Animator>();
            weaponRenderer = GetComponentInChildren<SpriteRenderer>();

            animator.speed = 1.0f / delay;
            transform.localScale = Vector3.one * weaponSize;
        }

        protected virtual void Start()
        {

        }

        public virtual void Attack()
        {
            AttackAnimation();

            if (attackSoundClip != null)
                TopDownSoundManager.PlayClip(attackSoundClip);
        }

        public void AttackAnimation()
        {
            animator.SetTrigger(IsAttack);
        }

        public virtual void Rotate(bool isLeft)
        {
            weaponRenderer.flipY = isLeft;
        }


    }

}