using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine.Elements.Space {
    abstract class Dimension {
        abstract Dimension Parse(XElement xml);
    }
}
