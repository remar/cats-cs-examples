using System;
using SDL2;
using cats;

namespace catscsexamples {
	public class CatsExample {
		private Cats cats;
		private int spriteId;
		private uint lastFrameTime = SDL.SDL_GetTicks();

		public static int Main() {
			CatsExample example = new CatsExample ();
			example.Setup ();

			SDL.SDL_Event Event;
			bool running = true;
			while (running) {
				while (SDL.SDL_PollEvent(out Event) != 0) {
					if (Event.type == SDL.SDL_EventType.SDL_QUIT) {
						running = false;
					} else if(Event.type == SDL.SDL_EventType.SDL_KEYDOWN) {
						if(Event.key.keysym.sym == SDL.SDL_Keycode.SDLK_ESCAPE) {
							running = false;
						}
					}
				}
				example.Redraw ();
			}

			example.Quit ();

			return 0;
		}

		public void Setup() {
			SDL.SDL_Init (SDL.SDL_INIT_VIDEO);
			cats = new Cats ();
			cats.Init (640, 480);
			cats.SetBackgroundColor (0xff, 0x00, 0x80);
			cats.LoadSprite ("../../data/sprite.json");
			spriteId = cats.CreateSpriteInstance ("sprite");
		}

		public void Redraw() {
			float delta = (SDL.SDL_GetTicks () - lastFrameTime)/1000.0f;
			lastFrameTime = SDL.SDL_GetTicks ();
			cats.Redraw (delta);
		}

		public void Quit() {
			SDL.SDL_Quit ();
		}
	}
}
