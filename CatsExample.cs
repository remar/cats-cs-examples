using System;
using SDL2;
using cats;

namespace catscsexamples {
	public class CatsExample {
		Cats cats;

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
			}

			example.Quit ();

			return 0;
		}

		public void Setup() {
			SDL.SDL_Init (SDL.SDL_INIT_VIDEO);
			cats = new Cats ();
			cats.Init (640, 480);
		}

		public void Quit() {
			SDL.SDL_Quit ();
		}
	}
}
