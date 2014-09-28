using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace GameEngine.Elements.Space {
    class RectangleDimension : Dimension {
        public static RectangleDimension Parse(XElement xml) {
            RectangleDimension toReturn = new RectangleDimension();
            if (xml.HasAttribute("Width")) {
                toReturn.Width = xml.AttributeDouble("Width");
            }
            if (xml.HasAttribute("Height")) {
                toReturn.Height = xml.AttributeDouble("Height");
            }
            return toReturn;
        }

        public double Width { get; set; }
        public double Height { get; set; }
    }
}
