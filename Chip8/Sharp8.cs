using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sharp8
{
	class MainClass : Form
	{
		private static CHIP8CPU cpu;
		private bool running = false;
		// 17 milliseconds between cycles ends up around
		// 60 cycles a second, which is the speed the counters
		// decrement at.  This is "normal speed".
		private int sleep_time = 17;
		private RichTextBox debugger;
		private Timer shotClock;
		private Graphics g;
		private Button pause;
		private Button step;
		private TextBox rom;

		public static void Main (string[] args)
		{
			cpu = new CHIP8CPU (new CHIP8MMU ("MAZE"));
			Application.Run (new MainClass ());
		}

		private void Render ()
		{

			g.DrawImage (cpu.raster, 0, 0, 384, 192);

		}

		public MainClass ()
		{


			shotClock = new Timer ();
			shotClock.Interval = sleep_time;
			shotClock.Tick += Emulate;
			shotClock.Start ();

			Text = "Sharp8";
			Size = new Size (390, 480);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			CenterToScreen ();

			int buttonWidth = 75;

			pause = new Button ();
			pause.Parent = this;
			pause.Text = "Pause";
			pause.Width = buttonWidth;
			pause.Location = new Point (10, 202);
			pause.Click += PauseClicked;

			step = new Button ();
			step.Parent = this;
			step.Text = "Step";
			step.Width = buttonWidth;
			step.Location = new Point (95, 202);
			step.Click += StepClicked;

			Button speed = new Button ();
			speed.Parent = this;
			speed.Text = "Speed";
			speed.Width = buttonWidth;
			speed.Location = new Point (180, 202);
			speed.Click += SpeedToggle;

			Button reset = new Button ();
			reset.Parent = this;
			reset.Text = "Reset";
			reset.Width = buttonWidth;
			reset.Location = new Point (265, 202);
			reset.Click += ResetClicked;

			debugger = new RichTextBox ();
			debugger.Parent = this;
			debugger.Width = 150;
			debugger.Height = 200;
			debugger.Location = new Point (10, 240);
			debugger.Enabled = false;

			Label rom_label = new Label ();
			rom_label.Parent = this;
			rom_label.Text = "Rom name:";
			rom_label.Location = new Point (200, 240);
			rom_label.Height = 12;

			rom = new TextBox ();
			rom.Parent = this;
			rom.Width = 100;
			rom.Location = new Point (200, 260);
			rom.Text = "MAZE";

			g = this.CreateGraphics ();
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
			g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
			this.DoubleBuffered = true;
		}

		void ResetClicked (object sender, EventArgs e)
		{
			running = false;
			cpu.Reset (rom.Text);
			debugger.Text = "System Reset and paused.";
			Render ();
		}

		public void PauseClicked (object sender, EventArgs e)
		{
			running = !running;
		}

		public void StepClicked (object sender, EventArgs e)
		{
			if (!running && !cpu.crashed) {
				cpu.RunCycle ();
				UpdateDebugger ();
				Render ();
			}
		}

		void SpeedToggle (object sender, EventArgs e)
		{
			if (sleep_time == 17) {
				sleep_time = 1;
			} else {
				sleep_time = 17;
			}
			shotClock.Interval = sleep_time;
		}

		public void Emulate (object sender, EventArgs e)
		{
			Emulate ();
		}

		public void Emulate ()
		{
			if (running) {
				pause.Text = "Pause";
				step.Enabled = false;
			} else {
				pause.Text = "Run";
				step.Enabled = true;
			}

			if (cpu.crashed) {
				debugger.Text = "Sharp8 has crashed.  Damn.";
				cpu.CrashDump ();
				return;
			}
			if (running) {
				cpu.RunCycle ();
				UpdateDebugger ();
				Render ();
			}
		}

		private void UpdateDebugger ()
		{
			debugger.Text = cpu.DumpState ();
		}
	}
}
