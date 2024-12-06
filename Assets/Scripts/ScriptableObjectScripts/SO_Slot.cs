using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Slot")]
    public class SO_Slot : ScriptableObject
    {
        [SerializeField] private Sprite _sprite;

        public Sprite Sprite { get { return _sprite; } }
    }

}