using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GameEngine.Elements.Assets {
    class SpriteSheet : Asset {
        public string Path { get; set; }
        
        ///Get the correct image from the sprite sheet
        ///
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
