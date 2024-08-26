namespace TownOfUs.Roles.Modifiers
{
    public class Multitasker : Modifier
    {
        public Multitasker(PlayerControl player) : base(player)
        {
            Name = "Multitasker";
            TaskText = () => "Your task windows are transparent";
            Color = Patches.Colors.CrewModifier;
            ModifierType = ModifierEnum.Multitasker;
        }
    }
}