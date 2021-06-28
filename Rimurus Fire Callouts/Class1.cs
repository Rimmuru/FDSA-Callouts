using Rage;
using LSPD_First_Response.Mod.API;
using FDSA.Callouts;

namespace FDSA
{
    public class EntryPoint : Plugin
    {
        /// <summary>
        /// This method is run when the plugin is first initialized.
        /// </summary>
        public override void Initialize()
        {
            //Subscribe to the OnOnDutyStateChanged event, so we don't register our callouts unless the player is on duty.
            Functions.OnOnDutyStateChanged += OnDutyStateChangedEvent;

            //Logging is a great tool, so we log to make sure the plugins loaded.
            Game.LogTrivial("FDSA Initialized");
            GameFiber.Wait(6000);
            Game.DisplayNotification("mp_medal_gold", "mp_medal_gold", "FDSA Callouts", "~y~v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString() + " ~p~by Rimuru", "~b~Has been ~g~Loaded");

        }

        /// <summary>
        /// Called when the OnOnDutyStateChanged event is raised.
        /// </summary>
        /// <param name="onDuty"></param>
        public void OnDutyStateChangedEvent(bool onDuty)
        {
            //If the player is going on duty, register the callout.
            if (onDuty)           
                RegisterCallouts();    
        }
        public void RegisterCallouts()
        {
            Functions.RegisterCallout(typeof(BrushFires));
         
        }
        /// <summary>
        /// Called before the plugin is unloaded.
        /// </summary>
        public override void Finally()
        {
            Functions.StopCurrentCallout();
        }
    }
}