using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace GeometryWars.Code.Base
{
    abstract class Effect : SFML.Graphics.Drawable
    {

        public void Update(Texture texture, float sigma, float glow)
        {
            if (Shader.IsAvailable)
                OnUpdate(texture, sigma, glow);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (Shader.IsAvailable)
            {
                OnDraw(target, states);
            }
        }

        protected abstract void OnUpdate(Texture texture, float sigma, float glow);
        protected abstract void OnDraw(RenderTarget target, RenderStates states);
        
    }
}
