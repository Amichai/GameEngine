using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine {
    class PhysicsUtil {
        public static void Update(ref Vec2 position, ref Vec2 velocity, Vec2 acceleration, double dt) {
            velocity = velocity + acceleration * dt;
            position = position + velocity * dt;
        }


    }
}
