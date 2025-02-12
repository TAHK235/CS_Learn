﻿using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace PieChart1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            ArrayList data = new ArrayList();
            data.Add(new PieChartElement("East", (float)50.75));
            data.Add(new PieChartElement("West", (float)22));
            data.Add(new PieChartElement("North", (float)72.32));
            data.Add(new PieChartElement("South", (float)12));
            data.Add(new PieChartElement("Central", (float)44));

            chart.Image = drawPieChart(data, new Size(chart.Width, chart.Height));
        }

        private Image drawPieChart(ArrayList elements, Size s)
        {
            Bitmap bm = new Bitmap(s.Width, s.Height);
            Graphics g = Graphics.FromImage(bm);

            // Calculate total value of all rows
            float total = 0;

            foreach (PieChartElement e in elements)
            {
                if (e.value < 0)
                {
                    throw new ArgumentException("All elements must have positive values");
                }
                total += e.value;
            }

            if (!(total > 0))
            {
                throw new ArgumentException("Must provide at least one PieChartElement with a positive value");
            }

            // Define the rectangle that the pie chart will use
            Rectangle rect = new Rectangle(1, 1, s.Width - 2, s.Height - 2);

            Pen p = new Pen(Color.Black, 1);

            // Draw the first section at 0 degrees
            float startAngle = 0;

            // Draw each of the pie shapes
            foreach (PieChartElement e in elements)
            {
                // Calculate the degrees that this section will consume,
                // based on the percentage of the total
                float sweepAngle = (e.value / total) * 360;

                // Draw the pie shape
                g.DrawPie(p, rect, startAngle, sweepAngle);

                // Calculate the angle for the next pie shape by adding
                // the current shape's degrees to the previous total.
                startAngle += sweepAngle;
            }
            return bm;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // Display the Save dialog
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = ".jpg";
            saveDialog.Filter = "JPEG files (*.jpg)|*.jpg;*.jpeg|All files (*.*)|*.*";

            if (saveDialog.ShowDialog() != DialogResult.Cancel)
            {
                // Define the Bitmap, Graphics, Font, and Brush for copyright logo
                Bitmap bm = (Bitmap)chart.Image;
                Graphics g = Graphics.FromImage(bm);
                Font f = new Font("Arial", 12);

                // Create the foreground text brush
                Brush b = new SolidBrush(Color.White);

                // Create the backround text brush
                Brush bb = new SolidBrush(Color.Black);

                // Add the copyright text background
                string ct = "Copyright 2006, Contoso, Inc.";
                g.DrawString(ct, f, bb, 4, 4);
                g.DrawString(ct, f, bb, 4, 6);
                g.DrawString(ct, f, bb, 6, 4);
                g.DrawString(ct, f, bb, 6, 6);

                // Add the copyright text foreground
                g.DrawString(ct, f, b, 5, 5);

                // Save the image to the specified file in JPEG format
                bm.Save(saveDialog.FileName, ImageFormat.Jpeg);
            }
        }
    }
}
