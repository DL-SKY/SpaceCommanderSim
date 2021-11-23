using UnityEngine;

namespace SCS.ScriptableObjects.GlobalConfigs
{
    [CreateAssetMenu(menuName = "ScriptableObjects/GlobalConfigs/SkillsConfig", fileName = "SkillsConfig")]
    public class SkillsConfig : ScriptableObject
    {
        public int levelMin = 1;
        public int levelMax = 10;
    }
}
