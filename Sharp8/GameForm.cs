using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Sharp8
{
    public partial class GameForm : Form
    {
        private bool running = false;

        private Graphics g;
        private CHIP8CPU cpu = new CHIP8CPU();

        private int drawScale = 6;

        public GameForm()
        {
            InitializeComponent();
            InitGraphics();
            ClientSize = new Size(64 * drawScale, (32 * drawScale) + menuStrip1.Height);
            Application.Idle += GameLoop;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PeekMsg
        {
            public IntPtr hWnd;
            public Message msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool PeekMessage(out PeekMsg msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        private bool IsApplicationIdle()
        {
            PeekMsg result = new PeekMsg();
            return !PeekMessage(out result, IntPtr.Zero, 0, 0, 0);
        }

        private void InitGraphics()
        {
            g = CreateGraphics();
            g.PixelOffsetMode = PixelOffsetMode.Half;
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                if (running && !cpu.crashed)
                    StepEmulation();
            }
        }

        private void StepEmulation()
        {
            cpu.RunCycle();
            g.DrawImage(cpu.raster, 0, menuStrip1.Height, 64 * drawScale, 32 * drawScale);
        }

        private void ResizeDisplay(int scale)
        {
            drawScale = scale;
            ClientSize = new Size(64 * drawScale, (32 * drawScale) + menuStrip1.Height);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void aboutSharp8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog romSelection = new OpenFileDialog();
            romSelection.Multiselect = false;

            if(romSelection.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    cpu.LoadRom(romSelection.FileName.ToString());
                    resetEmulatorMenuItem.Enabled = true;
                    running = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong.\n" + ex.Message.ToString());
                }
            }
        }

        private void closeRomMenuItem_Click(object sender, EventArgs e)
        {
            running = false;
            resetEmulatorMenuItem.Enabled = false;
            cpu = new CHIP8CPU();
        }

        private void quadDisplayScaleMenuItem_Click(object sender, EventArgs e)
        {
            ResizeDisplay(4);
        }

        private void sexDisplayScaleMenuItem_Click(object sender, EventArgs e)
        {
            ResizeDisplay(6);
        }

        private void octoSizeMenuItem_Click(object sender, EventArgs e)
        {
            ResizeDisplay(8);
        }

        private void resetEmulatorMenuItem_Click(object sender, EventArgs e)
        {
            cpu.Reset();
        }
    }
}
