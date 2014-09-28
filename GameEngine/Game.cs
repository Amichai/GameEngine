using GameEngine.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameEngine {
    class Game {
        public Game() {
            this.GameElements = new List<GameElement>();
        }
        public static Game Parse(XElement xml) {
            var toReturn = new Game();

            toReturn.Title = xml.Attribute("Title").Value;
            toReturn.HasMultipleLevels = bool.Parse(xml.Attribute("HasMultipleLevels").Value);
            if (!toReturn.HasMultipleLevels) {
                toReturn.GameBoard = Board.Parse(xml.Element("GameBoard"));
            } else {
                ///parse all the levels
            }

            foreach (var e in xml.Element("GameElements").Elements()) {
                GameElement element = GameElement.Parse(e);
                toReturn.GameElements.Add(element);
            }
            return toReturn;
        }

        public bool HasMultipleLevels { get; set; }

        public string Title { get; set; }

        /// <summary>
        /// Max frame rate in frames per second
        /// </summary>
        public int MaxFrameRate { get; set; }

        public List<Level> Levels { get; set; }

        public Board GameBoard { get; set; }

        public WindowSettings WindowSetings { get; set; }

        public List<GameElement> GameElements { get; set; }

        public string Icon { get; set; }

    }
}
