namespace TownOfUs.Roles.Modifiers
{
    public class Bait : Modifier
    {
        public Bait(PlayerControl player) : base(player)
        {
            Name = "Bait";
            TaskText = () => "Killing you causes an instant self-report";
            Color = Patches.Colors.CrewModifier;
            ModifierType = ModifierEnum.Bait;
        }
    }
}