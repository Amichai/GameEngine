using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace GameEngine.Elements.Assets {
    class SpriteSheet : Asset {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private string _Path;
        public string Path {
            get { return _Path; }
            private set {
                _Path = value;
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(Path);
                img.EndInit();
                
                this.rowCount = (int)Math.Round(img.PixelHeight / this.ClipHeight);
                this.colCount = (int)Math.Round(img.PixelWidth / this.ClipWidth);
                this.baseImage = img;
            }
        }

        private BitmapImage baseImage;
        private int colCount, rowCount;
        private int subImageCount {
            get {
                return colCount * rowCount;
            }
        }

        public double ClipWidth { get; private set; }
        public double ClipHeight { get; private set; }
        private double _FrameRate;
        public double FrameRate {
            get {
                return _FrameRate;
            }
            private set {
                _FrameRate = value;
                this.frameSpan = TimeSpan.FromSeconds(1.0 / this.FrameRate);
            }
        }

        internal static SpriteSheet Parse(XElement asset) {
            var toReturn = new SpriteSheet();
            if (asset.HasAttribute("Width")) {
                toReturn.Width = asset.AttributeDouble("Width");
            }
            if (asset.HasAttribute("Height")) {
                toReturn.Height = asset.AttributeDouble("Height");
            }
            if (asset.HasAttribute("ClipWidth")) {
                toReturn.ClipWidth = asset.AttributeDouble("ClipWidth");
            }
            if (asset.HasAttribute("ClipHeight")) {
                toReturn.ClipHeight = asset.AttributeDouble("ClipHeight");
            }
            if (asset.HasAttribute("FrameRate")) {
                toReturn.FrameRate = asset.AttributeDouble("FrameRate");
            }
            toReturn.Path = asset.Attribute("Path").Value;
            return toReturn;
        }

        private DateTime lastFrameIdxIncrement = DateTime.MinValue;
        private int frameIdx = 0;
        private TimeSpan frameSpan;

        public override Image GetImage() {
            if (this.lastFrameIdxIncrement == DateTime.MinValue) {
                this.lastFrameIdxIncrement = DateTime.Now;
            }
            if (DateTime.Now - lastFrameIdxIncrement > this.frameSpan) {
                this.frameIdx = ++this.frameIdx % this.subImageCount;
                lastFrameIdxIncrement = DateTime.Now;
            }


            int colIdx = this.frameIdx % this.colCount;
            int rowIdx = this.frameIdx / this.colCount;


            double x = colIdx * this.ClipWidth;
            double y = rowIdx * this.ClipHeight;

            //log.DebugFormat("col: {0}, row: {1}, frame: {2}", colIdx, rowIdx, frameIdx);
            Image finalImage = null;
            App.Current.Dispatcher.Invoke((Action)(() => {
                finalImage = new Image();

                finalImage.Source = new CroppedBitmap(this.baseImage,
                    new Int32Rect(x.Round(), y.Round(), this.ClipWidth.Round(), this.ClipHeight.Round()));
            }));
            this.setImageDimensions(finalImage);
            return finalImage;
        }
    }
}
