# Sharp8

A somewhat handy Chip8 emulator.

### What's complete and what's not?
All of the opcodes are emulated, save for the one that jumps to a machine code subrutine.  As far as I know none of the Chip8 software you're going to find in the wild today uses it.  From what I can tell it was used to run raw metal (as opposed to Chip8) code on the original machines that ran Chip8 games.  Chip8 was a sort of virtual machine, after all.  I do not plan to emulate these machine code operations, as it would require an entire seperate emulator of the original machines that ran Chip8

Grahics are working, but only as a bitmap dump at the end of emulation.  From what I can tell graphics are being rendered correctly, but I'll really need to see it in motion to know for sure.

There's a laughable trace of running opcodes barfed into the console when the emulator is running.  I plan to turn this isn't a debugger/disassembler next.  I also do a crash dump at the end of emulation, though I haven't had the emulator legit crash yet.

The gamepad is working yet.  At current I fake a gamepad press with 0x04 (a number I pulled out of the air) to proceed through the halts that wait for gamepad input.  It looks like some games do not like having a button stuck down like this.  Won't know for sure until it's added and working, though.

### Why?
I've always wanted to write a NES or GameBoy emulator, but despite many attempts those projects are above my skill level.  I'm doing this as a way to improve my skills.

### What's required?
Just the ability to run .Net C#.  It was actually developed on OS X using Xamarin/Mono, so Windows is certainly not a requirement.  There's isn't a GUI at the moment.  Not sure how I'll implement that, but I wil.
