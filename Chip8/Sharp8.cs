using System;

namespace Sharp8
{
	class MainClass
	{
		private static CHIP8CPU cpu;

		public static void Main (string[] args)
		{
			cpu = new CHIP8CPU (new CHIP8MMU ("INVADERS"));

			Emulate (200);
			cpu.CrashDump ();
		}

		public static void Emulate (int cycles = 0)
		{
			while (cycles > 0) {
				cpu.RunCycle ();
				cycles--;
				if (cpu.crashed)
					cycles = 0;
			}
			cpu.UpdateScreen ();
		}
	}
}
