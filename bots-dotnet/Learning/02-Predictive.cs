using System;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using Robocode;
using Robocode.Util;
using SampleCs;

namespace Itenium.Robots
{
    /// <summary>
    /// Ideas:
    /// - Once out of Energy: Change mode: RUN!!!!
    /// - Track enemy behavior
    ///     - Has fired (Compare prev/cur Energy. Rules.MaxBulletPower)
    /// - Draw lines movement, turret etc for debugging purposes
    /// </summary>
    public class Predictive : AdvancedRobot
    {
        private readonly Enemy _enemy;

        public Predictive()
        {
            _enemy = new Enemy(this);
        }

        private void SetupRobot()
        {
            BodyColor = Color.Red;
            GunColor = Color.Black;
            RadarColor = Color.Yellow;
            BulletColor = Color.White;
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
                EscapeTheWalls();

                // Scan for enemies
                TurnRadarLeft(10);
            }
        }

        private readonly Random _rnd = new Random();
        private int _scanDirection = -1;
        private int _moveDirection = -1;
        private const int MoveDistance = 300;

        public override void OnScannedRobot(ScannedRobotEvent e)
        {
            // Track enemy
            _enemy.Update(e);


            // Radar: Oscillation
            _scanDirection *= -1;
            SetTurnRadarRight(360 * _scanDirection);



            // Turret: Aiming
            //var degrees = NormalizeDegrees(Heading - GunHeading + e.Bearing);
            ////Console.WriteLine("Turning:" + degrees);
            //SetTurnGunRight(degrees);

            var firePower = Math.Min(400 / e.Distance, 3);
            //Console.WriteLine("Firing:" + firePower);
            double bulletSpeed = 20 - firePower * 3;
            long time = (long)(e.Distance / bulletSpeed);
            var expectedLocationAngle = _enemy.GetFutureAngle(time);
            var turn = NormalizeDegrees(expectedLocationAngle - GunHeading);
            SetTurnGunRight(turn);


            // Firing!? (Power between 0.1 and 3)
            if (Utils.IsNear(GunHeat, 0) && Energy > 3 && GunTurnRemaining < 10)
            {
                SetFire(firePower);
            }



            // Trying to avoid walls
            if (_wallEscapeGoingOn)
            {
                return;
            }



            // Moving: Squaring Off
            double turnDegrees = e.Bearing + 90;
            if (e.Distance > 70)
            {
                // Moving: Closing in
                turnDegrees -= 35 * _moveDirection;
            }
            SetTurnRight(turnDegrees);



            // Moving: Strafing
            if (TurnRemaining < 5)
            {
                // Change direction when hitting wall/robot
                //if (Utils.IsNear(Velocity, 0))

                // Change direction based on time
                if (Time % 50 == 0)
                //if (Utils.IsNear(DistanceRemaining, 0))
                {
                    // MaxVelocity = Rules.MAX_VELOCITY; // 8
                    _moveDirection *= -1;
                    SetAhead(MoveDistance * _moveDirection);
                }
            }
        }

        

        #region Wall Escaping
        private bool _wallEscapeGoingOn = false;

        private bool IsCloseToWall()
        {
            int wallMargin = 60;
            return (
                // we're too close to the left wall
                X <= wallMargin ||
                // or we're too close to the right wall
                X >= BattleFieldWidth - wallMargin ||
                // or we're too close to the bottom wall
                Y <= wallMargin ||
                // or we're too close to the top wall
                Y >= BattleFieldHeight - wallMargin
            );
        }

        private void EscapeTheWalls()
        {
            if (IsCloseToWall() && !_wallEscapeGoingOn)
            {
                _wallEscapeGoingOn = true;

                _moveDirection *= -1;
                SetAhead(MoveDistance * _moveDirection);
                Execute();
            }
            else if (Utils.IsNear(Velocity, 0) && _wallEscapeGoingOn)
            {
                _wallEscapeGoingOn = false;
            }
        }
        #endregion


        private static double NormalizeDegrees(double degrees)
        {
            while (degrees > 180) degrees -= 360;
            while (degrees < -180) degrees += 360;
            return degrees;
        }

        public override void OnRoundEnded(RoundEndedEvent e)
        {
            _wallEscapeGoingOn = false;
            _enemy.Clear();
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
