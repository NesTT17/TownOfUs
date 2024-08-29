using System;
using HarmonyLib;
using Hazel;
using TownOfUs.Roles;
using UnityEngine;
using Reactor;
using TownOfUs.CrewmateRoles.MedicMod;
using TownOfUs.Patches;
using TownOfUs.NeutralRoles.MercenaryMod;

namespace TownOfUs.ImpostorRoles.PoisonerMod
{
    [HarmonyPatch(typeof(KillButton), nameof(KillButton.DoClick))]
    public class PerformPoisonKill
    {
        public static Sprite PoisonSprite => TownOfUs.PoisonSprite;
        public static Sprite PoisonedSprite => TownOfUs.PoisonedSprite;

        public static bool Prefix(KillButton __instance)
        {
            var flag = PlayerControl.LocalPlayer.Is(RoleEnum.Poisoner);
            if (!flag) return true;
            var role = Role.GetRole<Poisoner>(PlayerControl.LocalPlayer);
            if (!PlayerControl.LocalPlayer.CanMove) return false;
            if (PlayerControl.LocalPlayer.Data.IsDead) return false;
            var flag2 = role.PoisonTimer() == 0f;
            if (!flag2) return false;
            if (!__instance.isActiveAndEnabled || role.ClosestPlayer == null) return false;

            if (role.Player.inVent)
            {
                role.PoisonButton.SetCoolDown(0.01f, 1f);
                return false;
            }

            if (role.ClosestPlayer.Is(RoleEnum.Pestilence))
            {
                if (!role.Player.IsMercShielded())
                {
                    if (role.ClosestPlayer.IsMercShielded())
                    {
                        var merc = role.ClosestPlayer.GetMerc().Player.PlayerId;
                        Utils.Rpc(CustomRPC.MercShield, merc, role.ClosestPlayer.PlayerId);
                        role.LastPoisoned = DateTime.UtcNow;
                        role.PoisonButton.SetCoolDown(0.01f, 1f);
                        StopAbility.BreakShield(merc, role.ClosestPlayer.PlayerId);
                    }
                    else if (role.ClosestPlayer.IsShielded())
                    {
                        var medic = role.ClosestPlayer.GetMedic().Player.PlayerId;
                        Utils.Rpc(CustomRPC.AttemptSound, medic, role.ClosestPlayer.PlayerId);

                        if (CustomGameOptions.ShieldBreaks) role.LastPoisoned = DateTime.UtcNow;
                        role.PoisonButton.SetCoolDown(0.01f, 1f);
                        StopKill.BreakShield(medic, role.ClosestPlayer.PlayerId, CustomGameOptions.ShieldBreaks);
                    }
                    else if (role.Player.IsShielded())
                    {
                        var medic = role.Player.GetMedic().Player.PlayerId;
                        var writer = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                            (byte)CustomRPC.AttemptSound, SendOption.Reliable, -1);
                        writer.Write(medic);
                        writer.Write(role.Player.PlayerId);
                        AmongUsClient.Instance.FinishRpcImmediately(writer);

                        if (CustomGameOptions.ShieldBreaks) role.LastPoisoned = DateTime.UtcNow;
                        role.PoisonButton.SetCoolDown(0.01f, 1f);

                        StopKill.BreakShield(medic, role.Player.PlayerId, CustomGameOptions.ShieldBreaks);
                    }
                    else
                    {
                        Utils.RpcMurderPlayer(role.ClosestPlayer, PlayerControl.LocalPlayer);
                    }
                }
                else
                {
                    // The Merc shield absorbs the Pest Kill, and the kill attempt goes through.
                    var merc = role.Player.GetMerc().Player.PlayerId;
                    Utils.Rpc(CustomRPC.MercShield, merc, role.Player.PlayerId);
                    StopAbility.BreakShield(merc, role.Player.PlayerId);
                }
                return false;
            }

