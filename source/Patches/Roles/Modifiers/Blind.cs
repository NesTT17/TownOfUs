using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Blind : Modifier
    {
        public Blind(PlayerControl player) : base(player)
        {
            Name = "Blind";
            TaskText = () => "Your report button does not light up";
            Color = Patches.Colors.CrewModifier;
            ModifierType = ModifierEnum.Blind;
        }
    }
}