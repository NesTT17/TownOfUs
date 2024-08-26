using UnityEngine;

namespace TownOfUs.Roles.Modifiers
{
    public class Drunk : Modifier
    {
        public Drunk(PlayerControl player) : base(player)
        {
            Name = "Drunk";
            TaskText = () => "Inverrrrrted contrrrrols";
            Color = Patches.Colors.GlobalModifier;
            ModifierType = ModifierEnum.Drunk;
        }
    }
}