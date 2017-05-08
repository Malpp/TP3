using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Base;
using SFML.Graphics;

namespace GeometryWars.Code.Effects
{
    class Pixelate : Effect
    {
        public Pixelate()
        {
            // Load the texture and initialize the sprite
            mySprite = new Sprite();

            // Load the shader
            myShader = new Shader(null, "Assets/Shaders/pixelate.frag");
            myShader.SetParameter("texture", Shader.CurrentTexture);
        }

        protected override void OnUpdate(Texture texture, float sigma, float glow)
        {
            mySprite.Texture = texture;
        }

        protected override void OnDraw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(states);
            states.Shader = myShader;
            target.Draw(mySprite, states);
        }

        private Texture myTexture = null;
        private Sprite mySprite = null;
        private Shader myShader = null;
    }
}
