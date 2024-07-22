using TownOfUs.Roles;
using TownOfUs.Roles.Modifiers;
using HarmonyLib;
using AmongUs.GameOptions;
using System.Linq;
using AmongUs.Data;

namespace TownOfUs
{
    [HarmonyPatch]
    public static class HauntMenuMinigamePatch
    {
        // Show the role name instead of just Crewmate / Impostor
        [HarmonyPostfix]
        [HarmonyPatch(typeof(HauntMenuMinigame), nameof(HauntMenuMinigame.SetFilterText))]
        public static void Postfix(HauntMenuMinigame __instance)
        {
            if (GameOptionsManager.Instance.currentGameOptions.GameMode != GameModes.Normal) return;
            var target = __instance.HauntTarget;
            string roleInfo = Role.GetRole(target).Name;
            string factionInfo;
            if (target.Is(Faction.Impostors)) factionInfo = "Impostor";
            else if (target.Is(Faction.Crewmates)) factionInfo = "Crewmate";
            else factionInfo = "Neutral";

            if (CustomGameOptions.DeadSeeRoles && !DataManager.Settings.Gameplay.StreamerMode) {
                __instance.FilterText.text = roleInfo;
            } else if (CustomGameOptions.DeadSeeRoles && DataManager.Settings.Gameplay.StreamerMode || !CustomGameOptions.DeadSeeRoles) {
                __instance.FilterText.text = factionInfo;
            }
            return;
        }

        // The impostor filter now includes neutral killing roles
        [HarmonyPostfix]
        [HarmonyPatch(typeof(HauntMenuMinigame), nameof(HauntMenuMinigame.MatchesFilter))]
        public static void MatchesFilterPostfix(HauntMenuMinigame __instance, PlayerControl pc, ref bool __result) {
            if (GameOptionsManager.Instance.currentGameOptions.GameMode != GameModes.Normal) return;
            if (__instance.filterMode == HauntMenuMinigame.HauntFilters.Impostor) {
                __result = (pc.Is(Faction.Impostors) || pc.Is(Faction.NeutralKilling)) && !pc.Data.IsDead;
            }
        }

        // Shows the "haunt evil roles button"
        [HarmonyPrefix]
        [HarmonyPatch(typeof(HauntMenuMinigame), nameof(HauntMenuMinigame.Start))]
        public static bool StartPrefix(HauntMenuMinigame __instance) {
            if (GameOptionsManager.Instance.currentGameOptions.GameMode != GameModes.Normal) return true;
            if (CustomGameOptions.DeadSeeRoles && DataManager.Settings.Gameplay.StreamerMode) return true;
            if (!CustomGameOptions.DeadSeeRoles) return true;
            __instance.FilterButtons[0].gameObject.SetActive(true);
            int numActive = 0;
            int numButtons = __instance.FilterButtons.Count((PassiveButton s) => s.isActiveAndEnabled);
            float edgeDist = 0.6f * (float)numButtons;
		    for (int i = 0; i< __instance.FilterButtons.Length; i++)
		    {
			    PassiveButton passiveButton = __instance.FilterButtons[i];
			    if (passiveButton.isActiveAndEnabled)
			    {
				    passiveButton.transform.SetLocalX(FloatRange.SpreadToEdges(-edgeDist, edgeDist, numActive, numButtons));
				    numActive++;
			    }
            }
            return false;
        }
    }
}