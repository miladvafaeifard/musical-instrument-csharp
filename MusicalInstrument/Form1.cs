using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicalInstrument
{
    public partial class Form1 : Form
    {
        readonly SignalGenerator sine = new SignalGenerator()
        {
            Type = SignalGeneratorType.Sin,
            Gain = 0.2
        };
        readonly WaveOutEvent player = new WaveOutEvent();
        private Point CursorPositionOnMouseDown;
        private bool isButtonDown = false;
        public Form1()
        {
            InitializeComponent();

            player.Init(sine);

            trackFrequency.ValueChanged += (sender, ev) => sine.Frequency = trackFrequency.Value;
            trackFrequency.Value = 600;

            trackVolume.ValueChanged += (sender, ev) => player.Volume = trackVolume.Value / 100F;
            trackVolume.Value = 50;

        }

        private void trackVolume_Scroll(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TheMouseDown(object sender, MouseEventArgs e)
        {
            player.Play();

            CursorPositionOnMouseDown = e.Location;
            isButtonDown = true;
        }

        private void TheMouseUp(object sender, MouseEventArgs e)
        {
            player.Stop();
            isButtonDown = true;
        }

        private void panel_MouseMove(object sender, MouseEventArgs e)
        {
            var dX = e.X - CursorPositionOnMouseDown.X;
            var vol = player.Volume + (dX / 1000F);

            var dY = CursorPositionOnMouseDown.Y - e.Y;
            var freg = sine.Frequency + dY;

            if (isButtonDown)
            {
                player.Volume = (vol > 0) ? (vol < 1) ? vol : 1 : 0;
                sine.Frequency = (freg > 100) ? (freg < 1000) ? freg: 1000 : 100;
                trackVolume.Value = (int)Math.Round(player.Volume * 100);
                trackFrequency.Value = (int)Math.Round(sine.Frequency);
            }


            Text = $"Musical Instrument ({dX}, {dY}): ({vol}, {freg}) ";
        }
    }
}
