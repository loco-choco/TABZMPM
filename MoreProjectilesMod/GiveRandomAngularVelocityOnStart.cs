using UnityEngine;

namespace MoreProjectilesMod
{
    public class GiveRandomAngularVelocityOnStart : MonoBehaviour
    {
        public float MinVelocity = 10f;
        public float MaxVelocity = 50f;
        private void Start()
        {
            float randomMagnitude = Random.Range(MinVelocity, MaxVelocity);
            Vector3 randomAngularVelocity = new Vector3(Random.Range(-100, 100), Random.Range(-100, 100), Random.Range(-100, 100)).normalized * randomMagnitude;
            
            gameObject.GetComponent<Rigidbody>().angularVelocity = randomAngularVelocity;
            gameObject.GetComponent<Rigidbody>().rotation = Quaternion.Euler(randomAngularVelocity);
        }
    }
}
