using UnityEngine;

namespace MoreProjectilesMod.RaycastProjectiles
{
    //Copy from RaycastProjectile
    public class DestroyOnDestroy : RaycastProjectile
    {
        private void OnDestroy()
        {
            PhotonPlayer mPhotonPlayer = HarmonyLib.AccessTools.FieldRefAccess<RaycastProjectile, PhotonPlayer>(this, "mPhotonPlayer");
            ProjectileHit projectileHit = HarmonyLib.AccessTools.FieldRefAccess<RaycastProjectile, ProjectileHit>(this, "projectileHit");
            projectileHit.Hit(null, transform.position, transform.up, transform.forward, mPhotonPlayer);
        }
    }
}
