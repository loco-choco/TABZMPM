using UnityEngine;

namespace MoreProjectilesMod.ParticleSystems
{
    public class TornadoParticleSystem : MonoBehaviour
    {
        public int frequency = 2;
        public int resolution = 20;
        public float amplitude = 1f;
        public float ZSpeed = 0f;

        ParticleSystem particleSystem;

        void Start()
        {
            particleSystem = GetComponent<ParticleSystem>();
            CreateTornado();
            particleSystem.Play();
        }

        void CreateTornado()
        {
            var velOverLife = particleSystem.velocityOverLifetime;
            velOverLife.enabled = true;
            velOverLife.space = ParticleSystemSimulationSpace.Local;

            var main = particleSystem.main;
            main.startSpeed = 0f;
            velOverLife.z = new ParticleSystem.MinMaxCurve(10f, ZSpeed);

            AnimationCurve curveX = new AnimationCurve();
            for (int i =0; i< resolution; i++)
            {
                float step = i / (float)(resolution - 1);
                curveX.AddKey(step, amplitude * Mathf.Cos(step * 2 * Mathf.PI * frequency));
            }
            velOverLife.x = new ParticleSystem.MinMaxCurve(10f, curveX);

            AnimationCurve curveY = new AnimationCurve();
            for (int i = 0; i < resolution; i++)
            {
                float step = i / (float)(resolution - 1);
                curveY.AddKey(step, amplitude * Mathf.Sin(step * 2 * Mathf.PI * frequency));
            }
            velOverLife.y = new ParticleSystem.MinMaxCurve(10f, curveY);
        }
    }
}
