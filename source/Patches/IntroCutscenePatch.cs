using System;
using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using TownOfUs.Roles;
using TownOfUs.Roles.Modifiers;
using UnityEngine;

namespace TownOfUs.Patches
{
    [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.OnDestroy))]
    class IntroCutsceneOnDestroyPatch
    {
        public static PoolablePlayer playerPrefab;
        public static Vector3 bottomLeft;

        public static void Prefix(IntroCutscene __instance)
        {
            // Generate and initialize player icons
            if (PlayerControl.LocalPlayer != null && DestroyableSingleton<HudManager>.Instance != null)
            {
                float aspect = Camera.main.aspect;
                float safeOrthographicSize = CameraSafeArea.GetSafeOrthographicSize(Camera.main);
                float xpos = 1.75f - safeOrthographicSize * aspect * 1.70f;
                float ypos = 0.15f - safeOrthographicSize * 1.7f;
                bottomLeft = new Vector3(xpos / 2, ypos/2, -61f);

                foreach (PlayerControl p in PlayerControl.AllPlayerControls)
                {
                    NetworkedPlayerInfo data = p.Data;
                    PoolablePlayer player = UnityEngine.Object.Instantiate<PoolablePlayer>(__instance.PlayerPrefab, DestroyableSingleton<HudManager>.Instance.transform);
                    playerPrefab = __instance.PlayerPrefab;
                    p.SetPlayerMaterialColors(player.cosmetics.currentBodySprite.BodySprite);
                    player.SetSkin(data.DefaultOutfit.SkinId, data.DefaultOutfit.ColorId);
                    player.cosmetics.SetHat(data.DefaultOutfit.HatId, data.DefaultOutfit.ColorId);
                    player.cosmetics.nameText.text = data.PlayerName;
                    player.SetFlipX(true);
                    Utils.playerIcons[p.PlayerId] = player;
                    player.gameObject.SetActive(false);

                    player.transform.localPosition = bottomLeft;
                    player.transform.localScale = Vector3.one * 0.4f;
                    player.gameObject.SetActive(false);
                }
            }

            // Force Reload of SoundEffectHolder
            SoundEffectsManager.Load();

            // Place new vent
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

    [HarmonyPatch]
    class IntroPatch {
        public static void setupIntroTeamIcons(IntroCutscene __instance, ref  Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
            // Intro solo teams
            if (PlayerControl.LocalPlayer.Is(Faction.NeutralBenign) || PlayerControl.LocalPlayer.Is(Faction.NeutralEvil) || PlayerControl.LocalPlayer.Is(Faction.NeutralKilling)) {
                var soloTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
                soloTeam.Add(PlayerControl.LocalPlayer);
                yourTeam = soloTeam;
            }
        }

        public static void setupIntroTeam(IntroCutscene __instance, ref  Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
            Role role = Role.GetRole(PlayerControl.LocalPlayer);
            if (role == null) return;
            if (role.Faction == Faction.NeutralBenign || role.Faction == Faction.NeutralEvil || role.Faction == Faction.NeutralKilling) {
                var neutralColor = new Color32(76, 84, 78, 255);
                __instance.BackgroundBar.material.color = neutralColor;
                __instance.TeamTitle.text = "Neutral";
                __instance.TeamTitle.color = neutralColor;
            }
        }

        public static IEnumerator<WaitForSeconds> EndShowRole(IntroCutscene __instance) {
            yield return new WaitForSeconds(5f);
            __instance.YouAreText.gameObject.SetActive(false);
            __instance.RoleText.gameObject.SetActive(false);
            __instance.RoleBlurbText.gameObject.SetActive(false);
            __instance.ourCrewmate.gameObject.SetActive(false);  
        }

        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.CreatePlayer))]
        class CreatePlayerPatch {
            public static void Postfix(IntroCutscene __instance, bool impostorPositioning, ref PoolablePlayer __result) {
                if (impostorPositioning) __result.SetNameColor(Palette.ImpostorRed);
            }
        }

        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.ShowRole))]
        class SetUpRoleTextPatch {
            static int seed = 0;
            static public void SetRoleTexts(IntroCutscene __instance) {
                // Don't override the intro of the vanilla roles
                Role role = Role.GetRole(PlayerControl.LocalPlayer);
                Modifier modifier = Modifier.GetModifier(PlayerControl.LocalPlayer);

                __instance.RoleBlurbText.text = "";
                if (role != null) {
                    __instance.RoleText.text = role.Name;
                    __instance.RoleText.color = role.Color;
                    __instance.RoleBlurbText.text = role.ImpostorText();
                    __instance.RoleBlurbText.color = role.Color;
                }
                if (modifier != null) {
                    __instance.RoleBlurbText.text += Utils.ColorString(modifier.Color, $"\n{modifier.TaskText()}");
                }
            }
            public static bool Prefix(IntroCutscene __instance) {
                seed = Utils.rnd.Next(5000);
                DestroyableSingleton<HudManager>.Instance.StartCoroutine(Effects.Lerp(1f, new Action<float>((p) => {
                    SetRoleTexts(__instance);
                })));
                return true;
            }
        }

        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginCrewmate))]
        class BeginCrewmatePatch {
            public static void Prefix(IntroCutscene __instance, ref  Il2CppSystem.Collections.Generic.List<PlayerControl> teamToDisplay) {
                setupIntroTeamIcons(__instance, ref teamToDisplay);
            }

            public static void Postfix(IntroCutscene __instance, ref  Il2CppSystem.Collections.Generic.List<PlayerControl> teamToDisplay) {
                setupIntroTeam(__instance, ref teamToDisplay);
            }
        }

        [HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.BeginImpostor))]
        class BeginImpostorPatch {
            public static void Prefix(IntroCutscene __instance, ref  Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
                setupIntroTeamIcons(__instance, ref yourTeam);
            }

            public static void Postfix(IntroCutscene __instance, ref  Il2CppSystem.Collections.Generic.List<PlayerControl> yourTeam) {
                setupIntroTeam(__instance, ref yourTeam);
            }
        }
    }
}