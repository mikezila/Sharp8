# Sharp8

A somewhat handy Chip8 emulator.

### What's complete and what's not?
All of the opcodes are emulated, save for the one that jumps to a machine code subrutine.  As far as I know none of the Chip8 software you're going to find in the wild today uses it.  From what I can tell it was used to run raw metal (as opposed to Chip8) code on the original machines that ran Chip8 games.  Chip8 was a sort of virtual machine, after all.  I do not plan to emulate these machine code operations, as it would require an entire seperate emulator of the original machines that ran Chip8.

There's a winforms UI that lets you load and run games, along with a debugger you can open and watch the registers.  You can pause the emulator, open the debugger, and press S to step the emulation.

The opcode to render the screen is buggy, and will skip drawing sprites that would wrap around the screen.  This will cause some games to crash or act oddly.  I need to overhaul the screen rendering so that it's less buggy.  The opcode to load the address for the built-in fontset is also not working correctly, and that needs to be looked into.

The gamepad isn't working yet.  Right now the emulator will get stuck anywhere that waits for gamepad input.

The emulator seems to run many games correctly otherwise, but it doesn't pass any of the Chip8 "test" roms I've been able to find, likely due to the above.

### Why?
I've always wanted to write a NES or GameBoy emulator, but despite many attempts those projects are still above my skill level.  I'm doing this as a way to improve my skills.

### What's required?
It's being developed on Windows 7 using .Net, but it "should" work on Mac or Linux via Mono.  There may be issues with the graphics being painted on those platforms.  For now I'd recommend just running it on Windows.  I'd love to make a native Mac version once I have the emulator core working more or less 100%, and I'll try to make a native (or MonoMac) verison then.
