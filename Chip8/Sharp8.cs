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

			PictureBox screen = new PictureBox ();
			screen.Parent = this;
			screen.Location = new Point (0, 0);
			screen.Width = this.Width;
			screen.Height = 32 * 6;
			screen.Image = cpu.raster;
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			base.OnPaint (e);
			//e.Graphics.DrawImage (cpu.raster, 0, 0, 64 * 6, 32 * 6);
			if (running) {
				Emulate ();
				UpdateDebugger ();

			}
			base.Invalidate (true);
			System.Threading.Thread.Sleep (sleep_time);
		}

		void ResetClicked (object sender, EventArgs e)
		{
			cpu.Reset ();
			debugger.Text = "";
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
				Emulate ();
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
		}

		public void Emulate ()
		{
			if (cpu.crashed) {
				cpu.CrashDump ();
				return;
			}
			cpu.RunCycle ();
		}

		private void UpdateDebugger ()
		{
			debugger.Text = cpu.DumpState ();
			debugger.Invalidate ();
		}
	}
}
