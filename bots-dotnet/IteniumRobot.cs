using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

// Reference not found? It's located in where Robocode is installed
// Need to install both robocode-1.9.3.3-setup.jar and robocode.dotnet-1.9.3.3-setup.jar
// ex: c:\robocode\libs\robocode.dll
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


    public class IteniumRobot : Robot
    {
        public override void Run()
        {
            BodyColor = Color.Red;
            GunColor = Color.Black;
            RadarColor = Color.Yellow;
            BulletColor = Color.Green;
            ScanColor = Color.Green;

            var startEnergy = Energy;

            while (true)
            {
                Console.WriteLine("Starting while loop");

                Ahead(100); // pixels


                TurnGunRight(360); // degrees
                Back(100);
                TurnGunRight(360);

                //Energy

                //Velocity

                // Fire(1);


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

        public override void OnBulletHit(BulletHitEvent evnt)
        {
            base.OnBulletHit(evnt);
        }

        public override void OnBulletHitBullet(BulletHitBulletEvent evnt)
        {
            base.OnBulletHitBullet(evnt);
        }

        public override void OnBulletMissed(BulletMissedEvent evnt)
        {
            base.OnBulletMissed(evnt);
        }
    }
}
