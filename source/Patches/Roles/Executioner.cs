using Il2CppSystem.Collections.Generic;

namespace TownOfUs.Roles
{
    public class Executioner : Role
    {
        public PlayerControl target;
        public bool TargetVotedOut = false;

        public Executioner(PlayerControl player) : base(player)
        {
            Name = "Executioner";
            ImpostorText = () =>
                target == null ? "You don't have a target for some reason... weird..." : $"Vote {target.name} Out";
            TaskText = () =>
                target == null
                    ? "You don't have a target for some reason... weird..."
                    : $"Vote {target.name} out!\nFake Tasks:";
            Color = Patches.Colors.Executioner;
            RoleType = RoleEnum.Executioner;
            AddToRoleHistory(RoleType);
            Faction = Faction.NeutralEvil;
            Scale = 1.4f;
        }

        public void Wins()
        {
            if (Player.Data.IsDead || Player.Data.Disconnected) return;
            TargetVotedOut = true;
        }
    }
}