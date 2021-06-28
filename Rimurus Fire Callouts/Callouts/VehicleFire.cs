using LSPD_First_Response.Mod.Callouts;
using Rage;
using System.Drawing;

namespace FDSA.Callouts
{
    [CalloutInfo("VehicleFire", CalloutProbability.VeryLow)]
    public class VehicleFire : Callout
    {
        private Vector3 Spawn;

        private Vehicle Car;
        private Ped Driver;
        private Blip DriverBlip;
        public override bool OnBeforeCalloutDisplayed()
        {
            Spawn = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(1000f));

            ShowCalloutAreaBlipBeforeAccepting(Spawn, 12f);
            AddMinimumDistanceCheck(5f, Spawn);

            CalloutMessage = "VehicleFire";
            CalloutPosition = Spawn;

            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {//Placeholder vehicle
            Car = new Vehicle("Phantom", Spawn);

            Driver = Car.CreateRandomDriver();
            Driver.IsPersistent = true;
            Driver.BlockPermanentEvents = true;

            Driver.Tasks.LeaveVehicle(Car, LeaveVehicleFlags.WarpOut);
           
            DriverBlip = Driver.AttachBlip();
            DriverBlip.Color = Color.AliceBlue;

            Game.DisplayNotification("mp_medal_gold", "mp_medal_gold", "Dispatch", "", "~w~We have reports of a vehicle fire respond code 3");

            return base.OnCalloutAccepted();
        }

        public override void OnCalloutNotAccepted()
        {
            //First Line
            base.OnCalloutNotAccepted();

            if (Driver.Exists())
                Driver.Delete();
        }

        public override void Process()
        {
            //First Line
            base.Process();

            if (Vector3.Distance(Game.LocalPlayer.Character.Position, Driver.Position) < 16.0)
                Driver.TravelDistanceTo(Spawn);
        }

        public override void End()
        {
            //First Line
            base.End();
        }
    }
}
