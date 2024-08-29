using AmongUs.GameOptions;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace TownOfUs
{
    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingTracker_Update
    {
        [HarmonyPostfix]
        public static void Postfix(PingTracker __instance)
        {
            __instance.text.alignment = TextAlignmentOptions.Top;
            var position = __instance.GetComponent<AspectPosition>();
            position.Alignment = AspectPosition.EdgeAlignments.Top;
            var host = GameData.Instance?.GetHost();
            int lighterColors = 0;
            int darkerColors = 0;
            foreach (PlayerControl player in PlayerControl.AllPlayerControls) {
                if (Utils.isLighterColor(player.Data.DefaultOutfit.ColorId)) lighterColors++;
                else darkerColors++;
            }

            if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started) {
                string gameModeText = $"";
                if (CustomGameOptions.GameMode == GameMode.Classic) gameModeText = "Classic";
                else if (CustomGameOptions.GameMode == GameMode.KillingOnly) gameModeText = "Killing Only";
                else if (CustomGameOptions.GameMode == GameMode.AllAny) gameModeText = "All Any";
                else if (CustomGameOptions.GameMode == GameMode.Cultist) gameModeText = "Cultist";
                if (gameModeText != "") gameModeText = Utils.ColorString(Color.yellow, gameModeText);

                __instance.text.text = $"<size=90%><color=#018001FF>TownOfUs</size></color> <size=60%>v{TownOfUs.VersionString}\n" + __instance.text.text + "</size>";
                position.DistanceFromEdge = new Vector3(2.25f, 0.11f, 0);
            } else {
                string gameModeText = $"";
                if (CustomGameOptions.GameMode == GameMode.Classic) gameModeText = "Classic";
                else if (CustomGameOptions.GameMode == GameMode.KillingOnly) gameModeText = "Killing Only";
                else if (CustomGameOptions.GameMode == GameMode.AllAny) gameModeText = "All Any";
                else if (CustomGameOptions.GameMode == GameMode.Cultist) gameModeText = "Cultist";
                if (gameModeText != "") gameModeText = Utils.ColorString(Color.yellow, gameModeText);

                string ldHandler = $"L: {lighterColors} D: {darkerColors}";

                __instance.text.text = $"<size=90%><color=#018001FF>TownOfUs</size></color> <size=60%>v{TownOfUs.VersionString}\nImproved by <color=#018001FF>NesTT</color>\nHost: {host?.PlayerName}\n{ldHandler}\n" + __instance.text.text + "</size>";
                position.DistanceFromEdge = new Vector3( 0f, 0.1f, 0);

                try {
                    var GameModeText = GameObject.Find("GameModeText")?.GetComponent<TextMeshPro>();
                    GameModeText.text = gameModeText == "" ? (GameOptionsManager.Instance.currentGameOptions.GameMode == GameModes.HideNSeek ? "Hide 'N Seek" : "Classic" ) : gameModeText;
                    var ModeLabel = GameObject.Find("ModeLabel")?.GetComponentInChildren<TextMeshPro>();
                    ModeLabel.text = "Game Mode";
                } catch { }
            }
            position.AdjustPosition();
        }
    }
}