using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine.Elements {
    class StaticImageAsset : Asset {
        public string Path { get; set; }

        internal static StaticImageAsset Parse(XElement asset) {
            var toReturn = new StaticImageAsset();
            toReturn.Path = asset.Attribute("Path").Value;
            return toReturn;
        }
    }
}
