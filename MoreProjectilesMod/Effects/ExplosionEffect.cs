using MoreProjectilesMod.ParticleSystems;
using UnityEngine;

namespace MoreProjectilesMod.Effects
{
    public class ExplosionEffect
    {
        //Data:
        //0 - Explosion Base Damage (float)
        //1 - Explosion Max Range (float)
        //2 - Explosion Damage Ramp Up (AnimationCurve)
        static public GameObject CreateExplosionEffect(Vector3 position, Quaternion rotation, Vector3 normal, params object[] data)
        {
            GameObject effect = new GameObject("ExplosionEffect"); //GameObject.CreatePrimitive(PrimitiveType.Sphere);//new GameObject("ExplosionEffect");

            Explosion explo = effect.AddComponent<Explosion>();
            explo.baseDamage = (float)data[0];
            explo.maxRange = (float)data[1];
            explo.mask = Physics.AllLayers ^ LayerMask.GetMask("TerrainBox");
            explo.damageRampUp = (AnimationCurve)data[2];

            effect.AddComponent<DestroyAfter>();
            AddShake addShake = effect.AddComponent<AddShake>();
            addShake.strenght = (float)data[0] * 2f;
            addShake.distance = (float)data[1] * 1.2f;
            addShake.lenght = 0.5f;
            //Particles
            GameObject particle1 = new GameObject("particle");
            particle1.transform.parent = effect.transform;
            ParticleSystem system = particle1.AddComponent<ParticleSystem>();

            BigExplosionParticleSystem bigExplosion = particle1.AddComponent<BigExplosionParticleSystem>();
            bigExplosion.explosionMesh = MoreProjectilesModInnit.ProjecMesh;
            bigExplosion.material = new Material(Shader.Find("Diffuse")) { color = Color.red };
            bigExplosion.maxSize = 20f;
            bigExplosion.explosionBehaviour = new AnimationCurve(new Keyframe(0f, 0.01f), new Keyframe(0.7f, 1f), new Keyframe(2f, 1.2f));
            bigExplosion.particleLifeTime = 2f;
            bigExplosion.startColor = Color.magenta;
            bigExplosion.endColor = Color.red;
            //TornadoParticleSystem tornado = particle1.AddComponent<TornadoParticleSystem>();
            //tornado.resolution = 25;
            //tornado.frequency = 10;
            //tornado.ZSpeed = 10f;

            //var emission = system.emission;
            //emission.enabled = true;
            //emission.rateOverTime = 25f;

            //ParticleSystemRenderer renderer = particle1.GetComponent<ParticleSystemRenderer>();
            //renderer.material = new Material(Shader.Find("Diffuse"))
            //{
            //    color = Color.white,
            //};

            effect.transform.position = position;
            effect.transform.rotation = rotation;
            return effect;
        }
    }
}
