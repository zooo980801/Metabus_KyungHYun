using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace TopDown
{
    public class TopDownProjectileManager : MonoBehaviour
    {
        private static TopDownProjectileManager instance;
        public static TopDownProjectileManager Instance { get { return instance; } }

        [SerializeField] private GameObject[] projectilePrefabs;

        [SerializeField] private ParticleSystem impactParticleSystem;

        private void Awake()
        {
            instance = this;
        }

        public void ShootBullet(TopDownRangeWeaponHandler rangeWeaponHandler, Vector2 startPostiion, Vector2 direction)
        {
            GameObject origin = projectilePrefabs[rangeWeaponHandler.BulletIndex];
            GameObject obj = Instantiate(origin, startPostiion, Quaternion.identity);

            TopDownProjectileController projectileController = obj.GetComponent<TopDownProjectileController>();
            projectileController.Init(direction, rangeWeaponHandler, this);
        }

        public void CreateImpactParticlesAtPostion(Vector3 position, TopDownRangeWeaponHandler weaponHandler)
        {
            impactParticleSystem.transform.position = position;
            ParticleSystem.EmissionModule em = impactParticleSystem.emission;
            em.SetBurst(0, new ParticleSystem.Burst(0, Mathf.Ceil(weaponHandler.BulletSize * 5)));
            ParticleSystem.MainModule mainModule = impactParticleSystem.main;
            mainModule.startSpeedMultiplier = weaponHandler.BulletSize * 10f;
            impactParticleSystem.Play();
        }
    }

}