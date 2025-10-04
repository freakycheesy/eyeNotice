using BoneLib;
using eyeNotice;
using Il2CppSLZ.Marrow.Interaction;
using MelonLoader;
using UnityEngine;
using Il2CppInterop.Runtime;
using Il2CppInterop.Runtime.InteropTypes.Fields;

namespace eyeNotice {
    [RegisterTypeInIl2Cpp]
    public class NoticableBehaviour : MonoBehaviour {

        public NoticableBehaviour(IntPtr ptr) : base(ptr) { }

        private Transform _pointer {
            get; set;
        }
        public MarrowBody Body {
            get; private set;
        }

        public void Start(MarrowBody body) {
            Body = body;
            if (Player.GetPhysicsRig().marrowEntity == body.Entity || body.Entity.name.Contains("Rig") || body._rigidbody.isKinematic) {
                DestroyObject(this);
                return;
            }
            _pointer = new GameObject($"Pointer ({name})").transform;
            _pointer.transform.parent = transform;
        }

        public void Update() {
            if(!Core.PlayerHead) return;
            _pointer.LookAt(Player.Head.position);
            bool hasHit = Physics.Raycast(transform.position, _pointer.eulerAngles, out var hit, EyeNoticeImplementation.MaxDistFromHead, -1, QueryTriggerInteraction.Ignore);
            if (hasHit && hit.transform == Core.PlayerHead && !Core.Noticables.Contains(this))
                Core.Noticables.Add(this);
            else Core.Noticables.Remove(this);
        }
    }
}
