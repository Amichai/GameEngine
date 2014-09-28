using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    [DebuggerDisplay("{X},{Y}")]
    struct Vec2 {
        public double X { get; set; }
        public double Y { get; set; }

        public Vec2(double x, double y) : this() {
            this.X = x;
            this.Y = y;
        }

        public static Vec2 operator*(Vec2 a, double v){
            return new Vec2(a.X * v, a.Y * v);
        }

        public static Vec2 operator +(Vec2 a, Vec2 b) {
            return new Vec2(a.X + b.X, a.Y + b.Y);
        }

        internal static Vec2 Parse(string p) {
            var a = p.Split(',');
            if (a.Count() != 2) {
                throw new ArgumentException("Input string must be formatted as 'X,Y'");
            }
            return new Vec2() {
                X = double.Parse(a[0]),
                Y = double.Parse(a[1])
            };
        }

        public static Vec2 Zero = new Vec2(0, 0);
    }
}
