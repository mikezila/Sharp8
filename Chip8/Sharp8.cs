using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sharp8
{
	class MainClass : Form
	{
		private static CHIP8CPU cpu;
		private bool running = true;
		private int sleep_time = 17;

		public static void Main (string[] args)
		{
			cpu = new CHIP8CPU (new CHIP8MMU ("INVADERS"));
			Application.Run (new MainClass ());

		}

		public MainClass ()
		{
			Text = "Sharp8";
			Size = new Size (64 * 6, 480);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			CenterToScreen ();

			int buttonWidth = 100;

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
			step.Location = new Point (110, 202);
			step.Click += StepClicked;

			Button speed = new Button ();
			speed.Parent = this;
			speed.Text = "Speed";
			speed.Width = buttonWidth;
			speed.Location = new Point (210, 202);
			speed.Click += SpeedToggle;
		}

		protected override void OnPaint (PaintEventArgs e)
		{
			base.OnPaint (e);
			e.Graphics.DrawImage (cpu.raster, 0, 0, 64 * 6, 32 * 6);
			if (running) {
				Emulate ();
			}
			this.Invalidate (true);
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
			if (!running)
				Emulate ();
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
			cpu.UpdateScreen ();
			System.Threading.Thread.Sleep (sleep_time);

		}
	}
}
