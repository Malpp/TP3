using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Main;
using SFML.Graphics;
using SFML.System;

namespace GeometryWars.Code.Base
{
    abstract class ScreenText
    {
        static Font font = new Font("Assets/Fonts/emulogic.ttf");
        private SFML.Graphics.Text text;
        private Vector2f pos;
        private static Vector2f adjustPos = new Vector2f(30, 0);

        public Vector2f Pos
        {
            get { return pos; }
            protected set { pos = value; }
        }

        public ScreenText(Vector2f pos, string initString, int size)
        {
            text = new SFML.Graphics.Text(initString, font, (uint)size);
            text.Position = pos;
            this.pos = pos;
        }

        public virtual void Update()
        {
            //text.Position = pos + Camera.Center * 0.2f - Camera.Pos - adjustPos;
            if (ShouldUpdate())
            {
                text.DisplayedString = UpdateText();
            }
        }

	    public virtual void ForceUpdate()
	    {
		    text.DisplayedString = UpdateText();
	    }

        public virtual void Draw(RenderTarget window)
        {
            window.Draw(text);
        }


        protected abstract string UpdateText();
        protected abstract bool ShouldUpdate();

    }
}
