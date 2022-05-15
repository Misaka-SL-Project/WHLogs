using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Exiled.API.Features;
using HarmonyLib;
using MEC;
using Utf8Json;
using UnityEngine.Networking;

namespace WHLogs
{
    public class Plugin : Plugin<Config, Translation>
    {
        public override string Author => "xRoier";
        public override string Name => "WebhookLogs";
        public override string Prefix => "webhooklogs";
        public override Version Version => new Version(4, 0, 1);
        public override Version RequiredExiledVersion => new Version(5,2,1);
        public static Plugin Singleton;
        public List<string> GameLogsQueue = new List<string>();
        public List<string> PvPLogsQueue = new List<string>();
        public List<string> CommandLogsQueue = new List<string>();
        private List<CoroutineHandle> Coroutines = new List<CoroutineHandle>();
        private EventHandlers EventHandlers;
        private Harmony _harmony;
        
        public override void OnEnabled()
        {
            if (Config.GameEventsLogsWebhookUrl == "fill me" || Config.CommandLogsWebhookUrl == "fill me" || Config.PvpEventsLogsWebhookUrl == "fill me")
            {
                Log.Warn("You didn't change one or more \"webhook_url\" configs, the plugin will not load.");
                OnDisabled();
                return;
            }
            
            if (Config.LogQueueDelay < 1.2f)
            {
                Log.Warn("You should not set the delay below 1.2 seconds to avoid Discord ratelimits, the plugin will not load.");
                OnDisabled();
                return;
            }
            
            Singleton = this;
            EventHandlers = new EventHandlers();
            _harmony = new Harmony($"com.xroier.webhooklogs-{DateTime.Now.Ticks}");
            _harmony.PatchAll();
            Exiled.Events.Handlers.Map.Decontaminating += EventHandlers.OnDecontaminating;
            Exiled.Events.Handlers.Map.GeneratorActivated += EventHandlers.OnGeneratorActivated;
            Exiled.Events.Handlers.Warhead.Starting += EventHandlers.OnStartingWarhead;
            Exiled.Events.Handlers.Warhead.Stopping += EventHandlers.OnStoppingWarhead;
            Exiled.Events.Handlers.Warhead.Detonated += EventHandlers.OnWarheadDetonated;
            Exiled.Events.Handlers.Server.WaitingForPlayers += EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted += EventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded += EventHandlers.OnRoundEnded;
            Exiled.Events.Handlers.Server.RespawningTeam += EventHandlers.OnRespawningTeam;
            Exiled.Events.Handlers.Scp914.ChangingKnobSetting += EventHandlers.OnChangingScp914KnobSetting;
            Exiled.Events.Handlers.Player.UsingItem += EventHandlers.OnUsedMedicalItem;
            Exiled.Events.Handlers.Scp079.InteractingTesla += EventHandlers.OnInteractingTesla;
            Exiled.Events.Handlers.Player.PickingUpItem += EventHandlers.OnPickingUpItem;
            Exiled.Events.Handlers.Scp079.GainingLevel += EventHandlers.OnGainingLevel;
            Exiled.Events.Handlers.Player.EscapingPocketDimension += EventHandlers.OnEscapingPocketDimension;
            Exiled.Events.Handlers.Player.EnteringPocketDimension += EventHandlers.OnEnteringPocketDimension;
            Exiled.Events.Handlers.Player.ActivatingWarheadPanel += EventHandlers.OnActivatingWarheadPanel;
            Exiled.Events.Handlers.Player.TriggeringTesla += EventHandlers.OnTriggeringTesla;
            Exiled.Events.Handlers.Player.ThrowingItem += EventHandlers.OnThrowingGrenade;
            Exiled.Events.Handlers.Player.Hurting += EventHandlers.OnHurting;
            Exiled.Events.Handlers.Player.Dying += EventHandlers.OnDying;
            Exiled.Events.Handlers.Player.InteractingDoor += EventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.InteractingElevator += EventHandlers.OnInteractingElevator;
            Exiled.Events.Handlers.Player.InteractingLocker += EventHandlers.OnInteractingLocker;
            Exiled.Events.Handlers.Player.IntercomSpeaking += EventHandlers.OnIntercomSpeaking;
            Exiled.Events.Handlers.Player.Handcuffing += EventHandlers.OnHandcuffing;
            Exiled.Events.Handlers.Player.RemovingHandcuffs += EventHandlers.OnRemovingHandcuffs;
            Exiled.Events.Handlers.Scp106.Teleporting += EventHandlers.OnTeleporting;
            Exiled.Events.Handlers.Player.DroppingItem += EventHandlers.OnItemDropped;
            Exiled.Events.Handlers.Player.Verified += EventHandlers.OnVerified;
            Exiled.Events.Handlers.Player.Destroying += EventHandlers.OnDestroying;
            Exiled.Events.Handlers.Player.ChangingRole += EventHandlers.OnChangingRole;
            Exiled.Events.Handlers.Player.ChangingItem += EventHandlers.OnChangingItem;
            Exiled.Events.Handlers.Scp914.Activating += EventHandlers.OnActivatingScp914;
            Exiled.Events.Handlers.Scp106.Containing += EventHandlers.OnContaining;

            Coroutines.Add(Timing.RunCoroutine(QueueSender(Config.LogQueueDelay)));
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            Exiled.Events.Handlers.Map.Decontaminating -= EventHandlers.OnDecontaminating;
            Exiled.Events.Handlers.Map.GeneratorActivated -= EventHandlers.OnGeneratorActivated;
            Exiled.Events.Handlers.Warhead.Starting -= EventHandlers.OnStartingWarhead;
            Exiled.Events.Handlers.Warhead.Stopping -= EventHandlers.OnStoppingWarhead;
            Exiled.Events.Handlers.Warhead.Detonated -= EventHandlers.OnWarheadDetonated;
            Exiled.Events.Handlers.Server.WaitingForPlayers -= EventHandlers.OnWaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted -= EventHandlers.OnRoundStarted;
            Exiled.Events.Handlers.Server.RoundEnded -= EventHandlers.OnRoundEnded;
            Exiled.Events.Handlers.Server.RespawningTeam -= EventHandlers.OnRespawningTeam;
            Exiled.Events.Handlers.Scp914.ChangingKnobSetting -= EventHandlers.OnChangingScp914KnobSetting;
            Exiled.Events.Handlers.Player.UsingItem -= EventHandlers.OnUsedMedicalItem;
            Exiled.Events.Handlers.Scp079.InteractingTesla -= EventHandlers.OnInteractingTesla;
            Exiled.Events.Handlers.Player.PickingUpItem -= EventHandlers.OnPickingUpItem;
            Exiled.Events.Handlers.Scp079.GainingLevel -= EventHandlers.OnGainingLevel;
            Exiled.Events.Handlers.Player.EscapingPocketDimension -= EventHandlers.OnEscapingPocketDimension;
            Exiled.Events.Handlers.Player.EnteringPocketDimension -= EventHandlers.OnEnteringPocketDimension;
            Exiled.Events.Handlers.Player.ActivatingWarheadPanel -= EventHandlers.OnActivatingWarheadPanel;
            Exiled.Events.Handlers.Player.TriggeringTesla -= EventHandlers.OnTriggeringTesla;
            Exiled.Events.Handlers.Player.ThrowingItem -= EventHandlers.OnThrowingGrenade;
            Exiled.Events.Handlers.Player.Hurting -= EventHandlers.OnHurting;
            Exiled.Events.Handlers.Player.Dying -= EventHandlers.OnDying;
            Exiled.Events.Handlers.Player.InteractingDoor -= EventHandlers.OnInteractingDoor;
            Exiled.Events.Handlers.Player.InteractingElevator -= EventHandlers.OnInteractingElevator;
            Exiled.Events.Handlers.Player.InteractingLocker -= EventHandlers.OnInteractingLocker;
            Exiled.Events.Handlers.Player.IntercomSpeaking -= EventHandlers.OnIntercomSpeaking;
            Exiled.Events.Handlers.Player.Handcuffing -= EventHandlers.OnHandcuffing;
            Exiled.Events.Handlers.Player.RemovingHandcuffs -= EventHandlers.OnRemovingHandcuffs;
            Exiled.Events.Handlers.Scp106.Teleporting -= EventHandlers.OnTeleporting;
            Exiled.Events.Handlers.Player.DroppingItem -= EventHandlers.OnItemDropped;
            Exiled.Events.Handlers.Player.Verified -= EventHandlers.OnVerified;
            Exiled.Events.Handlers.Player.Destroying -= EventHandlers.OnDestroying;
            Exiled.Events.Handlers.Player.ChangingRole -= EventHandlers.OnChangingRole;
            Exiled.Events.Handlers.Player.ChangingItem -= EventHandlers.OnChangingItem;
            Exiled.Events.Handlers.Scp914.Activating -= EventHandlers.OnActivatingScp914;
            Exiled.Events.Handlers.Scp106.Containing -= EventHandlers.OnContaining;

            EventHandlers = null;
            _harmony.UnpatchAll(_harmony.Id);

            foreach (var coroutine in Coroutines)
                Timing.KillCoroutines(coroutine);

            base.OnDisabled();
        }

