using System.Globalization;
using System.Xml.Linq;

namespace BeerPack
{
    internal class Panel
    {
        protected readonly static string[] items = new string[] { "panelId", "panelName", "minRot", "maxRot", "initialRot", "startRot",
            "endRot", "hingeOffset", "panelWidth", "panelHeight", "attachedToSide", "creaseBottom", "creaseTop", "creaseLeft",
            "creaseRight", "ignoreCollisions", "mouseEnabled" };

        private string panelId;
        private string panelName;
        private float minRot;
        private float maxRot;
        private float initialRot;
        private float startRot;
        private float endRot;
        private float hingeOffset;
        private float panelWidth;
        private float panelHeight;
        private int attachedToSide;
        private float creaseBottom;
        private float creaseTop;
        private float creaseLeft;
        private float creaseRight;
        private bool ignoreCollisions;
        private bool mouseEnabled;
        private float panelX;
        private float panelY;
        private int rotAngle;
        private XElement xElement;

        public Panel(XElement xElement, float parentX, float parentY, int parentAngle, float rootHeight, float rootWidth)
        {
            this.xElement = xElement;
            for (int i = 0; i < items.Length; i++)
            {
                if (xElement.Attribute(items[i]) != null)
                {
                    var fieldName = "_" + items[i].ToLower();
                    var field = typeof(Panel).GetProperty(fieldName);
                    var type = field.PropertyType;

                    if (type == typeof(float))
                    {
                        field.SetValue(this, float.Parse(xElement.Attribute(items[i]).Value, CultureInfo.InvariantCulture.NumberFormat), null);
                    }
                    else if (type == typeof(int))
                    {
                        field.SetValue(this, int.Parse(xElement.Attribute(items[i]).Value), null);
                    }
                    else if (type == typeof(bool))
                    {
                        field.SetValue(this, bool.Parse(xElement.Attribute(items[i]).Value), null);
                    }
                    else
                    {
                        field.SetValue(this, xElement.Attribute(items[i]).Value, null);
                    }
                }
            }

            if (panelName != "root panel")
            {
                if (attachedToSide == 0) _rotangle = parentAngle + 180;
                else if (attachedToSide == 1) _rotangle = parentAngle + 90;
                else if (attachedToSide == 2) _rotangle = parentAngle;
                else _rotangle = parentAngle + 270;

                if (_rotangle >= 360) _rotangle -= 360;

                switch (_rotangle)
                {
                    case 0:
                        Convert((rootWidth - panelWidth + hingeOffset) / 2, rootHeight, parentX, parentY);
                        break;
                    case 90:
                        Swap(ref panelHeight, ref panelWidth);
                        Convert(rootWidth, (rootHeight - panelHeight + hingeOffset) / 2, parentX, parentY);
                        break;
                    case 180:
                        Convert((rootWidth - panelWidth + hingeOffset) / 2, -panelHeight, parentX, parentY);
                        break;
                    case 270:
                        Swap(ref panelHeight, ref panelWidth);
                        Convert(-panelWidth, (rootHeight - panelHeight + hingeOffset) / 2, parentX, parentY);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _panelx = parentX;
                _panely = parentY;
            }
        }

        private void Swap(ref float height, ref float width)
        {
            float swaper = height;
            height = width;
            width = swaper;
        }

        public void Convert(float panelHeight, float panelWidth, float x, float y)
        {
            _panelx = x + panelHeight;
            _panely = y + panelWidth;
        }

        public string _panelid { get => panelId; set => panelId = value; }
        public string _panelname { get => panelName; set => panelName = value; }
        public float _minrot { get => minRot; set => minRot = value; }
        public float _maxrot { get => maxRot; set => maxRot = value; }
        public float _initialrot { get => initialRot; set => initialRot = value; }
        public float _startrot { get => startRot; set => startRot = value; }
        public float _endrot { get => endRot; set => endRot = value; }
        public float _hingeoffset { get => hingeOffset; set => hingeOffset = value; }
        public float _panelwidth { get => panelWidth; set => panelWidth = value; }
        public float _panelheight { get => panelHeight; set => panelHeight = value; }
        public int _attachedtoside { get => attachedToSide; set => attachedToSide = value; }
        public float _creasebottom { get => creaseBottom; set => creaseBottom = value; }
        public float _creasetop { get => creaseTop; set => creaseTop = value; }
        public float _creaseleft { get => creaseLeft; set => creaseLeft = value; }
        public float _creaseright { get => creaseRight; set => creaseRight = value; }
        public bool _ignorecollisions { get => ignoreCollisions; set => ignoreCollisions = value; }
        public bool _mouseenabled { get => mouseEnabled; set => mouseEnabled = value; }
        public float _panelx { get => panelX; set => panelX = value; }
        public float _panely { get => panelY; set => panelY = value; }
        public int _rotangle { get => rotAngle; set => rotAngle = value; }
    }
}
