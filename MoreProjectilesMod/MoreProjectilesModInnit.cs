using System;
using System.IO;
using System.Reflection;
using UnityEngine;
using CAMOWA;
using MoreProjectilesMod.Projectiles;
using TABZMoreGunsMod.InventoryItemEditingHelper;

namespace MoreProjectilesMod
{
    public class MoreProjectilesModInnit : MonoBehaviour
    {
        static bool foi = false;
        private static string gamePath;
        public static string DllExecutablePath
        {
            get
            {
                if (string.IsNullOrEmpty(gamePath))
                    gamePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return gamePath;
            }

            private set { }
        }
        public static Mesh ProjecMesh;
        public static Texture2D ProjecTexture;
        [IMOWAModInnit("MoreProjectilesMod", 0, 1)]
        static public void MoreProjectilesInnit(string startingPoint)
        {
            if (!foi)
            {
                Debug.Log("MoreProjectilesMod :)");
                try
                {
                    ProjecMesh = new ObjImporter().ImportFile(Path.Combine(DllExecutablePath, "projectileB.obj"));
                    ProjecTexture = FileImporting.ImportImage(Path.Combine(DllExecutablePath, "projectileBTexture.png"));
                    TABZMoreGunsMod.RuntimeResources.RuntimeResourcesHandler.AddNonNetworkedResource(ExplodeOnImpact.CreateExplodeOnImpactProjectile(), "explodeOnImpact");
                    TABZMoreGunsMod.RuntimeResources.RuntimeResourcesHandler.AddNonNetworkedResource(RollAndExplode.CreateRollAndExplodeProjectile(), "rollAndExplode");
                    foi = true;
                }
                catch (Exception ex)
                {
                    Debug.Log("Erro: " + ex.Message);
                }
            }
        }
    }
}
