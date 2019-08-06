using System;
using System.Collections.Generic;

namespace CodeJam
{
    class NoPointSegment
    {
        public string Intersection(int[] seg1, int[] seg2)
        {
            Coordinate coordinate1 = new Coordinate(seg1[0],seg1[1]);
            Coordinate coordinate2 = new Coordinate(seg1[2],seg1[3]);
            Segment segment1 = new Segment(coordinate1,coordinate2);
            coordinate1.X = seg2[0];
            coordinate1.Y = seg2[1];
            coordinate2.X = seg2[2];
            coordinate2.Y = seg2[3];
            Segment segment2 = new Segment(coordinate1, coordinate2);
            return segment1._intersection.GetIntersection(segment2);
        }

        #region Testing code Do not change
        //public static void Main(String[] args)
        //{
        //    string input = Console.ReadLine();
        //    NoPointSegment solver = new NoPointSegment();
        //    do
        //    {
        //        var segments = input.Split('|');
        //        var segParts = segments[0].Split(',');
        //        var seg1 = new int[4] { int.Parse(segParts[0]), int.Parse(segParts[1]), int.Parse(segParts[2]), int.Parse(segParts[3]) };
        //        segParts = segments[1].Split(',');
        //        var seg2 = new int[4] { int.Parse(segParts[0]), int.Parse(segParts[1]), int.Parse(segParts[2]), int.Parse(segParts[3]) };
        //        Console.WriteLine(solver.Intersection(seg1, seg2));
        //        input = Console.ReadLine();
        //    } while (input != "-1");
        //}
        #endregion
    }

    class Segment
    {
        public Coordinate _coordinate1;
        public Coordinate _coordinate2;
        public Axis _isParallelTo;
        public Intersection _intersection;
        public int _minX;
        public int _maxX;
        public int _minY;
        public int _maxY;

        public Segment(Coordinate c1,Coordinate c2)
        {
            _coordinate1 = c1;
            _coordinate2 = c2;
            _minX = (c1.X <= c2.X) ? c1.X : c2.X;
            _minY = (c1.Y <= c2.Y) ? c1.Y : c2.Y;
            _maxX = (c1.X >= c2.X) ? c1.X : c2.X;
            _maxY = (c1.Y >= c2.Y) ? c1.Y : c2.Y;
            _isParallelTo = GetParallelAxis(c1,c2);
            _intersection = new Intersection(this);
        }

        Axis GetParallelAxis(Coordinate c1,Coordinate c2)
        {
            if (c1.X == c2.X && c1.Y == c2.Y)
            {
                return Axis.P;
            }
            if (c1.X == c2.X)
                return Axis.Y;
            else
                return Axis.X;
        }
    }

    interface IIntersection
    {
        string Name();

        bool CheckIntersection(Segment segment, Segment segment1);
    }

    class Intersection
    {
        List<IIntersection> _intersectionTypes = new List<IIntersection>()
        {
            new PointIntersection(),
            new SegmentIntersection()
        };
        private Segment segment;

        public Intersection(Segment segment)
        {
            this.segment = segment;
        }

        public string GetIntersection(Segment segment)
        {
            foreach(IIntersection _intersection in _intersectionTypes)
            {
                if (_intersection.CheckIntersection(this.segment,segment))
                    return _intersection.Name();
            }
            return "NO";
        }
    }

    class PointIntersection : IIntersection
    {
        public string Name() { return "POINT"; }

        public bool CheckIntersection(Segment s1, Segment s2)
        {
            if (s1._isParallelTo == s2._isParallelTo)
            {
                return CheckPointIntersectionIfParallel(s1,s2);
            }
            else
            {
                return CheckPointIntersectionIfPerpendicular(s1, s2);
            }

        }

        private bool CheckPointIntersectionIfParallel(Segment s1, Segment s2)
        {
            if (s1._isParallelTo == Axis.X && s1._maxY==s2._maxY)
            {
                if (s1._maxX == s2._minX || s1._minX == s2._maxX)
                    return true;
                return false;
            }
            else if(s1._isParallelTo == Axis.Y && s1._maxX == s2._maxX)
            {
                if (s1._maxY == s2._minY || s1._minY == s2._maxY)
                    return true;
                return false;
            }
            else if(s1._isParallelTo == Axis.P && s1._minX==s2._minX && s1._minY==s2._minY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool CheckPointIntersectionIfPerpendicular(Segment s1, Segment s2)
        {
            if ((s1._isParallelTo == Axis.X 
                && s1._minX <= s2._maxX && s1._maxX >= s2._maxX && s2._minY <= s1._minY && s2._maxY >= s1._minY) 
                || (s1._isParallelTo == Axis.Y 
                && s1._minY <= s2._maxY && s1._maxY >= s2._maxY && s2._minX <= s1._minX && s2._maxX >= s1._minX))
            {
                return true;
            }
            else
            {
                if (s1._isParallelTo == Axis.P)
                {
                    if (s2._isParallelTo == Axis.X)
                    {
                        return (s2._minX <= s1._minX && s1._minX <= s2._maxX) ? true : false;
                    }
                    else
                    {
                        return (s2._minX <= s1._minX && s1._minX <= s2._maxX) ? true : false;
                    }
                }
                if (s2._isParallelTo == Axis.P)
                {
                    if (s1._isParallelTo == Axis.X)
                    {
                        return (s1._minX <= s2._minX && s2._minX <= s1._maxX) ? true : false;
                    }
                    else
                    {
                        return (s1._minX <= s2._minX && s2._minX <= s1._maxX) ? true : false;
                    }
                }
                return false;
            }
        }
    }

    class SegmentIntersection : IIntersection
    {
        public string Name() { return "SEGMENT"; }

        public bool CheckIntersection(Segment s1, Segment s2)
        {
            if (s1._isParallelTo == s2._isParallelTo)
            {
                if (s1._isParallelTo == Axis.P || s2._isParallelTo == Axis.P)
                    return false;
                if (s1._isParallelTo == Axis.X && s1._maxY==s2._maxY)
                {
                    if (s1._minX <= s2._maxX && s1._maxX >= s2._minX || s2._minX <= s1._maxX && s2._maxX >= s1._minX)
                        return true;
                    return false;
                }
                else if(s1._isParallelTo == Axis.Y && s1._maxX == s2._maxX)
                {
                    if (s1._minY <= s2._maxY && s1._maxY >= s2._minY || s2._minY <= s1._maxY && s2._maxY >= s1._minY)
                        return true;
                    return false;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }

    class Coordinate
    {
        public Coordinate(int x , int y)
        {
            X = x;
            Y = y;
        }

        public int X;
        public int Y;
    }

    enum Axis
    {
        X,
        Y,
        P//for point(parallel to both axis)
    }

}
