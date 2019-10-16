using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace BATTERYLOW
{
    public class Chappie : Robot
    {
        public RobotStatus Status;

        public List<Point> EnemyCoordinates = new List<Point>();
        //public bool HitRobot;

        public long LastHit { get; set; }

        public override void Run()
        {
            SetColors(Color.Firebrick, Color.Bisque, Color.Crimson);

            // Here we turn the robot to point upwards, and move the gun 90 degrees
            TurnLeft(Heading - 90);
            TurnGunRight(90);

            this.Ahead(5000);

            // Infinite loop making sure this robot runs till the end of the battle round
            while (true)
            {
                var strat = StrategyFactory.Create(this);
                strat.Execute(this);
                //HitRobot = false;
            }

            // Our robot will move along the borders of the battle field
            // by repeating the above two statements.
        }


        // Robot event handler, when the robot sees another robot
        public override void OnScannedRobot(ScannedRobotEvent e)
        {

            if (e.Velocity < 2 && e.Distance < 200)
                FireBullet(Rules.MAX_BULLET_POWER);
            else
                Fire(1);

            Console.WriteLine($"Distance: {e.Distance}");

            GetEnemyCoordinates(e);
        }

        public override void OnBulletHit(BulletHitEvent evnt)
        {
            base.OnBulletHit(evnt);
            LastHit = evnt.Time;
        }

        //public override void OnHitRobot(HitRobotEvent evnt)
        //{
        //    base.OnHitRobot(evnt);
        //    HitRobot = true;
        //}

        private void GetEnemyCoordinates(ScannedRobotEvent e)
        {
            double angleToEnemy = e.Bearing;

            //Console.WriteLine($"RobosStatus: {Convert.ToInt32(Math.Round(Status.X))}:{Convert.ToInt32(Math.Round(Status.Y))}");

            // Calculate the angle to the scanned robot
            double angle = ToRadians(Status.Heading + angleToEnemy % 360);

            // Calculate the coordinates of the robot
            double enemyX = Status.X + Math.Sin(angle) * e.Distance;
            double enemyY = Status.Y + Math.Cos(angle) * e.Distance;
            var newCoordinate = new Point(Convert.ToInt32(Math.Round(enemyX, 0)),
                Convert.ToInt32(Math.Round(enemyY, 0)));

            EnemyCoordinates.Add(newCoordinate);

            //Console.WriteLine($"New Enemy Coordinate: {newCoordinate.X}:{newCoordinate.Y}");
            //Console.WriteLine($"Enemy Coordinates: {string.Join(Environment.NewLine, EnemyCoordinates.Select(p => $"{p.X.ToString().PadLeft(3)}:{p.Y.ToString().PadLeft(3)}"))}");
        }

        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }

        public override void OnStatus(StatusEvent e)
        {
            base.OnStatus(e);
            this.Status = e.Status;
        }
    }

    public class StrategyFactory
    {
        public static IStrategy Create(Chappie robot)
        {
            if (robot.EnemyCoordinates.Count > 20 && (robot.Time - robot.LastHit) > 50)
            {
                return new HugCornersStrategy();
            }

            return new BullsEyeStrategy();
        }
    }

    public interface IStrategy
    {
        void Execute(Chappie robot);
    }

    public class HugCornersStrategy : IStrategy
    {
        public void Execute(Chappie robot)
        {
            robot.TurnRight(90);
            Console.WriteLine($"Heading: {robot.Heading}");

            var driveAheadDistance = 5000;
            if (robot.IsDrivingDown() || robot.IsDrivingUp())
                driveAheadDistance = Convert.ToInt32(Math.Round(robot.BattleFieldHeight, 0));
            else if (robot.IsDrivingLeft() || robot.IsDrivingRight())
                driveAheadDistance = Convert.ToInt32(Math.Round(robot.BattleFieldWidth, 0));

            Console.WriteLine($"Driving ahead: {driveAheadDistance}");
            robot.Ahead(driveAheadDistance);
        }
    }

    public class BullsEyeStrategy : IStrategy
    {
        public void Execute(Chappie robot)
        {
            robot.TurnRight(90);
            Console.WriteLine($"Heading: {robot.Heading}");
            var driveAheadDistance = robot.BattleFieldWidth;
            if (robot.IsDrivingDown())
                driveAheadDistance = (robot.Status.Y - (robot.EnemyCoordinates.Any() ? robot.EnemyCoordinates.Min(p => p.Y) : 0) + 100);
            else if (robot.IsDrivingLeft())
                driveAheadDistance = (robot.Status.X - (robot.EnemyCoordinates.Any() ? robot.EnemyCoordinates.Min(p => p.X) : 0) + 100);
            else if (robot.IsDrivingUp())
                driveAheadDistance = ((robot.EnemyCoordinates.Any() ? robot.EnemyCoordinates.Max(p => p.Y) : 0) - robot.Status.Y) + 100;
            else if (robot.IsDrivingRight())
                driveAheadDistance = ((robot.EnemyCoordinates.Any() ? robot.EnemyCoordinates.Max(p => p.X) : 0) - robot.Status.X) + 100;

            Console.WriteLine($"Driving ahead: {driveAheadDistance}");

            robot.Ahead(driveAheadDistance);
        }
    }

    public class CircledStrategy : IStrategy
    {
        public void Execute(Chappie robot)
        {
        }
    }

    public static class RobotExtensions
    {
        public static bool IsDrivingDown(this Robot robot) => robot.Heading > 90 && robot.Heading < 270;
        public static bool IsDrivingUp(this Robot robot) => robot.Heading > 0 && robot.Heading < 90 || robot.Heading > 270;
        public static bool IsDrivingRight(this Robot robot) => robot.Heading < 180;
        public static bool IsDrivingLeft(this Robot robot) => robot.Heading > 180;
    }
}

