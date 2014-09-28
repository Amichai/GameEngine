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
                try {
                    this.loop();
                } catch {

                }
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

        private void draw() {

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
            var dt = now - lastUpdate;
            this.visibleArea.Update(sinceLastUpdate);
            App.Current.Dispatcher.Invoke((Action)(() => {
                Canvas.SetLeft(this.canvas, this.visibleArea.Position.X);
            }));
            foreach (var element in game.GameElements) {
                if (!element.WasAddedToCanvas) {
                    App.Current.Dispatcher.Invoke((Action)(() => {
                        var r = element.Render();
                        element.SetVisualElement(r);
                        this.canvas.Children.Add(r);
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
                    this.draw();
                } else {
                    Thread.Sleep(50);
                }
            }
        }

        private DateTime startTime;
    }
}
