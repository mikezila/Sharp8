using System;
using System.Drawing;

namespace Sharp8
{
    public class CHIP8CPU
    {
        private byte[] pixels = new byte[64 * 32];
        private byte[] register = new byte[16];
        // States of the 16 hex keys
        private bool[] gamepad = new bool[16];
        private ushort index_pointer, program_counter;
        private byte delay_timer, sound_timer;
        private CHIP8MMU memory;
        private Random random;
        public bool crashed = false;
        public Bitmap raster;
        private bool draw_flag = false;

        public CHIP8CPU()
        {
            this.random = new Random();
            raster = new Bitmap(64, 32);
            ClearScreen();
        }

        public void LoadRom(string rom)
        {
            memory = new CHIP8MMU(rom);
            Reset();
        }

        public void Reset()
        {
            crashed = false;
            program_counter = 0x200;
            index_pointer = 0;
            delay_timer = 0;
            sound_timer = 0;
            for (int i = 0; i < register.Length; i++)
            {
                register[i] = 0;
            }
            for (int i = 0; i < gamepad.Length; i++)
            {
                gamepad[i] = false;
            }
            memory.Reset();
            ClearScreen();
        }

        public void RunCycle()
        {
            //TODO Need to poll input at this time
            Execute(memory.ReadOpcode(program_counter));
            if (draw_flag)
                //UpdateScreen();
                // Decrement our timers
                if (sound_timer > 0)
                    sound_timer--;
            if (delay_timer > 0)
                delay_timer--;
        }

