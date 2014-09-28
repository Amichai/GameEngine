using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine {
    class Board {
        public double? Width { get; set; }
        public double? Height { get; set; }
        public CoordinateSystemOrigin Origin { get; set; }
        public VisibleArea VisibleArea { get; set; }

        internal static Board Parse(XElement xElement) {
            Board toReturn = new Board();
            if (xElement.HasAttribute("Width")) {
                toReturn.Width = xElement.AttributeDouble("Width");
            }

            if (xElement.HasAttribute("Height")) {
                toReturn.Height = xElement.AttributeDouble("Height");
            }

            if (xElement.HasAttribute("Origin")) {
                toReturn.Origin = xElement.AttributeEnum<CoordinateSystemOrigin>("Origin");
            }

            toReturn.VisibleArea = VisibleArea.Parse(xElement.Element("VisibleArea"));
            return toReturn;
        }
    }

    public enum CoordinateSystemOrigin { BottomLeft, TopLeft };
    public enum CoordinateSystem { Board, Visible };
}
