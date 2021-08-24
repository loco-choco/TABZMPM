using UnityEngine;

namespace MoreProjectilesMod.ParticleSystems
{
    public class BigExplosionParticleSystem : MonoBehaviour
    {
        public float particleLifeTime;
        public AnimationCurve explosionBehaviour;
        public float maxSize;        
        public Material material;
        public Color startColor;
        public Color endColor;
        public Mesh explosionMesh;

        ParticleSystem particleSystem;

        void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
            CreateBigExplosion();
            particleSystem.Play();
        }

        void CreateBigExplosion()
        {
            ParticleSystemRenderer renderer = particleSystem.GetComponent<ParticleSystemRenderer>();
            renderer.enabled = true;
            renderer.renderMode = ParticleSystemRenderMode.Mesh;
            renderer.material = material;
            renderer.mesh = explosionMesh;
            
            var main = particleSystem.main;
            main.startSpeed = 0f;
            main.startColor = startColor;
            main.maxParticles = 1;
            main.startLifetime = particleLifeTime;

            var size = particleSystem.sizeOverLifetime;
            size.enabled = true;
            size.size = new ParticleSystem.MinMaxCurve(maxSize, explosionBehaviour);

            var color = particleSystem.colorOverLifetime;
            color.enabled = true;
            color.color = new ParticleSystem.MinMaxGradient(startColor, endColor);
        }
    }
}
