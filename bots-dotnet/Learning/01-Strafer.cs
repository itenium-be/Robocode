using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Robocode;
using Robocode.Util;

namespace Itenium.Robots
{
    public class Strafer : AdvancedRobot
    {
        private void SetupRobot()
        {
            BodyColor = Color.Red;
            GunColor = Color.Black;
            RadarColor = Color.Yellow;
            BulletColor = Color.Green;
            ScanColor = Color.Green;

            IsAdjustGunForRobotTurn = false;
            IsAdjustRadarForGunTurn = false;
            IsAdjustRadarForRobotTurn = false;
        }

        public override void Run()
        {
            SetupRobot();

            while (true)
            {
                TurnRadarLeft(10);
            }
        }

        private readonly Random _rnd = new Random();
        private int _scanDirection = -1;
        private int _moveDirection = -1;
        private int _moveDistance = 20;

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            // Radar: Oscillation
            _scanDirection *= -1;
            SetTurnRadarRight(360 * _scanDirection);



            // Turret: Aiming
            var degrees = NormalizeDegrees(Heading - GunHeading + e.Bearing);
            //Console.WriteLine("Turning:" + degrees);
            SetTurnGunRight(degrees);



            // Firing!? (Power between 0.1 and 3)
            if (Utils.IsNear(GunHeat, 0) && Energy > 3 && GunTurnRemaining < 10)
            {
                var firePower = Math.Min(400 / e.Distance, 3);
                //Console.WriteLine("Firing:" + firePower);
                SetFire(firePower);

                // TODO: Well... Calculate where they're going!
                //double bulletSpeed = 20 - firePower * 3;
                //long time = (long)(e.Distance / bulletSpeed);
            }

            

            // Moving: Squaring Off
            double turnDegrees = e.Bearing + 90;
            // if (e.Distance > 70)
            {
                turnDegrees -= 15 * _moveDirection;
            }
            SetTurnRight(turnDegrees);



            // Moving: Strafing
            if (TurnRemaining < 5)
            {
                // Change direction when hitting wall/robot
                //if (Utils.IsNear(Velocity, 0))
                // Change direction based on time

                if (Time % 20 == 0)
                //if (Utils.IsNear(DistanceRemaining, 0))
                {
                    _moveDirection *= -1;
                    _moveDistance = 300; // + _rnd.Next(500);

                    // TODO: Math.max(distanceToWall)
                    SetAhead(_moveDistance * _moveDirection);
                }
            }
        }

        private static double NormalizeDegrees(double degrees)
        {
            while (degrees > 180) degrees -= 360;
            while (degrees < -180) degrees += 360;
            return degrees;
        }

        //public override void OnHitByBullet(HitByBulletEvent evnt)
        //{
        //    Back(20);
        //}

        //public override void OnHitWall(HitWallEvent evnt)
        //{
        //    Back(20);
        //}

        //public override void OnBulletHit(BulletHitEvent evnt)
        //{
        //    base.OnBulletHit(evnt);
        //}

        //public override void OnBulletHitBullet(BulletHitBulletEvent evnt)
        //{
        //    base.OnBulletHitBullet(evnt);
        //}

        //public override void OnBulletMissed(BulletMissedEvent evnt)
        //{
        //    base.OnBulletMissed(evnt);
        //}
    }
}
