# Sharp8

A somewhat handy Chip8 emulator.

### What's complete and what's not?
All of the opcodes are emulated, save for the one that jumps to a machine code subrutine.  As far as I know none of the Chip8 software you're going to find in the wild today uses it.  From what I can tell it was used to run raw metal (as opposed to Chip8) code on the original machines that ran Chip8 games.  Chip8 was a sort of virtual machine, after all.  I do not plan to emulate these machine code operations, as it would require an entire seperate emulator of the original machines that ran Chip8

There's a WinForms UI that shows the display and a debugger with the status of the registers and counters.  The UI is a little buggy on Windows, but it works correctly on OS X via Mono.  I assume it will work via Mono on Linux as well, but it is not tested.

The gamepad isn't working yet.  At current I fake a gamepad press to proceed through the halts that wait for gamepad input.  It looks like some games do not like having a button stuck down like this.  It breaks some games (Tetris blocks spin at the top of the well) and skips the cool title screens of some games (Like the Invaders marque).

### Why?
I've always wanted to write a NES or GameBoy emulator, but despite many attempts those projects are still above my skill level.  I'm doing this as a way to improve my skills.

### What's required?
Just the ability to run C#.  It was actually developed on OS X using Xamarin/Mono, so Windows is certainly not a requirement.  It currently works as it should on OS X, and is buggy on Windows.  I will find a way to improve the UI shorty.
