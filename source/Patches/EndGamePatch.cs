using System.Collections.Generic;
using System.Linq;
using System.Text;
using HarmonyLib;
using Reactor.Utilities.Extensions;
using TownOfUs.CrewmateRoles.AltruistMod;
using TownOfUs.CrewmateRoles.ImitatorMod;
using TownOfUs.CrewmateRoles.MedicMod;
using TownOfUs.CultistRoles.NecromancerMod;
using TownOfUs.Extensions;
using TownOfUs.Patches;
using TownOfUs.Patches.ScreenEffects;
using TownOfUs.Roles;
using TownOfUs.Roles.Modifiers;
using UnityEngine;

namespace TownOfUs
{
    enum CustomGameOverReason {
        LoversWin = 10,
        JesterWin = 11,
        ExecutionerWin = 12,
        DoomsayerWin = 13,
        ScavengerWin = 14,
        PhantomWin = 15,
        TeamVampiresWin = 16,
        GlitchWin = 17,
        JuggernautWin = 18,
        ArsonistWin = 19,
        PlaguebearerWin = 20,
        PestilenceWin = 21,
        WerewolfWin = 22
    }

    enum WinCondition {
        Default,
        LoversTeamWin,
        LoversSoloWin,
        JesterWin,
        ExecutionerWin,
        DoomsayerWin,
        ScavengerWin,
        PhantomWin,
        TeamVampiresWin,
        GlitchWin,
        JuggernautWin,
        ArsonistWin,
        PlaguebearerWin,
        PestilenceWin,
        WerewolfWin,
        AdditionalGuardianAngelWin,
        AdditionalLawyerWin,
        AdditionalAliveSurvivorWin,
        AdditionalAliveMercenaryWin
    }

    static class AdditionalTempData {
        public static WinCondition winCondition = WinCondition.Default;
        public static List<WinCondition> additionalWinConditions = new List<WinCondition>();
        public static List<PlayerRoleInfo> playerRoles = new List<PlayerRoleInfo>();
        public static List<Winners> otherWinners = new List<Winners>();

        public static void clear() {
            playerRoles.Clear();
            otherWinners.Clear();
            additionalWinConditions.Clear();
            winCondition = WinCondition.Default;
        }

        internal class PlayerRoleInfo {
            public string PlayerName { get; set; }
            public string Role { get; set; }
        }

