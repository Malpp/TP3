using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;

namespace GeometryWars.Code
{
	class Hero : BaseEntity
	{
		private static Texture heroTexture = new Texture("Assets/Textures/hero.png");
		private const float heroSpeed = 500f;
		private List<Projectile> projectiles;
		private const float fireDelay = 0.2f;
		private float fireDelta = 0;
		private bool canFire = true;
		private static Hero hero;

		public static Hero GetInstance()
		{
			
			if(hero == null)
				hero = new Hero(Game.GAME_WIDTH * 0.5f, Game.GAME_HEIGHT * 0.5f);
			return hero;

		}

		private Hero(float x, float y) 
			: base(x, y, heroSpeed, heroTexture)
		{
			
			projectiles = new List<Projectile>();

		}

		public override void Update(float deltaTime, IEnumerable<BaseEntity> entities = null)
		{

			if (Keyboard.IsKeyPressed(Keyboard.Key.Space) && canFire)
			{
				canFire = false;
				projectiles.Add(new Projectile(Pos + Common.MovePointByAngle(heroTexture.Size.X * 0.3f, Angle), Angle));
			}

			if(!canFire)
				fireDelta += deltaTime;

			if (fireDelta > fireDelay)
			{
				fireDelta = 0;
				canFire = true;
			}

			foreach (Projectile projectile in projectiles)
			{
				
				projectile.Update(deltaTime, entities);
				
			}

			projectiles.RemoveAll(x => x.ToDelete);

			base.Update(deltaTime, entities);
		}

		public override void Draw(RenderTarget window)
		{

			foreach (Projectile projectile in projectiles)
			{
				projectile.Draw(window);
			}

			base.Draw(window);
		}

		protected override void HandleCollision(BaseEntity entity)
		{

			entity.Delete();

			base.HandleCollision(entity);
		}
	}
}
