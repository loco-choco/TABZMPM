using System.Collections.Generic;
using UnityEngine;

namespace MoreProjectilesMod.Effects
{
    public class CreateEffect : MonoBehaviour
    {
        private static Dictionary<string, CreateEffectData> EffectsCreatorsHolder = new Dictionary<string, CreateEffectData>();
        public delegate GameObject CreateObjectEffect(Vector3 position, Quaternion rotation, Vector3 normal, object[] data);

        public string effectName;
        
        public static void AddEffectCreator(string effectName, CreateObjectEffect effectCreator, params object[] data)
        {
            if (!EffectsCreatorsHolder.ContainsKey(effectName))
            {
                CreateEffectData effectData = new CreateEffectData
                {
                    Data = data,
                    EffectCreateObject = effectCreator
                };
                EffectsCreatorsHolder.Add(effectName, effectData);
            }
        }
        private void Start()
        {
            if (SceneManagerHelper.ActiveSceneBuildIndex == 1)
            {
                EffectsCreatorsHolder[effectName].EffectCreateObject(transform.position, transform.rotation, transform.forward, EffectsCreatorsHolder[effectName].Data);
                Destroy(gameObject);
            }
        }
    }

    public struct CreateEffectData
    {
        public CreateEffect.CreateObjectEffect EffectCreateObject;
        public object[] Data; 

    }
}