        public void Execute(ushort opcode)
        {
            //Console.Write ("PC: " + program_counter.ToString ("X4") + " Opcode: " + opcode.ToString ("X4") + " ");
            // 00E0 - Clear display
            if (opcode == 0x00E0)
            {
                //Console.WriteLine ("Found clear display opcode");
                ClearScreen();
                program_counter += 2;
                return;
            }

            // 00EE - Return from subroutine
            if (opcode == 0x00EE)
            {
                //Console.WriteLine ("Found return form subroutine opcode");
                program_counter = memory.PopStack();
                return;
            }

            // 1nnn - Jump to location nnn
            if ((opcode & 0xF000) == 0x1000)
            {
                //Console.WriteLine ("Found jump opcode."); 
                program_counter = (ushort)(opcode & 0x0FFF);
                return;
            }

            // 2nnn - Call subroutine at nnn
            if ((opcode & 0xF000) == 0x2000)
            {
                //Console.WriteLine ("Found call subroutine opcode.");
                memory.PushStack((ushort)(program_counter + 2));
                program_counter = (ushort)(opcode & 0x0FFF);
                return;
            }

            // 3xkk - Skip next opcode if register x == kk
            if ((opcode & 0xF000) == 0x3000)
            {
                //Console.WriteLine ("Found compare register and skip if equal opcode.");
                //Console.WriteLine ("Register value: " + (register [(opcode & 0x0F00) >> 8]).ToString ("X") + " Value: " + (opcode & 0x00FF).ToString ("X2"));
                if ((opcode & 0x00FF) == register[(opcode & 0x0F00) >> 8])
                    program_counter += 4;
                else
                    program_counter += 2;
                return;
            }

            // 4xkk - Skip next opcode if register x != kk
            if ((opcode & 0xF000) == 0x4000)
            {
                //Console.WriteLine ("Found compare register and skip if not equal opcode.");
                //Console.WriteLine ("Register value: " + (register [(opcode & 0x0F00) >> 8]).ToString ("X") + " Value: " + (opcode & 0x00FF).ToString ("X2"));
                if ((opcode & 0x00FF) != register[(opcode & 0x0F00) >> 8])
                    program_counter += 4;
                else
                    program_counter += 2;
                return;
            }

            // 5xy0 - Skip next instruction if register x and y are equal
            if ((opcode & 0xF000) == 0x5000)
            {
                //Console.WriteLine ("Found compare registers and skip if equal opcode.");
                //Console.WriteLine ("Register values: " + register [(opcode & 0x0F00) >> 8].ToString ("X2") + " - " + register [(opcode & 0x00F0) >> 4].ToString ("X2"));
                if (register[(opcode & 0x0F00) >> 8] == register[(opcode & 0x00F0) >> 4])
                    program_counter += 4;
                else
                    program_counter += 2;
                return;
            }

            // 6xkk - Load register x with value kk
            if ((opcode & 0xF000) == 0x6000)
            {
                //Console.WriteLine ("Found load register opcode.");
                register[(opcode & 0x0F00) >> 8] = (byte)(opcode & 0x00FF);
                program_counter += 2;
                return;
            }

            // 7xkk - Add kk to register x
            if ((opcode & 0xF000) == 0x7000)
            {
                //Console.WriteLine ("Found add kk to register opcode.");
                register[(opcode & 0x0F00) >> 8] = AddBytes(register[(opcode & 0x0F00) >> 8], (byte)(opcode & 0x00FF));
                program_counter += 2;
                return;
            }


            // 8xy0 - Store the value of register y in register x
            if ((opcode & 0xF00F) == 0x8000)
            {
                //Console.WriteLine ("Found store register Y in register X - 8xy0 opcode");
                register[(opcode & 0x0F00) >> 8] = register[(opcode & 0x00F0) >> 4];
                program_counter += 2;
                return;
            }

            // 8xy1 - Bitwise OR on the values of registers x and y, store results in register x
            if ((opcode & 0xF00F) == 0x8001)
            {
                //Console.WriteLine ("Found OR between registers x and y, store in x - 8xy1 opcode");
                register[(opcode & 0x0F00) >> 8] |= register[(opcode & 0x00F0) >> 4];
                program_counter += 2;
                return;
            }

            // 8xy2 - Bitwise AND on the values of registers x and y, store results in register x
            if ((opcode & 0xF00F) == 0x8002)
            {
                //Console.WriteLine ("Found AND between registers x and y, store in x - 8xy2 opcode");
                register[(opcode & 0x0F00) >> 8] &= register[(opcode & 0x00F0) >> 4];
                program_counter += 2;
                return;
            }

            // 8xy3 - Bitwise XOR on the values of registers x and y, store results in register x
            if ((opcode & 0xF00F) == 0x8003)
            {
                //Console.WriteLine ("Found XOR between registers x and y, store in x - 8xy3 opcode");
                register[(opcode & 0x0F00) >> 8] ^= register[(opcode & 0x00F0) >> 4];
                program_counter += 2;
                return;
            }

            // 8xy4 - ADD the values of registers x and y, store results in register x
            if ((opcode & 0xF00F) == 0x8004)
            {
                //Console.WriteLine ("Found ADD registers x and y, store in x - 8xy4 opcode");
                register[(opcode & 0x0F00) >> 8] = AddBytes(register[(opcode & 0x00F0) >> 4], register[(opcode & 0x0F00) >> 8]);
                program_counter += 2;
                return;
            }

            // 8xy5 - SUB the values of registers x and y, (y from x) store results in register x
            if ((opcode & 0xF00F) == 0x8005)
            {
                //Console.WriteLine ("Found SUB registers x and y, (y from x) store in x - 8xy5 opcode");
                register[(opcode & 0x0F00) >> 8] = SubtractBytes(register[(opcode & 0x0F00) >> 8], register[(opcode & 0x00F0) >> 4]);
                program_counter += 2;
                return;
            }

            // 8xy6 - If the least-significant bit of register x is 1, then register 15 is set to 1, otherwise 0. Then register x is divided by 2.
            if ((opcode & 0xF00F) == 0x8006)
            {
                //Console.WriteLine ("Found divide if least signficant byte set");
                if (((register[(opcode & 0x0F00) >> 8]) & 0x01) > 0)
                {
                    register[15] = 0x01;
                }
                else
                {
                    register[15] = 0x00;
                }
                register[(opcode & 0x0F00) >> 8] /= 2;
                program_counter += 2;
                return;
            }

            // 8xy7 - SUB the values of registers x and y, (x from y) store results in register x
            if ((opcode & 0xF00F) == 0x8007)
            {
                //Console.WriteLine ("Found SUB registers x and y, (x from y) store in x - 8xy7 opcode");
                register[(opcode & 0x0F00) >> 8] = SubtractBytes(register[(opcode & 0x00F0) >> 4], register[(opcode & 0x0F00) >> 8]);
                program_counter += 2;
                return;
            }

            // 8xyE - If the most-significant bit of Vx is 1, then VF is set to 1, otherwise to 0. Then Vx is multiplied by 2.
            if ((opcode & 0xF00F) == 0x800E)
            {
                //Console.WriteLine ("Found 8xyE bit checking nonsense");
                if (((opcode >> 12) & 0x01) > 0)
                {
                    register[15] = 0x01;
                }
                else
                {
                    register[15] = 0x00;
                }
                register[(opcode & 0x0F00) >> 8] *= 2;
                program_counter += 2;
                return;
            }

            // 9xy0 - Skip next instruction if register x != register y
            if ((opcode & 0xF000) == 0x9000)
            {
                //Console.WriteLine ("Found Jump if registers not equal opcode 9xy0");
                if (register[(opcode & 0x0F00) >> 8] != register[(opcode & 0x00F0) >> 4])
                {
                    program_counter += 4;
                    return;
                }
                else
                {
                    program_counter += 2;
                    return;
                }
            }

            // Annn - Load I with value nnn
            if ((opcode & 0xF000) == 0xA000)
            {
                //Console.WriteLine ("Found Load I with nnn opcode.");
                index_pointer = (ushort)(opcode & 0x0FFF);
                program_counter += 2;
                return;
            }

            // Bnnn - Jump to I + V0
            if ((opcode & 0xF000) == 0xB000)
            {
                //Console.WriteLine ("Found jump to I + register 0.");
                program_counter = (ushort)(index_pointer + register[0]);
                return;
            }

            // Cxkk - Generate a random byte, AND it with kk, and store the results in register x
            if ((opcode & 0xF000) == 0xC000)
            {
                //Console.WriteLine ("Found generate random number opcode.");
                register[(opcode & 0x0F00) >> 8] = (byte)(random.Next(255) & (opcode & 0x00FF));
                program_counter += 2;
                return;
            }

            // Dxyn - Draw a sprite to the screen
            if ((opcode & 0xF000) == 0xD000)
            {
                //Console.WriteLine ("Found sprite drawing opcode");
                DrawSprite(opcode);
                program_counter += 2;
                return;
            }

            // Ex9E - Skip next if key with value register[x] is pressed
            if ((opcode & 0xF0FF) == 0xE09E)
            {
                //Console.WriteLine ("Found key pressed check opcode - NYI");
                program_counter += 2;
                return;
            }

            // ExA1 - Skip next if key with value register[x] is not pressed
            if ((opcode & 0xF0FF) == 0xE0A1)
            {
                //Console.WriteLine ("Found key not pressed check opcode - NYI");
                program_counter += 4;
                return;
            }

            // Fx07 - Load the current value of the delay timer into register[x]
            if ((opcode & 0xF0FF) == 0xF007)
            {
                //Console.WriteLine ("Set register x with value of delay timer");
                register[(opcode & 0x0F00) >> 8] = delay_timer;
                program_counter += 2;
                return;
            }

            // Fx0A - Wait for key press, place value of pressed key into register x
            if ((opcode & 0xF0FF) == 0xF00A)
            {
                //Console.WriteLine ("Waiting for button press - NYI, proceeding");
                //Console.WriteLine ("Faking keypress with key 06");
                register[(opcode & 0x0F00) >> 8] = 0x00;
                program_counter += 2;
                return;
            }

            // Fx15 - Set delay timer with value from register x
            if ((opcode & 0xF0FF) == 0xF015)
            {
                //Console.WriteLine ("Load delay timer from register x");
                delay_timer = register[(opcode & 0x0F00) >> 8];
                program_counter += 2;
                return;
            }

            // Fx18 - Set sound timer with value from register x
            if ((opcode & 0xF0FF) == 0xF018)
            {
                //Console.WriteLine ("Load delay timer from register x");
                sound_timer = register[(opcode & 0x0F00) >> 8];
                program_counter += 2;
                return;
            }

            // Fx1E - ADD I, Vx
            if ((opcode & 0xF0FF) == 0xF01E)
            {
                //Console.WriteLine ("Add Vx to I");
                index_pointer += register[((opcode & 0x0F00) >> 8)];
                program_counter += 2;
                return;
            }

            // Fx29 - Set I to memory location for sprite for hex digit x
            if ((opcode & 0xF0FF) == 0xF029)
            {
                Console.WriteLine("Setting I to location of requested digit");
                index_pointer = 0x0050;

                switch (register[(opcode & 0x0F00) >> 8])
                {
                    case 0x01:
                        break;
                    case 0x02:
                        index_pointer += 5;
                        break;
                    case 0x03:
                        index_pointer += 10;
                        break;
                    case 0x04:
                        index_pointer += 15;
                        break;
                    case 0x05:
                        index_pointer += 20;
                        break;
                    case 0x06:
                        index_pointer += 25;
                        break;
                    case 0x07:
                        index_pointer += 30;
                        break;
                    case 0x08:
                        index_pointer += 35;
                        break;
                    case 0x09:
                        index_pointer += 40;
                        break;
                    case 0x0A:
                        index_pointer += 45;
                        break;
                    case 0x0B:
                        index_pointer += 50;
                        break;
                    case 0x0C:
                        index_pointer += 55;
                        break;
                    case 0x0D:
                        index_pointer += 60;
                        break;
                    case 0x0E:
                        index_pointer += 60;
                        break;
                    case 0x0F:
                        index_pointer += 65;
                        break;
                    default:
                        Console.WriteLine("Opcode Fx29 is broken! Invalid character requested.");
                        crashed = true;
                        break;
                }
                program_counter += 2;
                return;
            }

            // Fx33 - Load BCD into memory starting at I (this implementation stolen from TJA)
            if ((opcode & 0xF0FF) == 0xF033)
            {
                //Console.WriteLine ("Store binary coded decimal of register x's contents at I, I+1, and I+2");
                memory.WriteByte(index_pointer, (byte)(register[(opcode & 0x0F00 >> 8)] / 100));
                memory.WriteByte(index_pointer + 1, (byte)((register[(opcode & 0x0F00 >> 8)] / 10) % 10));
                memory.WriteByte(index_pointer + 2, (byte)((register[(opcode & 0x0F00 >> 8)] % 100) % 10));
                return;
            }

            // Fx55 - Write registers to memory starting at address I
            if ((opcode & 0xF0FF) == 0xF055)
            {
                //Console.WriteLine ("Storing opcodes in memory.");
                for (int i = 0; i < register.Length; i++)
                {
                    memory.WriteByte(index_pointer + i, register[i]);
                }
                program_counter += 2;
                return;
            }

            // Fx65 - Read registers from memory starting at address I
            if ((opcode & 0xF0FF) == 0xF065)
            {
                //Console.WriteLine ("Reading registers from memory.");
                for (int i = 0; i < register.Length; i++)
                {
                    register[i] = memory.ReadByte(index_pointer + i);
                }
                program_counter += 2;
                return;
            }

            // Unknown Opcode encountered, shit has become real.
            Console.WriteLine("ERROR : Unknown or unsupported Opcode = " + opcode.ToString("X4"));
            crashed = true;
            this.CrashDump();
            Console.WriteLine("Dump completed.");
        }

