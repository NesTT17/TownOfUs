using HarmonyLib;
using System.Linq;

namespace TownOfUs.Patches
{
    [HarmonyPatch(typeof(OverlayKillAnimation), nameof(OverlayKillAnimation.Initialize))]
    static class OverlayKillAnimationPatch
    {
        static int currentOutfitTypeCache = 0;

        [HarmonyPrefix]
        public static void Prefix(NetworkedPlayerInfo kInfo, NetworkedPlayerInfo vInfo)
        {
            PlayerControl playerControl = PlayerControl.AllPlayerControls.ToArray().FirstOrDefault(p => p.PlayerId == kInfo.PlayerId);
            currentOutfitTypeCache = (int)playerControl.CurrentOutfitType;
            playerControl.CurrentOutfitType = PlayerOutfitType.Default;

        }
        [HarmonyPostfix]
        public static void Postfix(NetworkedPlayerInfo kInfo, NetworkedPlayerInfo vInfo)
        {
            PlayerControl playerControl = PlayerControl.AllPlayerControls.ToArray().FirstOrDefault(p => p.PlayerId == kInfo.PlayerId);
            playerControl.CurrentOutfitType = (PlayerOutfitType)currentOutfitTypeCache;
        }

    }
}
