using System;
using HarmonyLib;
using TownOfUs.Roles;
using AmongUs.GameOptions;
using UnityEngine;
using Reactor.Utilities;

namespace TownOfUs.NeutralRoles.ScavengerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformKill
    {
        public static bool Prefix(KillButton __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Scavenger);
            if (!flag) return true;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            var role = Role.GetRole<Scavenger>(PlayerControl.LocalPlayer);
            if (role.DevourTimer() != 0) return false;
            var maxDistance = GameOptionsData.KillDistances[GameOptionsManager.Instance.currentNormalGameOptions.KillDistance];
            if (Vector2.Distance(role.CurrentTarget.TruePosition,
                PlayerControl.LocalPlayer.GetTruePosition()) > maxDistance) return false;
            var playerId = role.CurrentTarget.ParentId;
            var player = Utils.PlayerById(playerId);
            if (player.IsInfected() || role.Player.IsInfected())
            {
                foreach (var pb in Role.GetRoles(RoleEnum.Plaguebearer)) ((Plaguebearer)pb).RpcSpreadInfection(player, role.Player);
            }
            if (PlayerControl.LocalPlayer.IsCampaigned() || player.IsCampaigned())
            {
                foreach (var pn in Role.GetRoles(RoleEnum.Politician)) ((Politician)pn).RpcSpreadCampaign(PlayerControl.LocalPlayer, player);
            }

            Utils.Rpc(CustomRPC.ScavengerClean, PlayerControl.LocalPlayer.PlayerId, playerId);

            Coroutines.Start(Coroutine.DevourCoroutine(role.CurrentTarget, role));
            role.LastDevoured = DateTime.UtcNow;
            return false;
        }
    }
}