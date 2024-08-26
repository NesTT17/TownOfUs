using System.Linq;
using HarmonyLib;
using UnityEngine;

namespace TownOfUs.Patches
{
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.OnDestroy))]
    class IntroCutsceneOnDestroyPatch
    {
        public static void Prefix(IntroCutscene __instance)
        {
            if (Utils.polusVent == null && GameOptionsManager.Instance.currentNormalGameOptions.MapId == 2) {
                var list = GameObject.FindObjectsOfType<Vent>().ToList();
                var adminVent = list.FirstOrDefault(x => x.gameObject.name == "AdminVent");
                var bathroomVent = list.FirstOrDefault(x => x.gameObject.name == "BathroomVent");
                Utils.polusVent = UnityEngine.Object.Instantiate<Vent>(adminVent);
                Utils.polusVent.gameObject.AddSubmergedComponent(SubmergedCompatibility.Classes.ElevatorMover);
                Utils.polusVent.transform.position = new Vector3(36.55068f, -21.5168f, -0.0215168f);
                Utils.polusVent.Left = adminVent;
                Utils.polusVent.Right = bathroomVent;
                Utils.polusVent.Center = null;
                Utils.polusVent.Id = ShipStatus.Instance.AllVents.Select(x => x.Id).Max() + 1; // Make sure we have a unique id
                var allVentsList = ShipStatus.Instance.AllVents.ToList();
                allVentsList.Add(Utils.polusVent);
                ShipStatus.Instance.AllVents = allVentsList.ToArray();
                Utils.polusVent.gameObject.SetActive(true);
                Utils.polusVent.name = "newVent_" + Utils.polusVent.Id;

                adminVent.Center = Utils.polusVent;
                bathroomVent.Center = Utils.polusVent;
            }
        }
    }
}