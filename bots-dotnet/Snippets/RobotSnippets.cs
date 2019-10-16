namespace dotnet_bots.Snippets
{
    // Calculate the distance between
    // point pt and the segment p1 --> p2.
    //private double FindDistanceToSegment(
    //    PointF pt, PointF p1, PointF p2, out PointF closest)
    //{
    //    float dx = p2.X - p1.X;
    //    float dy = p2.Y - p1.Y;
    //    if ((dx == 0) && (dy == 0))
    //    {
    //        // It's a point not a line segment.
    //        closest = p1;
    //        dx = pt.X - p1.X;
    //        dy = pt.Y - p1.Y;
    //        return Math.Sqrt(dx * dx + dy * dy);
    //    }

    //    // Calculate the t that minimizes the distance.
    //    float t = ((pt.X - p1.X) * dx + (pt.Y - p1.Y) * dy) /
    //              (dx * dx + dy * dy);

    //    // See if this represents one of the segment's
    //    // end points or a point in the middle.
    //    if (t < 0)
    //    {
    //        closest = new PointF(p1.X, p1.Y);
    //        dx = pt.X - p1.X;
    //        dy = pt.Y - p1.Y;
    //    }
    //    else if (t > 1)
    //    {
    //        closest = new PointF(p2.X, p2.Y);
    //        dx = pt.X - p2.X;
    //        dy = pt.Y - p2.Y;
    //    }
    //    else
    //    {
    //        closest = new PointF(p1.X + t * dx, p1.Y + t * dy);
    //        dx = pt.X - closest.X;
    //        dy = pt.Y - closest.Y;
    //    }

    //    return Math.Sqrt(dx * dx + dy * dy);
    //}

    // Calc absolute Bearing between two points
    //private double AbsoluteBearing(double x1, double y1, double x2, double y2)
    //{
    //    double xo = x2 - x1;
    //    double yo = y2 - y1;
    //    double hyp = Point.distance(x1, y1, x2, y2);
    //    double arcSin = Math.toDegrees(Math.Asin(xo / hyp));
    //    double bearing = 0;

    //    if (xo > 0 && yo > 0)
    //    { // both pos: lower-Left
    //        bearing = arcSin;
    //    }
    //    else if (xo < 0 && yo > 0)
    //    { // x neg, y pos: lower-right
    //        bearing = 360 + arcSin; // arcsin is negative here, actuall 360 - ang
    //    }
    //    else if (xo > 0 && yo < 0)
    //    { // x pos, y neg: upper-left
    //        bearing = 180 - arcSin;
    //    }
    //    else if (xo < 0 && yo < 0)
    //    { // both neg: upper-right
    //        bearing = 180 - arcSin; // arcsin is negative here, actually 180 + ang
    //    }

    //    return bearing;
    //}
}