using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Slot/Item")]
    public class SO_Item : SO_Slot
    {
        [SerializeField] private int defaultAmount;
        [SerializeField] private int level;

        public int DefaultAmount { get { return defaultAmount; } }
        public int Level { get { return level; } }
    }
}
