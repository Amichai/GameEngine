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

        protected void setImageDimensions(Image finalImage) {
            App.Current.Dispatcher.BeginInvoke((Action)(() => {    
                if (this.Width.HasValue) {
                    finalImage.Width = this.Width.Value;
                }
                if (this.Height.HasValue) {
                    finalImage.Height = this.Height.Value;
                }
            }));
        }
    }
}