        public void CrashDump()
        {
            Console.WriteLine("Blam!  Chip-8 crashed.  Damn.\nCrash Dump :\nRegisters :");
            for (int i = 0; i < register.Length; i++)
            {
                Console.WriteLine("V" + i.ToString() + " " + register[i].ToString("X2"));
            }
            Console.WriteLine("\nStack pointer : " + memory.stack_pointer.ToString("X4"));
            Console.WriteLine("Program Counter : " + program_counter.ToString("X4"));
            Console.WriteLine("Indexer : " + index_pointer.ToString("X4") + "\n");
            //memory.CrashDump ();
        }

        public string DumpState()
        {
            string blob = "Registers:\n";
            for (int i = 0; i < register.Length; i++)
            {
                blob += "V" + i.ToString("D2") + " " + register[i].ToString("X2") + "   " + "V" + (++i).ToString("D2") + " " + register[i].ToString("X2") + "\n";
            }
            blob += "PC : " + program_counter.ToString("X4") + " Indexer : " + index_pointer.ToString("X4") + "\nTimers : Sound - " + sound_timer.ToString() + " Delay - " + delay_timer.ToString();
            blob += "\n";
            return blob;
        }

        private bool IsBitSet(byte b, int pos)
        {
            return (b & (1 << pos)) != 0;
        }

