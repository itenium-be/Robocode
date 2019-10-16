using Robocode;

namespace Itenium.Robots
{
    public class AdvancedIteniumRobot : AdvancedRobot
    {
        public override void Run()
        {
            while (true)
            {
                Ahead(100); // pixels

                SetAhead(100);
                

            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            Fire(1); // between 0.1 and 3
        }

        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            Back(20);
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            Back(20);
        }
    }
}