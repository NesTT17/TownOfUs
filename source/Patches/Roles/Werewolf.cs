using System;
using System.Linq;
using UnityEngine;
using TownOfUs.Extensions;

namespace TownOfUs.Roles
{
    public class Werewolf : Role
    {
        private KillButton _rampageButton;
        public bool Enabled;
        public bool WerewolfWins;
        public PlayerControl ClosestPlayer;
        public DateTime LastRampaged;
        public DateTime LastKilled;
        public float TimeRemaining;


        public Werewolf(PlayerControl player) : base(player)
        {
            Name = "Werewolf";
            ImpostorText = () => "Rampage To Kill Everyone";
            TaskText = () => "Rampage to kill everyone\nFake Tasks:";
            Color = Patches.Colors.Werewolf;
            LastRampaged = DateTime.UtcNow;
            LastKilled = DateTime.UtcNow;
            RoleType = RoleEnum.Werewolf;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralKilling;
        }

        public KillButton RampageButton
        {
            get => _rampageButton;
            set
            {
                _rampageButton = value;
                ExtraButtons.Clear();
                ExtraButtons.Add(value);
            }
        }

        public void Wins()
        {
            WerewolfWins = true;
        }

        public bool Rampaged => TimeRemaining > 0f;

        public float RampageTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastRampaged;
            var num = CustomGameOptions.RampageCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }

        public void Rampage()
        {
            Enabled = true;
            TimeRemaining -= Time.deltaTime;
            if (Player.Data.IsDead)
            {
                TimeRemaining = 0f;
            }
        }

        public void Unrampage()
        {
            Enabled = false;
            LastRampaged = DateTime.UtcNow;
        }

        public float KillTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastKilled;
            var num = CustomGameOptions.RampageKillCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}
