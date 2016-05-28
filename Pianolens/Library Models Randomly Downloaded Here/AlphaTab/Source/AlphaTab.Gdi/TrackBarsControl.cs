﻿using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using alphatab.model;

namespace AlphaTab.Gdi
{
    class TrackBarsControl : Control
    {
        private static readonly Size BlockSize = new Size(25, 25);
        private bool[] _usedBars;
        private Color _startColor;
        private Color _endColor;

        public TrackBarsControl(Track track)
        {
            SetStyle(ControlStyles.FixedHeight, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            base.DoubleBuffered = true;
            base.BackColor = Color.FromArgb(93, 95, 94);

            _usedBars = new bool[track.bars.length];
            for (int barI = 0; barI < track.bars.length; barI++)
            {
                Bar bar = (Bar)track.bars[barI];
                _usedBars[barI] = false;

                for (int voiceI = 0; voiceI < bar.voices.length && (!_usedBars[barI]); voiceI++)
                {
                    Voice voice = (Voice)bar.voices[voiceI];
                    for (int beatI = 0; beatI < voice.beats.length; beatI++)
                    {
                        Beat b = (Beat)voice.beats[beatI];
                        if (!b.isRest())
                        {
                            _usedBars[barI] = true;
                        }
                    }

                }
            }
            PerformLayout();
            Width = BlockSize.Width * _usedBars.Length;
            Height = BlockSize.Height;
            MinimumSize = BlockSize;

            SetColor(track.color);
        }

        private void SetColor(alphatab.platform.model.Color color)
        {
            var baseColor = Color.FromArgb(color.getR(), color.getG(), color.getB());
            double h, s, l;
            ColorTools.RGB2HSL(baseColor, out h, out s, out l);

            _startColor = ColorTools.HSL2RGB(h, System.Math.Max(0, System.Math.Min(1, s - 0.2)),
                System.Math.Max(0, System.Math.Min(1, l + 0.2)));
            _endColor = ColorTools.HSL2RGB(h, System.Math.Max(0, System.Math.Min(1, s - 0.2)),
                System.Math.Max(0, System.Math.Min(1, l - 0.2)));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (_usedBars == null) return;

            using (LinearGradientBrush brush = new LinearGradientBrush(DisplayRectangle, _startColor, _endColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, new Rectangle(0, 0, _usedBars.Length * BlockSize.Width, BlockSize.Height));
            }

            using (Pen pen = new Pen(Color.FromArgb(75,255,255,255)))
            {
                for (int i = 0; i < _usedBars.Length; i++)
                {
                    e.Graphics.DrawLine(pen, (i + 1) * BlockSize.Width, 0, (i + 1) * BlockSize.Width, BlockSize.Height);
                }
                pen.Color = Color.FromArgb(51, 51, 51);
                e.Graphics.DrawLine(pen, 0, Height - 1, _usedBars.Length * BlockSize.Width, Height - 1);
            }

        }
    }
}
