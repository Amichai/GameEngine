using GameEngine.Elements.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace GameEngine.Elements {
    class GameElement {
        public GameElement() {
            this.Dimensions = new List<Dimension>();
            this.State = new State();
        }
        public Asset Asset { get; set; }
        public List<Dimension> Dimensions { get; set; }
        public State State { get; set; }
        public CoordinateSystem ParentCoordinateSystem { get; set; }
        public bool WasAddedToCanvas = false;
        public string Name { get; set; }

        internal static GameElement Parse(XElement e) {
            GameElement toReturn = new GameElement();
            if (e.HasAttribute("Parent")) {
                toReturn.ParentCoordinateSystem = e.AttributeEnum<CoordinateSystem>("Parent");
            }
            if (e.HasAttribute("Name")) {
                toReturn.Name = e.Attribute("Name").Value;
            }
            foreach (var element in e.Elements()) {
                switch (element.Name.ToString()) {
                    case "StaticImageAsset":
                        toReturn.Asset = StaticImageAsset.Parse(element);
                        break;
                    case "State":
                        toReturn.State = State.Parse(element);
                        break;
                    case "Dimensions":
                        foreach (var dim in element.Elements()) {
                            Dimension toAdd = null;
                            switch (dim.Name.ToString()) {
                                case "Rectangle":
                                    toAdd = RectangleDimension.Parse(dim);
                                    break;
                                    
                            }
                            if (toAdd != null) {
                                toReturn.Dimensions.Add(toAdd);
                            }
                        }
                        //toReturn.Dimensions = Dimensions.Parse(element);
                        break;
                }
            }
            return toReturn;
        }

        internal UIElement Render() {
            if (this.Asset == null) {
                return null;
            }
            var finalImage = this.Asset.GetImage();
            if (this.Asset != null && this.Asset.Width.HasValue) {
                finalImage.Width = this.Asset.Width.Value;
            }
            if (this.Asset != null && this.Asset.Height.HasValue) {
                finalImage.Height = this.Asset.Height.Value;
            }
            return finalImage;
        }

        private UIElement visual;

        public void SetVisualElement(UIElement visual) {
            this.visual = visual;
        }

        internal void Update(TimeSpan dt) {
            this.State.Update(dt);
            if (this.visual == null) {
                return;
            }
            App.Current.Dispatcher.BeginInvoke((Action)(() => {
                Canvas.SetLeft(this.visual, this.State.Position.X);
                Canvas.SetTop(this.visual, this.State.Position.Y);    
            }));
        }
    }
}
