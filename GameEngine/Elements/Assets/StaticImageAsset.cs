using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace GameEngine.Elements {
    class StaticImageAsset : Asset {
        public string Path { get; set; }

        internal static StaticImageAsset Parse(XElement asset) {
            var toReturn = new StaticImageAsset();
            toReturn.Path = asset.Attribute("Path").Value;
            if (asset.HasAttribute("Width")) {
                toReturn.Width = asset.AttributeDouble("Width");
            }

            if (asset.HasAttribute("Height")) {
                toReturn.Height = asset.AttributeDouble("Height");
            }
            return toReturn;

        }

        public override Image GetImage() {
            Image finalImage = new Image();
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(Path);
            img.EndInit();
            finalImage.Source = img;
            return finalImage;
        }
    }
}
