using TownOfUs.Extensions;
using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Giant : Modifier, IVisualAlteration
    {
        public Giant(PlayerControl player) : base(player)
        {
            var slowText = CustomGameOptions.GiantSlow != 1? " and slow!" : "!";
            Name = "Giant";
            TaskText = () => "You are ginormous" + slowText;
            Color = Patches.Colors.GlobalModifier;
            ModifierType = ModifierEnum.Giant;
        }

        public bool TryGetModifiedAppearance(out VisualAppearance appearance)
        {
            appearance = Player.GetDefaultAppearance();
            if (CamouflageUnCamouflage.IsCamoed) {
                appearance.SpeedFactor = Player.GetDefaultAppearance().SpeedFactor;
                appearance.SizeFactor = Player.GetDefaultAppearance().SizeFactor;
            } else {
                appearance.SpeedFactor = CustomGameOptions.GiantSlow;
                appearance.SizeFactor = new Vector3(1.0f, 1.0f, 1.0f);
            }
            return true;
        }
    }
}