            if (role.ClosestPlayer.IsInfected() || role.Player.IsInfected())
            {
                foreach (var pb in Role.GetRoles(RoleEnum.Plaguebearer)) ((Plaguebearer)pb).RpcSpreadInfection(role.ClosestPlayer, role.Player);
            }
            foreach (Role hunterRole in Role.GetRoles(RoleEnum.Hunter))
            {
                Hunter hunter = (Hunter)hunterRole;
                hunter.CatchPlayer(role.Player);
            }
            if (role.ClosestPlayer.IsCampaigned() || role.Player.IsCampaigned())
            {
                foreach (var pn in Role.GetRoles(RoleEnum.Politician)) ((Politician)pn).RpcSpreadCampaign(role.ClosestPlayer, role.Player);
            }

            if (role.ClosestPlayer == ShowRoundOneShield.FirstRoundShielded) 
            {
                role.PoisonButton.SetCoolDown(0.01f, 1f);
                return false;
            }
            else if (role.ClosestPlayer.IsShielded())
            {
                var medic = role.ClosestPlayer.GetMedic().Player.PlayerId;
                Utils.Rpc(CustomRPC.AttemptSound, medic, role.ClosestPlayer.PlayerId);

                if (CustomGameOptions.ShieldBreaks) role.LastPoisoned = DateTime.UtcNow;
                role.PoisonButton.SetCoolDown(0.01f, 1f);

                StopKill.BreakShield(medic, role.ClosestPlayer.PlayerId, CustomGameOptions.ShieldBreaks);

                if (!PlayerControl.LocalPlayer.IsProtected())
                {
                    Utils.RpcMurderPlayer(role.ClosestPlayer, role.Player);
                }
            }
            else if (role.ClosestPlayer.IsMercShielded())
            {
                var merc = role.ClosestPlayer.GetMerc().Player.PlayerId;
                Utils.Rpc(CustomRPC.MercShield, merc, role.ClosestPlayer.PlayerId);
                role.LastPoisoned = DateTime.UtcNow;
                role.LastPoisoned = role.LastPoisoned.AddSeconds(CustomGameOptions.ProtectAbsorbCd - CustomGameOptions.PoisonCd);
                role.PoisonButton.SetCoolDown(0.01f, 1f);
                StopAbility.BreakShield(merc, role.ClosestPlayer.PlayerId);

                return false;
            }
            else if (role.ClosestPlayer.IsArmored())
            {
                role.LastPoisoned = DateTime.UtcNow;
                role.LastPoisoned = role.LastPoisoned.AddSeconds(CustomGameOptions.ProtectAbsorbCd - CustomGameOptions.SheriffKillCd);
                role.PoisonButton.SetCoolDown(0.01f, 1f);
                return false;
            }
            else if (role.ClosestPlayer.IsVesting())
            {
                role.LastPoisoned.AddSeconds(CustomGameOptions.VestKCReset + 0.01f);
                role.PoisonButton.SetCoolDown(0.01f, 1f);
                return false;
            }
            else if (role.ClosestPlayer.IsProtected())
            {
                role.LastPoisoned.AddSeconds(CustomGameOptions.ProtectKCReset + 0.01f);
                role.PoisonButton.SetCoolDown(0.01f, 1f);
                return false;
            }

            role.PoisonedPlayer = role.ClosestPlayer;
            role.PoisonButton.SetTarget(null);
            DestroyableSingleton<HudManager>.Instance.KillButton.SetTarget(null);
            role.TimeRemaining = CustomGameOptions.PoisonDuration;
            role.PoisonButton.SetCoolDown(role.TimeRemaining, CustomGameOptions.PoisonDuration);
            var writer2 = AmongUsClient.Instance.StartRpcImmediately(PlayerControl.LocalPlayer.NetId,
                (byte)CustomRPC.Poison,
            SendOption.Reliable, -1);
            writer2.Write(PlayerControl.LocalPlayer.PlayerId);
            writer2.Write(role.PoisonedPlayer.PlayerId);
            AmongUsClient.Instance.FinishRpcImmediately(writer2);
            SoundEffectsManager.play("poisonerPoison");
            return false;
        }
    }
}