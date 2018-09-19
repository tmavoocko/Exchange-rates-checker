using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Xml;
using System.Xml.Linq;
using System.ServiceModel.Syndication;
using System.Net;
using System.Windows.Forms.DataVisualization.Charting;

//using System.Windows.Forms.DataVisualization.Charting;
using System.Diagnostics;

namespace ExRaChe
{
    public class Chrt:Panel
    {
        private Color Clr = new Color();
        private Size Szz = new Size();
        private List<Rectangle> thGrid = new List<Rectangle>();
        Chrt(Color clr, Size szz)
        {
            Clr = clr;
            Szz = szz;
            ChrtDSGN();
        }
        public void ChrtDSGN()
        {
            Dock = DockStyle.Fill;
            {//The grid - Math
                {//The grid - Math
                    int rws = (Height - 40) / 40;
                    int clmns= (Width - 40) / 40;
                    Rectangle frst = new Rectangle(20, 20, 40, 40);
                }//The grid - Math
            }//The grid - Math

            Paint += (sender, e) => 
            {
                e.Graphics.DrawRectangle(new Pen(Clr, 1), new Rectangle(22, 22, 22, 22));
            };
        }
    }
}