        private IEnumerator<float> QueueSender(float delay)
        {
            var builder = new StringBuilder();
            while (true)
            {
                if (GameLogsQueue.Any())
                {
                    foreach (var log in GameLogsQueue)
                    {
                        builder.AppendLine(log);
                    }

                    GameLogsQueue.Clear();
                    Coroutines.Add(Timing.RunCoroutine(SendMessage(builder.ToString(), Config.GameEventsLogsWebhookUrl)));
                    builder.Clear();
                }

                if (CommandLogsQueue.Any())
                {
                    foreach (var log in CommandLogsQueue)
                    {
                        builder.AppendLine(log);
                    }

                    CommandLogsQueue.Clear();
                    Coroutines.Add(Timing.RunCoroutine(SendMessage(builder.ToString(), Config.CommandLogsWebhookUrl)));
                    builder.Clear();
                }

                if (PvPLogsQueue.Any())
                {
                    foreach (var log in PvPLogsQueue)
                    {
                        builder.AppendLine(log);
                    }

                    PvPLogsQueue.Clear();
                    Coroutines.Add(Timing.RunCoroutine(SendMessage(builder.ToString(), Config.PvpEventsLogsWebhookUrl)));
                    builder.Clear();
                }

                yield return Timing.WaitForSeconds(delay);
            }
        }

        private IEnumerator<float> SendMessage(string content, string url)
        {
            var webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
            var uploadHandler = new UploadHandlerRaw(JsonSerializer.Serialize(new Message(content)))
            {
                contentType = "application/json"
            };
            webRequest.uploadHandler = uploadHandler;

            yield return Timing.WaitUntilDone(webRequest.SendWebRequest());

            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Log.Error(
                    $"An error occurred while sending log message: {webRequest.responseCode}\n{webRequest.error}");
            }
        }
    }
}
