using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sharp8
{
	class MainClass : Form
	{
		private static CHIP8CPU cpu;
		private bool running = true;
		// 17 milliseconds between cycles ends up around
		// 60 cycles a second, which is the speed the counters
		// decrement at.  This is "normal speed".
		private int sleep_time = 17;
		RichTextBox debugger;
		Timer shotClock;

		public static void Main (string[] args)
		{
			if (args.Length != 1) {
				Console.WriteLine ("Need exactly one argument.\nAnd make it a valid Chip8 ROM.");
				return;
			} else {
				cpu = new CHIP8CPU (new CHIP8MMU (args [0]));
				Application.Run (new MainClass ());
			}
		}

		public MainClass ()
		{
			shotClock = new Timer ();
			shotClock.Interval = sleep_time;
			shotClock.Tick += Emulate;
			shotClock.Start ();

			Text = "Sharp8";
			Size = new Size (64 * 6, 480);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			CenterToScreen ();

			int buttonWidth = 75;

			Button pause = new Button ();
			pause.Parent = this;
			pause.Text = "Pause";
			pause.Width = buttonWidth;
			pause.Location = new Point (10, 202);
			pause.Click += PauseClicked;

			Button step = new Button ();
			step.Parent = this;
			step.Text = "Step";
			step.Width = buttonWidth;
			step.Location = new Point (95, 202);
			step.Click += StepClicked;
			step.Enabled = false;

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
			debugger.Width = this.Width - 30;
			debugger.Height = 200;
			debugger.Location = new Point (10, 240);
			debugger.Enabled = false;
		
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			base.OnPaint (e);
			e.Graphics.DrawImage (cpu.raster, 0, 0, 64 * 6, 32 * 6);
		}

		void ResetClicked (object sender, EventArgs e)
		{
			cpu.Reset ();
			debugger.Text = "System Reset.";
			Invalidate (true);
		}

		public void PauseClicked (object sender, EventArgs e)
		{
			Button button = (Button)sender;
			if (running)
				button.Text = "Run";
			else
				button.Text = "Pause";
			running = !running;
		}

		public void StepClicked (object sender, EventArgs e)
		{
			if (!running) {
				//Emulate ();
				UpdateDebugger ();
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
			if (cpu.crashed) {
				cpu.CrashDump ();
				return;
			}
			if (running) {
				cpu.RunCycle ();
				UpdateDebugger ();
				Invalidate (true);
			}
		}

		private void UpdateDebugger ()
		{
			debugger.Text = cpu.DumpState ();
		}
	}
}
