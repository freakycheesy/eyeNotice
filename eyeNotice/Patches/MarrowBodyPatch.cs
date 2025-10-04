using BoneLib;
using HarmonyLib;
using Il2CppSLZ.Marrow.Interaction;
using MelonLoader;
using UnityEngine;

namespace eyeNotice.Patches {
    [HarmonyPatch(typeof(MarrowBody))]
    public static class MarrowBodyPatch {
        [HarmonyPatch(nameof(MarrowBody.Awake))]
        [HarmonyPrefix]
        public static void Awake(MarrowBody __instance) {
            MelonLogger.Msg("Attempt Awake Patch");
            if (Player.GetPhysicsRig().marrowEntity == __instance.Entity || __instance.Entity.name.Contains("Rig") || __instance._rigidbody.isKinematic)
                return;
            MelonLogger.Msg("Patched Successful");
            __instance.gameObject.AddComponent<NoticableBehaviour>().Start(__instance);
        }
    }
}
