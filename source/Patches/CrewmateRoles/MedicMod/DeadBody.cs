using System;
using System.Collections.Generic;
using TownOfUs.Extensions;

namespace TownOfUs.CrewmateRoles.MedicMod
{
    public class DeadPlayer
    {
        public byte KillerId { get; set; }
        public byte PlayerId { get; set; }
        public DateTime KillTime { get; set; }
    }

    //body report class for when medic reports a body
    public class BodyReport
    {
        public PlayerControl Killer { get; set; }
        public PlayerControl Reporter { get; set; }
        public PlayerControl Body { get; set; }
        public float KillAge { get; set; }

        public static string ParseBodyReport(BodyReport br)
        {
            if (br.KillAge > CustomGameOptions.MedicReportColorDuration * 1000)
                return
                    $"Body Report: The corpse is too old to gain information from. (Killed {Math.Round(br.KillAge / 1000)}s ago)";

            if (br.Killer.PlayerId == br.Body.PlayerId)
                return
                    $"Body Report: The kill appears to have been a suicide! (Killed {Math.Round(br.KillAge / 1000)}s ago)";

            if (br.KillAge < CustomGameOptions.MedicReportNameDuration * 1000)
                return
                    $"Body Report: The killer appears to be {br.Killer.Data.PlayerName}! (Killed {Math.Round(br.KillAge / 1000)}s ago)";

            return
                $"Body Report: The killer appears to be a {(Utils.isLighterColor(br.Killer.Data.DefaultOutfit.ColorId) ? "lighter" : "darker")} color. (Killed {Math.Round(br.KillAge / 1000)}s ago)";
        }
    }
}
