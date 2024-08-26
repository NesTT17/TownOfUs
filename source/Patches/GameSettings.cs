using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TownOfUs.CustomOption;
using AmongUs.GameOptions;
using System.Linq;
using UnityEngine;
using System;
using TownOfUs.Roles;

namespace TownOfUs
{
    [HarmonyPatch]
    class GameSettings
    {
        private static string buildOptionsOfType(MultiMenu menu)
        {
            StringBuilder sb = new StringBuilder("\n");
            var options = CustomOption.CustomOption.AllOptions.Where(o => o.Menu == menu);

            foreach (var option in options) {
                if (option.Type == CustomOptionType.Button)
                    continue;
                
                if (option.Type == CustomOptionType.Header && option.ShouldShow())
                    sb.AppendLine($"\n{option.Name}");
                else if (option.ShouldShow())
                    sb.AppendLine($"    {option.Name}: {option}");
            }

            return sb.ToString();
        }

        public static int SettingsPage = -1;
        public static string buildAllOptions(string vanillaSettings = "", bool hideExtras = false)
        {
            if (vanillaSettings == "")
                vanillaSettings = GameOptionsManager.Instance.CurrentGameOptions.ToHudString(PlayerControl.AllPlayerControls.Count);
            
            string hudString = SettingsPage != -1 && !hideExtras ? (DateTime.Now.Second % 2 == 0 ? $"<color=#{Color.white.ToHtmlStringRGBA()}>(Use scroll wheel if necessary)</color>\n\n" : $"<color=#{Color.red.ToHtmlStringRGBA()}>(Use scroll wheel if necessary)</color>\n\n") : "";

            if (SettingsPage == -1)
                hudString += (!hideExtras ? "" : "Page 1: Vanilla Settings \n\n") + vanillaSettings;
            else if (SettingsPage == 0)
                hudString += "Page 2: Town Of Us Settings" + buildOptionsOfType(MultiMenu.main);
            else if (SettingsPage == 1)
                hudString += "Page 3: Crewmate Roles Settings" + buildOptionsOfType(MultiMenu.crewmate);
            else if (SettingsPage == 2)
                hudString += "Page 4: Neutral Roles Settings" + buildOptionsOfType(MultiMenu.neutral);
            else if (SettingsPage == 3)
                hudString += "Page 5: Impostor Roles Settings" + buildOptionsOfType(MultiMenu.imposter);
            else if (SettingsPage == 4)
                hudString += "Page 6: Modifiers Settings" + buildOptionsOfType(MultiMenu.modifiers);
            
            if (!hideExtras || SettingsPage != -1) hudString += $"\n Press TAB or Page Number for more... ({SettingsPage + 2}/6)";
            return hudString;
        }

        [HarmonyPatch(typeof(IGameOptionsExtensions), nameof(IGameOptionsExtensions.ToHudString))]
        private static void Postfix(ref string __result)
        {
            if (GameOptionsManager.Instance.currentGameOptions.GameMode == AmongUs.GameOptions.GameModes.HideNSeek) return; // Allow Vanilla Hide N Seek
            __result = buildAllOptions(vanillaSettings:__result);
        }

        [HarmonyPatch(typeof(GameOptionsMenu), nameof(GameOptionsMenu.Update))]
        public static class Update
        {
            public static void Postfix(ref GameOptionsMenu __instance)
            {
                __instance.GetComponentInParent<Scroller>().ContentYBounds.max = (__instance.Children.Length - 6.5f) / 2;
            }
        }
    }

    [HarmonyPatch(typeof(KeyboardJoystick), nameof(KeyboardJoystick.Update))]
    public static class GameOptionsNextPagePatch
    {
        public static void Postfix(KeyboardJoystick __instance)
        {
            if (Input.GetKeyDown(KeyCode.Tab)) {
                GameSettings.SettingsPage++;
                if (GameSettings.SettingsPage == 5) GameSettings.SettingsPage = -1;
            }
            if (Input.GetKeyDown(KeyCode.LeftShift)) {
                GameSettings.SettingsPage--;
                if (GameSettings.SettingsPage == -2) GameSettings.SettingsPage = 4;
            }
            if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
                GameSettings.SettingsPage = -1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
                GameSettings.SettingsPage = 0;
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) {
                GameSettings.SettingsPage = 1;
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) {
                GameSettings.SettingsPage = 2;
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) {
                GameSettings.SettingsPage = 3;
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) {
                GameSettings.SettingsPage = 4;
            }

            if (Input.GetKeyDown(KeyCode.F1))
                CustomOption.Patches.HudManagerUpdate.ToggleSettings(HudManager.Instance);
                
            if (Input.GetKeyDown(KeyCode.F2))
                CustomOption.Patches.HudManagerUpdate.ToggleSummary(HudManager.Instance);

            bool dead = false;
            if (Utils.ShowDeadBodies)
            {
                if (PlayerControl.LocalPlayer.Is(RoleEnum.Haunter))
                {
                    var haunter = Role.GetRole<Haunter>(PlayerControl.LocalPlayer);
                    if (haunter.Caught) dead = true;
                }
                else if (PlayerControl.LocalPlayer.Is(RoleEnum.Phantom))
                {
                    var phantom = Role.GetRole<Phantom>(PlayerControl.LocalPlayer);
                    if (phantom.Caught) dead = true;
                }
                else dead = true;
            }
                
            if (Input.GetKeyDown(KeyCode.KeypadPlus) && !MeetingHud.Instance && !ExileController.Instance && dead && !LobbyBehaviour.Instance)
                Utils.toggleZoom();
        }
    }

    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public class GameSettingsScalePatch {
        public static void Prefix(HudManager __instance) {
            if (__instance.GameSettings != null) __instance.GameSettings.fontSize = 1.2f; 
        }
    }
}