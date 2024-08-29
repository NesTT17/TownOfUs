using HarmonyLib;

namespace TownOfUs
{
    [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Update))]
    public static class GameStartManagerUpdatePatch
    {
        public static void Prefix(GameStartManager __instance)
        {
            __instance.MinPlayers = 1;
        }
    }
    
    public class BlockGameStartPatch {
        [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.BeginGame))]
        public class GameStartManagerBeginGame {
            public static bool Prefix(GameStartManager __instance) {
                bool continueStart = true;

                // Block game start if lighter/darker > darker/lighter more than one
                int lighterColors = 0;
                int darkerColors = 0;
                foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                    if (Utils.isLighterColor(player.Data.DefaultOutfit.ColorId)) lighterColors++;
                    else darkerColors++;

                    if (lighterColors - darkerColors == 2 || lighterColors - darkerColors == -2)
                        continueStart = false;
                }
                return continueStart;
            }
        }
    }
}