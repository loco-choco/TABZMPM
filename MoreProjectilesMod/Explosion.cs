using System.Collections.Generic;
using UnityEngine;

namespace MoreProjectilesMod
{
    public class Explosion : MonoBehaviour
    {
        public float baseDamage;
        public float maxRange;
        public LayerMask mask = Physics.AllLayers;
        public AnimationCurve damageRampUp;

        private void Start()
        {
            Explode();
        }

        private void Explode()
        {
            Debug.Log("BOOM!");
            Collider[] array = Physics.OverlapSphere(base.transform.position, maxRange, mask);
            List<Transform> alreadyUsedRoots = new List<Transform>();
            foreach (Collider collider in array)
            {
                if (!alreadyUsedRoots.Contains(collider.transform.root))
                {
                    Vector3 direction = (collider.transform.position - transform.position).normalized;
                    float distance = Vector3.Distance(collider.transform.position, transform.position);
                    float damage = baseDamage * damageRampUp.Evaluate(distance);

                    //Physics.Raycast(transform.position, direction, out RaycastHit hit, distance, mask);  -> It was made to avoid damage passing walls and stuff
                    //if (hit.collider == collider)

                    alreadyUsedRoots.Add(collider.transform.root);
                    PhotonView photonView = collider.GetComponentInParent<PhotonView>();
                    if ((bool)photonView)
                    {
                        if (photonView.owner == PhotonNetwork.player)
                        {
                            HealthHandler componentInParent = photonView.GetComponent<HealthHandler>();

                            photonView.RPC("TakeDamage", PhotonTargets.All, damage, PhotonNetwork.player, componentInParent.currentHealth <= damage);

                            RigidBodyIndexHolder damagedRigidbody = collider.GetComponent<RigidBodyIndexHolder>();
                            if ((bool)damagedRigidbody)
                            {
                                byte index = damagedRigidbody.Index;
                                photonView.RPC("AddForce", PhotonTargets.All, index, direction * damage * 10, 1);
                            }
                        }

                    }
                }
            }
        }
    }


}
