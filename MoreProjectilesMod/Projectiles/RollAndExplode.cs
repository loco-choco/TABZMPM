using MoreProjectilesMod.Effects;
using TABZMoreGunsMod.WeaponMakerHelper;
using UnityEngine;
using MoreProjectilesMod.RaycastProjectiles;
using TABZMoreGunsMod.RuntimeResources;
using System.IO;

namespace MoreProjectilesMod.Projectiles
{
    class RollAndExplode
    {
        public static GameObject CreateRollAndExplodeProjectile()
        {
            GameObject projectile = new GameObject("RollAndExplodeProjectile");
            // projectile.tag = "Default";

            CapsuleCollider collider = projectile.AddComponent<CapsuleCollider>();
            //projectile.layer = LayerMask.NameToLayer("AllLayers");
            collider.radius = 0.25f;
            collider.height = 0.5f;

            Rigidbody rigidbody = projectile.AddComponent<Rigidbody>();
            rigidbody.drag = 0f;

            DestroyOnDestroy ray = projectile.AddComponent<DestroyOnDestroy>();
            ray.rayLength = 0f;
            ray.mask = Physics.AllLayers;//Physics.AllLayers - LayerMask.GetMask("TerrainBox");
            WeaponEditing.BulletLifeTimeRef(ray) = 5f;

            ProjectileHit hit = projectile.AddComponent<ProjectileHit>();
            hit.damage = 1f;
            hit.force = 600f;
            hit.fall = 0f;

            GiveRandomAngularVelocityOnStart angularVelocityOnStart = projectile.AddComponent<GiveRandomAngularVelocityOnStart>();
            angularVelocityOnStart.MinVelocity = 10f;
            angularVelocityOnStart.MaxVelocity = 100f;

            GameObject effect = new GameObject("EffectCreator");
            CreateEffect effectCreator = effect.AddComponent<CreateEffect>();
            CreateEffect.AddEffectCreator("BigExplosionEffect", ExplosionEffect.CreateExplosionEffect, 50f, 10f, AnimationCurve.Linear(0, 1f, 10f, 0f));
            effectCreator.effectName = "BigExplosionEffect";
            Object.DontDestroyOnLoad(effect);
            hit.effect = effect;

            AddForce addForce = projectile.AddComponent<AddForce>();
            addForce.force = 20f;
            addForce.upForce = 0f;
            
            //Trail
            GameObject trail = new GameObject("trail");
            trail.transform.parent = projectile.transform;
            TrailRenderer trailRend = trail.AddComponent<TrailRenderer>();
            trailRend.motionVectorGenerationMode = MotionVectorGenerationMode.Object;
            trailRend.material = new Material(Shader.Find("Diffuse"))
            {
                color = new Color(0.75f, 0.5f, 0.1f, 0.75f)
            };
            trailRend.startColor = new Color(0.75f, 0.5f, 0.1f, 0.75f);
            trailRend.endColor = Color.red;

            trailRend.time = 1f;
            trailRend.minVertexDistance = 0.1f;
            trailRend.widthCurve = AnimationCurve.EaseInOut(0f, 0.1f, 0.5f, 0f);

            //Model
            GameObject Model = new GameObject("Model");
            Model.transform.parent = projectile.transform;
            //Model.transform.localScale = Vector3.one * 0.25f;
            Model.AddComponent<MeshFilter>().mesh = MoreProjectilesModInnit.ProjecMesh;
            MeshRenderer renderer = Model.AddComponent<MeshRenderer>();
            renderer.material = new Material(Shader.Find("Diffuse"))
            {
                mainTexture = MoreProjectilesModInnit.ProjecTexture,
            };

            return projectile;
        }
    }
}
