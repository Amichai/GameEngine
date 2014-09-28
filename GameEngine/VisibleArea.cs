using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine {
    class VisibleArea {
        public CoordinateSystemOrigin Origin { get; set; }
        public Vec2 Position { get; set; }
        public Vec2 Velocity { get; set; }
        public Vec2 Acceleration { get; set; }


        internal static VisibleArea Parse(XElement xElement) {
            VisibleArea toReturn = new VisibleArea();
            if (xElement.Attribute("Position") != null) {
                toReturn.Position = Vec2.Parse(xElement.Attribute("Position").Value);
            }
            if (xElement.Attribute("Velocity") != null) {
                toReturn.Velocity = Vec2.Parse(xElement.Attribute("Velocity").Value);
            } 
            
            if (xElement.Attribute("Acceleration") != null) {
                toReturn.Acceleration = Vec2.Parse(xElement.Attribute("Acceleration").Value);
            }
            return toReturn;
        }

        internal void Update(TimeSpan sinceLastUpdate) {
            var p = this.Position;
            var v = this.Velocity;
            PhysicsUtil.Update(ref p, ref v, this.Acceleration, sinceLastUpdate.TotalSeconds);
            this.Position = p;
            this.Velocity = v;
        }
    }
}
