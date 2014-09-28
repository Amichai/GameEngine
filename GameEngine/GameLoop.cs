using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace GameEngine {
    class GameLoop {
        public GameLoop(Game game, Canvas canvas, Canvas visibleArea) {
            this.game = game;
            this.canvas = canvas;
            this.visibleAreaCanvas = visibleArea;
            Task.Factory.StartNew((Action)(() => {
                this.loop();    
            }), TaskCreationOptions.LongRunning);
        }


        private Canvas canvas;
        private Canvas visibleAreaCanvas;
        private Game game;

        public void Start() {
            this.startTime = DateTime.Now;
            this.running = true;
        }

        public void Pause() {
            this.running = false;
        }

        private DateTime now {
            get {
                return DateTime.Now;
            }
        }

        public TimeSpan GameTime {
            get {
                return now - startTime;
            }
        }

        private DateTime? lastUpdate = null;
        private TimeSpan sinceLastUpdate {
            get {
                if (lastUpdate == null) {
                    return TimeSpan.FromSeconds(0);
                }
                return now - lastUpdate.Value;
            }
        }

        private VisibleArea visibleArea {
            get {
                return this.game.GameBoard.VisibleArea;
            }
        }

        private void update() {
            this.visibleArea.Update(sinceLastUpdate);
            App.Current.Dispatcher.BeginInvoke((Action)(() => {
                Canvas.SetLeft(this.canvas, this.visibleArea.Position.X);
            }));
            foreach (var element in game.GameElements) {
                var dt = now - lastUpdate;
                if (!element.WasAddedToCanvas) {
                    App.Current.Dispatcher.BeginInvoke((Action)(() => {
                        var r = element.Render();
                        element.SetVisualElement(r);
                        if (r != null) {
                            switch (element.ParentCoordinateSystem) {
                                case CoordinateSystem.Board:
                                    this.canvas.Children.Add(r);
                                    break;
                                case CoordinateSystem.Visible:
                                    this.visibleAreaCanvas.Children.Add(r);
                                    break;
                            }
                        }
                    }));
                    element.WasAddedToCanvas = true;
                }
                if (dt.HasValue) {
                    element.Update(dt.Value);
                }
            }
            this.lastUpdate = now;
        }

        private bool running = false;

        private void loop() {
            while (true) {
                if (running) {
                    this.update();
                    Thread.Sleep(2);
                } else {
                    Thread.Sleep(50);
                }
            }
        }

        private DateTime startTime;
    }
}
