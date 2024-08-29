using System;
using HarmonyLib;
using TownOfUs.Extensions;

namespace TownOfUs
{
    [HarmonyPatch]
    public static class TasksHandler {
        public static Tuple<int, int> taskInfo(NetworkedPlayerInfo playerInfo) {
            int TotalTasks = 0;
            int CompletedTasks = 0;
            if (!playerInfo.Disconnected && playerInfo.Tasks != null &&
                playerInfo.Object &&
                playerInfo.Role && playerInfo.Role.TasksCountTowardProgress &&
                !playerInfo.Object.hasFakeTasks() && !playerInfo.Role.IsImpostor
                ) {
                foreach (var playerInfoTask in playerInfo.Tasks.GetFastEnumerator())
                {
                    if (playerInfoTask.Complete) CompletedTasks++;
                    TotalTasks++;
                }
            }
            return Tuple.Create(CompletedTasks, TotalTasks);
        }

        [HarmonyPatch(typeof(GameData), nameof(GameData.RecomputeTaskCounts))]
        private static class GameDataRecomputeTaskCountsPatch {
            private static bool Prefix(GameData __instance) {
                var totalTasks = 0;
                var completedTasks = 0;
                foreach (var playerInfo in GameData.Instance.AllPlayers.GetFastEnumerator())
                {
                    if (playerInfo.Object 
                        && playerInfo.Object.hasAliveKillingLover() // Tasks do not count if a Crewmate has an alive killing Lover
                        ) continue;
                    
                    var (playerCompleted, playerTotal) = taskInfo(playerInfo);
                    totalTasks += playerTotal;
                    completedTasks += playerCompleted;
                }
                __instance.TotalTasks = totalTasks;
                __instance.CompletedTasks = completedTasks;
                return false;
            }
        }
    }

    internal static class TaskPatches
    {
        [HarmonyPatch(typeof(Console), nameof(Console.CanUse))]
        private class Console_CanUse
        {
            private static bool Prefix(Console __instance, [HarmonyArgument(0)] NetworkedPlayerInfo playerInfo, ref float __result)
            {
                var playerControl = playerInfo.Object;

                var flag = playerControl.Is(RoleEnum.Glitch)
                           || playerControl.Is(RoleEnum.Jester)
                           || playerControl.Is(RoleEnum.Executioner)
                           || playerControl.Is(RoleEnum.Juggernaut)
                           || playerControl.Is(RoleEnum.Arsonist)
                           || playerControl.Is(RoleEnum.Plaguebearer)
                           || playerControl.Is(RoleEnum.Pestilence)
                           || playerControl.Is(RoleEnum.Werewolf)
                           || playerControl.Is(RoleEnum.Doomsayer)
                           || playerControl.Is(RoleEnum.Scavenger)
                           || playerControl.Is(RoleEnum.Vampire);

                // If the console is not a sabotage repair console
                if (flag && !__instance.AllowImpostor)
                {
                    __result = float.MaxValue;
                    return false;
                }

                return true;
            }
        }
    }
}