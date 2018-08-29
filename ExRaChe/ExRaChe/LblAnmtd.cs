using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Xml.Linq;


namespace ExRaChe
{
    public class LblAnmtd:Label
    {
        public Control MPrnt;
        public Point FtrLocat;
        public Point ActlLocat;
        public Point OriLocat;
        public LblAnmtd(Point p,Control prnt,string txIn)
        {
            OriLocat = ActlLocat = p;
            MPrnt = prnt;
            Text = txIn;
            BackColor = Color.Transparent;
            DoubleBuffered = true;
            AutoSize = true;
            TextAlign = ContentAlignment.MiddleCenter;
            

            BringToFront();
            Paint += (sender, e) =>
            {
                Random rndm = new Random();
                ForeColor = Color.FromArgb(rndm.Next(50, 255), rndm.Next(0, 255), rndm.Next(25, 255));
            };
                //prnt.Controls.Add(this);

            }
        private int moveTickDuration = 100;
        public bool isMoving = false;
        public void ReceiveMove(Move2D e, int moveDuration = 100)
        {

            //S.Cnvs.KillLists3D();
            int ftrX = ActlLocat.X + e.DeltaX;
            int ftrY = ActlLocat.Y + e.DeltaY;
            FtrLocat = new Point(ftrX, ftrY);//X1
            

            moveTickDuration = moveDuration;
            //S.Canvas.Pohyby.Add(this);
            Cnst.S.Movement2D.Add(this);
            isMoving = true;
            if (!Cnst.S.Ticker.Enabled) Cnst.S.Ticker.Start();
        }
        public void Ticker_Tick2()
        {
            if (moveTickDuration > 0)
            {
                int step2dX=FtrLocat.X-ActlLocat.X / moveTickDuration;
                int step2dY = FtrLocat.Y - ActlLocat.Y / moveTickDuration;

                ActlLocat.X += step2dX;
                ActlLocat.Y += step2dY;
                
                PdateMe();//RELOCATION
                {//Functions Influence
                    //if (S.Cnvs.FncIntersect == true) S.Cnvs.IntrsctPrsms();

                    //if (S.Cnvs.FncCmbn == true) S.Cnvs.CmbnPrsms();

                    //if (S.Cnvs.FncDiffr == true) S.Cnvs.DffrPrsms();
                }//Functions Influence
                moveTickDuration--;
            }
            else
            {
                ActlLocat.X = FtrLocat.X;
                ActlLocat.Y = FtrLocat.Y;
                
                
                PdateMe();//RELOCATION
                {//Functions Influence
                    //if (S.Cnvs.FncIntersect == true) S.Cnvs.IntrsctPrsms();

                    //if (S.Cnvs.FncCmbn == true) S.Cnvs.CmbnPrsms();

                    //if (S.Cnvs.FncDiffr == true) S.Cnvs.DffrPrsms();
                }//Functions Influence
                isMoving = false;

            }
        }//FUNKCNI
        public void PdateMe()
        {
            Location = ActlLocat;
            //Invalidate();
        }
        public void RstMe()
        {
            Location = OriLocat;
            Invalidate();
            
        }
        public struct Move2D
        {
            private int deltaX, deltaY;
            public int DeltaX { get { return deltaX; } }
            public int DeltaY { get { return deltaY; } }


            public Move2D(int moveX, int moveY)
            {
                deltaX = moveX;
                deltaY = moveY;

            }
        }
    }
}
