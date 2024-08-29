using System.Collections.Generic;
using TownOfUs.Patches;
using UnityEngine;
using Types = TownOfUs.CustomOption.CustomOption.CustomOptionType;

namespace TownOfUs.CustomOption {
    public class CustomOptionHolder {
        public static string[] presets = new string[]{"Preset 1", "Preset 2", "Preset 3", "Preset 4", "Preset 5"};
        public static string cs(Color c, string s)
        {
            return string.Format("<color=#{0:X2}{1:X2}{2:X2}{3:X2}>{4}</color>", ToByte(c.r), ToByte(c.g), ToByte(c.b), ToByte(c.a), s);
        }
        private static byte ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }

        public static CustomOption PresetSelection;

        public static CustomOption GameMode;

        public static CustomOption NonKillingNeutralRolesCountMin;
        public static CustomOption NonKillingNeutralRolesCountMax;
        public static CustomOption KillingNeutralRolesCountMin;
        public static CustomOption KillingNeutralRolesCountMax;

        public static CustomOption RandomNumberImps;

        public static CustomOption NeutralRoles;
        public static CustomOption VeteranCount;
        public static CustomOption VigilanteCount;
        public static CustomOption AddArsonist;
        public static CustomOption AddPlaguebearer;

        public static CustomOption PoliticianCultistOn;
        public static CustomOption SeerCultistOn;
        public static CustomOption SheriffCultistOn;
        public static CustomOption LawyerCultistOn;
        public static CustomOption SurvivorCultistOn;
        public static CustomOption NumberOfSpecialRoles;
        public static CustomOption MaxChameleons;
        public static CustomOption MaxEngineers;
        public static CustomOption MaxInvestigators;
        public static CustomOption MaxMystics;
        public static CustomOption MaxSnitches;
        public static CustomOption MaxSpies;
        public static CustomOption MaxTransporters;
        public static CustomOption MaxVigilantes;
        public static CustomOption WhisperCooldown;
        public static CustomOption IncreasedCooldownPerWhisper;
        public static CustomOption WhisperRadius;
        public static CustomOption ConversionPercentage;
        public static CustomOption DecreasedPercentagePerConversion;
        public static CustomOption ReviveCooldown;
        public static CustomOption IncreasedCooldownPerRevive;
        public static CustomOption MaxReveals;
        
        public static CustomOption ColourblindComms;
        public static CustomOption ImpostorSeeRoles;
        public static CustomOption DeadSeeRoles;
        public static CustomOption InitialCooldowns;
        public static CustomOption ParallelMedScans;
        public static CustomOption SkipButtonDisable;
        public static CustomOption HiddenRoles;
        public static CustomOption FirstDeathShield;
        public static CustomOption NeutralEvilWinEndsGame;
        public static CustomOption GhostsDoTasks;
        public static CustomOption WeightedRoleSelection;

        public static CustomOption SeeTasksDuringRound;
        public static CustomOption SeeTasksDuringMeeting;
        public static CustomOption SeeTasksWhenDead;

        public static CustomOption VentImprovements;
        public static CustomOption VitalsLab;
        public static CustomOption ColdTempDeathValley;
        public static CustomOption WifiChartCourseSwap;
        
        public static CustomOption RandomMapEnabled;
        public static CustomOption RandomMapSkeld;
        public static CustomOption RandomMapMira;
        public static CustomOption RandomMapPolus;
        public static CustomOption RandomMapAirship;
        public static CustomOption RandomMapFungle;
        public static CustomOption RandomMapSubmerged;

        public static CustomOption AurialOn;
        public static CustomOption DetectiveOn;
        public static CustomOption HaunterOn;
        public static CustomOption InvestigatorOn;
        public static CustomOption ImmortalOn;
        public static CustomOption MysticOn;
        public static CustomOption OracleOn;
        public static CustomOption SeerOn;
        public static CustomOption SnitchOn;
        public static CustomOption SpyOn;
        public static CustomOption TrackerOn;
        public static CustomOption TrapperOn;

        public static CustomOption AltruistOn;
        public static CustomOption MedicOn;

        public static CustomOption HunterOn;
        public static CustomOption SheriffOn;
        public static CustomOption PoliticianOn;
        public static CustomOption VampireHunterOn;
        public static CustomOption VeteranOn;
        public static CustomOption VigilanteOn;
        
        public static CustomOption EngineerOn;
        public static CustomOption ImitatorOn;
        public static CustomOption MediumOn;
        public static CustomOption ProsecutorOn;
        public static CustomOption SwapperOn;
        public static CustomOption TransporterOn;

        public static CustomOption AmnesiacOn;
        public static CustomOption GuardianAngelOn;
        public static CustomOption LawyerOn;
        public static CustomOption MercenaryOn;
        public static CustomOption SurvivorOn;

        public static CustomOption DoomsayerOn;
        public static CustomOption ExecutionerOn;
        public static CustomOption JesterOn;
        public static CustomOption ScavengerOn;
        public static CustomOption PhantomOn;

        public static CustomOption ArsonistOn;
        public static CustomOption JuggernautOn;
        public static CustomOption PlaguebearerOn;
        public static CustomOption GlitchOn;
        public static CustomOption VampireOn;
        public static CustomOption WerewolfOn;

        public static CustomOption EscapistOn;
        public static CustomOption MorphlingOn;
        public static CustomOption SwooperOn;
        public static CustomOption GrenadierOn;
        public static CustomOption VenererOn;

        public static CustomOption BomberOn;
        public static CustomOption PoisonerOn;
        public static CustomOption TraitorOn;
        public static CustomOption WarlockOn;

        public static CustomOption BlackmailerOn;
        public static CustomOption JanitorOn;
        public static CustomOption MinerOn;
        public static CustomOption UndertakerOn;

        public static CustomOption AftermathOn;
        public static CustomOption BaitOn;
        public static CustomOption BlindOn;
        public static CustomOption DiseasedOn;
        public static CustomOption FrostyOn;
        public static CustomOption MultitaskerOn;
        public static CustomOption TorchOn;

        public static CustomOption ButtonBarryOn;
        public static CustomOption DrunkOn;
        public static CustomOption FlashOn;
        public static CustomOption GiantOn;
        public static CustomOption LoversOn;
        public static CustomOption RadarOn;
        public static CustomOption SleuthOn;
        public static CustomOption TiebreakerOn;

        public static CustomOption DisperserOn;
        public static CustomOption DoubleShotOn;
        public static CustomOption UnderdogOn;

        public static CustomOption NumberOfImpostorAssassins;
        public static CustomOption NumberOfNeutralAssassins;
        public static CustomOption AmneTurnImpAssassin;
        public static CustomOption AmneTurnNeutAssassin;
        public static CustomOption TraitorCanAssassin;
        public static CustomOption AssassinKills;
        public static CustomOption AssassinMultiKill;
        public static CustomOption AssassinCrewmateGuess;
        public static CustomOption AssassinGuessNeutralBenign;
        public static CustomOption AssassinGuessNeutralEvil;
        public static CustomOption AssassinGuessNeutralKilling;
        public static CustomOption AssassinGuessImpostors;
        public static CustomOption AssassinGuessModifiers;
        public static CustomOption AssassinGuessLovers;
        public static CustomOption AssassinateAfterVoting;

        public static CustomOption SheriffKillOther;
        public static CustomOption SheriffKillsDoomsayer;
        public static CustomOption SheriffKillsExecutioner;
        public static CustomOption SheriffKillsJester;
        public static CustomOption SheriffKillsScavenger;
        public static CustomOption SheriffKillsArsonist;
        public static CustomOption SheriffKillsJuggernaut;
        public static CustomOption SheriffKillsPlaguebearer;
        public static CustomOption SheriffKillsGlitch;
        public static CustomOption SheriffKillsVampire;
        public static CustomOption SheriffKillsWerewolf;
        public static CustomOption SheriffKillCd;
        public static CustomOption SheriffBodyReport;

        public static CustomOption HunterKillCd;
        public static CustomOption HunterStalkCd;
        public static CustomOption HunterStalkDuration;
        public static CustomOption HunterStalkUses;
        public static CustomOption HunterBodyReport;

        public static CustomOption MaxFixes;

        public static CustomOption FootprintSize;
        public static CustomOption FootprintInterval;
        public static CustomOption FootprintDuration;
        public static CustomOption AnonymousFootPrint;
        public static CustomOption VentFootprintVisible;

        public static CustomOption ShowShielded;
        public static CustomOption WhoGetsNotification;
        public static CustomOption ShieldBreaks;
        public static CustomOption MedicReportSwitch;
        public static CustomOption MedicReportNameDuration;
        public static CustomOption MedicReportColorDuration;

        public static CustomOption SeerCooldown;
        public static CustomOption CrewKillingRed;
        public static CustomOption NeutBenignRed;
        public static CustomOption NeutEvilRed;
        public static CustomOption NeutKillingRed;
        public static CustomOption TraitorColourSwap;

        public static CustomOption WhoSeesDead;

        public static CustomOption SwapperButton;

        public static CustomOption TransportCooldown;
        public static CustomOption TransportMaxUses;
        public static CustomOption TransporterVitals;

        public static CustomOption JesterButton;
        public static CustomOption JesterVent;
        public static CustomOption JesterImpVision;
        public static CustomOption JesterHaunt;

        public static CustomOption MimicCooldownOption;
        public static CustomOption MimicDurationOption;
        public static CustomOption HackCooldownOption;
        public static CustomOption HackDurationOption;
        public static CustomOption GlitchKillCooldownOption;
        public static CustomOption GlitchHackDistanceOption;
        public static CustomOption GlitchVent;

        public static CustomOption JuggKillCooldown;
        public static CustomOption ReducedKCdPerKill;
        public static CustomOption JuggVent;

        public static CustomOption MorphlingCooldown;
        public static CustomOption MorphlingDuration;
        public static CustomOption MorphlingVent;

