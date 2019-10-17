using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Robocode;

namespace Itenium.Robots
{
    public class Enemy
    {
        private readonly IList<ScannedRobotEvent> _events = new List<ScannedRobotEvent>();
        private readonly AdvancedRobot _robot;
        private ScannedRobotEvent Current => _events.Last();

        public Enemy(AdvancedRobot robot)
        {
            _robot = robot;
        }

        public void Update(ScannedRobotEvent e)
        {
            _events.Add(e);
        }

        public void Clear()
        {
            _events.Clear();
        }

        public double GetFutureAngle(long time)
        {
            var loc = GetFuture(time);
            double absDeg = AbsoluteBearing(_robot.X, _robot.Y, loc.X, loc.Y);
            return absDeg;
        }

        // http://mark.random-article.com/weber/java/ch5/lab4.html
        private PointF GetFuture(long time)
        {
            double absBearingDeg = (_robot.Heading + Current.Bearing);
            if (absBearingDeg < 0) absBearingDeg += 360;

            var x = _robot.X + Math.Sin(ToRadians(absBearingDeg)) * Current.Distance;
            var y = _robot.Y + Math.Cos(ToRadians(absBearingDeg)) * Current.Distance;

            x += Math.Sin(ToRadians(Current.Heading)) * Current.Velocity * time;
            y += Math.Cos(ToRadians(Current.Heading)) * Current.Velocity * time;

            return new PointF((float)x, (float)y);
        }

        private double ToRadians(double angle)
        {
            return (Math.PI / 180) * angle;
        }

        private double ToDegrees(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private static double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
        }

        private double AbsoluteBearing(double x1, double y1, double x2, double y2)
        {
            double xo = x2 - x1;
            double yo = y2 - y1;
            double hyp = GetDistance(x1, y1, x2, y2);
            double arcSin = ToDegrees(Math.Asin(xo / hyp));
            double bearing = 0;

            if (xo > 0 && yo > 0)
            { // both pos: lower-Left
                bearing = arcSin;
            }
            else if (xo < 0 && yo > 0)
            { // x neg, y pos: lower-right
                bearing = 360 + arcSin; // arcsin is negative here, actuall 360 - ang
            }
            else if (xo > 0 && yo < 0)
            { // x pos, y neg: upper-left
                bearing = 180 - arcSin;
            }
            else if (xo < 0 && yo < 0)
            { // both neg: upper-right
                bearing = 180 - arcSin; // arcsin is negative here, actually 180 + ang
            }

            return bearing;
        }
    }
}