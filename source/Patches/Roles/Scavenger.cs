using System.Collections.Generic;
using System.Linq;
using TMPro;
using TownOfUs.Patches;
using UnityEngine;
using System;

namespace TownOfUs.Roles
{
    public class Scavenger : Role
    {
        public int CorpsesEaten { get; set; } = 0;
        public DateTime LastDevoured { get; set; }
        public DeadBody CurrentTarget { get; set; }
        public bool WonByDevouring
        {
            get
            {
                return CorpsesEaten >= CustomGameOptions.ScavCorpsesToWin;
            }
        }

        public Scavenger(PlayerControl player) : base(player)
        {
            Name = "Scavenger";
            ImpostorText = () => "Eat Bodies To Win!";
            TaskText = () => "Win by consuming bodies\nFake Tasks:";
            Color = Colors.Scavenger;
            RoleType = RoleEnum.Scavenger;
            LastDevoured = DateTime.UtcNow;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralEvil;
        }

        public float DevourTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastDevoured;
            var num = CustomGameOptions.DevourCd * 1000f;
            var flag2 = num - (float)timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float)timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}