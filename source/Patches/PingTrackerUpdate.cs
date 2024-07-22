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

            __instance.text.text =
                "<size=2><color=#00FF00FF>TownOfUs v" + TownOfUs.VersionString + "</color>" + "\n" +
                $"Ping: {AmongUsClient.Instance?.Ping}ms\n" +
                $"Improved by <color=#00FF00FF>NesTT</color>\n" + 
                $"Host: {host?.PlayerName}</size>";
        }
    }
}