        public void DrawSprite(ushort opcode)
        {
            draw_flag = true;
            ushort x = register[(opcode & 0x0F00) >> 8];
            ushort y = register[(opcode & 0x00F0) >> 4];
            ushort height = (ushort)(opcode & 0x000F);

            // Reset hit detection register
            register[15] = 0x00;

            for (int i = 0; i < height; i++)
            {
                byte spriteLine = memory.ReadByte(index_pointer + i);
                for (int j = 0; j < 8; j++)
                {
                    int drawX, drawY;
                    if (x + j > raster.Width - 1)
                        drawX = ((x + j) - (raster.Width));
                    else
                        drawX = x + j;

                    if (y + i > raster.Height - 1)
                        drawY = ((y + i) - (raster.Height));
                    else
                        drawY = y + i;

                    if (IsBitSet(spriteLine, 7 - j))
                    {
                        if (raster.GetPixel(drawX, drawY) == Color.White)
                        {
                            raster.SetPixel(drawX, drawY, Color.Black);
                            register[15] = 0x01;
                        }
                        else
                            raster.SetPixel(drawX, drawY, Color.White);

                    }
                }
            }
        }

        public void ClearScreen()
        {
            draw_flag = true;
            for (int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = 0;
            }
            register[15] = 0;
            UpdateScreen();
        }

        public void UpdateScreen()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i < pixels.Length; i++)
            {
                if (pixels[i] != 0)
                    raster.SetPixel(x, y, Color.White);
                else
                    raster.SetPixel(x, y, Color.Black);
                x++;
                if (x > 63)
                {
                    x = 0;
                    y++;
                }
            }
            draw_flag = false;
        }
        // This adds two bytes and returns the result.
        // It will set the overflow flag and wrap the result around as needed.
        // Setting the carry flag can be stopped by supplying "false" to "setCarry".
        private byte AddBytes(byte byte_one, byte byte_two, bool setCarry = true)
        {
            int value = (byte_one + byte_two);
            if (value > 0xFF)
            {
                if (setCarry)
                    register[15] = 0x01;
            }
            return (byte)value;
        }
        // This will subtract two bytes and set the carry (borrow in this case) flag
        // as needed, if desired.
        private byte SubtractBytes(byte principal, byte subtract, bool setCarry = true)
        {
            if (principal > subtract)
            {
                register[15] = 1;
            }
            else
            {
                register[15] = 0;
            }
            return (byte)(principal - subtract);
        }
    }
}

