using System.Collections.Generic;
using UnityEngine;
using TABZMoreGunsMod;
using CAMOWA;
using TABZMoreGunsMod.WeaponMakerHelper;
using TABZMoreGunsMod.RuntimeResources;

namespace MoreProjectilesMod
{
    public static class CreateBasicProjectile
    {
        static public GameObject CreateProjectile()
        {
			GameObject projectile = new GameObject("newProjectile");
			Rigidbody rigidbody = projectile.AddComponent<Rigidbody>();
			rigidbody.drag = 0f;
            rigidbody.useGravity = false;

            RaycastProjectile ray = projectile.AddComponent<RaycastProjectile>();
			ray.rayLength = 0.5f;
            ray.mask = LayerMask.GetMask("Everything") ^ LayerMask.GetMask("TerrainBox");
            WeaponEditing.BulletLifeTimeRef(ray) = 10f;
			
			ProjectileHit hit = projectile.AddComponent<ProjectileHit>();
			hit.damage = 28f;
			hit.force = 600f;
			hit.fall = 0f;

            GameObject effect = CreateHitEffect();
            effect.SetActive(false);
            Object.DontDestroyOnLoad(effect);

            hit.effect = effect;
			
			AddForce addForce = projectile.AddComponent<AddForce>();
			addForce.force = 15f;
			addForce.upForce = 0f;
			
			projectile.AddComponent<LookAtVelocity>();

            //Trail
            GameObject trail = new GameObject("trail");
			trail.transform.parent = projectile.transform;
			TrailRenderer trailRend = trail.AddComponent<TrailRenderer>();
            trailRend.motionVectorGenerationMode = MotionVectorGenerationMode.Object;
            trailRend.material = new Material(Shader.Find("Diffuse"));  //"BulletTrail";
            trailRend.material.color = Color.white;

            trailRend.time = 0.5f;
			trailRend.minVertexDistance = 0.1f;
            trailRend.widthCurve = AnimationCurve.EaseInOut(0f, 0.1f, 0.5f, 0f);

            //Model
            GameObject Model = GameObject.CreatePrimitive(PrimitiveType.Cube);//new GameObject("Model");
			Model.transform.parent = projectile.transform;
            Object.Destroy(Model.GetComponent<Collider>());
            //Model.AddComponent<MeshFilter>().mesh = "mesh";
            //Model.AddComponent<MeshRenderer>().material = "material";

            return projectile;
        }
		
		static public GameObject CreateHitEffect()
        {
			GameObject effect = new GameObject("newEffect");

            effect.AddComponent<DestroyAfter>();
            AddShake addShake = effect.AddComponent<AddShake>();
            addShake.strenght = 1f;
            addShake.distance = 25f;
            addShake.lenght = 0.5f;
            //Particles
            GameObject particle1 = new GameObject("particle");
			particle1.transform.parent = effect.transform;
            ParticleSystem  particleSystem = particle1.AddComponent<ParticleSystem>();

            return effect;
        }

        public class EnableIfNotEnabled :MonoBehaviour
        {
            void Awake()
            {
                if (!gameObject.GetActive())
                    gameObject.SetActive(true);
            }
        }
    }
}
