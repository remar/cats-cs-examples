using System;
using System.IO;
using System.Reflection;
using SDL2;
using cats;

namespace catscsexamples {
	public class CatsExample {
		private Cats cats;
		private int spriteId = -1;
		private uint lastFrameTime = SDL.SDL_GetTicks();
		private float pos = 0;
		private float dx;
		private bool visible = true;

		public static int Main() {
			// Make sure current dir is correct
			Directory.SetCurrentDirectory(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));

			CatsExample example = new CatsExample ();
			example.Setup ();

			SDL.SDL_Event Event;
			bool running = true;
			while (running) {
				while (SDL.SDL_PollEvent(out Event) != 0) {
					if (Event.type == SDL.SDL_EventType.SDL_QUIT) {
						running = false;
					} else if(Event.type == SDL.SDL_EventType.SDL_KEYDOWN) {
						if (Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE) {
							running = false;
						} else if (Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_h) {
							example.ToggleVisibility ();
						} else if (Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_r) {
							example.RemoveSpriteInstance ();
						} else if (Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_c) {
							example.CreateSpriteInstance ();
						}
					}
				}
				example.Update ();
			}

			example.Quit ();

			return 0;
		}

		public void Setup() {
			SDL.SDL_Init (SDL.SDL_INIT_VIDEO);
			cats = new Cats ();
			cats.Init (640, 480);
			cats.SetBackgroundColor (0x30, 0x00, 0x30);
			cats.SetupTileLayer (40, 15, 32, 32);
			cats.LoadTileset ("data/tiles.json");
			for (int y = 0; y < 15; y++) {
				for (int x = 0; x < 40; x++) {
					if (y != 6) {
						cats.SetTile (x, y, "tiles", 0, 0);
					}
				}
			}
			cats.LoadSprite ("data/sprite.json");
			CreateSpriteInstance ();
		}

		public void Update() {
			float delta = (SDL.SDL_GetTicks () - lastFrameTime)/1000.0f;
			lastFrameTime = SDL.SDL_GetTicks ();

			if (spriteId != -1) {
				pos += dx * delta;
				if (pos >= 640 - 16 && dx > 0) {
					pos = 640 - 16;
					dx = -dx;
					cats.SetAnimation (spriteId, "walk left");
				} else if (pos <= 0 && dx < 0) {
					pos = 0;
					dx = -dx;
					cats.SetAnimation (spriteId, "walk right");
				}
				cats.SetSpritePosition (spriteId, (int)pos, 200);
			}
			cats.SetScroll (-(int)pos, 0);
			Redraw (delta);
		}

		void ToggleVisibility () {
			if (spriteId != -1) {
				visible = !visible;
				cats.ShowSprite (spriteId, visible);
			}
		}

		void RemoveSpriteInstance () {
			if (spriteId != -1) {
				cats.RemoveSpriteInstance (spriteId);
			}
			spriteId = -1;
		}

		void CreateSpriteInstance () {
			if (spriteId == -1) {
				spriteId = cats.CreateSpriteInstance ("sprite");
				cats.SetAnimation (spriteId, "walk right");
				pos = 0;
				dx = 100f;
			}
		}

		public void Redraw(float delta) {
			cats.Redraw (delta);
		}

		public void Quit() {
			SDL.SDL_Quit ();
		}
	}
}
