using BoneLib;
using EyeTracking;
using EyeTracking.EyeGaze;
using Il2CppSLZ.Marrow;
using Il2CppSLZ.Marrow.Interaction;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace eyeNotice {
    public class EyeNoticeImplementation : EyeGazeImplementation {
        public override string Name => "EyeNotice";

        public override string DeviceId => "Quest/Pc";
        public override bool IsLoaded => true;

        public NoticableBehaviour SelectedBody;
        public const float MinDistFromHead = 0.03f;
        public const float MaxDistFromHead = 10f;
        public override void Initialize() {
            MelonLogger.Msg($"Initialized {Name}.");
        }

        public override void Update() {
            Tracking.Data.Eye.Left.Openness = 1;
            Tracking.Data.Eye.Right.Openness = 1;
            if (!Core.PlayerHead) {
                return;
            }
            foreach (var body in Core.Noticables) {
                float bodyDistanceFromHead = Vector3.Distance(body.transform.position, Core.PlayerHead.position);
                float selectedBodyDistanceFromHead = SelectedBody ? Vector3.Distance(SelectedBody.transform.position, Core.PlayerHead.position) : MaxDistFromHead;
                if (bodyDistanceFromHead >= MinDistFromHead && bodyDistanceFromHead <= selectedBodyDistanceFromHead) {
                    if(SelectedBody) MelonLogger.Msg($"Changed Body from ({SelectedBody.Body.Entity.name}/{SelectedBody.name}) to ({body.Body.Entity.name}/{body.name})");
                    SelectedBody = body;
                }
            }
            if (SelectedBody)
                LookAtSelectedBody();
        }

        private void LookAtSelectedBody() {
            var gaze = (Core.PlayerHead.position - SelectedBody.transform.position);
            gaze.Normalize();
            gaze *= 0.5f;
            Tracking.Data.Eye.Left.Gaze = gaze;
            Tracking.Data.Eye.Right.Gaze = gaze;
            Tracking.Data.Eye.MaxDilation = gaze.z;
        }
    }
}
