using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Robocode;
using Robocode.Util;

namespace Itenium.Robots
{
    public class TheDuck : Robot
    {
        private bool PanicMode => Energy <= 3;
        private bool AlertMode => Energy < 15;

        private readonly Random _rnd = new Random();
        private long _timeLastScanned = 0;
        private bool _ahead = true;
        private bool _left = true;

        public override void Run()
        {
            PrepareTheDuck();
            Scan();

            while (true)
            {
                //Console.WriteLine($"RoundNum={Time}");

                var timePassed = Time - _timeLastScanned;
                if (timePassed > 100)
                {
                    TurnLeft(10);
                    continue;
                }

                if (timePassed > 30)
                {
                    int min = 35;
                    int max = 65;
                    if (_left)
                    {
                        TurnLeft(_rnd.Next(min, max));
                    }
                    else
                    {
                        TurnRight(_rnd.Next(min, max));
                    }
                }
                else
                {
                    int speed = 100;
                    if (_ahead)
                    {
                        Ahead(speed);
                    }
                    else
                    {
                        Back(speed);
                    }
                }
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            _timeLastScanned = Time;
            SmartFire(e.Distance);
        }

        public override void OnHitWall(HitWallEvent e)
        {
            //Back(100);
            if (!_ahead)
            {
                _ahead = true;
            }
            TurnLeft(90);
        }

        public override void OnHitByBullet(HitByBulletEvent e)
        {
            //TurnLeft(25);
            if (_rnd.Next(100) < 10)
            {
                _ahead = !_ahead;
            }
            //_left = !_left;
        }

        

        //public override void OnBulletHit(BulletHitEvent e)
        //{
        //    // Remember the location of the fire and pick TRBL with most hits and only do that part?
        //    base.OnBulletHit(e);
        //}

        //public override void OnBulletHitBullet(BulletHitBulletEvent e)
        //{
        //    base.OnBulletHitBullet(e);
        //}

        //public override void OnBulletMissed(BulletMissedEvent e)
        //{
        //    base.OnBulletMissed(e);
        //}


        private void SmartFire(double distance)
        {
            // power: between 0.1 and 3

            if (PanicMode || GunHeat > 0)
                return;

            double power;
            if (AlertMode)
                power = 1;

            else if (distance > 500)
                power = 1;

            else if (distance < 300)
                power = 3;

            else
            {
                power = 2;
            }

            Fire(power);
            Console.WriteLine($"Shot d={Math.Floor(distance)} with p={power} (energy={Energy})");
        }

        #region Cosmetics
        private void PrepareTheDuck()
        {
            BodyColor = Color.Yellow;
            GunColor = Color.Black;
            RadarColor = Color.Yellow;
            BulletColor = Color.Green;
            ScanColor = Color.Green;
        }
        #endregion
    }
}
