using System;
using System.IO;
using template;

namespace Template {

class Game
{
	// member variables
	public Surface screen;
    public Application app;
	// initialize
	public void Init()
	{
        app = new Application();
	}
	// tick: renders one frame
	public void Tick()
	{
		screen.Clear( 0 );
        app.Update();
	}
}

} // namespace Template