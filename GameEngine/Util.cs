using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine {
    static class Util {
        public static double AttributeDouble(this XElement xml, string attribute) {
            return double.Parse(xml.Attribute(attribute).Value);
        }

        public static bool HasAttribute(this XElement xml, string attribute) {
            return xml.Attribute(attribute) != null;    
        }

        public static T AttributeEnum<T>(this XElement xml, string attribute) {
            return (T)Enum.Parse(typeof(T),
                    xml.Attribute(attribute).Value);
        }

        public static int Round(this double val) {
            return (int)Math.Round(val);
        }
    }
}
