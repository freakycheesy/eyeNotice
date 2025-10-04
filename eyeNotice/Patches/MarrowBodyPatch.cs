using BoneLib;
using HarmonyLib;
using Il2CppSLZ.Marrow.Interaction;
using MelonLoader;
using UnityEngine;

namespace eyeNotice.Patches {
    [HarmonyPatch(typeof(MarrowBody))]
    public static class MarrowBodyPatch {
        [HarmonyPatch(nameof(MarrowBody.Awake))]
        [HarmonyPostfix]
        public static void Awake(MarrowBody __instance) {
            MelonLogger.Msg("Attempt Awake Patch");
            __instance.gameObject.AddComponent<NoticableBehaviour>().Start(__instance);
            MelonLogger.Msg("Patched Successful");
        }
    }
}
