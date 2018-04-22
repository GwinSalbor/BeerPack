using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;
using System.Drawing;
using System.Configuration;

namespace BeerPack
{
    class Program
    {
        public static List<Coordinates> coordinatesLists = new List<Coordinates>();
        public static int originalDocumentWidth;
        public static int originalDocumentHeight;

        static void Main(string[] args)
        {
            XDocument document = XDocument.Load(ConfigurationManager.AppSettings["InputFileName"]);
            float rootX = float.Parse(document.Element("folding").Attribute("rootX").Value, CultureInfo.InvariantCulture.NumberFormat);
            float rootY = float.Parse(document.Element("folding").Attribute("rootY").Value, CultureInfo.InvariantCulture.NumberFormat);
            originalDocumentWidth = int.Parse(document.Element("folding").Attribute("originalDocumentWidth").Value, CultureInfo.InvariantCulture.NumberFormat) + 1000;
            originalDocumentHeight = int.Parse(document.Element("folding").Attribute("originalDocumentHeight").Value, CultureInfo.InvariantCulture.NumberFormat) + 1000;

            var parent = document.Root.Element("panels").Elements("item");
            var rootPanel = new Panel(parent.First(), rootX, rootY, 0, 0, 0);

            coordinatesLists.Add(new Coordinates(rootPanel._panelx, rootPanel._panely, rootPanel._panelwidth, rootPanel._panelheight, 0, rootPanel._panelid));
            AddElement(parent.Elements("attachedPanels").Elements("item"), rootPanel);
            Draw(coordinatesLists);
        }

        private static void AddElement(IEnumerable<XElement> xElement, Panel rootPanel)
        {
            foreach (var xElementItem in xElement)
            {
                var childPanel = new Panel(xElementItem, rootPanel._panelx, rootPanel._panely, rootPanel._rotangle, rootPanel._panelheight, rootPanel._panelwidth);
                if (childPanel._attachedtoside == 0)
                {
                    coordinatesLists.Add(new Coordinates(childPanel._panelx, childPanel._panely, childPanel._panelwidth, childPanel._panelheight, childPanel._rotangle, childPanel._panelid));
                }
                if (childPanel._attachedtoside == 1)
                {
                    coordinatesLists.Add(new Coordinates(childPanel._panelx, childPanel._panely, childPanel._panelwidth, childPanel._panelheight, childPanel._rotangle, childPanel._panelid));
                }
                if (childPanel._attachedtoside == 2)
                {
                    coordinatesLists.Add(new Coordinates(childPanel._panelx, childPanel._panely, childPanel._panelwidth, childPanel._panelheight, childPanel._rotangle, childPanel._panelid));
                }
                if (childPanel._attachedtoside == 3)
                {
                    coordinatesLists.Add(new Coordinates(childPanel._panelx, childPanel._panely, childPanel._panelwidth, childPanel._panelheight, childPanel._rotangle, childPanel._panelid));
                }
                if (xElementItem.Elements("attachedPanels").Elements("item").Any() && xElementItem.Elements("attachedPanels").First().Elements().Count() > 0)
                {
                    AddElement(xElementItem.Elements("attachedPanels").Elements("item"), childPanel);
                }
            }
        }

        public static void Draw(List<Coordinates> coordinates)
        {
            Bitmap image = new Bitmap(originalDocumentWidth, originalDocumentHeight);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            Pen p;
            p = new Pen(Color.Black);
            p.Width = 5;

            foreach (var item in coordinates)
            {
                g.DrawRectangle(p, item._panelx, item._panely, item._panelwidth, item._panelheight);
            }

            image.RotateFlip(RotateFlipType.Rotate180FlipX);
            image.Save("BeerPack.png", System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