        public static CustomOption OnTargetDead;
        public static CustomOption ExecutionerButton;
        public static CustomOption ExecutionerTorment;

        public static CustomOption PhantomTasksRemaining;
        public static CustomOption PhantomSpook;

        public static CustomOption SnitchSeesNeutrals;
        public static CustomOption SnitchTasksRemaining;
        public static CustomOption SnitchSeesImpInMeeting;
        public static CustomOption SnitchSeesTraitor;

        public static CustomOption ReviveDuration;
        public static CustomOption AltruistTargetBody;

        public static CustomOption MineCooldown;

        public static CustomOption SwoopCooldown;
        public static CustomOption SwoopDuration;
        public static CustomOption SwooperVent;

        public static CustomOption DouseCooldown;
        public static CustomOption MaxDoused;
        public static CustomOption ArsoImpVision;
        public static CustomOption IgniteCdRemoved;

        public static CustomOption DragCooldown;
        public static CustomOption UndertakerDragSpeed;
        public static CustomOption UndertakerVent;
        public static CustomOption UndertakerVentWithBody;

        public static CustomOption UnderdogKillBonus;
        public static CustomOption UnderdogIncreasedKC;

        public static CustomOption VigilanteKills;
        public static CustomOption VigilanteMultiKill;
        public static CustomOption VigilanteGuessNeutralBenign;
        public static CustomOption VigilanteGuessNeutralEvil;
        public static CustomOption VigilanteGuessNeutralKilling;
        public static CustomOption VigilanteGuessLovers;
        public static CustomOption VigilanteAfterVoting;

        public static CustomOption HaunterTasksRemainingClicked;
        public static CustomOption HaunterTasksRemainingAlert;
        public static CustomOption HaunterRevealsNeutrals;
        public static CustomOption HaunterCanBeClickedBy;

        public static CustomOption GrenadeCooldown;
        public static CustomOption GrenadeDuration;
        public static CustomOption GrenadierIndicators;
        public static CustomOption GrenadierVent;
        public static CustomOption FlashRadius;

        public static CustomOption KilledOnAlert;
        public static CustomOption AlertCooldown;
        public static CustomOption AlertDuration;
        public static CustomOption MaxAlerts;

        public static CustomOption UpdateInterval;
        public static CustomOption TrackCooldown;
        public static CustomOption ResetOnNewRound;
        public static CustomOption MaxTracks;

        public static CustomOption TrapCooldown;
        public static CustomOption TrapsRemoveOnNewRound;
        public static CustomOption MaxTraps;
        public static CustomOption MinAmountOfTimeInTrap;
        public static CustomOption TrapSize;
        public static CustomOption MinAmountOfPlayersInTrap;

        public static CustomOption LatestSpawn;
        public static CustomOption NeutralKillingStopsTraitor;

        public static CustomOption RememberArrows;
        public static CustomOption RememberArrowDelay;

        public static CustomOption MediateCooldown;
        public static CustomOption ShowMediatePlayer;
        public static CustomOption ShowMediumToDead;
        public static CustomOption DeadRevealed;

        public static CustomOption VestCd;
        public static CustomOption VestDuration;
        public static CustomOption VestKCReset;
        public static CustomOption MaxVests;

        public static CustomOption ProtectCd;
        public static CustomOption ProtectDuration;
        public static CustomOption ProtectKCReset;
        public static CustomOption MaxProtects;
        public static CustomOption ShowProtect;
        public static CustomOption GaOnTargetDeath;
        public static CustomOption GATargetKnows;
        public static CustomOption GAKnowsTargetRole;
        public static CustomOption EvilTargetPercent;

        public static CustomOption MysticArrowDuration;

        public static CustomOption BlackmailCooldown;
        public static CustomOption BlackmailInvisible;

        public static CustomOption InfectCooldown;
        public static CustomOption PestKillCooldown;
        public static CustomOption PestVent;

        public static CustomOption RampageCooldown;
        public static CustomOption RampageDuration;
        public static CustomOption RampageKillCooldown;
        public static CustomOption WerewolfVent;

        public static CustomOption InitialExamineCooldown;
        public static CustomOption ExamineCooldown;
        public static CustomOption RecentKill;
        public static CustomOption DetectiveReportOn;
        public static CustomOption DetectiveRoleDuration;
        public static CustomOption DetectiveFactionDuration;
        public static CustomOption ExamineReportOn;

        public static CustomOption EscapeCooldown;
        public static CustomOption EscapistVent;

        public static CustomOption MaxKillsInDetonation;
        public static CustomOption DetonateDelay;
        public static CustomOption DetonateRadius;
        public static CustomOption BomberVent;

        public static CustomOption ObserveCooldown;
        public static CustomOption DoomsayerGuessNeutralBenign;
        public static CustomOption DoomsayerGuessNeutralEvil;
        public static CustomOption DoomsayerGuessNeutralKilling;
        public static CustomOption DoomsayerGuessImpostors;
        public static CustomOption DoomsayerAfterVoting;
        public static CustomOption DoomsayerGuessesToWin;
        public static CustomOption DoomsayerCantObserve;

        public static CustomOption BiteCooldown;
        public static CustomOption VampImpVision;
        public static CustomOption VampVent;
        public static CustomOption NewVampCanAssassin;
        public static CustomOption MaxVampiresPerGame;
        public static CustomOption CanBiteNeutralBenign;
        public static CustomOption CanBiteNeutralEvil;

        public static CustomOption StakeCooldown;
        public static CustomOption MaxFailedStakesPerGame;
        public static CustomOption CanStakeRoundOne;
        public static CustomOption SelfKillAfterFinalStake;
        public static CustomOption BecomeOnVampDeaths;

        public static CustomOption ProsDiesOnIncorrectPros;

        public static CustomOption ChargeUpDuration;
        public static CustomOption ChargeUseDuration;

        public static CustomOption ConfessCooldown;
        public static CustomOption RevealAccuracy;
        public static CustomOption NeutralBenignShowsEvil;
        public static CustomOption NeutralEvilShowsEvil;
        public static CustomOption NeutralKillingShowsEvil;

        public static CustomOption AbilityCooldown;
        public static CustomOption AbilityDuration;
        public static CustomOption SprintSpeed;
        public static CustomOption FreezeSpeed;

        public static CustomOption RadiateRange;
        public static CustomOption RadiateCooldown;
        public static CustomOption RadiateSucceedChance;
        public static CustomOption RadiateCount;
        public static CustomOption RadiateInvis;
        
        public static CustomOption PoisonCooldown;
        public static CustomOption PoisonDuration;
        public static CustomOption PoisonerVent;

        public static CustomOption LawyerDies;
        public static CustomOption DefendantImpPercent;
        public static CustomOption NeutralDefendant;
        public static CustomOption LawyerCanTalkDefendant;
        public static CustomOption OnDefendantDead;

        public static CustomOption MercenaryBrildersRequired;
        public static CustomOption ShowMercShielded;
        public static CustomOption WhoGetsMercNotification;
        public static CustomOption MercAbsorbCd;
        public static CustomOption MercArmorCd;
        public static CustomOption MercArmorDuration;
        
        public static CustomOption CampaignCooldown;
        public static CustomOption PoliticianCanSeeCampaigned;
        public static CustomOption KilledOnBodyguard;
        public static CustomOption BodyguardCooldown;
        public static CustomOption BodyguardDuration;
        
        public static CustomOption ImmortalReviveDuration;
        
        public static CustomOption DevourCooldown;
        public static CustomOption ScavCorpsesToWin;
        public static CustomOption ScavengerImpVision;
        public static CustomOption ScavengerVent;

        public static CustomOption GiantSlow;

        public static CustomOption FlashSpeed;

        public static CustomOption DiseasedKillMultiplier;

        public static CustomOption BaitMinDelay;
        public static CustomOption BaitMaxDelay;

        public static CustomOption BothLoversDie;
        public static CustomOption LovingImpPercent;
        public static CustomOption NeutralLovers;

        public static CustomOption ChillDuration;
        public static CustomOption ChillStartSpeed;

