using TownOfUs.Extensions;

namespace TownOfUs.Roles
{
    public class Immortal : Role
    {

        public PlayerControl LastKiller;
        public bool KilledMe;
        public bool FullyDead;

        public Immortal(PlayerControl player) : base(player)
        {
            Name = "Immortal";
            ImpostorText = () => "Figure Out Who Killed You";
            TaskText = () => "You cannot die, but you cannot see";
            Color = Patches.Colors.Immortal;
            RoleType = RoleEnum.Immortal;
            AddToRoleHistory(RoleType);
            LastKiller = null;
            KilledMe = false;
            FullyDead = false;
        }

        internal override bool RoleCriteria()
        {
            return KilledMe || base.RoleCriteria();
        }
    }
}