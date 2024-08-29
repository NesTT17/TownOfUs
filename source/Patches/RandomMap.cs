using HarmonyLib;
using System;
using TownOfUs.Patches;
using AmongUs.GameOptions;

namespace TownOfUs
{
    [HarmonyPatch]
    class RandomMap
    {
        public static byte previousMap;
        public static float vision;
        public static int commonTasks;
        public static int shortTasks;
        public static int longTasks;

        [HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.BeginGame))]
        [HarmonyPrefix]
        public static bool Prefix(GameStartManager __instance)
        {
            if (AmongUsClient.Instance.AmHost)
            {
                previousMap = GameOptionsManager.Instance.currentNormalGameOptions.MapId;
                vision = GameOptionsManager.Instance.currentNormalGameOptions.CrewLightMod;
                commonTasks = GameOptionsManager.Instance.currentNormalGameOptions.NumCommonTasks;
                shortTasks = GameOptionsManager.Instance.currentNormalGameOptions.NumShortTasks;
                longTasks = GameOptionsManager.Instance.currentNormalGameOptions.NumLongTasks;
                byte map = GameOptionsManager.Instance.currentNormalGameOptions.MapId;
                if (CustomGameOptions.RandomMapEnabled)
                {
                    map = GetRandomMap();
                    GameOptionsManager.Instance.currentNormalGameOptions.MapId = map;
                }
                GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Scientist, 0, 0);
                GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Engineer, 0, 0);
                GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.GuardianAngel, 0, 0);
                GameOptionsManager.Instance.currentNormalGameOptions.RoleOptions.SetRoleRate(RoleTypes.Shapeshifter, 0, 0);
                Utils.Rpc(CustomRPC.SetSettings, map);
            }
            return true;
        }

        [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
        [HarmonyPostfix]
        public static void Postfix(AmongUsClient __instance)
        {
            if (__instance.AmHost)
            {
                if (CustomGameOptions.RandomMapEnabled) GameOptionsManager.Instance.currentNormalGameOptions.MapId = previousMap;
                if (!(commonTasks == 0 && shortTasks == 0 && longTasks == 0))
                {
                    GameOptionsManager.Instance.currentNormalGameOptions.NumCommonTasks = commonTasks;
                    GameOptionsManager.Instance.currentNormalGameOptions.NumShortTasks = shortTasks;
                    GameOptionsManager.Instance.currentNormalGameOptions.NumLongTasks = longTasks;
                }
            }
        }

        public static byte GetRandomMap()
        {
            Random _rnd = new Random();
            float totalWeight = 0;
            totalWeight += CustomGameOptions.RandomMapSkeld;
            totalWeight += CustomGameOptions.RandomMapMira;
            totalWeight += CustomGameOptions.RandomMapPolus;
            totalWeight += CustomGameOptions.RandomMapAirship;
            totalWeight += CustomGameOptions.RandomMapFungle;
            if (SubmergedCompatibility.Loaded) totalWeight += CustomGameOptions.RandomMapSubmerged;

            if (totalWeight == 0) return GameOptionsManager.Instance.currentNormalGameOptions.MapId;

            float randomNumber = _rnd.Next(0, (int)totalWeight);
            if (randomNumber < CustomGameOptions.RandomMapSkeld) return 0;
            randomNumber -= CustomGameOptions.RandomMapSkeld;
            if (randomNumber < CustomGameOptions.RandomMapMira) return 1;
            randomNumber -= CustomGameOptions.RandomMapMira;
            if (randomNumber < CustomGameOptions.RandomMapPolus) return 2;
            randomNumber -= CustomGameOptions.RandomMapPolus;
            if (randomNumber < CustomGameOptions.RandomMapAirship) return 4;
            randomNumber -= CustomGameOptions.RandomMapAirship;
            if (randomNumber < CustomGameOptions.RandomMapFungle) return 5;
            randomNumber -= CustomGameOptions.RandomMapFungle;
            if (SubmergedCompatibility.Loaded && randomNumber < CustomGameOptions.RandomMapSubmerged) return 6;

            return GameOptionsManager.Instance.currentNormalGameOptions.MapId;
        }
    }
}