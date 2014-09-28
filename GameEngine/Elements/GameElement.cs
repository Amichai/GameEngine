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
        public Asset Asset { get; set; }
        public List<Dimension> Dimensions { get; set; }
        public State State { get; set; }
        public CoordinateSystem ParentCoordinateSystem { get; set; }
        public bool WasAddedToCanvas = false;

        internal static GameElement Parse(XElement e) {
            GameElement toReturn = new GameElement();
            toReturn.ParentCoordinateSystem = (CoordinateSystem)Enum.Parse(typeof(CoordinateSystem), e.Attribute("Parent").Value);
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
                            Dimension toAdd;
                            switch (dim.Name.ToString()) {
                                case "Rectangle":
                                    break;
                                    
                            }
                        }
                        //toReturn.Dimensions = Dimensions.Parse(element);
                        break;
                }
            }
            return toReturn;
        }

        internal UIElement Render() {
            Image finalImage = new Image();
            BitmapImage logo = new BitmapImage();
            logo.BeginInit();
            logo.UriSource = new Uri(@"C:\Users\amichai\Documents\MyProjects\runnerp2\assets\background1.jpg");
            logo.EndInit();
            finalImage.Source = logo;
            return finalImage;
        }

        private UIElement visual;

        public void SetVisualElement(UIElement visual) {
            this.visual = visual;
        }

        internal void Update(TimeSpan dt) {
            this.State.Update(dt);
            App.Current.Dispatcher.Invoke((Action)(() => {
                Canvas.SetLeft(this.visual, this.State.Position.X);
                Canvas.SetTop(this.visual, this.State.Position.Y);    
            }));
        }
    }
}