        public static void Load() {
            CustomOption.vanillaSettings = TownOfUs.Instance.Config.Bind("Preset0", "VanillaOptions", "");
#region Custom Options
            PresetSelection = CustomOption.Create(0, Types.General, "Preset", presets, null, true);
            WeightedRoleSelection = CustomOption.Create(654, Types.General, "Enable Weighted Role Selection", false);

            GameMode = CustomOption.Create(15, Types.General, "Game Mode", new string[] { "Classic", "All Any", "Killing Only", "Cultist" }, null, true, heading: "Game Mode Settings");

            RandomNumberImps = CustomOption.Create(5, Types.General, "Random Number Of Impostors", true, null, true, heading: "All Any Settings");

            NonKillingNeutralRolesCountMin = CustomOption.Create(10, Types.General, "Min Non-Killing Neutral Roles", 0f, 0f, 15f, 1f, null, true, heading: "Classic Game Mode Settings");
            NonKillingNeutralRolesCountMax = CustomOption.Create(11, Types.General, "Max Non-Killing Neutral Roles", 0f, 0f, 15f, 1f);
            KillingNeutralRolesCountMin = CustomOption.Create(12, Types.General, "Min Killing Neutral Roles", 0f, 0f, 15f, 1f);
            KillingNeutralRolesCountMax = CustomOption.Create(13, Types.General, "Max Killing Neutral Roles", 0f, 0f, 15f, 1f);

            NeutralRoles = CustomOption.Create(20, Types.General, "Neutral Roles", 1f, 0f, 5f, 1f, null, true, heading: "Killing Only Settings");
            VeteranCount = CustomOption.Create(21, Types.General, "Veteran Count", 1f, 0f, 5f, 1f);
            VigilanteCount = CustomOption.Create(22, Types.General, "Vigilante Count", 1f, 0f, 5f, 1f);
            AddArsonist = CustomOption.Create(23, Types.General, "Add Arsonist", false);
            AddPlaguebearer = CustomOption.Create(24, Types.General, "Add Plaguebearer", false);

            PoliticianCultistOn = CustomOption.Create(30, Types.General, cs(Colors.Politician, "Politician"), 0f, 0f, 100f, 10f, null, true, heading: "Cultist Settings", format: "%");
            SeerCultistOn = CustomOption.Create(31, Types.General, cs(Colors.Seer, "Seer"), 0f, 0f, 100f, 10f, format: "%");
            SheriffCultistOn = CustomOption.Create(32, Types.General, cs(Colors.Sheriff, "Sheriff"), 0f, 0f, 100f, 10f, format: "%");
            LawyerCultistOn = CustomOption.Create(33, Types.General, cs(Colors.Lawyer, "Lawyer"), 0f, 0f, 100f, 10f, format: "%");
            SurvivorCultistOn = CustomOption.Create(34, Types.General, cs(Colors.Survivor, "Survivor"), 0f, 0f, 100f, 10f, format: "%");
            NumberOfSpecialRoles = CustomOption.Create(35, Types.General, "Number Of Special Roles", 4f, 0f, 4f, 1f);
            MaxChameleons = CustomOption.Create(36, Types.General, "Max Chameleons", 3f, 0f, 5f, 1f);
            MaxEngineers = CustomOption.Create(37, Types.General, "Max Engineers", 3f, 0f, 5f, 1f);
            MaxInvestigators = CustomOption.Create(38, Types.General, "Max Investigators", 3f, 0f, 5f, 1f);
            MaxMystics = CustomOption.Create(39, Types.General, "Max Mystics", 3f, 0f, 5f, 1f);
            MaxSnitches = CustomOption.Create(40, Types.General, "Max Snitches", 3f, 0f, 5f, 1f);
            MaxSpies = CustomOption.Create(41, Types.General, "Max Spies", 3f, 0f, 5f, 1f);
            MaxTransporters = CustomOption.Create(42, Types.General, "Max Transporters", 3f, 0f, 5f, 1f);
            MaxVigilantes = CustomOption.Create(43, Types.General, "Max Vigilantes", 3f, 0f, 5f, 1f);
            WhisperCooldown = CustomOption.Create(44, Types.General, "Initial Whisper Cooldown", 30f, 10f, 60f, 2.5f, format: "s");
            IncreasedCooldownPerWhisper = CustomOption.Create(45, Types.General, "Increased Cooldown Per Whisper", 5f, 0f, 15f, 0.5f, format: "s");
            WhisperRadius = CustomOption.Create(46, Types.General, "Whisper Radius", 1f, 0.25f, 5f, 0.25f, format: "x");
            ConversionPercentage = CustomOption.Create(47, Types.General, "Conversion Percentage", 50f, 0f, 100f, 5f, format: "%");
            DecreasedPercentagePerConversion = CustomOption.Create(48, Types.General, "Decreased Conversion Percentage Per Conversion", 5f, 0f, 25f, 1f, format: "%");
            ReviveCooldown = CustomOption.Create(49, Types.General, "Initial Revive Cooldown", 30f, 10f, 60f, 2.5f, format: "s");
            IncreasedCooldownPerRevive = CustomOption.Create(50, Types.General, "Increased Cooldown Per Revive", 30f, 10f, 60f, 2.5f, format: "s");
            MaxReveals = CustomOption.Create(51, Types.General, "Maximum Number Of Reveals", 5f, 1f, 15f, 1f);

            ColourblindComms = CustomOption.Create(60, Types.General, "Camouflaged Comms", false, null, true, heading: "Custom Game Options");
            ImpostorSeeRoles = CustomOption.Create(61, Types.General, "Impostors Can See The Roles Of Their Team", false);
            DeadSeeRoles = CustomOption.Create(62, Types.General, "Dead Can See Everyone's Roles/Votes", false);
            InitialCooldowns = CustomOption.Create(463, Types.General, "Initial Cooldowns", 30f, 10f, 60f, 2.5f, format: "s");
            ParallelMedScans = CustomOption.Create(64, Types.General, "Parallel Medbay Scans", false);
            SkipButtonDisable = CustomOption.Create(65, Types.General, "Disable Meeting Skip Button", new string[] { "No", "Emergency", "Always" });
            HiddenRoles = CustomOption.Create(66, Types.General, "Enable Hidden Roles", true);
            FirstDeathShield = CustomOption.Create(67, Types.General, "First Death Shield Next Game", false);
            NeutralEvilWinEndsGame = CustomOption.Create(68, Types.General, "Neutral Evil Win Ends Game", true);
            GhostsDoTasks = CustomOption.Create(69, Types.General, "Ghosts Do Tasks", false);
            
            SeeTasksDuringRound = CustomOption.Create(70, Types.General, "See Tasks During Round", false, null, true, heading: "Task Tracking Settings");
            SeeTasksDuringMeeting = CustomOption.Create(71, Types.General, "See Tasks During Meetings", false);
            SeeTasksWhenDead = CustomOption.Create(72, Types.General, "See Tasks When Dead", true);

            VentImprovements = CustomOption.Create(55, Types.General, "Better Polus Vent Layout", false, heading: "Better Polus Settings");
            VitalsLab = CustomOption.Create(56, Types.General, "Vitals Moved To Lab", false);
            ColdTempDeathValley = CustomOption.Create(57, Types.General, "Cold Temp Moved To Death Valley", false);
            WifiChartCourseSwap = CustomOption.Create(58, Types.General, "Reboot Wifi And Chart Course Swapped", false);

            RandomMapEnabled = CustomOption.Create(75, Types.General, "Enable Random Map", false, null, true, heading: "Map Settings");
            RandomMapSkeld = CustomOption.Create(76, Types.General, "Skeld Chance", 0f, 0f, 100f, 10f, format: "%");
            RandomMapMira = CustomOption.Create(77, Types.General, "Mira Chance", 0f, 0f, 100f, 10f, format: "%");
            RandomMapPolus = CustomOption.Create(78, Types.General, "Polus Chance", 0f, 0f, 100f, 10f, format: "%");
            RandomMapAirship = CustomOption.Create(79, Types.General, "Airship Chance", 0f, 0f, 100f, 10f, format: "%");
            RandomMapFungle = CustomOption.Create(80, Types.General, "Fungle Chance", 0f, 0f, 100f, 10f, format: "%");
            RandomMapSubmerged = CustomOption.Create(81, Types.General, "Submerged Chance", 0f, 0f, 100f, 10f, format: "%");
#endregion

#region Spawn Rates
    #region Crewmate Roles
            AurialOn = CustomOption.Create(100, Types.Crewmate, cs(Colors.Aurial, "Aurial"), 0f, 0f, 100f, 10f, null, true, format: "%");
            RadiateRange = CustomOption.Create(220, Types.Crewmate, "Radiate Range", 1f, 0.25f, 5f, 0.25f, AurialOn, format: "x");
            RadiateCooldown = CustomOption.Create(221, Types.Crewmate, "Radiate Cooldown", 30f, 10f, 60f, 2.5f, AurialOn, format: "s");
            RadiateInvis = CustomOption.Create(222, Types.Crewmate, "Radiate See Delay", 10f, 0f, 20f, 0.5f, AurialOn, format: "s");
            RadiateCount = CustomOption.Create(223, Types.Crewmate, "Radiate Uses To See", 3f, 1f, 5f, 1f, AurialOn);
            RadiateSucceedChance = CustomOption.Create(224, Types.Crewmate, "Radiate Succeed Chance", 100f, 0f, 100f, 10f, AurialOn, format: "%");

            DetectiveOn = CustomOption.Create(101, Types.Crewmate, cs(Colors.Detective, "Detective"), 0f, 0f, 100f, 10f, null, true, format: "%");
            InitialExamineCooldown = CustomOption.Create(225, Types.Crewmate, "Initial Examine Cooldown", 30f, 10f, 60f, 2.5f, DetectiveOn, format: "s");
            ExamineCooldown = CustomOption.Create(226, Types.Crewmate, "Examine Cooldown", 10f, 3f, 20f, 0.5f, DetectiveOn, format: "s");
            RecentKill = CustomOption.Create(227, Types.Crewmate, "How Long Players Stay Bloody For", 30f, 10f, 60f, 2.5f, DetectiveOn, format: "s");
            DetectiveReportOn = CustomOption.Create(228, Types.Crewmate, "Show Detective Reports", true, DetectiveOn);
            DetectiveRoleDuration = CustomOption.Create(229, Types.Crewmate, "Time Where Detective Will Have Role", 15f, 0f, 60f, 0.5f, DetectiveOn, format: "s");
            DetectiveFactionDuration = CustomOption.Create(230, Types.Crewmate, "Time Where Detective Will Have Faction", 30f, 0f, 60f, 0.5f, DetectiveOn, format: "s");
            ExamineReportOn = CustomOption.Create(231, Types.Crewmate, "Show Examine Reports", true, DetectiveOn);

            HaunterOn = CustomOption.Create(102, Types.Crewmate, cs(Colors.Haunter, "Haunter"), 0f, 0f, 100f, 10f, null, true, format: "%");
            HaunterTasksRemainingClicked =  CustomOption.Create(232, Types.Crewmate, "Tasks Remaining When Haunter Can Be Clicked", 5f, 1f, 15f, 1f, HaunterOn);
            HaunterTasksRemainingAlert =  CustomOption.Create(233, Types.Crewmate, "Tasks Remaining When Alert Is Sent", 1f, 1f, 5f, 1f, HaunterOn);
            HaunterRevealsNeutrals = CustomOption.Create(234, Types.Crewmate, "Haunter Reveals Neutral Roles", false, HaunterOn);
            HaunterCanBeClickedBy = CustomOption.Create(235, Types.Crewmate, "Who Can Click Haunter", new string[] { "All", "Non-Crew", "Imps Only" }, HaunterOn);

            InvestigatorOn = CustomOption.Create(103, Types.Crewmate, cs(Colors.Investigator, "Investigator"), 0f, 0f, 100f, 10f, null, true, format: "%");
            FootprintSize = CustomOption.Create(236, Types.Crewmate, "Footprint Size", 4f, 1f, 10f, 1f, InvestigatorOn, format: "x");
            FootprintInterval = CustomOption.Create(237, Types.Crewmate, "Footprint Interval", 0.1f, 0.05f, 1f, 0.05f, InvestigatorOn, format: "s");
            FootprintDuration = CustomOption.Create(238, Types.Crewmate, "Footprint Duration", 10f, 1f, 15f, 0.5f, InvestigatorOn, format: "s");
            AnonymousFootPrint = CustomOption.Create(239, Types.Crewmate, "Anonymous Footprint", false, InvestigatorOn);
            VentFootprintVisible = CustomOption.Create(240, Types.Crewmate, "Footprint Vent Visible", false, InvestigatorOn);

            ImmortalOn = CustomOption.Create(104, Types.Crewmate, cs(Colors.Immortal, "Immortal"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ImmortalReviveDuration = CustomOption.Create(241, Types.Crewmate, "Immortal Revive Duration", 10f, 1f, 25f, 1f, ImmortalOn, format: "s");

            MysticOn = CustomOption.Create(105, Types.Crewmate, cs(Colors.Mystic, "Mystic"), 0f, 0f, 100f, 10f, null, true, format: "%");
            MysticArrowDuration = CustomOption.Create(242, Types.Crewmate, "Dead Body Arrow Duration", 0.1f, 0f, 1f, 0.05f, MysticOn, format: "s");

            OracleOn = CustomOption.Create(106, Types.Crewmate, cs(Colors.Oracle, "Oracle"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ConfessCooldown = CustomOption.Create(243, Types.Crewmate, "Confess Cooldown", 30f, 10f, 60f, 2.5f, OracleOn, format: "s");
            RevealAccuracy = CustomOption.Create(244, Types.Crewmate, "Reveal Accuracy", 80f, 0f, 100f, 10f, OracleOn, format: "%");
            NeutralBenignShowsEvil = CustomOption.Create(245, Types.Crewmate, "Neutral Benign Roles Show Evil", false, OracleOn);
            NeutralEvilShowsEvil = CustomOption.Create(246, Types.Crewmate, "Neutral Evil Roles Show Evil", false, OracleOn);
            NeutralKillingShowsEvil = CustomOption.Create(247, Types.Crewmate, "Neutral Killing Roles Show Evil", true, OracleOn);

            SeerOn = CustomOption.Create(107, Types.Crewmate, cs(Colors.Seer, "Seer"), 0f, 0f, 100f, 10f, null, true, format: "%");
            SeerCooldown = CustomOption.Create(248, Types.Crewmate, "Seer Cooldown", 30f, 10f, 60f, 2.5f, SeerOn, format: "s");
            CrewKillingRed = CustomOption.Create(249, Types.Crewmate, "Crewmate Killing Roles Are Red", false, SeerOn);
            NeutBenignRed = CustomOption.Create(250, Types.Crewmate, "Neutral Benign Roles Are Red", false, SeerOn);
            NeutEvilRed = CustomOption.Create(251, Types.Crewmate, "Neutral Evil Roles Are Red", false, SeerOn);
            NeutKillingRed = CustomOption.Create(252, Types.Crewmate, "Neutral Killing Roles Are Red", true, SeerOn);
            TraitorColourSwap = CustomOption.Create(253, Types.Crewmate, "Traitor Does Not Swap Colours", false, SeerOn);

            SnitchOn = CustomOption.Create(108, Types.Crewmate, cs(Colors.Snitch, "Snitch"), 0f, 0f, 100f, 10f, null, true, format: "%");
            SnitchSeesNeutrals = CustomOption.Create(254, Types.Crewmate, "Snitch Sees Neutral Roles", false, SnitchOn);
            SnitchTasksRemaining =  CustomOption.Create(255, Types.Crewmate, "Tasks Remaining When Revealed", 1f, 1f, 5f, 1f, SnitchOn);
            SnitchSeesImpInMeeting = CustomOption.Create(256, Types.Crewmate, "Snitch Sees Impostors In Meetings", true, SnitchOn);
            SnitchSeesTraitor = CustomOption.Create(257, Types.Crewmate, "Snitch Sees Traitor", true, SnitchOn);

            SpyOn = CustomOption.Create(109, Types.Crewmate, cs(Colors.Spy, "Spy"), 0f, 0f, 100f, 10f, null, true, format: "%");
            WhoSeesDead = CustomOption.Create(258, Types.Crewmate, "Who Sees Dead Bodies On Admin", new string[] { "Nobody", "Spy", "Everyone But Spy", "Everyone" }, SpyOn);

            TrackerOn = CustomOption.Create(110, Types.Crewmate, cs(Colors.Tracker, "Tracker"), 0f, 0f, 100f, 10f, null, true, format: "%");
            UpdateInterval = CustomOption.Create(259, Types.Crewmate, "Arrow Update Interval", 3f, 0f, 5f, 0.25f, TrackerOn, format: "s");
            TrackCooldown = CustomOption.Create(260, Types.Crewmate, "Track Cooldown", 30f, 10f, 60f, 2.5f, TrackerOn, format: "s");
            ResetOnNewRound = CustomOption.Create(261, Types.Crewmate, "Tracker Arrows Reset After Each Round", false, TrackerOn);
            MaxTracks = CustomOption.Create(262, Types.Crewmate, "Maximum Number Of Tracks Per Round", 5f, 1f, 15f, 1f, TrackerOn);

            TrapperOn = CustomOption.Create(111, Types.Crewmate, cs(Colors.Trapper, "Trapper"), 0f, 0f, 100f, 10f, null, true, format: "%");
            MinAmountOfTimeInTrap = CustomOption.Create(263, Types.Crewmate, "Min Amount Of Time In Trap To Register", 1f, 0f, 15f, 0.5f, TrapperOn, format: "s");
            TrapCooldown = CustomOption.Create(264, Types.Crewmate, "Trap Cooldown", 30f, 10f, 60f, 2.5f, TrapperOn, format: "s");
            TrapsRemoveOnNewRound = CustomOption.Create(265, Types.Crewmate, "Traps Removed After Each Round", true, TrapperOn);
            MaxTraps = CustomOption.Create(266, Types.Crewmate, "Maximum Number Of Traps Per Game", 5f, 1f, 15f, 1f, TrapperOn);
            TrapSize = CustomOption.Create(267, Types.Crewmate, "Trap Size", 0.25f, 0.05f, 1f, 0.05f, TrapperOn, format: "x");
            MinAmountOfPlayersInTrap = CustomOption.Create(268, Types.Crewmate, "Minimum Number Of Roles Required To Trigger Trap", 3f, 1f, 5f, 1f, TrapperOn);

            AltruistOn = CustomOption.Create(115, Types.Crewmate, cs(Colors.Altruist, "Altruist"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ReviveDuration = CustomOption.Create(308, Types.Crewmate, "Altruist Revive Duration", 10f, 1f, 15f, 1f, AltruistOn, format: "s");
            AltruistTargetBody = CustomOption.Create(309, Types.Crewmate, "Target's Body Disappears On Beginning Of Revive", false, AltruistOn);

            MedicOn = CustomOption.Create(116, Types.Crewmate, cs(Colors.Medic, "Medic"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ShowShielded = CustomOption.Create(310, Types.Crewmate, "Show Shielded Player", new string[] { "Self", "Medic", "Self+Medic", "Everyone" }, MedicOn);
            WhoGetsNotification = CustomOption.Create(311, Types.Crewmate, "Who Gets Murder Attempt Indicator", new string[] { "Medic", "Shielded", "Everyone", "Nobody" }, MedicOn);
            ShieldBreaks = CustomOption.Create(312, Types.Crewmate, "Shield Breaks On Murder Attempt", false, MedicOn);
            MedicReportSwitch = CustomOption.Create(313, Types.Crewmate, "Show Medic Reports", true, MedicOn);
            MedicReportNameDuration = CustomOption.Create(314, Types.Crewmate, "Time Where Medic Will Have Name", 0f, 0f, 60f, 0.5f, MedicOn, format: "s");
            MedicReportColorDuration = CustomOption.Create(315, Types.Crewmate, "Time Where Medic Will Have Color Type", 15f, 0f, 60f, 0.5f, MedicOn, format: "s");

            HunterOn = CustomOption.Create(120, Types.Crewmate, cs(Colors.Hunter, "Hunter"), 0f, 0f, 100f, 10f, null, true, format: "%");
            HunterKillCd = CustomOption.Create(269, Types.Crewmate, "Hunter Kill Cooldown", 30f, 10f, 60f, 2.5f, HunterOn, format: "s");
            HunterStalkCd = CustomOption.Create(270, Types.Crewmate, "Hunter Stalk Cooldown", 30f, 10f, 60f, 2.5f, HunterOn, format: "s");
            HunterStalkDuration = CustomOption.Create(271, Types.Crewmate, "Hunter Stalk Duration", 10f, 3f, 20f, 0.5f, HunterOn, format: "s");
            HunterStalkUses = CustomOption.Create(272, Types.Crewmate, "Maximum Stalk Uses", 5f, 1f, 15f, 1f, HunterOn);
            HunterBodyReport = CustomOption.Create(273, Types.Crewmate, "Hunter Can Report Who They've Killed", true, HunterOn);

            PoliticianOn = CustomOption.Create(122, Types.Crewmate, cs(Colors.Politician, "Politician"), 0f, 0f, 100f, 10f, null, true, format: "%");
            CampaignCooldown = CustomOption.Create(274, Types.Crewmate, "Campaign Cooldown", 30f, 10f, 60f, 2.5f, PoliticianOn, format: "s");
            PoliticianCanSeeCampaigned = CustomOption.Create(275, Types.Crewmate, "See Campaigned In Meetings", false, PoliticianOn);
            KilledOnBodyguard = CustomOption.Create(276, Types.Crewmate, "Mayor Killed With Bodyguard", false, PoliticianOn);
            BodyguardCooldown = CustomOption.Create(277, Types.Crewmate, "Mayor Bodyguard Cooldown", 30f, 10f, 60f, 2.5f, PoliticianOn, format: "s");
            BodyguardDuration = CustomOption.Create(278, Types.Crewmate, "Mayor Bodyguard Duration", 10f, 3f, 20f, 0.5f, PoliticianOn, format: "s");

            SheriffOn = CustomOption.Create(121, Types.Crewmate, cs(Colors.Sheriff, "Sheriff"), 0f, 0f, 100f, 10f, null, true, format: "%");
            SheriffKillOther = CustomOption.Create(279, Types.Crewmate, "Sheriff Miskill Kills Crewmate", false, SheriffOn);
            SheriffKillsDoomsayer = CustomOption.Create(280, Types.Crewmate, "Sheriff Kills Doomsayer", false, SheriffOn);
            SheriffKillsExecutioner = CustomOption.Create(281, Types.Crewmate, "Sheriff Kills Executioner", false, SheriffOn);
            SheriffKillsJester = CustomOption.Create(282, Types.Crewmate, "Sheriff Kills Jester", false, SheriffOn);
            SheriffKillsScavenger = CustomOption.Create(283, Types.Crewmate, "Sheriff Kills Scavenger", false, SheriffOn);
            SheriffKillsArsonist = CustomOption.Create(284, Types.Crewmate, "Sheriff Kills Arsonist", false, SheriffOn);
            SheriffKillsGlitch = CustomOption.Create(285, Types.Crewmate, "Sheriff Kills The Glitch", false, SheriffOn);
            SheriffKillsJuggernaut = CustomOption.Create(286, Types.Crewmate, "Sheriff Kills Juggernaut", false, SheriffOn);
            SheriffKillsPlaguebearer = CustomOption.Create(287, Types.Crewmate, "Sheriff Kills Plaguebearer", false, SheriffOn);
            SheriffKillsVampire = CustomOption.Create(288, Types.Crewmate, "Sheriff Kills Vampire", false, SheriffOn);
            SheriffKillsWerewolf = CustomOption.Create(289, Types.Crewmate, "Sheriff Kills Werewolf", false, SheriffOn);
            SheriffKillCd = CustomOption.Create(290, Types.Crewmate, "Sheriff Kill Cooldown", 30f, 10f, 60f, 2.5f, SheriffOn, format: "s");
            SheriffBodyReport = CustomOption.Create(291, Types.Crewmate, "Sheriff Can Report Who They've Killed", true, SheriffOn);

            VampireHunterOn = CustomOption.Create(123, Types.Crewmate, cs(Colors.VampireHunter, "Vampire Hunter"), 0f, 0f, 100f, 10f, null, true, format: "%");
            StakeCooldown = CustomOption.Create(292, Types.Crewmate, "Stake Cooldown", 30f, 10f, 60f, 2.5f, VampireHunterOn, format: "s");
            MaxFailedStakesPerGame = CustomOption.Create(293, Types.Crewmate, "Maximum Failed Stakes Per Game", 5f, 1f, 15f, 1f, VampireHunterOn);
            CanStakeRoundOne = CustomOption.Create(294, Types.Crewmate, "Can Stake Round One", false, VampireHunterOn);
            SelfKillAfterFinalStake = CustomOption.Create(295, Types.Crewmate, "Self Kill On Failure To Kill A Vamp With All Stakes", false, VampireHunterOn);
            BecomeOnVampDeaths = CustomOption.Create(296, Types.Crewmate, "What Vampire Hunter Becomes On All Vampire Deaths", new string[] { "Crewmate", "Sheriff", "Veteran", "Vigilante", "Hunter"}, VampireHunterOn);

            VeteranOn = CustomOption.Create(124, Types.Crewmate, cs(Colors.Veteran, "Veteran"), 0f, 0f, 100f, 10f, null, true, format: "%");
            KilledOnAlert = CustomOption.Create(297, Types.Crewmate, "Can Be Killed On Alert", false, VeteranOn);
            AlertCooldown = CustomOption.Create(298, Types.Crewmate, "Alert Cooldown", 30f, 10f, 60f, 2.5f, VeteranOn, format: "s");
            AlertDuration = CustomOption.Create(299, Types.Crewmate, "Alert Duration", 10f, 3f, 20f, 0.5f, VeteranOn, format: "s");
            MaxAlerts = CustomOption.Create(300, Types.Crewmate, "Maximum Number Of Alerts", 5f, 1f, 15f, 1f, VeteranOn);

            VigilanteOn = CustomOption.Create(125, Types.Crewmate, cs(Colors.Vigilante, "Vigilante"), 0f, 0f, 100f, 10f, null, true, format: "%");
            VigilanteKills = CustomOption.Create(301, Types.Crewmate, "Number Of Vigilante Kills", 1f, 1f, 15f, 1f, VigilanteOn);
            VigilanteMultiKill = CustomOption.Create(302, Types.Crewmate, "Vigilante Can Kill More Than Once Per Meeting", false, VigilanteOn);
            VigilanteGuessNeutralBenign = CustomOption.Create(303, Types.Crewmate, "Vigilante Can Guess Neutral Benign Roles", false, VigilanteOn);
            VigilanteGuessNeutralEvil = CustomOption.Create(304, Types.Crewmate, "Vigilante Can Guess Neutral Evil Roles", false, VigilanteOn);
            VigilanteGuessNeutralKilling = CustomOption.Create(305, Types.Crewmate, "Vigilante Can Guess Neutral Killing Roles", false, VigilanteOn);
            VigilanteGuessLovers = CustomOption.Create(306, Types.Crewmate, "Vigilante Can Guess Lovers", false, VigilanteOn);
            VigilanteAfterVoting = CustomOption.Create(307, Types.Crewmate, "Vigilante Can Guess After Voting", false, VigilanteOn);

            EngineerOn = CustomOption.Create(130, Types.Crewmate, cs(Colors.Engineer, "Engineer"), 0f, 0f, 100f, 10f, null, true, format: "%");
            MaxFixes = CustomOption.Create(316, Types.Crewmate, "Maximum Number Of Fixes", 5f, 1f, 5f, 1f, EngineerOn);
            
            ImitatorOn = CustomOption.Create(131, Types.Crewmate, cs(Colors.Imitator, "Imitator"), 0f, 0f, 100f, 10f, null, true, format: "%");
            
            MediumOn = CustomOption.Create(132, Types.Crewmate, cs(Colors.Medium, "Medium"), 0f, 0f, 100f, 10f, null, true, format: "%");
            MediateCooldown = CustomOption.Create(317, Types.Crewmate, "Mediate Cooldown", 10f, 3f, 20f, 0.5f, MediumOn, format: "s");
            ShowMediatePlayer = CustomOption.Create(318, Types.Crewmate, "Reveal Appearance Of Mediate Target", true, MediumOn);
            ShowMediumToDead = CustomOption.Create(319, Types.Crewmate, "Reveal The Medium To The Mediate Target", true, MediumOn);
            DeadRevealed = CustomOption.Create(320, Types.Crewmate, "Who Is Revealed With Mediate", new string[] { "Oldest Dead", "Newest Dead", "All Dead" }, MediumOn);
            
            ProsecutorOn = CustomOption.Create(133, Types.Crewmate, cs(Colors.Prosecutor, "Prosecutor"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ProsDiesOnIncorrectPros = CustomOption.Create(321, Types.Crewmate, "Prosecutor Dies When They Exile A Crewmate", false, ProsecutorOn);
            
            SwapperOn = CustomOption.Create(134, Types.Crewmate, cs(Colors.Swapper, "Swapper"), 0f, 0f, 100f, 10f, null, true, format: "%");
            SwapperButton = CustomOption.Create(322, Types.Crewmate, "Swapper Can Button", true, SwapperOn);
            
            TransporterOn = CustomOption.Create(135, Types.Crewmate, cs(Colors.Transporter, "Transporter"), 0f, 0f, 100f, 10f, null, true, format: "%");
            TransportCooldown = CustomOption.Create(323, Types.Crewmate, "Transport Cooldown", 30f, 10f, 60f, 2.5f, TransporterOn, format: "s");
            TransportMaxUses = CustomOption.Create(324, Types.Crewmate, "Maximum Number Of Transports", 5f, 1f, 15f, 1f, TransporterOn);
            TransporterVitals = CustomOption.Create(325, Types.Crewmate, "Transporter Can Use Vitals", false, TransporterOn);
    #endregion

    #region Neutral Roles
            AmnesiacOn = CustomOption.Create(140, Types.Neutral, cs(Colors.Amnesiac, "Amnesiac"), 0f, 0f, 100f, 10f, null, true, format: "%");
            RememberArrows = CustomOption.Create(330, Types.Neutral, "Amnesiac Gets Arrows Pointing To Dead Bodies", false, AmnesiacOn);
            RememberArrowDelay = CustomOption.Create(331, Types.Neutral, "Time After Death Arrow Appears", 5f, 0f, 15f, 1f, AmnesiacOn, format: "s");

            GuardianAngelOn = CustomOption.Create(141, Types.Neutral, cs(Colors.GuardianAngel, "GuardianAngel"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ProtectCd = CustomOption.Create(332, Types.Neutral, "Protect Cooldown", 30f, 10f, 60f, 2.5f, GuardianAngelOn, format: "s");
            ProtectDuration = CustomOption.Create(333, Types.Neutral, "Protect Duration", 10f, 3f, 20f, 0.5f, GuardianAngelOn, format: "s");
            ProtectKCReset = CustomOption.Create(334, Types.Neutral, "Kill Cooldown Reset When Protected", 30f, 10f, 60f, 2.5f, GuardianAngelOn, format: "s");
            MaxProtects = CustomOption.Create(335, Types.Neutral, "Maximum Number Of Protects", 5f, 1f, 15f, 1f, GuardianAngelOn);
            ShowProtect = CustomOption.Create(336, Types.Neutral, "Show Protected Player", new string[] { "Self", "Guardian Angel", "Self+GA", "Everyone" }, GuardianAngelOn);
            GaOnTargetDeath = CustomOption.Create(337, Types.Neutral, "GA Becomes On Target Dead", new string[] { "Crew", "Amnesiac", "Survivor", "Jester" }, GuardianAngelOn);
            GATargetKnows = CustomOption.Create(338, Types.Neutral, "Target Knows GA Exists", false, GuardianAngelOn);
            GAKnowsTargetRole = CustomOption.Create(339, Types.Neutral, "GA Knows Targets Role", false, GuardianAngelOn);
            EvilTargetPercent = CustomOption.Create(340, Types.Neutral, "Odds Of Target Being Evil", 0f, 0f, 100f, 10f, GuardianAngelOn, format: "%");

            LawyerOn = CustomOption.Create(142, Types.Neutral, cs(Colors.Lawyer, "Lawyer"), 0f, 0f, 100f, 10f, null, true, format: "%");
            LawyerDies = CustomOption.Create(341, Types.Neutral, "Lawyer Dies With Defendant", true, LawyerOn);
            DefendantImpPercent = CustomOption.Create(342, Types.Neutral, "Killer Defendant Probability", 0f, 0f, 100f, 10f, LawyerOn, format: "%");
            NeutralDefendant = CustomOption.Create(343, Types.Neutral, "Neutral Evil Roles Can Be Defendants", false, LawyerOn);
            LawyerCanTalkDefendant = CustomOption.Create(344, Types.Neutral, "Lawyer And Defendant Can Talk", false, LawyerOn);
            OnDefendantDead = CustomOption.Create(345, Types.Neutral, "Lawyer Becomes On Defendant Dead", new string[] { "Crew", "Amnesiac", "Survivor", "Jester" }, LawyerOn);

            MercenaryOn = CustomOption.Create(143, Types.Neutral, cs(Colors.Mercenary, "Mercenary"), 0f, 0f, 100f, 10f, null, true, format: "%");
            MercenaryBrildersRequired = CustomOption.Create(346, Types.Neutral, "Brilders Required To Win", 5f, 1f, 15f, 1f, MercenaryOn);
            ShowMercShielded = CustomOption.Create(347, Types.Neutral, "Show Shielded Player", new string[] { "Mercenary", "Self", "Self+Mercenary", "Everyone" }, MercenaryOn);
            WhoGetsMercNotification = CustomOption.Create(348, Types.Neutral, "Who Gets Ability Block Indicator", new string[] { "Mercenary", "Shielded", "Mercenary+Shielded", "Everyone", "Nobody" }, MercenaryOn);
            MercAbsorbCd = CustomOption.Create(349, Types.Neutral, "Cooldown Reset On Absorbed", 30f, 10f, 60f, 2.5f, MercenaryOn, format: "s");
            MercArmorCd = CustomOption.Create(350, Types.Neutral, "Armor Cooldown", 30f, 10f, 60f, 2.5f, MercenaryOn, format: "s");
            MercArmorDuration = CustomOption.Create(351, Types.Neutral, "Armor Duration", 10f, 3f, 20f, 0.5f, MercenaryOn, format: "s");

            SurvivorOn = CustomOption.Create(144, Types.Neutral, cs(Colors.Survivor, "Survivor"), 0f, 0f, 100f, 10f, null, true, format: "%");
            VestCd = CustomOption.Create(352, Types.Neutral, "Vest Cooldown", 30f, 10f, 60f, 2.5f, SurvivorOn, format: "s");
            VestDuration = CustomOption.Create(353, Types.Neutral, "Vest Duration", 10f, 3f, 20f, 0.5f, SurvivorOn, format: "s");
            VestKCReset = CustomOption.Create(354, Types.Neutral, "Kill Cooldown Reset On Attack", 30f, 10f, 60f, 2.5f, SurvivorOn, format: "s");
            MaxVests = CustomOption.Create(355, Types.Neutral, "Maximum Number Of Vests", 5f, 1f, 15f, 1f, SurvivorOn);

            DoomsayerOn = CustomOption.Create(145, Types.Neutral, cs(Colors.Doomsayer, "Doomsayer"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ObserveCooldown = CustomOption.Create(356, Types.Neutral, "Observe Cooldown", 30f, 10f, 60f, 2.5f, DoomsayerOn, format: "s");
            DoomsayerGuessNeutralBenign = CustomOption.Create(357, Types.Neutral, "Doomsayer Can Guess Neutral Benign Roles", false, DoomsayerOn);
            DoomsayerGuessNeutralEvil = CustomOption.Create(358, Types.Neutral, "Doomsayer Can Guess Neutral Evil Roles", false, DoomsayerOn);
            DoomsayerGuessNeutralKilling = CustomOption.Create(359, Types.Neutral, "Doomsayer Can Guess Neutral Killing Roles", false, DoomsayerOn);
            DoomsayerGuessImpostors = CustomOption.Create(360, Types.Neutral, "Doomsayer Can Guess Impostor Roles", false, DoomsayerOn);
            DoomsayerAfterVoting = CustomOption.Create(361, Types.Neutral, "Doomsayer Can Guess After Voting", false, DoomsayerOn);
            DoomsayerGuessesToWin = CustomOption.Create(362, Types.Neutral, "Number Of Doomsayer Kills To Win", 3f, 1f, 5f, 1f, DoomsayerOn);
            DoomsayerCantObserve = CustomOption.Create(363, Types.Neutral, "(Experienced) Doomsayer can't observe", false, DoomsayerOn);

            ExecutionerOn = CustomOption.Create(146, Types.Neutral, cs(Colors.Executioner, "Executioner"), 0f, 0f, 100f, 10f, null, true, format: "%");
            OnTargetDead = CustomOption.Create(364, Types.Neutral, "Executioner Becomes On Target Dead", new string[] { "Crew", "Amnesiac", "Survivor", "Jester" }, ExecutionerOn);
            ExecutionerButton = CustomOption.Create(365, Types.Neutral, "Executioner Can Button", true, ExecutionerOn);
            ExecutionerTorment = CustomOption.Create(366, Types.Neutral, "Executioner Torments Player On Victory", true, ExecutionerOn);

            JesterOn = CustomOption.Create(147, Types.Neutral, cs(Colors.Jester, "Jester"), 0f, 0f, 100f, 10f, null, true, format: "%");
            JesterButton = CustomOption.Create(367, Types.Neutral, "Jester Can Button", true, JesterOn);
            JesterVent = CustomOption.Create(368, Types.Neutral, "Jester Can Hide In Vents", false, JesterOn);
            JesterImpVision = CustomOption.Create(369, Types.Neutral, "Jester Has Impostor Vision", false, JesterOn);
            JesterHaunt = CustomOption.Create(370, Types.Neutral, "Jester Haunts Player On Victory", true, JesterOn);

            ScavengerOn = CustomOption.Create(148, Types.Neutral, cs(Colors.Scavenger, "Scavenger"), 0f, 0f, 100f, 10f, null, true, format: "%");
            DevourCooldown = CustomOption.Create(371, Types.Neutral, "Devour Cooldown", 30f, 10f, 60f, 2.5f, ScavengerOn, format: "s");
            ScavCorpsesToWin = CustomOption.Create(372, Types.Neutral, "Corpses Devoured To Win", 5f, 1f, 15f, 1f, ScavengerOn);
            ScavengerImpVision = CustomOption.Create(373, Types.Neutral, "Scavenger Has Impostor Vision", false, ScavengerOn);
            ScavengerVent = CustomOption.Create(374, Types.Neutral, "Scavenger Can Vent", false, ScavengerOn);

            PhantomOn = CustomOption.Create(149, Types.Neutral, cs(Colors.Phantom, "Phantom"), 0f, 0f, 100f, 10f, null, true, format: "%");
            PhantomTasksRemaining = CustomOption.Create(375, Types.Neutral, "Tasks Remaining When Phantom Can Be Clicked", 5f, 1f, 15f, 1f, PhantomOn);
            PhantomSpook = CustomOption.Create(376, Types.Neutral, "Phantom Spooks Player On Victory", true, PhantomOn);
            
            ArsonistOn = CustomOption.Create(150, Types.Neutral, cs(Colors.Arsonist, "Arsonist"), 0f, 0f, 100f, 10f, null, true, format: "%");
            DouseCooldown = CustomOption.Create(377, Types.Neutral, "Douse Cooldown", 30f, 10f, 60f, 2.5f, ArsonistOn, format: "s");
            MaxDoused = CustomOption.Create(378, Types.Neutral, "Maximum Alive Players Doused", 5f, 1f, 15f, 1f, ArsonistOn);
            ArsoImpVision = CustomOption.Create(379, Types.Neutral, "Arsonist Has Impostor Vision", false, ArsonistOn);
            IgniteCdRemoved = CustomOption.Create(380, Types.Neutral, "Ignite Cooldown Removed When Arsonist Is Last Killer", false, ArsonistOn);

            JuggernautOn = CustomOption.Create(520, Types.Neutral, cs(Colors.Juggernaut, "Juggernaut"), 0f, 0f, 100f, 10f, null, true, format: "%");
            JuggKillCooldown = CustomOption.Create(381, Types.Neutral, "Juggernaut Initial Kill Cooldown", 30f, 10f, 60f, 2.5f, JuggernautOn, format: "s");
            ReducedKCdPerKill = CustomOption.Create(382, Types.Neutral, "Reduced Kill Cooldown Per Kill", 5f, 2.5f, 10f, 2.5f, JuggernautOn, format: "s");
            JuggVent = CustomOption.Create(383, Types.Neutral, "Juggernaut Can Vent", false, JuggernautOn);

            PlaguebearerOn = CustomOption.Create(151, Types.Neutral, cs(Colors.Plaguebearer, "Plaguebearer"), 0f, 0f, 100f, 10f, null, true, format: "%");
            InfectCooldown = CustomOption.Create(384, Types.Neutral, "Infect Cooldown", 30f, 10f, 60f, 2.5f, PlaguebearerOn, format: "s");
            PestKillCooldown = CustomOption.Create(385, Types.Neutral, "Pestilence Kill Cooldown", 30f, 10f, 60f, 2.5f, PlaguebearerOn, format: "s");
            PestVent = CustomOption.Create(386, Types.Neutral, "Pestilence Can Vent", false, PlaguebearerOn);

            GlitchOn = CustomOption.Create(152, Types.Neutral, cs(Colors.Glitch, "Glitch"), 0f, 0f, 100f, 10f, null, true, format: "%");
            MimicCooldownOption = CustomOption.Create(387, Types.Neutral, "Mimic Cooldown", 30f, 10f, 60f, 2.5f, GlitchOn, format: "s");
            MimicDurationOption = CustomOption.Create(388, Types.Neutral, "Mimic Duration", 10f, 3f, 20f, 0.5f, GlitchOn, format: "s");
            HackCooldownOption = CustomOption.Create(389, Types.Neutral, "Hack Cooldown", 30f, 10f, 60f, 2.5f, GlitchOn, format: "s");
            HackDurationOption = CustomOption.Create(390, Types.Neutral, "Hack Duration", 10f, 3f, 20f, 0.5f, GlitchOn, format: "s");
            GlitchKillCooldownOption = CustomOption.Create(391, Types.Neutral, "Glitch Kill Cooldown", 30f, 10f, 60f, 2.5f, GlitchOn, format: "s");
            GlitchHackDistanceOption = CustomOption.Create(392, Types.Neutral, "Glitch Hack Distance", new string[] { "Short", "Normal", "Long" }, GlitchOn);
            GlitchVent = CustomOption.Create(393, Types.Neutral, "Glitch Can Vent", false, GlitchOn);

            VampireOn = CustomOption.Create(153, Types.Neutral, cs(Colors.Vampire, "Vampire"), 0f, 0f, 100f, 10f, null, true, format: "%");
            BiteCooldown = CustomOption.Create(394, Types.Neutral, "Vampire Bite Cooldown", 30f, 10f, 60f, 2.5f, VampireOn, format: "s");
            VampImpVision = CustomOption.Create(395, Types.Neutral, "Vampires Have Impostor Vision", false, VampireOn);
            VampVent = CustomOption.Create(396, Types.Neutral, "Vampires Can Vent", false, VampireOn);
            NewVampCanAssassin = CustomOption.Create(397, Types.Neutral, "New Vampire Can Assassinate", false, VampireOn);
            MaxVampiresPerGame = CustomOption.Create(398, Types.Neutral, "Maximum Vampires Per Game", 3f, 2f, 5f, 1f, VampireOn);
            CanBiteNeutralBenign = CustomOption.Create(399, Types.Neutral, "Can Convert Neutral Benign Roles", false, VampireOn);
            CanBiteNeutralEvil = CustomOption.Create(400, Types.Neutral, "Can Convert Neutral Evil Roles", false, VampireOn);

            WerewolfOn = CustomOption.Create(154, Types.Neutral, cs(Colors.Werewolf, "Werewolf"), 0f, 0f, 100f, 10f, null, true, format: "%");
            RampageCooldown = CustomOption.Create(401, Types.Neutral, "Rampage Cooldown", 30f, 10f, 60f, 2.5f, WerewolfOn, format: "s");
            RampageDuration = CustomOption.Create(402, Types.Neutral, "Rampage Duration", 30f, 10f, 60f, 2.5f, WerewolfOn, format: "s");
            RampageKillCooldown = CustomOption.Create(403, Types.Neutral, "Rampage Kill Cooldown", 10f, 0.5f, 15f, 0.5f, WerewolfOn, format: "s");
            WerewolfVent = CustomOption.Create(404, Types.Neutral, "Werewolf Can Vent When Rampaged", false, WerewolfOn);
    #endregion

    #region Impostor Roles
            NumberOfImpostorAssassins = CustomOption.Create(200, Types.Impostor, "Number Of Impostor Assassins", 1f, 0f, 4f, 1f, heading: cs(Colors.Impostor, "Assassin Ability"));
            NumberOfNeutralAssassins = CustomOption.Create(201, Types.Impostor, "Number Of Neutral Assassins", 15f, 0f, 15f, 1f);
            AmneTurnImpAssassin = CustomOption.Create(202, Types.Impostor, "Amnesiac Turned Impostor Gets Ability", false);
            AmneTurnNeutAssassin = CustomOption.Create(203, Types.Impostor, "Amnesiac Turned Neutral Killing Gets Ability", false);
            TraitorCanAssassin = CustomOption.Create(204, Types.Impostor, "Traitor Gets Ability", false);
            AssassinKills = CustomOption.Create(205, Types.Impostor, "Number Of Assassin Kills", 3f, 1f, 15f, 1f);
            AssassinMultiKill = CustomOption.Create(206, Types.Impostor, "Assassin Can Kill More Than Once Per Meeting", false);
            AssassinCrewmateGuess = CustomOption.Create(207, Types.Impostor, "Assassin Can Guess Crewmate", false);
            AssassinGuessNeutralBenign = CustomOption.Create(208, Types.Impostor, "Assassin Can Guess Neutral Benign Roles", false);
            AssassinGuessNeutralEvil = CustomOption.Create(209, Types.Impostor, "Assassin Can Guess Neutral Evil Roles", false);
            AssassinGuessNeutralKilling = CustomOption.Create(210, Types.Impostor, "Assassin Can Guess Neutral Killing Roles", false);
            AssassinGuessImpostors = CustomOption.Create(211, Types.Impostor, "Assassin Can Guess Impostor Roles", false);
            AssassinGuessModifiers = CustomOption.Create(212, Types.Impostor, "Assassin Can Guess Crewmate Modifiers", false);
            AssassinGuessLovers = CustomOption.Create(213, Types.Impostor, "Assassin Can Guess Lovers", false);
            AssassinateAfterVoting = CustomOption.Create(214, Types.Impostor, "Assassin Can Guess After Voting", false);

            EscapistOn = CustomOption.Create(155, Types.Impostor, cs(Colors.Impostor, "Escapist"), 0f, 0f, 100f, 10f, null, true, format: "%");
            EscapeCooldown = CustomOption.Create(405, Types.Impostor, "Recall Cooldown", 30f, 10f, 60f, 2.5f, EscapistOn, format: "s");
            EscapistVent = CustomOption.Create(406, Types.Impostor, "Escapist Can Vent", false, EscapistOn);

            MorphlingOn = CustomOption.Create(156, Types.Impostor, cs(Colors.Impostor, "Morphling"), 0f, 0f, 100f, 10f, null, true, format: "%");
            MorphlingCooldown = CustomOption.Create(412, Types.Impostor, "Morphling Cooldown", 30f, 10f, 60f, 2.5f, MorphlingOn, format: "s");
            MorphlingDuration = CustomOption.Create(413, Types.Impostor, "Morphling Duration", 10f, 3f, 20f, 0.5f, MorphlingOn, format: "s");
            MorphlingVent = CustomOption.Create(414, Types.Impostor, "Morphling Can Vent", false, MorphlingOn);

            SwooperOn = CustomOption.Create(157, Types.Impostor, cs(Colors.Impostor, "Swooper"), 0f, 0f, 100f, 10f, null, true, format: "%");
            SwoopCooldown = CustomOption.Create(415, Types.Impostor, "Swoop Cooldown", 30f, 10f, 60f, 2.5f, SwooperOn, format: "s");
            SwoopDuration = CustomOption.Create(416, Types.Impostor, "Swoop Duration", 10f, 3f, 20f, 0.5f, SwooperOn, format: "s");
            SwooperVent = CustomOption.Create(417, Types.Impostor, "Swooper Can Vent", false, SwooperOn);

            GrenadierOn = CustomOption.Create(158, Types.Impostor, cs(Colors.Impostor, "Grenadier"), 0f, 0f, 100f, 10f, null, true, format: "%");
            GrenadeCooldown = CustomOption.Create(407, Types.Impostor, "Flash Grenade Cooldown", 30f, 10f, 60f, 2.5f, GrenadierOn, format: "s");
            GrenadeDuration = CustomOption.Create(408, Types.Impostor, "Flash Grenade Duration", 10f, 3f, 20f, 0.5f, GrenadierOn, format: "s");
            FlashRadius = CustomOption.Create(409, Types.Impostor, "Flash Radius", 1f, 0.25f, 5f, 0.25f, GrenadierOn, format: "x");
            GrenadierIndicators = CustomOption.Create(410, Types.Impostor, "Indicate Flashed Crewmates", false, GrenadierOn);
            GrenadierVent = CustomOption.Create(411, Types.Impostor, "Grenadier Can Vent", false, GrenadierOn);
            
            VenererOn = CustomOption.Create(159, Types.Impostor, cs(Colors.Impostor, "Venerer"), 0f, 0f, 100f, 10f, null, true, format: "%");
            AbilityCooldown = CustomOption.Create(418, Types.Impostor, "Ability Cooldown", 30f, 10f, 60f, 2.5f, VenererOn, format: "s");
            AbilityDuration = CustomOption.Create(419, Types.Impostor, "Ability Duration", 10f, 3f, 20f, 0.5f, VenererOn, format: "s");
            SprintSpeed = CustomOption.Create(420, Types.Impostor, "Sprint Speed", 1.25f, 1.05f, 2.5f, 0.05f, VenererOn, format: "x");
            FreezeSpeed = CustomOption.Create(421, Types.Impostor, "Freeze Speed", 0.75f, 0f, 1f, 0.05f, VenererOn, format: "x");

            BomberOn = CustomOption.Create(160, Types.Impostor, cs(Colors.Impostor, "Bomber"), 0f, 0f, 100f, 10f, null, true, format: "%");
            DetonateDelay = CustomOption.Create(422, Types.Impostor, "Detonate Delay", 5f, 1f, 15f, 1f, BomberOn, format: "s");
            MaxKillsInDetonation = CustomOption.Create(423, Types.Impostor, "Max Kills In Detonation", 5f, 1f, 15f, 1f, BomberOn);
            DetonateRadius = CustomOption.Create(424, Types.Impostor, "Detonate Radius", 0.25f, 0.05f, 1f, 0.05f, BomberOn, format: "x");
            BomberVent = CustomOption.Create(425, Types.Impostor, "Bomber Can Vent", false, BomberOn);

            PoisonerOn = CustomOption.Create(161, Types.Impostor, cs(Colors.Impostor, "Poisoner"), 0f, 0f, 100f, 10f, null, true, format: "%");
            PoisonCooldown = CustomOption.Create(426, Types.Impostor, "Poison Cooldown", 30f, 10f, 60f, 2.5f, PoisonerOn, format: "s");
            PoisonDuration = CustomOption.Create(427, Types.Impostor, "Poison Duration", 10f, 3f, 20f, 0.5f, PoisonerOn, format: "s");
            PoisonerVent = CustomOption.Create(428, Types.Impostor, "Poisoner Can Vent", false, PoisonerOn);

            TraitorOn = CustomOption.Create(162, Types.Impostor, cs(Colors.Impostor, "Traitor"), 0f, 0f, 100f, 10f, null, true, format: "%");
            LatestSpawn = CustomOption.Create(429, Types.Impostor, "Minimum People Alive When Traitor Can Spawn", 5, 3, 15, 1, TraitorOn);
            NeutralKillingStopsTraitor = CustomOption.Create(430, Types.Impostor, "Traitor Won't Spawn If Any Neutral Killing Is Alive", false, TraitorOn);

            WarlockOn = CustomOption.Create(163, Types.Impostor, cs(Colors.Impostor, "Warlock"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ChargeUpDuration = CustomOption.Create(431, Types.Impostor, "Time It Takes To Fully Charge", 30f, 10f, 60f, 2.5f, WarlockOn, format: "s");
            ChargeUseDuration = CustomOption.Create(432, Types.Impostor, "Time It Takes To Use Full Charge", 1f, 0.05f, 5f, 0.05f, WarlockOn, format: "s");
            
            BlackmailerOn = CustomOption.Create(164, Types.Impostor, cs(Colors.Impostor, "Blackmailer"), 0f, 0f, 100f, 10f, null, true, format: "%");
            BlackmailCooldown = CustomOption.Create(433, Types.Impostor, "Initial Blackmail Cooldown", 10f, 1f, 15f, 1f, BlackmailerOn, format: "s");
            BlackmailInvisible = CustomOption.Create(434, Types.Impostor, "Only Target Sees Blackmail", false, BlackmailerOn);

            JanitorOn = CustomOption.Create(165, Types.Impostor, cs(Colors.Impostor, "Janitor"), 0f, 0f, 100f, 10f, null, true, format: "%");

            MinerOn = CustomOption.Create(166, Types.Impostor, cs(Colors.Impostor, "Miner"), 0f, 0f, 100f, 10f, null, true, format: "%");
            MineCooldown = CustomOption.Create(435, Types.Impostor, "Mine Cooldown", 30f, 10f, 60f, 2.5f, MinerOn, format: "s");

            UndertakerOn = CustomOption.Create(167, Types.Impostor, cs(Colors.Impostor, "Undertaker"), 0f, 0f, 100f, 10f, null, true, format: "%");
            DragCooldown = CustomOption.Create(436, Types.Impostor, "Drag Cooldown", 30f, 10f, 60f, 2.5f, UndertakerOn, format: "s");
            UndertakerDragSpeed = CustomOption.Create(437, Types.Impostor, "Undertaker Drag Speed", 0.75f, 0.25f, 1f, 0.05f, UndertakerOn, format: "x");
            UndertakerVent = CustomOption.Create(438, Types.Impostor, "Undertaker Can Vent", false, UndertakerOn);
            UndertakerVentWithBody = CustomOption.Create(439, Types.Impostor, "Undertaker Can Vent While Dragging", false, UndertakerOn);
    #endregion

    #region Modifiers
            AftermathOn = CustomOption.Create(170, Types.Modifier, cs(Colors.CrewModifier, "Aftermath"), 0f, 0f, 100f, 10f, null, true, format: "%");
            
            BaitOn = CustomOption.Create(171, Types.Modifier, cs(Colors.CrewModifier, "Bait"), 0f, 0f, 100f, 10f, null, true, format: "%");
            BaitMinDelay = CustomOption.Create(450, Types.Modifier, "Minimum Delay for the Bait Report", 0f, 0f, 15f, 0.5f, BaitOn, format: "s");
            BaitMaxDelay = CustomOption.Create(451, Types.Modifier, "Maximum Delay for the Bait Report", 1f, 0f, 15f, 0.5f, BaitOn, format: "s");
            
            BlindOn = CustomOption.Create(172, Types.Modifier, cs(Colors.CrewModifier, "Blind"), 0f, 0f, 100f, 10f, null, true, format: "%");
            
            DiseasedOn = CustomOption.Create(173, Types.Modifier, cs(Colors.CrewModifier, "Diseased"), 0f, 0f, 100f, 10f, null, true, format: "%");
            DiseasedKillMultiplier = CustomOption.Create(452, Types.Modifier, "Diseased Kill Multiplier", 3f, 1.5f, 5f, 0.5f, DiseasedOn, format: "x");
            
            FrostyOn = CustomOption.Create(174, Types.Modifier, cs(Colors.CrewModifier, "Frosty"), 0f, 0f, 100f, 10f, null, true, format: "%");
            ChillDuration = CustomOption.Create(453, Types.Modifier, "Chill Duration", 10f, 1f, 15f, 1f, FrostyOn, format: "s");
            ChillStartSpeed = CustomOption.Create(454, Types.Modifier, "Chill Start Speed", 0.75f, 0.25f, 0.95f, 0.05f, FrostyOn, format: "x");
            
            MultitaskerOn = CustomOption.Create(175, Types.Modifier, cs(Colors.CrewModifier, "Multitasker"), 0f, 0f, 100f, 10f, null, true, format: "%");
            
            TorchOn = CustomOption.Create(176, Types.Modifier, cs(Colors.CrewModifier, "Torch"), 0f, 0f, 100f, 10f, null, true, format: "%");

            ButtonBarryOn = CustomOption.Create(177, Types.Modifier, cs(Colors.GlobalModifier, "Button Barry"), 0f, 0f, 100f, 10f, null, true, format: "%");

            DrunkOn = CustomOption.Create(178, Types.Modifier, cs(Colors.GlobalModifier, "Drunk"), 0f, 0f, 100f, 10f, null, true, format: "%");

            FlashOn = CustomOption.Create(179, Types.Modifier, cs(Colors.GlobalModifier, "Flash"), 0f, 0f, 100f, 10f, null, true, format: "%");
            FlashSpeed = CustomOption.Create(455, Types.Modifier, "Flash Speed", 1.25f, 1.05f, 2.5f, 0.05f, FlashOn, format: "x");

            GiantOn = CustomOption.Create(180, Types.Modifier, cs(Colors.GlobalModifier, "Giant"), 0f, 0f, 100f, 10f, null, true, format: "%");
            GiantSlow = CustomOption.Create(456, Types.Modifier, "Giant Speed", 0.75f, 0.25f, 1f, 0.05f, GiantOn, format: "x");

            LoversOn = CustomOption.Create(181, Types.Modifier, cs(Colors.GlobalModifier, "Lovers"), 0f, 0f, 100f, 10f, null, true, format: "%");
            BothLoversDie = CustomOption.Create(457, Types.Modifier, "Both Lovers Die", true, LoversOn);
            LovingImpPercent = CustomOption.Create(458, Types.Modifier, "Loving Impostor Probability", 0f, 0f, 100f, 10f, LoversOn, format: "%");
            NeutralLovers = CustomOption.Create(459, Types.Modifier, "Neutral Roles Can Be Lovers", true, LoversOn);

            RadarOn = CustomOption.Create(182, Types.Modifier, cs(Colors.GlobalModifier, "Radar"), 0f, 0f, 100f, 10f, null, true, format: "%");

            SleuthOn = CustomOption.Create(183, Types.Modifier, cs(Colors.GlobalModifier, "Sleuth"), 0f, 0f, 100f, 10f, null, true, format: "%");

            TiebreakerOn = CustomOption.Create(184, Types.Modifier, cs(Colors.GlobalModifier, "Tiebreaker"), 0f, 0f, 100f, 10f, null, true, format: "%");

            DisperserOn = CustomOption.Create(185, Types.Modifier, cs(Colors.ImpModifier, "Disperser"), 0f, 0f, 100f, 10f, null, true, format: "%");

            DoubleShotOn = CustomOption.Create(186, Types.Modifier, cs(Colors.ImpModifier, "Double Shot"), 0f, 0f, 100f, 10f, null, true, format: "%");

            UnderdogOn = CustomOption.Create(187, Types.Modifier, cs(Colors.ImpModifier, "Underdog"), 0f, 0f, 100f, 10f, null, true, format: "%");
            UnderdogKillBonus = CustomOption.Create(460, Types.Modifier, "Kill Cooldown Bonus", 30f, 2.5f, 60f, 2.5f, UnderdogOn, format: "s");
            UnderdogIncreasedKC = CustomOption.Create(461, Types.Modifier, "Increased Kill Cooldown When 2+ Imps", true, UnderdogOn);
    #endregion
#endregion
        }
    }
}