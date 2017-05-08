using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeometryWars.Code.Base;
using SFML.Graphics;

namespace GeometryWars.Code.Effects
{
    class HorizontalPass : Effect
    {

        public HorizontalPass()
        {
            // Load the texture and initialize the sprite
            mySprite = new Sprite();

            // Load the shader
            myShader = new Shader(null, "Assets/Shaders/hori.frag");
            myShader.SetParameter("sourceTexture", Shader.CurrentTexture);
            myShader.SetParameter("sigma", 1f);
            myShader.SetParameter("width", Game.GAME_WIDTH);
            myShader.SetParameter("glowMultiplier", 1.5f);
        }

        protected override void OnUpdate(Texture texture, float sigma, float glow)
        {
            mySprite.Texture = texture;
            myShader.SetParameter("sigma", sigma);
            myShader.SetParameter("glowMultiplier", glow);
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
