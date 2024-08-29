using System;
using System.Linq;
using Il2CppSystem.Collections.Generic;
using TownOfUs.Extensions;

namespace TownOfUs.Roles
{
    public class Vampire : Role
    {
        public Vampire(PlayerControl player) : base(player)
        {
            Name = "Vampire";
            ImpostorText = () => "Convert Crewmates And Kill The Rest";
            TaskText = () => "Bite all other players\nFake Tasks:";
            Color = Patches.Colors.Vampire;
            LastBit = DateTime.UtcNow;
            RoleType = RoleEnum.Vampire;
            Faction = Faction.NeutralKilling;
            AddToRoleHistory(RoleType);
        }

        public PlayerControl ClosestPlayer;
        public DateTime LastBit { get; set; }

        public float BiteTimer()
        {
            var utcNow = DateTime.UtcNow;
            var timeSpan = utcNow - LastBit;
            var num = CustomGameOptions.BiteCd * 1000f;
            var flag2 = num - (float) timeSpan.TotalMilliseconds < 0f;
            if (flag2) return 0;
            return (num - (float) timeSpan.TotalMilliseconds) / 1000f;
        }
    }
}