﻿using System;
using System.IO;

namespace Template {

class Game
{
	// member variables
	public Surface screen;
	// initialize
	public void Init()
	{
	}
	// tick: renders one frame
	public void Tick()
	{
		screen.Clear( 0 );
	}
}

} // namespace Template