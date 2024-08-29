using HarmonyLib;
using Object = UnityEngine.Object;
using Reactor.Utilities.Extensions;
using UnityEngine;
using TownOfUs.Patches;

namespace TownOfUs
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class MeetingHud_Start
    {
        public static void Postfix(MeetingHud __instance)
        {
            Utils.ShowDeadBodies = PlayerControl.LocalPlayer.Data.IsDead;

            foreach (var player in PlayerControl.AllPlayerControls)
            {
                player.MyPhysics.ResetAnimState();
            }
        }
    }

    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Close))]
    public class MeetingHud_Close
    {
        public static void Postfix(MeetingHud __instance)
        {
            Utils.Rpc(CustomRPC.RemoveAllBodies);
            var buggedBodies = Object.FindObjectsOfType<DeadBody>();
            foreach (var body in buggedBodies)
            {
                body.gameObject.Destroy();
            }
        }
    }

    [HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
    public class ExileAnimStart
    {
        public static void Postfix(ExileController __instance, [HarmonyArgument(0)] NetworkedPlayerInfo exiled, [HarmonyArgument(1)] bool tie)
        {
            Utils.ShowDeadBodies = PlayerControl.LocalPlayer.Data.IsDead || exiled?.PlayerId == PlayerControl.LocalPlayer.PlayerId;
        }
    }
}