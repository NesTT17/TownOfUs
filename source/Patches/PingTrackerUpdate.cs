using HarmonyLib;
using UnityEngine;

namespace TownOfUs
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {

        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            var position = __instance.GetComponent<AspectPosition>();
            position.DistanceFromEdge = new Vector3(3.6f, 0.1f, 0);
            position.AdjustPosition();
            var host = GameData.Instance?.GetHost();

            if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started) {
                __instance.text.text = $"<size=90%><color=#018001FF>TownOfUs</size></color> <size=60%>v{TownOfUs.VersionString}\n" + __instance.text.text + "</size>";
            } else {
                __instance.text.text = $"<size=90%><color=#018001FF>TownOfUs</size></color> <size=60%>v{TownOfUs.VersionString}\nImproved by <color=#018001FF>NesTT</color>\nHost: {host?.PlayerName}\n" + __instance.text.text + "</size>";
            }
        }
    }
}