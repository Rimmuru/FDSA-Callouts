using LSPD_First_Response.Mod.Callouts;
using Rage;
using System.Drawing;

namespace FDSA.Callouts
{
    [CalloutInfo("BrushFire", CalloutProbability.VeryLow)]
    public class BrushFires : Callout
    {
        private Vector3 Spawn = new Vector3(424, 2535, 33);
        private Vector3 CallerSpawn = new Vector3(403, 3539, 260);

        private Ped Caller;
        private Blip CallerBlip;
        public override bool OnBeforeCalloutDisplayed()
        {
            ShowCalloutAreaBlipBeforeAccepting(Spawn, 12f);

            CalloutMessage = "BrushFire";

            CalloutPosition = Spawn;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            CallerBlip = Caller.AttachBlip();
            CallerBlip.Color = Color.AliceBlue;

            Caller = new Ped(CallerSpawn);

            Game.DisplayNotification("mp_medal_gold", "mp_medal_gold", "Dispatch", "", "~w~We have reports of a bush fire, please respond code 3");


            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            //First Line
            base.OnCalloutNotAccepted();

            if (Caller.Exists())
                Caller.Delete();
        }

        public override void Process()
        {
            //First Line
            base.Process();


        }

        public override void End()
        {
            //First Line
            base.End();
        }
    }
}