        internal class Winners
        {
            public string PlayerName { get; set; }
            public RoleEnum Role { get; set; }
        }
    }

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.Start))]
    public class EndGameManagerStartPatch
    {
        // Implement a method to record the roles assigned to each player at the end of a game.
        // Store the role history in a session-based collection.
        private static Dictionary<byte, List<RoleEnum>> _sessionRoleHistory = new Dictionary<byte, List<RoleEnum>>();

        public static void RecordSessionRoles() {
            foreach (var player in PlayerControl.AllPlayerControls) {
                var playerId = player.PlayerId;
                var roleType = Role.GetRole(player)?.RoleType ?? RoleEnum.None;

                if (!_sessionRoleHistory.ContainsKey(playerId)) {
                    _sessionRoleHistory[playerId] = new List<RoleEnum>();
                }
                if (_sessionRoleHistory[playerId].Count > CustomGameOptions.MaxRoleHistoryListSize) {
                    _sessionRoleHistory[playerId].RemoveRange(0, _sessionRoleHistory[playerId].Count - CustomGameOptions.MaxRoleHistoryListSize);
             }
                _sessionRoleHistory[playerId].Add(roleType);
            }
        }

        public static void Prefix() {
            RecordSessionRoles(); // Record roles at the end of each game
        }
    }

    [HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
    public class OnGameEndPatch {
        private static GameOverReason gameOverReason;
        public static void Prefix(AmongUsClient __instance, [HarmonyArgument(0)]ref EndGameResult endGameResult) {
            gameOverReason = endGameResult.GameOverReason;
            if ((int)endGameResult.GameOverReason >= 10) endGameResult.GameOverReason = GameOverReason.ImpostorByKill;

            // Reset zoomed out ghosts
            Utils.toggleZoom(reset: true);
        }

        public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)]ref EndGameResult endGameResult) {
            if (CameraEffect.singleton) CameraEffect.singleton.materials.Clear();
            AdditionalTempData.clear();
            
            var playerRole = "";
            foreach (var playerControl in PlayerControl.AllPlayerControls) {
                playerRole = "";
                foreach (var role in Role.RoleHistory.Where(x => x.Key == playerControl.PlayerId))
                {
                    if (role.Value == RoleEnum.Crewmate) { playerRole += "<color=#" + Patches.Colors.Crewmate.ToHtmlStringRGBA() + ">Crewmate</color> -> "; }
                    else if (role.Value == RoleEnum.Impostor) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Impostor</color> -> "; }
                    else if (role.Value == RoleEnum.Altruist) { playerRole += "<color=#" + Patches.Colors.Altruist.ToHtmlStringRGBA() + ">Altruist</color> -> "; }
                    else if (role.Value == RoleEnum.Engineer) { playerRole += "<color=#" + Patches.Colors.Engineer.ToHtmlStringRGBA() + ">Engineer</color> -> "; }
                    else if (role.Value == RoleEnum.Investigator) { playerRole += "<color=#" + Patches.Colors.Investigator.ToHtmlStringRGBA() + ">Investigator</color> -> "; }
                    else if (role.Value == RoleEnum.Mayor) { playerRole += "<color=#" + Patches.Colors.Mayor.ToHtmlStringRGBA() + ">Mayor</color> -> "; }
                    else if (role.Value == RoleEnum.Medic) { playerRole += "<color=#" + Patches.Colors.Medic.ToHtmlStringRGBA() + ">Medic</color> -> "; }
                    else if (role.Value == RoleEnum.Sheriff) { playerRole += "<color=#" + Patches.Colors.Sheriff.ToHtmlStringRGBA() + ">Sheriff</color> -> "; }
                    else if (role.Value == RoleEnum.Swapper) { playerRole += "<color=#" + Patches.Colors.Swapper.ToHtmlStringRGBA() + ">Swapper</color> -> "; }
                    else if (role.Value == RoleEnum.Seer || role.Value == RoleEnum.CultistSeer) { playerRole += "<color=#" + Patches.Colors.Seer.ToHtmlStringRGBA() + ">Seer</color> -> "; }
                    else if (role.Value == RoleEnum.Snitch || role.Value == RoleEnum.CultistSnitch) { playerRole += "<color=#" + Patches.Colors.Snitch.ToHtmlStringRGBA() + ">Snitch</color> -> "; }
                    else if (role.Value == RoleEnum.Spy) { playerRole += "<color=#" + Patches.Colors.Spy.ToHtmlStringRGBA() + ">Spy</color> -> "; }
                    else if (role.Value == RoleEnum.Vigilante) { playerRole += "<color=#" + Patches.Colors.Vigilante.ToHtmlStringRGBA() + ">Vigilante</color> -> "; }
                    else if (role.Value == RoleEnum.Hunter) { playerRole += "<color=#" + Patches.Colors.Hunter.ToHtmlStringRGBA() + ">Hunter</color> -> "; }
                    else if (role.Value == RoleEnum.Arsonist) { playerRole += "<color=#" + Patches.Colors.Arsonist.ToHtmlStringRGBA() + ">Arsonist</color> -> "; }
                    else if (role.Value == RoleEnum.Executioner) { playerRole += "<color=#" + Patches.Colors.Executioner.ToHtmlStringRGBA() + ">Executioner</color> -> "; }
                    else if (role.Value == RoleEnum.Glitch) { playerRole += "<color=#" + Patches.Colors.Glitch.ToHtmlStringRGBA() + ">The Glitch</color> -> "; }
                    else if (role.Value == RoleEnum.Jester) { playerRole += "<color=#" + Patches.Colors.Jester.ToHtmlStringRGBA() + ">Jester</color> -> "; }
                    else if (role.Value == RoleEnum.Phantom) { playerRole += "<color=#" + Patches.Colors.Phantom.ToHtmlStringRGBA() + ">Phantom</color> -> "; }
                    else if (role.Value == RoleEnum.Grenadier) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Grenadier</color> -> "; }
                    else if (role.Value == RoleEnum.Janitor) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Janitor</color> -> "; }
                    else if (role.Value == RoleEnum.Miner) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Miner</color> -> "; }
                    else if (role.Value == RoleEnum.Morphling) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Morphling</color> -> "; }
                    else if (role.Value == RoleEnum.Swooper) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Swooper</color> -> "; }
                    else if (role.Value == RoleEnum.Undertaker) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Undertaker</color> -> "; }
                    else if (role.Value == RoleEnum.Haunter) { playerRole += "<color=#" + Patches.Colors.Haunter.ToHtmlStringRGBA() + ">Haunter</color> -> "; }
                    else if (role.Value == RoleEnum.Veteran) { playerRole += "<color=#" + Patches.Colors.Veteran.ToHtmlStringRGBA() + ">Veteran</color> -> "; }
                    else if (role.Value == RoleEnum.Amnesiac) { playerRole += "<color=#" + Patches.Colors.Amnesiac.ToHtmlStringRGBA() + ">Amnesiac</color> -> "; }
                    else if (role.Value == RoleEnum.Juggernaut) { playerRole += "<color=#" + Patches.Colors.Juggernaut.ToHtmlStringRGBA() + ">Juggernaut</color> -> "; }
                    else if (role.Value == RoleEnum.Tracker) { playerRole += "<color=#" + Patches.Colors.Tracker.ToHtmlStringRGBA() + ">Tracker</color> -> "; }
                    else if (role.Value == RoleEnum.Transporter) { playerRole += "<color=#" + Patches.Colors.Transporter.ToHtmlStringRGBA() + ">Transporter</color> -> "; }
                    else if (role.Value == RoleEnum.Traitor) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Traitor</color> -> "; }
                    else if (role.Value == RoleEnum.Medium) { playerRole += "<color=#" + Patches.Colors.Medium.ToHtmlStringRGBA() + ">Medium</color> -> "; }
                    else if (role.Value == RoleEnum.Trapper) { playerRole += "<color=#" + Patches.Colors.Trapper.ToHtmlStringRGBA() + ">Trapper</color> -> "; }
                    else if (role.Value == RoleEnum.Survivor) { playerRole += "<color=#" + Patches.Colors.Survivor.ToHtmlStringRGBA() + ">Survivor</color> -> "; }
                    else if (role.Value == RoleEnum.GuardianAngel) { playerRole += "<color=#" + Patches.Colors.GuardianAngel.ToHtmlStringRGBA() + ">Guardian Angel</color> -> "; }
                    else if (role.Value == RoleEnum.Mystic || role.Value == RoleEnum.CultistMystic) { playerRole += "<color=#" + Patches.Colors.Mystic.ToHtmlStringRGBA() + ">Mystic</color> -> "; }
                    else if (role.Value == RoleEnum.Blackmailer) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Blackmailer</color> -> "; }
                    else if (role.Value == RoleEnum.Plaguebearer) { playerRole += "<color=#" + Patches.Colors.Plaguebearer.ToHtmlStringRGBA() + ">Plaguebearer</color> -> "; }
                    else if (role.Value == RoleEnum.Pestilence) { playerRole += "<color=#" + Patches.Colors.Pestilence.ToHtmlStringRGBA() + ">Pestilence</color> -> "; }
                    else if (role.Value == RoleEnum.Werewolf) { playerRole += "<color=#" + Patches.Colors.Werewolf.ToHtmlStringRGBA() + ">Werewolf</color> -> "; }
                    else if (role.Value == RoleEnum.Detective) { playerRole += "<color=#" + Patches.Colors.Detective.ToHtmlStringRGBA() + ">Detective</color> -> "; }
                    else if (role.Value == RoleEnum.Escapist) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Escapist</color> -> "; }
                    else if (role.Value == RoleEnum.Necromancer) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Necromancer</color> -> "; }
                    else if (role.Value == RoleEnum.Whisperer) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Whisperer</color> -> "; }
                    else if (role.Value == RoleEnum.Chameleon) { playerRole += "<color=#" + Patches.Colors.Chameleon.ToHtmlStringRGBA() + ">Chameleon</color> -> "; }
                    else if (role.Value == RoleEnum.Imitator) { playerRole += "<color=#" + Patches.Colors.Imitator.ToHtmlStringRGBA() + ">Imitator</color> -> "; }
                    else if (role.Value == RoleEnum.Bomber) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Bomber</color> -> "; }
                    else if (role.Value == RoleEnum.Doomsayer) { playerRole += "<color=#" + Patches.Colors.Doomsayer.ToHtmlStringRGBA() + ">Doomsayer</color> -> "; }
                    else if (role.Value == RoleEnum.Vampire) { playerRole += "<color=#" + Patches.Colors.Vampire.ToHtmlStringRGBA() + ">Vampire</color> -> "; }
                    else if (role.Value == RoleEnum.VampireHunter) { playerRole += "<color=#" + Patches.Colors.VampireHunter.ToHtmlStringRGBA() + ">Vampire Hunter</color> -> "; }
                    else if (role.Value == RoleEnum.Prosecutor) { playerRole += "<color=#" + Patches.Colors.Prosecutor.ToHtmlStringRGBA() + ">Prosecutor</color> -> "; }
                    else if (role.Value == RoleEnum.Warlock) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Warlock</color> -> "; }
                    else if (role.Value == RoleEnum.Oracle) { playerRole += "<color=#" + Patches.Colors.Oracle.ToHtmlStringRGBA() + ">Oracle</color> -> "; }
                    else if (role.Value == RoleEnum.Venerer) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Venerer</color> -> "; }
                    else if (role.Value == RoleEnum.Aurial) { playerRole += "<color=#" + Patches.Colors.Aurial.ToHtmlStringRGBA() + ">Aurial</color> -> "; }
                    else if (role.Value == RoleEnum.Poisoner) { playerRole += "<color=#"+Patches.Colors.Impostor.ToHtmlStringRGBA()+">Poisoner</color> -> "; }
                    else if (role.Value == RoleEnum.Lawyer) { playerRole += "<color=#" + Patches.Colors.Lawyer.ToHtmlStringRGBA() + ">Lawyer</color> -> "; }
                    else if (role.Value == RoleEnum.Mercenary) { playerRole += "<color=#" + Patches.Colors.Mercenary.ToHtmlStringRGBA() + ">Mercenary</color> -> "; }
                    else if (role.Value == RoleEnum.Politician) { playerRole += "<color=#" + Patches.Colors.Politician.ToHtmlStringRGBA() + ">Politician</color> -> "; }
                    else if (role.Value == RoleEnum.Immortal) { playerRole += "<color=#" + Patches.Colors.Immortal.ToHtmlStringRGBA() + ">Immortal</color> -> "; }
                    else if (role.Value == RoleEnum.Scavenger) { playerRole += "<color=#" + Patches.Colors.Scavenger.ToHtmlStringRGBA() + ">Scavenger</color> -> "; }
                    if (CustomGameOptions.GameMode == GameMode.Cultist && playerControl.Data.IsImpostor())
                    {
                        if (role.Value == RoleEnum.Engineer) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Demolitionist</color> -> "; }
                        else if (role.Value == RoleEnum.Investigator) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Consigliere</color> -> "; }
                        else if (role.Value == RoleEnum.CultistMystic) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Clairvoyant</color> -> "; }
                        else if (role.Value == RoleEnum.CultistSnitch) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Informant</color> -> "; }
                        else if (role.Value == RoleEnum.Spy) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Rogue Agent</color> -> "; }
                        else if (role.Value == RoleEnum.Vigilante) { playerRole += "<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + ">Assassin</color> -> "; }
                    }
                }
                playerRole = playerRole.Remove(playerRole.Length - 4);

                if (playerControl.Is(ModifierEnum.Giant))
                {
                    playerRole += " (<color=#" + Patches.Colors.GlobalModifier.ToHtmlStringRGBA() + ">Giant</color>)";
                }
                else if (playerControl.Is(ModifierEnum.ButtonBarry))
                {
                    playerRole += " (<color=#" + Patches.Colors.GlobalModifier.ToHtmlStringRGBA() + ">Button Barry</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Aftermath))
                {
                    playerRole += " (<color=#" + Patches.Colors.CrewModifier.ToHtmlStringRGBA() + ">Aftermath</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Bait))
                {
                    playerRole += " (<color=#" + Patches.Colors.CrewModifier.ToHtmlStringRGBA() + ">Bait</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Diseased))
                {
                    playerRole += " (<color=#" + Patches.Colors.CrewModifier.ToHtmlStringRGBA() + ">Diseased</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Flash))
                {
                    playerRole += " (<color=#" + Patches.Colors.GlobalModifier.ToHtmlStringRGBA() + ">Flash</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Tiebreaker))
                {
                    playerRole += " (<color=#" + Patches.Colors.GlobalModifier.ToHtmlStringRGBA() + ">Tiebreaker</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Torch))
                {
                    playerRole += " (<color=#" + Patches.Colors.CrewModifier.ToHtmlStringRGBA() + ">Torch</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Lover))
                {
                    playerRole += " (<color=#" + Patches.Colors.Lovers.ToHtmlStringRGBA() + ">Lover</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Sleuth))
                {
                    playerRole += " (<color=#" + Patches.Colors.GlobalModifier.ToHtmlStringRGBA() + ">Sleuth</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Radar))
                {
                    playerRole += " (<color=#" + Patches.Colors.GlobalModifier.ToHtmlStringRGBA() + ">Radar</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Disperser))
                {
                    playerRole += " (<color=#" + Patches.Colors.ImpModifier.ToHtmlStringRGBA() + ">Disperser</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Multitasker))
                {
                    playerRole += " (<color=#" + Patches.Colors.CrewModifier.ToHtmlStringRGBA() + ">Multitasker</color>)";
                }
                else if (playerControl.Is(ModifierEnum.DoubleShot))
                {
                    playerRole += " (<color=#" + Patches.Colors.ImpModifier.ToHtmlStringRGBA() + ">Double Shot</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Underdog))
                {
                    playerRole += " (<color=#" + Patches.Colors.ImpModifier.ToHtmlStringRGBA() + ">Underdog</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Frosty))
                {
                    playerRole += " (<color=#" + Patches.Colors.CrewModifier.ToHtmlStringRGBA() + ">Frosty</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Drunk))
                {
                    playerRole += " (<color=#" + Patches.Colors.GlobalModifier.ToHtmlStringRGBA() + ">Drunk</color>)";
                }
                else if (playerControl.Is(ModifierEnum.Blind))
                {
                    playerRole += " (<color=#" + Patches.Colors.CrewModifier.ToHtmlStringRGBA() + ">Blind</color>)";
                }
                var player = Role.GetRole(playerControl);
                if (playerControl.Is(RoleEnum.Phantom) || playerControl.Is(Faction.Crewmates))
                {
                    if ((player.TotalTasks - player.TasksLeft)/player.TotalTasks == 1) playerRole += " | Tasks: <color=#" + Color.green.ToHtmlStringRGBA() + $">{player.TotalTasks - player.TasksLeft}/{player.TotalTasks}</color>";
                    else playerRole += $" | Tasks: {player.TotalTasks - player.TasksLeft}/{player.TotalTasks}";
                }
                if (player.Kills > 0 && !playerControl.Is(Faction.Crewmates))
                {
                    playerRole += " |<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + $"> Kills: {player.Kills}</color>";
                }
                if (player.CorrectKills > 0)
                {
                    playerRole += " |<color=#" + Color.green.ToHtmlStringRGBA() + $"> Correct Kills: {player.CorrectKills}</color>";
                }
                if (player.IncorrectKills > 0)
                {
                    playerRole += " |<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + $"> Incorrect Kills: {player.IncorrectKills}</color>";
                }
                if (player.CorrectAssassinKills > 0)
                {
                    playerRole += " |<color=#" + Color.green.ToHtmlStringRGBA() + $"> Correct Guesses: {player.CorrectAssassinKills}</color>";
                }
                if (player.IncorrectAssassinKills > 0)
                {
                    playerRole += " |<color=#" + Patches.Colors.Impostor.ToHtmlStringRGBA() + $"> Incorrect Guesses: {player.IncorrectAssassinKills}</color>";
                }
                AdditionalTempData.playerRoles.Add(new AdditionalTempData.PlayerRoleInfo() { PlayerName = playerControl.Data.PlayerName, Role = playerRole });
            }

            if (!CustomGameOptions.NeutralEvilWinEndsGame) {
                foreach (var doomsayer in Role.GetRoles(RoleEnum.Doomsayer))
                {
                    var doom = (Doomsayer)doomsayer;
                    if (doom.WonByGuessing) AdditionalTempData.otherWinners.Add(new AdditionalTempData.Winners() { PlayerName = doom.Player.Data.PlayerName, Role = RoleEnum.Doomsayer });
                }
                foreach (var executioner in Role.GetRoles(RoleEnum.Executioner))
                {
                    var exe = (Executioner)executioner;
                    if (exe.TargetVotedOut) AdditionalTempData.otherWinners.Add(new AdditionalTempData.Winners() { PlayerName = exe.Player.Data.PlayerName, Role = RoleEnum.Executioner });
                }
                foreach (var jester in Role.GetRoles(RoleEnum.Jester))
                {
                    var jest = (Jester)jester;
                    if (jest.VotedOut) AdditionalTempData.otherWinners.Add(new AdditionalTempData.Winners() { PlayerName = jest.Player.Data.PlayerName, Role = RoleEnum.Jester });
                }
                foreach (var scavenger in Role.GetRoles(RoleEnum.Scavenger))
                {
                    var scav = (Scavenger)scavenger;
                    if (scav.WonByDevouring) AdditionalTempData.otherWinners.Add(new AdditionalTempData.Winners() { PlayerName = scav.Player.Data.PlayerName, Role = RoleEnum.Scavenger });
                }
                foreach (var phantom in Role.GetRoles(RoleEnum.Phantom))
                {
                    var phan = (Phantom)phantom;
                    if (phan.CompletedTasks) AdditionalTempData.otherWinners.Add(new AdditionalTempData.Winners() { PlayerName = phan.Player.Data.PlayerName, Role = RoleEnum.Phantom });
                }
            }

            // Remove Neutrals from winners (if they win, they'll be readded)
            List<PlayerControl> notWinners = new List<PlayerControl>();
            foreach (var role in Role.GetRoles(RoleEnum.Amnesiac))
            {
                var amne = (Amnesiac)role;
                notWinners.Add(amne.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.GuardianAngel))
            {
                var ga = (GuardianAngel)role;
                notWinners.Add(ga.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Lawyer))
            {
                var lwyr = (Lawyer)role;
                notWinners.Add(lwyr.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Mercenary))
            {
                var merc = (Mercenary)role;
                notWinners.Add(merc.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Survivor))
            {
                var surv = (Survivor)role;
                notWinners.Add(surv.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Doomsayer))
            {
                var doom = (Doomsayer)role;
                notWinners.Add(doom.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Executioner))
            {
                var exe = (Executioner)role;
                notWinners.Add(exe.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Jester))
            {
                var jest = (Jester)role;
                notWinners.Add(jest.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Scavenger))
            {
                var scav = (Scavenger)role;
                notWinners.Add(scav.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Phantom))
            {
                var phan = (Phantom)role;
                notWinners.Add(phan.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Arsonist))
            {
                var arso = (Arsonist)role;
                notWinners.Add(arso.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Juggernaut))
            {
                var jugg = (Juggernaut)role;
                notWinners.Add(jugg.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
            {
                var pest = (Pestilence)role;
                notWinners.Add(pest.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Plaguebearer))
            {
                var pb = (Plaguebearer)role;
                notWinners.Add(pb.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Glitch))
            {
                var glitch = (Glitch)role;
                notWinners.Add(glitch.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Vampire))
            {
                var vamp = (Vampire)role;
                notWinners.Add(vamp.Player);
            }
            foreach (var role in Role.GetRoles(RoleEnum.Werewolf))
            {
                var ww = (Werewolf)role;
                notWinners.Add(ww.Player);
            }

            List<CachedPlayerData> winnersToRemove = new List<CachedPlayerData>();
            foreach (CachedPlayerData winner in EndGameResult.CachedWinners.GetFastEnumerator()) {
                if (notWinners.Any(x => x.Data.PlayerName == winner.PlayerName)) winnersToRemove.Add(winner);
            }
            foreach (var winner in winnersToRemove) EndGameResult.CachedWinners.Remove(winner);

            bool LoversWin = false;
            foreach (var modifier in Modifier.GetModifiers(ModifierEnum.Lover))
            {
                var lover = (Lover)modifier;
                LoversWin = Utils.LoversExistingAndAlive() && (gameOverReason == (GameOverReason)CustomGameOverReason.LoversWin || (GameManager.Instance.DidHumansWin(gameOverReason) && !Utils.LoversExistingWithKiller()));
            }
            bool DoomsayerWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Doomsayer))
            {
                var Doomsayer = (Doomsayer)role;
                DoomsayerWin = Doomsayer.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.DoomsayerWin && CustomGameOptions.NeutralEvilWinEndsGame;
            }
            bool ExecutionerWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Executioner))
            {
                var Executioner = (Executioner)role;
                ExecutionerWin = Executioner.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.ExecutionerWin && CustomGameOptions.NeutralEvilWinEndsGame;
            }
            bool JesterWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Jester))
            {
                var Jester = (Jester)role;
                JesterWin = Jester.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.JesterWin && CustomGameOptions.NeutralEvilWinEndsGame;
            }
            bool ScavengerWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Scavenger))
            {
                var Scavenger = (Scavenger)role;
                ScavengerWin = Scavenger.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.ScavengerWin && CustomGameOptions.NeutralEvilWinEndsGame;
            }
            bool PhantomWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Phantom))
            {
                var Phantom = (Phantom)role;
                PhantomWin = Phantom.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.PhantomWin && CustomGameOptions.NeutralEvilWinEndsGame;
            }
            bool ArsonistWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Arsonist))
            {
                var Arsonist = (Arsonist)role;
                ArsonistWin = Arsonist.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.ArsonistWin;
            }
            bool JuggernautWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Juggernaut))
            {
                var Juggernaut = (Juggernaut)role;
                JuggernautWin = Juggernaut.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.JuggernautWin;
            }
            bool PestilenceWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
            {
                var Pestilence = (Pestilence)role;
                PestilenceWin = Pestilence.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.PestilenceWin;
            }
            bool PlaguebearerWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Plaguebearer))
            {
                var Plaguebearer = (Plaguebearer)role;
                PlaguebearerWin = Plaguebearer.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.PlaguebearerWin;
            }
            bool GlitchWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Glitch))
            {
                var Glitch = (Glitch)role;
                GlitchWin = Glitch.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.GlitchWin;
            }
            bool VampireWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Vampire))
            {
                var Vampire = (Vampire)role;
                VampireWin = Vampire.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.TeamVampiresWin;
            }
            bool WerewolfWin = false;
            foreach (var role in Role.GetRoles(RoleEnum.Werewolf))
            {
                var Werewolf = (Werewolf)role;
                WerewolfWin = Werewolf.Player != null && gameOverReason == (GameOverReason)CustomGameOverReason.WerewolfWin;
            }

            // Lovers Win
            if (LoversWin)
            {
                // Double win for lovers, crewmates also win
                if (!Utils.LoversExistingWithKiller())
                {
                    AdditionalTempData.winCondition = WinCondition.LoversTeamWin;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    foreach (PlayerControl p in PlayerControl.AllPlayerControls) {
                        if (p == null) continue;
                        if (p.Is(ModifierEnum.Lover))
                            EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                        else if (p.Is(RoleEnum.Survivor) && !p.Data.IsDead)
                            EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                        else if (p.Is(RoleEnum.GuardianAngel) && Role.GetRole<GuardianAngel>(p).target.Is(Faction.Crewmates))
                            EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                        else if (p.Is(RoleEnum.Lawyer) && Role.GetRole<Lawyer>(p).target.Is(Faction.Crewmates))
                            EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                        else if (p.Is(RoleEnum.Mercenary) && Role.GetRole<Mercenary>(p).HasEnoughBrilders && !p.Data.IsDead)
                            EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                        else if (p.Is(Faction.Crewmates))
                            EndGameResult.CachedWinners.Add(new CachedPlayerData(p.Data));
                    }
                }
                // Lovers solo win
                else
                {
                    AdditionalTempData.winCondition = WinCondition.LoversSoloWin;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    foreach (var modifier in Modifier.GetModifiers(ModifierEnum.Lover))
                    {
                        var lover = (Lover)modifier;
                        EndGameResult.CachedWinners.Add(new CachedPlayerData(lover.Player.Data));
                        EndGameResult.CachedWinners.Add(new CachedPlayerData(lover.OtherLover.Player.Data));
                    }
                }
            }
            // Doomsayer Win
            else if (DoomsayerWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Doomsayer))
                {
                    var Doomsayer = (Doomsayer)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Doomsayer.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    AdditionalTempData.winCondition = WinCondition.DoomsayerWin;
                }
            }
            // Executioner Win
            else if (ExecutionerWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Executioner))
                {
                    var Executioner = (Executioner)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Executioner.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    AdditionalTempData.winCondition = WinCondition.ExecutionerWin;
                }
            }
            // Jester Win
            else if (JesterWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Jester))
                {
                    var Jester = (Jester)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Jester.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    AdditionalTempData.winCondition = WinCondition.JesterWin;
                }
            }
            // Scavenger Win
            else if (ScavengerWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Scavenger))
                {
                    var Scavenger = (Scavenger)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Scavenger.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    AdditionalTempData.winCondition = WinCondition.ScavengerWin;
                }
            }
            // Phantom Win
            else if (PhantomWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Phantom))
                {
                    var Phantom = (Phantom)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Phantom.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    AdditionalTempData.winCondition = WinCondition.PhantomWin;
                }
            }
            // Arsonist Win
            else if (ArsonistWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Arsonist))
                {
                    var Arsonist = (Arsonist)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Arsonist.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    wpd.IsImpostor = false;
                    AdditionalTempData.winCondition = WinCondition.ArsonistWin;
                }
            }
            // Juggernaut Win
            else if (JuggernautWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Juggernaut))
                {
                    var Juggernaut = (Juggernaut)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Juggernaut.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    wpd.IsImpostor = false;
                    AdditionalTempData.winCondition = WinCondition.JuggernautWin;
                }
            }
            // Pestilence Win
            else if (PestilenceWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
                {
                    var Pestilence = (Pestilence)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Pestilence.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    wpd.IsImpostor = false;
                    AdditionalTempData.winCondition = WinCondition.PestilenceWin;
                }
            }
            // Plaguebearer Win
            else if (PlaguebearerWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Plaguebearer))
                {
                    var Plaguebearer = (Plaguebearer)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Plaguebearer.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    wpd.IsImpostor = false;
                    AdditionalTempData.winCondition = WinCondition.PlaguebearerWin;
                }
            }
            // Glitch Win
            else if (GlitchWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Glitch))
                {
                    var Glitch = (Glitch)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Glitch.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    wpd.IsImpostor = false;
                    AdditionalTempData.winCondition = WinCondition.GlitchWin;
                }
            }
            // Vampire Win
            else if (VampireWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Vampire))
                {
                    var Vampire = (Vampire)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Vampire.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    wpd.IsImpostor = false;
                    AdditionalTempData.winCondition = WinCondition.TeamVampiresWin;
                }
            }
            // Werewolf Win
            else if (WerewolfWin)
            {
                foreach (var role in Role.GetRoles(RoleEnum.Werewolf))
                {
                    var Werewolf = (Werewolf)role;
                    EndGameResult.CachedWinners = new Il2CppSystem.Collections.Generic.List<CachedPlayerData>();
                    CachedPlayerData wpd = new CachedPlayerData(Werewolf.Player.Data);
                    EndGameResult.CachedWinners.Add(wpd);
                    wpd.IsImpostor = false;
                    AdditionalTempData.winCondition = WinCondition.WerewolfWin;
                }
            }

            // Possible Additional winner: Survivor
            foreach (var role in Role.GetRoles(RoleEnum.Survivor))
            {
                var surv = (Survivor)role;
                if (surv.Player != null && !surv.Player.Data.IsDead && !surv.Player.Data.Disconnected)
                {
                    if (!EndGameResult.CachedWinners.ToArray().Any(x => x.PlayerName == surv.Player.Data.PlayerName))
                        EndGameResult.CachedWinners.Add(new CachedPlayerData(surv.Player.Data));
                    AdditionalTempData.additionalWinConditions.Add(WinCondition.AdditionalAliveSurvivorWin); // The Survivor wins if alive
                }
            }

            // Possible Additional winner: Guardian Angel
            foreach (var role in Role.GetRoles(RoleEnum.GuardianAngel))
            {
                CachedPlayerData winningClient = null;
                var ga = (GuardianAngel)role;
                if (ga.Player != null && !ga.Player.Data.Disconnected && ga.target != null)
                {
                    foreach (CachedPlayerData winner in EndGameResult.CachedWinners.GetFastEnumerator()) {
                        if (winner.PlayerName == ga.target.Data.PlayerName)
                            winningClient = winner;
                    }
                    if (winningClient != null) { // The GA wins if the client is winning
                        if (!EndGameResult.CachedWinners.ToArray().Any(x => x.PlayerName == ga.Player.Data.PlayerName))
                            EndGameResult.CachedWinners.Add(new CachedPlayerData(ga.Player.Data));
                        AdditionalTempData.additionalWinConditions.Add(WinCondition.AdditionalGuardianAngelWin); // The GA wins together with the client
                    }
                }
            }
            
            // Possible Additional winner: Lawyer
            foreach (var role in Role.GetRoles(RoleEnum.Lawyer))
            {
                CachedPlayerData winningClient = null;
                var lawyer = (Lawyer)role;
                if (lawyer.Player != null && !lawyer.Player.Data.Disconnected && lawyer.target != null)
                {
                    foreach (CachedPlayerData winner in EndGameResult.CachedWinners.GetFastEnumerator()) {
                        if (winner.PlayerName == lawyer.target.Data.PlayerName)
                            winningClient = winner;
                    }
                    if (winningClient != null) { // The Lawyer wins if the client is winning
                        if (!EndGameResult.CachedWinners.ToArray().Any(x => x.PlayerName == lawyer.Player.Data.PlayerName))
                            EndGameResult.CachedWinners.Add(new CachedPlayerData(lawyer.Player.Data));
                        AdditionalTempData.additionalWinConditions.Add(WinCondition.AdditionalLawyerWin); // The Lawyer wins together with the client
                    }
                }
            }

            // Possible Additional winner: Mercenary
            foreach (var role in Role.GetRoles(RoleEnum.Mercenary))
            {
                var merc = (Mercenary)role;
                if (!merc.Player.Data.IsDead && !merc.Player.Data.Disconnected && merc.HasEnoughBrilders)
                {
                    if (!EndGameResult.CachedWinners.ToArray().Any(x => x.PlayerName == merc.Player.Data.PlayerName))
                        EndGameResult.CachedWinners.Add(new CachedPlayerData(merc.Player.Data));
                    AdditionalTempData.additionalWinConditions.Add(WinCondition.AdditionalAliveMercenaryWin); // The Merc wins if alive and do his task
                }
            }
        }
    }

    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public class EndGameManagerSetUpPatch {
        public static void Postfix(EndGameManager __instance) {
            // Delete and readd PoolablePlayers always showing the name and role of the player
            foreach (PoolablePlayer pb in __instance.transform.GetComponentsInChildren<PoolablePlayer>()) {
                UnityEngine.Object.Destroy(pb.gameObject);
            }

            int num = Mathf.CeilToInt(7.5f);
            List<CachedPlayerData> list = EndGameResult.CachedWinners.ToArray().ToList().OrderBy(delegate(CachedPlayerData b)
            {
                if (!b.IsYou)
                {
                    return 0;
                }
                return -1;
            }).ToList<CachedPlayerData>();

            for (int i = 0; i < list.Count; i++) {
                CachedPlayerData CachedPlayerData2 = list[i];
                int num2 = (i % 2 == 0) ? -1 : 1;
                int num3 = (i + 1) / 2;
                float num4 = (float)num3 / (float)num;
                float num5 = Mathf.Lerp(1f, 0.75f, num4);
                float num6 = (float)((i == 0) ? -8 : -1);
                PoolablePlayer poolablePlayer = UnityEngine.Object.Instantiate<PoolablePlayer>(__instance.PlayerPrefab, __instance.transform);
                poolablePlayer.transform.localPosition = new Vector3(1f * (float)num2 * (float)num3 * num5, FloatRange.SpreadToEdges(-1.125f, 0f, num3, num), num6 + (float)num3 * 0.01f) * 0.9f;
                float num7 = Mathf.Lerp(1f, 0.65f, num4) * 0.9f;
                Vector3 vector = new Vector3(num7, num7, 1f);
                poolablePlayer.transform.localScale = vector;
                if (CachedPlayerData2.IsDead) {
                    poolablePlayer.SetBodyAsGhost();
                    poolablePlayer.SetDeadFlipX(i % 2 == 0);
                } else {
                    poolablePlayer.SetFlipX(i % 2 == 0);
                }
                poolablePlayer.UpdateFromPlayerOutfit(CachedPlayerData2.Outfit, PlayerMaterial.MaskType.None, CachedPlayerData2.IsDead, true);

                poolablePlayer.cosmetics.nameText.color = Color.white;
                poolablePlayer.cosmetics.nameText.transform.localScale = new Vector3(1f / vector.x, 1f / vector.y, 1f / vector.z);
                poolablePlayer.cosmetics.nameText.transform.localPosition = new Vector3(poolablePlayer.cosmetics.nameText.transform.localPosition.x, poolablePlayer.cosmetics.nameText.transform.localPosition.y, -15f);
                poolablePlayer.cosmetics.nameText.text = CachedPlayerData2.PlayerName;
            }

            // Additional code
            GameObject bonusText = UnityEngine.Object.Instantiate(__instance.WinText.gameObject);
            bonusText.transform.position = new Vector3(__instance.WinText.transform.position.x, __instance.WinText.transform.position.y - 0.5f, __instance.WinText.transform.position.z);
            bonusText.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            TMPro.TMP_Text textRenderer = bonusText.GetComponent<TMPro.TMP_Text>();
            textRenderer.text = "";

            if (AdditionalTempData.winCondition == WinCondition.LoversTeamWin)
            {
                textRenderer.text = "Lovers And Crewmates Win";
                textRenderer.color = Colors.Lovers;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Lovers);
            }
            if (AdditionalTempData.winCondition == WinCondition.LoversSoloWin)
            {
                textRenderer.text = "Lovers Wins";
                textRenderer.color = Colors.Lovers;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Lovers);
            }
            if (AdditionalTempData.winCondition == WinCondition.JesterWin)
            {
                textRenderer.text = "Jester Wins";
                textRenderer.color = Colors.Jester;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Jester);
            }
            if (AdditionalTempData.winCondition == WinCondition.ExecutionerWin)
            {
                textRenderer.text = "Executioner Wins";
                textRenderer.color = Colors.Executioner;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Executioner);
            }
            if (AdditionalTempData.winCondition == WinCondition.DoomsayerWin)
            {
                textRenderer.text = "Doomsayer Wins";
                textRenderer.color = Colors.Doomsayer;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Doomsayer);
            }
            if (AdditionalTempData.winCondition == WinCondition.ScavengerWin)
            {
                textRenderer.text = "Scavenger Wins";
                textRenderer.color = Colors.Scavenger;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Scavenger);
            }
            if (AdditionalTempData.winCondition == WinCondition.PhantomWin)
            {
                textRenderer.text = "Phantom Wins";
                textRenderer.color = Colors.Phantom;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Phantom);
            }
            if (AdditionalTempData.winCondition == WinCondition.TeamVampiresWin)
            {
                textRenderer.text = "Team Vampires Wins";
                textRenderer.color = Colors.Vampire;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Vampire);
            }
            if (AdditionalTempData.winCondition == WinCondition.GlitchWin)
            {
                textRenderer.text = "The Glitch Wins";
                textRenderer.color = Colors.Glitch;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Glitch);
            }
            if (AdditionalTempData.winCondition == WinCondition.JuggernautWin)
            {
                textRenderer.text = "Juggernaut Wins";
                textRenderer.color = Colors.Juggernaut;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Juggernaut);
            }
            if (AdditionalTempData.winCondition == WinCondition.ArsonistWin)
            {
                textRenderer.text = "Arsonist Wins";
                textRenderer.color = Colors.Arsonist;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Arsonist);
            }
            if (AdditionalTempData.winCondition == WinCondition.PlaguebearerWin)
            {
                textRenderer.text = "Plaguebearer Wins";
                textRenderer.color = Colors.Plaguebearer;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Plaguebearer);
            }
            if (AdditionalTempData.winCondition == WinCondition.PestilenceWin)
            {
                textRenderer.text = "Pestilence Wins";
                textRenderer.color = Colors.Pestilence;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Pestilence);
            }
            if (AdditionalTempData.winCondition == WinCondition.WerewolfWin)
            {
                textRenderer.text = "Werewolf Wins";
                textRenderer.color = Colors.Werewolf;
                __instance.BackgroundBar.material.SetColor("_Color", Colors.Werewolf);
            }

            foreach (WinCondition cond in AdditionalTempData.additionalWinConditions) {
                if (cond == WinCondition.AdditionalLawyerWin) {
                    textRenderer.text += $"\n{Utils.ColorString(Colors.Lawyer, "The Lawyer wins with the client")}";
                } else if (cond == WinCondition.AdditionalGuardianAngelWin) {
                    textRenderer.text += $"\n{Utils.ColorString(Colors.GuardianAngel, "The Guardian Angel wins with the target")}";
                } else if (cond == WinCondition.AdditionalAliveMercenaryWin) {
                    textRenderer.text += $"\n{Utils.ColorString(Colors.Mercenary, "The Mercenary has enough brilders")}";
                } else if (cond == WinCondition.AdditionalAliveSurvivorWin) {
                    textRenderer.text += $"\n{Utils.ColorString(Colors.Survivor, "The Survivor alive")}";
                }
            }

            var position = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, Camera.main.nearClipPlane));
            GameObject roleSummary = UnityEngine.Object.Instantiate(__instance.WinText.gameObject);
            roleSummary.transform.position = new Vector3(__instance.Navigation.ExitButton.transform.position.x + 0.1f, position.y - 0.1f, -214f); 
            roleSummary.transform.localScale = new Vector3(1f, 1f, 1f);

            var roleSummaryText = new StringBuilder();
            roleSummaryText.AppendLine("Players and roles at the end of the game:");
            foreach (var data in AdditionalTempData.playerRoles)
            {
                var role = string.Join(" ", data.Role);
                roleSummaryText.AppendLine($"{data.PlayerName} - {role}");
            }

            if (AdditionalTempData.otherWinners.Count != 0)
            {
                roleSummaryText.AppendLine("\n\n\nOther Winners:");
                foreach (var data in AdditionalTempData.otherWinners)
                {
                    if (data.Role == RoleEnum.Doomsayer) roleSummaryText.AppendLine("<color=#" + Patches.Colors.Doomsayer.ToHtmlStringRGBA() + $">{data.PlayerName}</color>");
                    else if (data.Role == RoleEnum.Executioner) roleSummaryText.AppendLine("<color=#" + Patches.Colors.Executioner.ToHtmlStringRGBA() + $">{data.PlayerName}</color>");
                    else if (data.Role == RoleEnum.Jester) roleSummaryText.AppendLine("<color=#" + Patches.Colors.Jester.ToHtmlStringRGBA() + $">{data.PlayerName}</color>");
                    else if (data.Role == RoleEnum.Phantom) roleSummaryText.AppendLine("<color=#" + Patches.Colors.Phantom.ToHtmlStringRGBA() + $">{data.PlayerName}</color>");
                    else if (data.Role == RoleEnum.Scavenger) roleSummaryText.AppendLine("<color=#" + Patches.Colors.Scavenger.ToHtmlStringRGBA() + $">{data.PlayerName}</color>");
                }
            }

            TMPro.TMP_Text roleSummaryTextMesh = roleSummary.GetComponent<TMPro.TMP_Text>();
            roleSummaryTextMesh.alignment = TMPro.TextAlignmentOptions.TopLeft;
            roleSummaryTextMesh.color = Color.white;
            roleSummaryTextMesh.fontSizeMin = 1.5f;
            roleSummaryTextMesh.fontSizeMax = 1.5f;
            roleSummaryTextMesh.fontSize = 1.5f;
                
            var roleSummaryTextMeshRectTransform = roleSummaryTextMesh.GetComponent<RectTransform>();
            roleSummaryTextMeshRectTransform.anchoredPosition = new Vector2(position.x + 3.5f, position.y - 0.1f);
            roleSummaryTextMesh.text = roleSummaryText.ToString();

            AdditionalTempData.clear();
        }
    }

    [HarmonyPatch(typeof(LogicGameFlowNormal), nameof(LogicGameFlowNormal.CheckEndCriteria))] 
    class CheckEndCriteriaPatch {
        public static bool Prefix(ShipStatus __instance) {
            if (!GameData.Instance) return false;
            if (DestroyableSingleton<TutorialManager>.InstanceExists) // InstanceExists | Don't check Custom Criteria when in Tutorial
                return true;
            var statistics = new PlayerStatistics(__instance);
            if (CheckAndEndGameForJesterWin(__instance)) return false;
            if (CheckAndEndGameForExecutionerWin(__instance)) return false;
            if (CheckAndEndGameForDoomsayerWin(__instance)) return false;
            if (CheckAndEndGameForScavengerWin(__instance)) return false;
            if (CheckAndEndGameForPhantomWin(__instance)) return false;
            if (CheckAndEndGameForSabotageWin(__instance)) return false;
            if (CheckAndEndGameForTaskWin(__instance)) return false;
            if (CheckAndEndGameForLoversSoloWin(__instance, statistics)) return false;
            if (CheckAndEndGameForTeamVampiresWin(__instance, statistics)) return false;
            if (CheckAndEndGameForGlitchWin(__instance, statistics)) return false;
            if (CheckAndEndGameForJuggernautWin(__instance, statistics)) return false;
            if (CheckAndEndGameForArsonistWin(__instance, statistics)) return false;
            if (CheckAndEndGameForPlaguebearerWin(__instance, statistics)) return false;
            if (CheckAndEndGameForPestilenceWin(__instance, statistics)) return false;
            if (CheckAndEndGameForWerewolfWin(__instance, statistics)) return false;
            if (CheckAndEndGameForImpostorWin(__instance, statistics)) return false;
            if (CheckAndEndGameForCrewmateWin(__instance, statistics)) return false;
            return false;
        }

        private static bool CheckAndEndGameForJesterWin(ShipStatus __instance) {
            foreach (var role in Role.GetRoles(RoleEnum.Jester))
            {
                var jester = (Jester)role;
                if (jester.VotedOut && CustomGameOptions.NeutralEvilWinEndsGame)
                {
                    GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.JesterWin, false);
                    return true;
                }
            }
            return false;
        }
        private static bool CheckAndEndGameForExecutionerWin(ShipStatus __instance) {
            foreach (var role in Role.GetRoles(RoleEnum.Executioner))
            {
                var exe = (Executioner)role;
                if (exe.TargetVotedOut && CustomGameOptions.NeutralEvilWinEndsGame)
                {
                    GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.ExecutionerWin, false);
                    return true;
                }
            }
            return false;
        }
        private static bool CheckAndEndGameForDoomsayerWin(ShipStatus __instance) {
            foreach (var role in Role.GetRoles(RoleEnum.Doomsayer))
            {
                var doom = (Doomsayer)role;
                if (doom.WonByGuessing && CustomGameOptions.NeutralEvilWinEndsGame)
                {
                    GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.DoomsayerWin, false);
                    return true;
                }
            }
            return false;
        }
        private static bool CheckAndEndGameForScavengerWin(ShipStatus __instance) {
            foreach (var role in Role.GetRoles(RoleEnum.Scavenger))
            {
                var scavenger = (Scavenger)role;
                if (scavenger.WonByDevouring && CustomGameOptions.NeutralEvilWinEndsGame)
                {
                    GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.ScavengerWin, false);
                    return true;
                }
            }
            return false;
        }
        private static bool CheckAndEndGameForPhantomWin(ShipStatus __instance) {
            foreach (var role in Role.GetRoles(RoleEnum.Phantom))
            {
                var phantom = (Phantom)role;
                if (phantom.CompletedTasks && !phantom.Caught && CustomGameOptions.NeutralEvilWinEndsGame)
                {
                    GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.PhantomWin, false);
                    return true;
                }
            }
            return false;
        }
        private static bool CheckAndEndGameForSabotageWin(ShipStatus __instance) {
            if (ShipStatus.Instance.Systems == null) return false;
            var systemType = ShipStatus.Instance.Systems.ContainsKey(SystemTypes.LifeSupp) ? ShipStatus.Instance.Systems[SystemTypes.LifeSupp] : null;
            if (systemType != null) {
                LifeSuppSystemType lifeSuppSystemType = systemType.TryCast<LifeSuppSystemType>();
                if (lifeSuppSystemType != null && lifeSuppSystemType.Countdown < 0f) {
                    EndGameForSabotage(__instance);
                    lifeSuppSystemType.Countdown = 10000f;
                    return true;
                }
            }
            var systemType2 = ShipStatus.Instance.Systems.ContainsKey(SystemTypes.Reactor) ? ShipStatus.Instance.Systems[SystemTypes.Reactor] : null;
            if (systemType2 == null) {
                systemType2 = ShipStatus.Instance.Systems.ContainsKey(SystemTypes.Laboratory) ? ShipStatus.Instance.Systems[SystemTypes.Laboratory] : null;
            }
            if (systemType2 != null) {
                ICriticalSabotage criticalSystem = systemType2.TryCast<ICriticalSabotage>();
                if (criticalSystem != null && criticalSystem.Countdown < 0f) {
                    EndGameForSabotage(__instance);
                    criticalSystem.ClearSabotage();
                    return true;
                }
            }
            return false;
        }
        private static bool CheckAndEndGameForTaskWin(ShipStatus __instance) {
            if (GameData.Instance.TotalTasks > 0 && GameData.Instance.TotalTasks <= GameData.Instance.CompletedTasks) {
                GameManager.Instance.RpcEndGame(GameOverReason.HumansByTask, false);
                return true;
            }
            return false;
        }

        private static bool CheckAndEndGameForLoversSoloWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.TeamLoversAlive == 2 && statistics.TotalAlive <= 3) {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.LoversWin, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForTeamVampiresWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.VampiresAlive >= statistics.TotalAlive - statistics.VampiresAlive && statistics.TeamImpostorsAlive == 0 && statistics.WerewolfAlive == 0 && statistics.GlitchAlive == 0 && statistics.JuggernautAlive == 0 && statistics.ArsonistAlive == 0 && statistics.PlaguebearerAlive == 0 && statistics.PestilenceAlive == 0 && !(statistics.VampiresHasLover && statistics.TeamLoversAlive == 2) && !Utils.isVHBlockGameEndForVampires() && !Utils.isSomeOneBlockGameEndForNonImps()) {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.TeamVampiresWin, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForGlitchWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.GlitchAlive >= statistics.TotalAlive - statistics.GlitchAlive && statistics.TeamImpostorsAlive == 0 && statistics.VampiresAlive == 0 && statistics.WerewolfAlive == 0 && statistics.JuggernautAlive == 0 && statistics.ArsonistAlive == 0 && statistics.PlaguebearerAlive == 0 && statistics.PestilenceAlive == 0 && !(statistics.GlitchHasLover && statistics.TeamLoversAlive == 2) && !Utils.isSomeOneBlockGameEndForNonImps()) {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.GlitchWin, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForJuggernautWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.JuggernautAlive >= statistics.TotalAlive - statistics.JuggernautAlive && statistics.TeamImpostorsAlive == 0 && statistics.VampiresAlive == 0 && statistics.GlitchAlive == 0 && statistics.WerewolfAlive == 0 && statistics.ArsonistAlive == 0 && statistics.PlaguebearerAlive == 0 && statistics.PestilenceAlive == 0 && !(statistics.JuggernautHasLover && statistics.TeamLoversAlive == 2) && !Utils.isSomeOneBlockGameEndForNonImps()) {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.JuggernautWin, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForArsonistWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.ArsonistAlive >= statistics.TotalAlive - statistics.ArsonistAlive && statistics.TeamImpostorsAlive == 0 && statistics.VampiresAlive == 0 && statistics.GlitchAlive == 0 && statistics.JuggernautAlive == 0 && statistics.WerewolfAlive == 0 && statistics.PlaguebearerAlive == 0 && statistics.PestilenceAlive == 0 && !(statistics.ArsonistHasLover && statistics.TeamLoversAlive == 2) && !Utils.isSomeOneBlockGameEndForNonImps()) {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.ArsonistWin, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForPlaguebearerWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.PlaguebearerAlive >= statistics.TotalAlive - statistics.PlaguebearerAlive && statistics.TeamImpostorsAlive == 0 && statistics.VampiresAlive == 0 && statistics.GlitchAlive == 0 && statistics.JuggernautAlive == 0 && statistics.ArsonistAlive == 0 && statistics.WerewolfAlive == 0 && statistics.PestilenceAlive == 0 && !(statistics.PlaguebearerHasLover && statistics.TeamLoversAlive == 2) && !Utils.isSomeOneBlockGameEndForNonImps()) {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.PlaguebearerWin, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForPestilenceWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.PestilenceAlive >= statistics.TotalAlive - statistics.PestilenceAlive && statistics.TeamImpostorsAlive == 0 && statistics.VampiresAlive == 0 && statistics.GlitchAlive == 0 && statistics.JuggernautAlive == 0 && statistics.ArsonistAlive == 0 && statistics.PlaguebearerAlive == 0 && statistics.WerewolfAlive == 0 && !(statistics.PestilenceHasLover && statistics.TeamLoversAlive == 2) && !Utils.isSomeOneBlockGameEndForNonImps()) {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.PestilenceWin, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForWerewolfWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.WerewolfAlive >= statistics.TotalAlive - statistics.WerewolfAlive && statistics.TeamImpostorsAlive == 0 && statistics.VampiresAlive == 0 && statistics.GlitchAlive == 0 && statistics.JuggernautAlive == 0 && statistics.ArsonistAlive == 0 && statistics.PlaguebearerAlive == 0 && statistics.PestilenceAlive == 0 && !(statistics.WerewolfHasLover && statistics.TeamLoversAlive == 2) && !Utils.isSomeOneBlockGameEndForNonImps()) {
                GameManager.Instance.RpcEndGame((GameOverReason)CustomGameOverReason.WerewolfWin, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForImpostorWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.TeamImpostorsAlive >= statistics.TotalAlive - statistics.TeamImpostorsAlive && statistics.VampiresAlive == 0 && statistics.GlitchAlive == 0 && statistics.JuggernautAlive == 0 && statistics.ArsonistAlive == 0 && statistics.PlaguebearerAlive == 0 && statistics.PestilenceAlive == 0 && statistics.WerewolfAlive == 0 && !(statistics.TeamImpostorsHasLover && statistics.TeamLoversAlive == 2) && !Utils.isSomeOneBlockGameEndForImps()) {
                GameOverReason endReason;
                switch (GameData.LastDeathReason) {
                    case DeathReason.Exile:
                        endReason = GameOverReason.ImpostorByVote;
                        break;
                    case DeathReason.Kill:
                        endReason = GameOverReason.ImpostorByKill;
                        break;
                    default:
                        endReason = GameOverReason.ImpostorByVote;
                        break;
                }
                GameManager.Instance.RpcEndGame(endReason, false);
                return true;
            }
            return false;
        }
        private static bool CheckAndEndGameForCrewmateWin(ShipStatus __instance, PlayerStatistics statistics) {
            if (statistics.TeamImpostorsAlive == 0 && statistics.VampiresAlive == 0 && statistics.GlitchAlive == 0 && statistics.JuggernautAlive == 0 && statistics.ArsonistAlive == 0 && statistics.PlaguebearerAlive == 0 && statistics.PestilenceAlive == 0 && statistics.WerewolfAlive == 0 && !Utils.isSomeOneBlockGameEndForCrew()) {
                GameManager.Instance.RpcEndGame(GameOverReason.HumansByVote, false);
                return true;
            }
            return false;
        }

        private static void EndGameForSabotage(ShipStatus __instance) {
            //__instance.enabled = false;
            GameManager.Instance.RpcEndGame(GameOverReason.ImpostorBySabotage, false);
            return;
        }
    }

    internal class PlayerStatistics {
        public int TeamImpostorsAlive {get;set;}
        public int TeamLoversAlive {get;set;}
        public int VampiresAlive {get;set;}
        public int GlitchAlive {get;set;}
        public int JuggernautAlive {get;set;}
        public int ArsonistAlive {get;set;}
        public int PlaguebearerAlive {get;set;}
        public int PestilenceAlive {get;set;}
        public int WerewolfAlive {get;set;}
        public int TotalAlive {get;set;}
        public bool TeamImpostorsHasLover {get;set;}
        public bool VampiresHasLover {get;set;}
        public bool GlitchHasLover {get;set;}
        public bool JuggernautHasLover {get;set;}
        public bool ArsonistHasLover {get;set;}
        public bool PlaguebearerHasLover {get;set;}
        public bool PestilenceHasLover {get;set;}
        public bool WerewolfHasLover {get;set;}

        public PlayerStatistics(ShipStatus __instance) {
            GetPlayerCounts();
        }

        private bool isLover(NetworkedPlayerInfo p) {
            bool isLover = false;
            foreach (var modifier in Modifier.GetModifiers(ModifierEnum.Lover))
            {
                var lover = (Lover)modifier;
                isLover = (lover.Player != null && lover.Player.PlayerId == p.PlayerId) || (lover.OtherLover.Player != null && lover.OtherLover.Player.PlayerId == p.PlayerId);
            }
            return isLover;
        }

        private void GetPlayerCounts() {
            int TeamImpostorsAlive = 0;
            int TeamLoversAlive = 0;
            int VampiresAlive = 0;
            int GlitchAlive = 0;
            int JuggernautAlive = 0;
            int ArsonistAlive = 0;
            int PlaguebearerAlive = 0;
            int PestilenceAlive = 0;
            int WerewolfAlive = 0;
            int TotalAlive = 0;
            bool TeamImpostorsHasLover = false;
            bool VampiresHasLover = false;
            bool GlitchHasLover = false;
            bool JuggernautHasLover = false;
            bool ArsonistHasLover = false;
            bool PlaguebearerHasLover = false;
            bool PestilenceHasLover = false;
            bool WerewolfHasLover = false;

            foreach (var playerInfo in GameData.Instance.AllPlayers.GetFastEnumerator()) {
                if (!playerInfo.Disconnected)
                {
                    if (!playerInfo.IsDead)
                    {
                        TotalAlive++;

                        bool lover = isLover(playerInfo);
                        if (lover) TeamLoversAlive++;

                        if (playerInfo.Role.IsImpostor) {
                            TeamImpostorsAlive++;
                            if (lover) TeamImpostorsHasLover = true;
                        }
                        foreach (var role in Role.GetRoles(RoleEnum.Vampire))
                        {
                            var Vampire = (Vampire)role;
                            if (Vampire.Player != null && Vampire.Player.PlayerId == playerInfo.PlayerId) {
                                VampiresAlive++;
                                if (lover) VampiresHasLover = true;
                            }
                        }
                        foreach (var role in Role.GetRoles(RoleEnum.Glitch))
                        {
                            var Glitch = (Glitch)role;
                            if (Glitch.Player != null && Glitch.Player.PlayerId == playerInfo.PlayerId) {
                                GlitchAlive++;
                                if (lover) GlitchHasLover = true;
                            }
                        }
                        foreach (var role in Role.GetRoles(RoleEnum.Juggernaut))
                        {
                            var Juggernaut = (Juggernaut)role;
                            if (Juggernaut.Player != null && Juggernaut.Player.PlayerId == playerInfo.PlayerId) {
                                JuggernautAlive++;
                                if (lover) JuggernautHasLover = true;
                            }
                        }
                        foreach (var role in Role.GetRoles(RoleEnum.Arsonist))
                        {
                            var Arsonist = (Arsonist)role;
                            if (Arsonist.Player != null && Arsonist.Player.PlayerId == playerInfo.PlayerId) {
                                ArsonistAlive++;
                                if (lover) ArsonistHasLover = true;
                            }
                        }
                        foreach (var role in Role.GetRoles(RoleEnum.Plaguebearer))
                        {
                            var Plaguebearer = (Plaguebearer)role;
                            if (Plaguebearer.Player != null && Plaguebearer.Player.PlayerId == playerInfo.PlayerId) {
                                PlaguebearerAlive++;
                                if (lover) PlaguebearerHasLover = true;
                            }
                        }
                        foreach (var role in Role.GetRoles(RoleEnum.Pestilence))
                        {
                            var Pestilence = (Pestilence)role;
                            if (Pestilence.Player != null && Pestilence.Player.PlayerId == playerInfo.PlayerId) {
                                PestilenceAlive++;
                                if (lover) PestilenceHasLover = true;
                            }
                        }
                        foreach (var role in Role.GetRoles(RoleEnum.Werewolf))
                        {
                            var Werewolf = (Werewolf)role;
                            if (Werewolf.Player != null && Werewolf.Player.PlayerId == playerInfo.PlayerId) {
                                WerewolfAlive++;
                                if (lover) WerewolfHasLover = true;
                            }
                        }
                    }
                }
            }


            this.TeamImpostorsAlive = TeamImpostorsAlive;
            this.TeamLoversAlive = TeamLoversAlive;
            this.VampiresAlive = VampiresAlive;
            this.GlitchAlive = GlitchAlive;
            this.JuggernautAlive = JuggernautAlive;
            this.ArsonistAlive = ArsonistAlive;
            this.PlaguebearerAlive = PlaguebearerAlive;
            this.PestilenceAlive = PestilenceAlive;
            this.WerewolfAlive = WerewolfAlive;
            this.TotalAlive = TotalAlive;
            this.TeamImpostorsHasLover = TeamImpostorsHasLover;
            this.VampiresHasLover = VampiresHasLover;
            this.GlitchHasLover = GlitchHasLover;
            this.JuggernautHasLover = JuggernautHasLover;
            this.ArsonistHasLover = ArsonistHasLover;
            this.PlaguebearerHasLover = PlaguebearerHasLover;
            this.PestilenceHasLover = PestilenceHasLover;
            this.WerewolfHasLover = WerewolfHasLover;
        }
    }
}