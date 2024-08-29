using System.Linq;
using HarmonyLib;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using UnityEngine;

namespace TownOfUs
{
    [HarmonyPatch(typeof(ImpostorRole), nameof(ImpostorRole.CanUse))]
    public class ImpTasks
    {
        private static bool Prefix(ImpostorRole __instance, ref IUsable usable, ref bool __result)
        {
            if (!PlayerControl.LocalPlayer.Is(RoleEnum.CultistSnitch)) return true;
            __result = true;
            return false;
        }
    }
}