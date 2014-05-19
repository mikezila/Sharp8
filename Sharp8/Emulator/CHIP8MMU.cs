using System;
using System.IO;
using System.Collections.Generic;

namespace Sharp8
{
	public class CHIP8MMU
	{
		private BinaryReader rom;
		private byte[] memory;
        private Stack<ushort> stack;
		public int stack_pointer;
		private string rom_path;
		// This is the offset at which the rom is loaded, added to the index during rom reading.
		private int padding = 0x0200;
        const int STACK_DEPTH = 16;

		public CHIP8MMU (string rom_path)
		{
			this.rom_path = rom_path;
			Reset ();
		}

		public void Reset ()
		{
			memory = new byte[4096]; // Total memory, includes rom/ram/video/everything.
			//stack = new ushort[16]; // For jump functions, the Chip8 didn't have a stack per-se, so it's on us to make one.
            stack = new Stack<ushort>();
            stack_pointer = 0;
			rom = new BinaryReader (File.OpenRead (rom_path));
			for (int i = 0; i < rom.BaseStream.Length; i++) {
				memory [padding + i] = rom.ReadByte ();
			}
			rom.Close ();
			PackFonts ();
		}
		// This loads the static fonts into the "bios" part of memory.
		private void PackFonts ()
		{
			byte[] chip8_fontset = new byte[80] { 
				0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
				0x20, 0x60, 0x20, 0x20, 0x70, // 1
				0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
				0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
				0x90, 0x90, 0xF0, 0x10, 0x10, // 4
				0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
				0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
				0xF0, 0x10, 0x20, 0x40, 0x40, // 7
				0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
				0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
				0xF0, 0x90, 0xF0, 0x90, 0x90, // A
				0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
				0xF0, 0x80, 0x80, 0x80, 0xF0, // C
				0xE0, 0x90, 0x90, 0x90, 0xE0, // D
				0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
				0xF0, 0x80, 0xF0, 0x80, 0x80  // F
			};

			for (int i = 0; i < chip8_fontset.Length; i++) {
				WriteByte (0x050 + i, chip8_fontset [i]);
			}
		}

		public byte ReadByte (int address)
		{
			return memory [address];
		}

		public ushort PopStack ()
		{
            return stack.Pop();
		}

		public void PushStack (ushort value)
		{
            stack.Push(value);
		}

		public ushort ReadOpcode (int address)
		{
			ushort value = (ushort)(ReadByte (address++) << 8);
			value |= ReadByte (address);
			return (ushort)value;
		}

		public ushort ReadPaddedOpcode (int address)
		{
			address += padding;
			ushort value = (ushort)(ReadByte (address++) << 8);
			value += ReadByte (address);
			return (ushort)value;
		}

		public void WriteByte (int address, byte value)
		{
			memory [address] = value;
		}

		public void CrashDump ()
		{
			Console.WriteLine ("Memory Dump:");
			for (int i = 0; i < memory.Length; i += 4) {
				Console.WriteLine (i.ToString ("X3") + ": " + memory [i].ToString ("X2") + " " + memory [i + 1].ToString ("X2") + " " + memory [i + 2].ToString ("X2") + " " + memory [i + 3].ToString ("X2"));
			}
			Console.WriteLine ("\nStack Dump:");
			for (int i = 0; i <= stack.Count; i++) {
				Console.WriteLine (i.ToString () + " : " + stack.Pop().ToString ("X3"));
			}
		}
	}
}
