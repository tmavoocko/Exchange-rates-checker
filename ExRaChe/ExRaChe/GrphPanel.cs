using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace ExRaChe
{
    public class GrphPanel : Panel
    {
        private static Font TitleFont = new Font("Litograph", 10, FontStyle.Bold);
        private static Font ContentFont = new Font("Litograph", 10, FontStyle.Regular);
        public float FontSize
        {
            get { return TitleFont.SizeInPoints; }
            set
            {
                float input = value;
                if (input < 10) input = 10;
                if (input > 40) input = 40;
                TitleFont = new Font(TitleFont.FontFamily, input, TitleFont.Style);
                //TitleLabel.Font = TitleFont;
                ContentFont = new Font(ContentFont.FontFamily, input, ContentFont.Style);
                //ContentLabel.Font = ContentFont;
            }
        }
        //NEW
        private List<PriceData> Tmprr = new List<PriceData>();
        private Panel GrphCnvs = new Panel() { Dock = DockStyle.Fill, Size = new Size(50, 25), Location = new Point(0, 0), BackColor = Color.FromArgb(180, Color.Black) };

        private PictureBox GrphPlace = new PictureBox() { Dock = DockStyle.Fill, Size = new Size(50, 25), Location = new Point(0, 0), BackColor = Color.FromArgb(180, Color.AliceBlue) };


        //OLD-OLD------------------------------------------------------
        private Matrix WtoDMatrix, DtoWMatrix;// The coordinate mappings

        // --design colors
        private Color ClrdBck = Color.FromArgb(132, 45, 37); // design colors
        private Color ClrdFrnt = Color.FromArgb(232, 145, 137);
        private Color ClrBasePenelHolder = Color.FromArgb(255, Color.WhiteSmoke);// --design colors
        private Control LstCntrl = new Control();//Old
        private List<Panel> PntsAll = new List<Panel>();
        private Point SpwnLck = new Point(10, 10);
        public GrphPanel(List<PriceData> tmprr)//NEW
        {
            Dock = DockStyle.Fill;
            Tmprr = tmprr;
            MinimumSize = new Size(30, 20);
            //Controls.Add(GrphPlace);







            Controls.Add(GrphCnvs);
            //DrwGrph();
            //DrawGraph();
            ClientSizeChanged += (sender, e) =>
            {
                //DrwGrph();
            };
        }//NEW
        // Draw the graph.
        private void DrwGrph()
        {
            PntsAll.Clear();
            for (int i = 0; i < Tmprr.Count; i++)
            {
                Panel point = new Panel() { Location = new Point(SpwnLck.X * i, Height - (SpwnLck.Y * i)), Size = new Size(3, 3), BackColor = ClrdFrnt };
                point.Name = Tmprr[i].Date.ToString() + " " + Tmprr[i].Price.ToString();
                point.BringToFront();
                point.MouseHover += (sender, e) =>
                {
                    ToolTip hnt = new ToolTip();
                    hnt.SetToolTip(point, Name);
                };
                Controls.Add(point);
                PntsAll.Add(point);
            }
        }



        private Bitmap GraphBm = null;
        private void DrawGraph()
        {


            //}
            //txtStartDate.Invalidate();
            int wid = GrphPlace.ClientSize.Width;
            int hgt = GrphPlace.ClientSize.Height;
            GraphBm = new Bitmap(wid, hgt);
            using (Graphics gr = Graphics.FromImage(GraphBm))
            {
                gr.SmoothingMode = SmoothingMode.AntiAlias;
                gr.Clear(Color.White);

                // Scale the data to fit.
                int num_points = Tmprr.Count;
                float min_price = (float)Tmprr.Min(data => data.Price);
                float max_price = (float)Tmprr.Max(data => data.Price);
                const int margin = 10;

                WtoDMatrix = MappingMatrix(
                    0, num_points - 1, min_price, max_price,
                    margin, wid - margin, margin, hgt - margin);
                gr.Transform = WtoDMatrix;

                DtoWMatrix = WtoDMatrix.Clone();
                DtoWMatrix.Invert();

                // Draw the graph.
                using (Pen pen = new Pen(Color.Black, 0))
                {
                    // Draw tic marks.
                    PointF[] pts = { new PointF(10, 10) };
                    DtoWMatrix.TransformVectors(pts);
                    float dy = pts[0].Y;
                    float dx = pts[0].X;

                    for (int x = 0; x < Tmprr.Count; x++)
                    {
                        gr.DrawLine(pen, x, min_price, x, min_price + dy);
                    }
                    for (int y = (int)min_price; y <= (int)max_price; y++)
                    {
                        gr.DrawLine(pen, 0, y, dx, y);
                    }

                    // Get a small distance in world coordinates.
                    dx = Math.Abs(dx / 5);
                    dy = Math.Abs(dy / 5);

                    // Draw the data.
                    PointF[] points = new PointF[num_points];
                    for (int i = 0; i < num_points; i++)
                    {
                        float price = (float)Tmprr[i].Price;
                        points[i] = new PointF(i, price);
                        gr.FillRectangle(Brushes.Red,
                            i - dx, price - dy, 2 * dx, 2 * dy);
                    }
                    pen.Color = Color.Blue;
                    gr.DrawLines(pen, points);
                }
            }

            // Display the result.
            GrphPlace.Image = GraphBm;
        }
        // Return a mapping matrix.
        private Matrix MappingMatrix(
            float wxmin, float wxmax, float wymin, float wymax,
            float dxmin, float dxmax, float dymin, float dymax)
        {
            RectangleF rect = new RectangleF(
                wxmin, wymin,
                wxmax - wxmin, wymax - wymin);
            PointF[] points =
            {
                new PointF(dxmin, dymax),
                new PointF(dxmax, dymax),
                new PointF(dxmin, dymin),
            };
            return new Matrix(rect, points);
        }

        // Display the data in a tooltip.
        private int LastTipNum = -1;
        // Draw date and price lines for the mouse position.
        private void ShowDatePriceLines()
        {
            try
            {
                if (LastTipNum < 0) return;

                Bitmap bm = (Bitmap)GraphBm.Clone();
                using (Graphics gr = Graphics.FromImage(bm))
                {
                    gr.Transform = WtoDMatrix;
                    PriceData data = Tmprr[LastTipNum];
                    using (Pen pen = new Pen(Color.Red, 0))
                    {
                        gr.DrawLine(pen, LastTipNum, 0, LastTipNum, 100000);
                        float price = (float)data.Price;
                        gr.DrawLine(pen, 0, price, 100 * Tmprr.Count, price);
                    }
                }

                GrphPlace.Image = bm;
            }
            catch (Exception) { }

        }
    }
}
