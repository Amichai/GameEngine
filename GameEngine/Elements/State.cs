using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine.Elements {
    class State {
        public State() {
            this.Position = Vec2.Zero;
            this.Velocity = Vec2.Zero;
            this.Acceleration = Vec2.Zero;
        }
        public Vec2 Position { get; set; }
        public Vec2 Velocity { get; set; }
        public Vec2 Acceleration { get; set; }


        internal static State Parse(XElement xml) {
            State toReturn = new State();
            if (xml.Attribute("Position") != null) {
                toReturn.Position = Vec2.Parse(xml.Attribute("Position").Value);
            }

            if (xml.Attribute("Velocity") != null) {
                toReturn.Velocity = Vec2.Parse(xml.Attribute("Velocity").Value);
            }

            if (xml.Attribute("Acceleration") != null) {
                toReturn.Acceleration = Vec2.Parse(xml.Attribute("Acceleration").Value);
            }
            return toReturn;
        }

        internal void Update(TimeSpan dt) {
            var p = this.Position;
            var v = this.Velocity;
            PhysicsUtil.Update(ref p, ref v, this.Acceleration, dt.TotalSeconds);
            this.Position = p;
            this.Velocity = v;
        }
    }
}
