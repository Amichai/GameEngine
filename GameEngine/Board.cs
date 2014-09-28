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
            if (xElement.Attribute("Width") != null) {
                toReturn.Width = double.Parse(xElement.Attribute("Width").Value);
            }

            if (xElement.Attribute("Height") != null) {
                toReturn.Height = double.Parse(xElement.Attribute("Height").Value);
            }

            if (xElement.Attribute("Origin") != null) {
                toReturn.Origin = (CoordinateSystemOrigin)Enum.Parse(typeof(CoordinateSystemOrigin),
                    xElement.Attribute("Origin").Value);
            }

            toReturn.VisibleArea = VisibleArea.Parse(xElement.Element("VisibleArea"));
            return toReturn;
        }
    }

    public enum CoordinateSystemOrigin { BottomLeft, TopLeft };
    public enum CoordinateSystem { Board, Visible };
}
