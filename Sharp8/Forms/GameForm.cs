using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Sharp8
{
    public partial class GameForm : Form
    {
        private bool running = false;

        private Graphics g;
        private CHIP8CPU cpu;
        private Debugger debugger;
        private bool gameLoaded = false;

        private int drawScale = 6;

        public GameForm()
        {
            InitializeComponent();
            ResizeDisplay(drawScale);
            cpu = new CHIP8CPU();
            Application.Idle += GameLoop;
            //this.KeyDown += (o, e) => { Console.Write(e. };
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
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.CompositingMode = CompositingMode.SourceCopy;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                if (running && !cpu.crashed)
                {
                    StepEmulation();
                    RenderEmulation();
                }
            }
        }

        private void StepEmulation()
        {
            cpu.RunCycle();
            if (debugger != null)
                debugger.debuggerTextBox.Text = cpu.DumpState();
        }

        private void RenderEmulation()
        {
            g.DrawImage(cpu.raster, 0, menuStrip1.Height, 64 * drawScale, 32 * drawScale);
        }

        private void ResizeDisplay(int scale)
        {
            drawScale = scale;
            ClientSize = new Size(64 * drawScale, (32 * drawScale) + menuStrip1.Height);
            InitGraphics();
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

            if (romSelection.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    cpu.LoadRom(romSelection.FileName.ToString());
                    resetEmulatorMenuItem.Enabled = true;
                    closeRomMenuItem.Enabled = true;
                    running = true;
                    gameLoaded = true;
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
            closeRomMenuItem.Enabled = false;
            resetEmulatorMenuItem.Enabled = false;
            if (debugger != null)
                debugger.debuggerTextBox.Clear();
            gameLoaded = false;
            cpu = new CHIP8CPU();
            RenderEmulation();
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

        private void debuggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            debugger = new Debugger();
            debugger.Show();
        }

        private void pauseMenuItem_Click(object sender, EventArgs e)
        {
            if (gameLoaded)
            {
                (sender as ToolStripMenuItem).Checked = running;
                running = !running; 
            }
        }

        private void stepMenuItem_Click(object sender, EventArgs e)
        {
            if (!running && gameLoaded)
            {
                StepEmulation();
                RenderEmulation(); 
            }
        }

        private void x10SizeMenuItem_Click(object sender, EventArgs e)
        {
            ResizeDisplay(10);
        }
    }
}
