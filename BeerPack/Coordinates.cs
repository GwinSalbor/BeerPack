using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerPack
{
    class Coordinates
    {
        public float _panelx;
        public float _panely;
        public float _panelwidth;
        public float _panelheight;
        public int _rotAngle;
        public string _panelId;

        public Coordinates(float panelx, float panely, float panelwidth, float panelheight, int rotangle, string panelId)
        {
            _panelx = panelx;
            _panely = panely;
            _panelwidth = panelwidth;
            _panelheight = panelheight;
            _rotAngle = rotangle;
            _panelId = panelId;
        }
    }
}
