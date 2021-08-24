using UnityEngine;

namespace MoreProjectilesMod.RaycastProjectiles
{
    //Copy from RaycastProjectile
    public class DestroyOnImpact : RaycastProjectile
    {
        private bool hasAlreadyEnteredInContact = false;
        public bool hitOnDestroy;
        private void OnCollisionEnter(Collision collision)
        {            
            if (collision.transform.root != shooterRoot && !hasAlreadyEnteredInContact)
            {
                PhotonPlayer mPhotonPlayer = HarmonyLib.AccessTools.FieldRefAccess<RaycastProjectile, PhotonPlayer>(this, "mPhotonPlayer");
                ProjectileHit projectileHit = HarmonyLib.AccessTools.FieldRefAccess<RaycastProjectile, ProjectileHit>(this, "projectileHit");
                ContactPoint contact = collision.contacts[0];
                projectileHit.Hit(collision.transform.GetComponent<Rigidbody>(), contact.point, contact.normal, transform.forward, mPhotonPlayer);

                hasAlreadyEnteredInContact = true;
                Destroy(gameObject);
            }
        }
        private void OnDestroy()
        {
            if (!hitOnDestroy || hasAlreadyEnteredInContact)
                return;
            PhotonPlayer mPhotonPlayer = HarmonyLib.AccessTools.FieldRefAccess<RaycastProjectile, PhotonPlayer>(this, "mPhotonPlayer");
            ProjectileHit projectileHit = HarmonyLib.AccessTools.FieldRefAccess<RaycastProjectile, ProjectileHit>(this, "projectileHit");
            projectileHit.Hit(null, transform.position, transform.up, transform.forward, mPhotonPlayer);
        }
    }
}
