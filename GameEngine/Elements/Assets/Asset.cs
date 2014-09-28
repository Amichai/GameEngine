using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GameEngine.Elements {
    abstract class Asset {
        public double? Width { get; set; }
        public double? Height { get; set; }

        public abstract Image GetImage();
    }
}
