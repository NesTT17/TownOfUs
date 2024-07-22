using TownOfUs.Extensions;

namespace TownOfUs.Roles.Modifiers
{
    public class Flash : Modifier, IVisualAlteration
    {

        public Flash(PlayerControl player) : base(player)
        {
            Name = "Flash";
            TaskText = () => "Superspeed!";
            Color = Patches.Colors.Flash;
            ModifierType = ModifierEnum.Flash;
        }

        public bool TryGetModifiedAppearance(out VisualAppearance appearance)
        {
            appearance = Player.GetDefaultAppearance();
            if (CamouflageUnCamouflage.IsCamoed) {
                appearance.SpeedFactor = Player.GetDefaultAppearance().SpeedFactor;
            } else {
                appearance.SpeedFactor = CustomGameOptions.FlashSpeed;
            }
            return true;
        }
    }
}