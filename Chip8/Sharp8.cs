using System;
using System.Windows.Forms;
using System.Drawing;

namespace Sharp8
{
	class MainClass : Form
	{
		private static CHIP8CPU cpu;

		public MainClass ()
		{
			Text = "Sharp8";
			Size = new Size (320, 240);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			MinimizeBox = false;
			CenterToScreen ();

			Button step = new Button ();
			step.Parent = this;
			step.Text = "Step";
			step.Location = new Point (10, 10);
			step.Click += StepClicked;

			Button ten_steps = new Button ();
			ten_steps.Parent = this;
			ten_steps.Text = "Step (10)";
			ten_steps.Location = new Point (200, 10);
			ten_steps.Click += TenStepsClicked;
		}

		public static void Main (string[] args)
		{
			cpu = new CHIP8CPU (new CHIP8MMU ("INVADERS"));
			Application.Run (new MainClass ());

		}

		protected override void OnPaint (PaintEventArgs e)
		{
			base.OnPaint (e);
			e.Graphics.DrawImage (cpu.raster, 50, 50, 64 * 4, 32 * 4);
		}

		public void TenStepsClicked (object sender, EventArgs e)
		{
			Emulate (10);
			this.Invalidate (true);
		}

		public void StepClicked (object sender, EventArgs e)
		{
			Emulate ();
			this.Invalidate (true);
		}

		public static void Emulate (int cycles = 1)
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
