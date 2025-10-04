using BoneLib;
using EyeTracking;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using Il2CppSLZ.Marrow.Interaction;
using MelonLoader;
using UnityEngine;

[assembly: MelonInfo(typeof(eyeNotice.Core), "eyeNotice", "1.0.0", "freakycheesy", null)]
[assembly: MelonGame("Stress Level Zero", "BONELAB")]

namespace eyeNotice
{
    public class Core : MelonMod {
        public static List<NoticableBehaviour> Noticables = new();

        public static Transform PlayerHead => Player.GetPhysicsRig()?.m_head;
        public override void OnInitializeMelon() {
            LoggerInstance.Msg("Initialized.");
        }
    }
    
}