using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ServiceModel.Syndication;
using System.Xml;
using System.Xml.Linq;

namespace ExRaChe
{
    public class NewsPanel : Panel
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
        private Panel Hrzntl = new Panel();
        private Label Ttle = new Label();
        private Label ttle;
        private Label NNw = new Label();
        private Label UrlImg = new Label();
        private PictureBox shwPicture = new PictureBox() { Dock = DockStyle.Top, MinimumSize = new Size(100, 100), SizeMode = PictureBoxSizeMode.Zoom, Visible = true };
        private RichTextBox shwSummary;
        private Panel allwaysShown = new Panel();
        private Panel btnHldr = new Panel();
        private Button icn = new Button();
        private Label pstdTime = new Label();
        private bool Fake = false;
        private Size MSzz = new Size();
        
        public List<string> NwsAll = new List<string>();
        public Panel smtimsShown = new Panel();
        public bool ShwNws = false;
        // --design colors
        public Color ClrdBck = Color.FromArgb(132, 45, 37); // design colors
        public Color ClrdFrnt = Color.FromArgb(232, 145, 137);
        public Color ClrBasePenelHolder = Color.FromArgb(255, Color.WhiteSmoke);// --design colors
        public Control LstCntrl = new Control();//NEW

        public NewsPanel()//NEW
        {
            Dock = DockStyle.Fill;

            //Controls.Add(TitlePanel);
            //TitlePanel.Controls.Add(TitleLabel);            
            //Controls.Add(ContentPanel);            
            //ContentPanel.Controls.Add(ContentLabel);
            {//Hrzntl
                Hrzntl.Width = ClientSize.Width;
                Hrzntl.Height = ClientSize.Height;

                Hrzntl.BackColor = ClrdBck;
                Hrzntl.Dock = DockStyle.Fill;
                Hrzntl.BringToFront();
                Hrzntl.Controls.Clear();
                Hrzntl.BringToFront();
                {//DSG



                    smtimsShown.Size = LstCntrl.Size;
                    smtimsShown.Width = LstCntrl.Width - 1;
                    smtimsShown.Location = new Point(0, 0);
                    smtimsShown.Dock = DockStyle.Fill;
                    {//SomeTimesShownDSGN
                        UrlImg.TextChanged += (sender, e) =>
                        {
                            if (UrlImg.Text != "" && Fake == false)
                            {

                                shwPicture.Load(UrlImg.Text);

                            }

                        };
                        //POLADIT CONTENT FONT
                        shwSummary = new RichTextBox() { Width = smtimsShown.Width, Height = smtimsShown.Height, Location = new Point(0, 0), Dock = DockStyle.Fill, Text = NNw.Text, BackColor = ClrdBck, ForeColor = ClrdFrnt, Font = ContentFont };
                        NNw.TextChanged += (sender, e) =>
                        {
                            shwSummary.Text = NNw.Text;
                        };
                        LstCntrl = shwSummary;
                        smtimsShown.Controls.Add(LstCntrl);
                        smtimsShown.Controls.Add(shwPicture);

                    }//SomeTimesShownDSGN
                    LstCntrl = smtimsShown;
                    Hrzntl.Controls.Add(LstCntrl);
                    smtimsShown.Hide();

                    allwaysShown = new Panel();
                    allwaysShown.BackColor = ClrdBck;
                    allwaysShown.Height = Hrzntl.Height;
                    allwaysShown.Width = Hrzntl.Width;
                    allwaysShown.Location = new Point(0, 0);
                    allwaysShown.Dock = DockStyle.Fill;
                    {//AlwaysShownDSGN
                        Size szz = new Size(allwaysShown.Width - (allwaysShown.Width / 6), LstCntrl.Height);
                        Point spwn = new Point(LstCntrl.Right, LstCntrl.Top);
                        ttle = new Label() { TextAlign = ContentAlignment.MiddleCenter, Size = szz, Location = new Point(0, 0), Text = Ttle.Text, BackColor = ClrdBck, ForeColor = ClrdFrnt, Font = TitleFont };
                        Ttle.TextChanged += (sender, e) =>
                        {
                            ttle.Text = Ttle.Text;
                        };
                        ttle.Dock = DockStyle.Fill;
                        ttle.MouseDown += (sender, e) =>
                        {
                            if (e.Button == MouseButtons.Left)
                            {
                                if (ShwNws == false)
                                {
                                    {//Show
                                        ShwNws = true;

                                        smtimsShown.Dock = DockStyle.Fill;
                                        allwaysShown.Dock = DockStyle.Top;
                                        allwaysShown.Height = MSzz.Height / 2; //(MSzz.Height - F.STATUS.Height) / 2;

                                        Height = MSzz.Height * 3;
                                        smtimsShown.Show();

                                    }//Show
                                }
                                else
                                {
                                    {//Hide
                                        ShwNws = false;
                                        smtimsShown.Invalidate();
                                        Height = MSzz.Height;// + S.F.STATUS.Height;
                                        smtimsShown.Dock = DockStyle.Bottom;
                                        smtimsShown.Hide();
                                        allwaysShown.Height = (MSzz.Height);// - S.F.STATUS.Height);
                                        allwaysShown.Dock = DockStyle.Fill;
                                    }//Hide
                                }

                            }


                        };


                        LstCntrl = ttle;
                        allwaysShown.Controls.Add(LstCntrl);

                        {
                            //Panel btnHldr = new Panel();
                            btnHldr.Height = allwaysShown.Height;
                            btnHldr.Width = allwaysShown.Width / 6;
                            btnHldr.Location = new Point(0, 0);
                            btnHldr.BackColor = ClrdBck;
                            btnHldr.Dock = DockStyle.Left;

                            icn = new Button();
                            icn.Text = "Icone";
                            icn.Size = new Size(btnHldr.Width, btnHldr.Height / 2);
                            icn.Location = new Point(0, 0);
                            icn.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                            btnHldr.ClientSizeChanged += (sender, e) => { icn.Width = btnHldr.Width; icn.Height = btnHldr.Height / 2; };
                            icn.BackColor = ClrdBck;
                            icn.ForeColor = ClrdFrnt;
                            icn.Font = ContentFont;
                            icn.Click += (sender, e) =>
                            {
                                ToolTip hnt = new ToolTip();
                                hnt.SetToolTip(icn, "Icon button was clicked");

                            };

                            btnHldr.Controls.Add(icn);



                            pstdTime = new Label();
                            pstdTime.BackColor = ClrdBck;
                            pstdTime.ForeColor = ClrdFrnt;
                            pstdTime.Font = ContentFont;
                            pstdTime.TextAlign = ContentAlignment.MiddleCenter;
                            pstdTime.Text = "42 sec.";
                            pstdTime.Size = new Size(btnHldr.Width, btnHldr.Height / 2);
                            pstdTime.Location = new Point(0, icn.Bottom);
                            pstdTime.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                            btnHldr.ClientSizeChanged += (sender, e) =>
                            {
                                pstdTime.Location = new Point(0, icn.Bottom);
                                pstdTime.Width = btnHldr.Width; pstdTime.Height = btnHldr.Height / 2;
                            };

                            pstdTime.MouseHover += (sender, e) =>
                            {
                                ToolTip hnt = new ToolTip();
                                hnt.SetToolTip(pstdTime, "Time, that elapsed from the Post Time");
                            };

                            btnHldr.Controls.Add(pstdTime);
                            LstCntrl = btnHldr;
                            allwaysShown.Controls.Add(LstCntrl);
                        }//btnholder

                        LstCntrl = allwaysShown;
                        Hrzntl.Controls.Add(LstCntrl);
                    }//AlwaysShownDSGN

                    {//HrzBottom
                        //Hrzntl.Controls.Add(S.F.STATUS);
                        //Height += S.F.STATUS.Height;
                    }//HrzBottom

                    ClientSizeChanged += (sender, e) =>
                    {
                        if (Width > MSzz.Width && ShwNws == false)
                        {
                            {//btnholder
                                btnHldr.Height = allwaysShown.Height / 2;
                                btnHldr.Width = allwaysShown.Width / 3;
                                btnHldr.Location = new Point(0, 0);
                                btnHldr.BackColor = ClrdBck;
                                btnHldr.Dock = DockStyle.Left;
                                //
                                icn.Size = new Size(btnHldr.Width / 2, btnHldr.Height);
                                icn.Location = new Point(0, 0);
                                icn.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                                icn.Invalidate();

                                //

                                pstdTime.Size = new Size(btnHldr.Width / 2, btnHldr.Height);
                                pstdTime.Location = new Point(icn.Right, 0); pstdTime.Anchor = AnchorStyles.Top;
                                pstdTime.Invalidate();
                                btnHldr.Invalidate();
                            }//btnholder
                        }
                        if (Height > MSzz.Height && ShwNws == false)
                        {
                            {//btnHldr
                                btnHldr.Height = allwaysShown.Height;
                                btnHldr.Width = allwaysShown.Width / 6;
                                btnHldr.Location = new Point(0, 0);
                                btnHldr.Dock = DockStyle.Left;

                                icn.Size = new Size(btnHldr.Width, btnHldr.Height / 2);
                                icn.Location = new Point(0, 0);
                                icn.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                                btnHldr.ClientSizeChanged += (sendef, f) =>
                                {
                                    icn.Width = btnHldr.Width;
                                    icn.Height = btnHldr.Height / 2;
                                    icn.Location = new Point(0, 0);
                                };

                                pstdTime.Size = new Size(btnHldr.Width, btnHldr.Height / 2);
                                pstdTime.Location = new Point(0, icn.Bottom);
                                pstdTime.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                                btnHldr.ClientSizeChanged += (senderr, r) =>
                                {
                                    pstdTime.Location = new Point(0, icn.Bottom);
                                    pstdTime.Width = btnHldr.Width; pstdTime.Height = btnHldr.Height / 2;
                                };
                            }//btnHldr

                        }

                        if (Width > MSzz.Width && ShwNws == true)
                        {
                            {//btnholder
                                btnHldr.Height = allwaysShown.Height / 2;
                                btnHldr.Width = allwaysShown.Width / 3;
                                btnHldr.Location = new Point(0, 0);
                                btnHldr.BackColor = ClrdBck;
                                btnHldr.Dock = DockStyle.Left;

                                //

                                icn.Size = new Size(btnHldr.Width / 2, btnHldr.Height);
                                icn.Location = new Point(0, 0);
                                icn.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                                icn.Invalidate();

                                //

                                pstdTime.Size = new Size(btnHldr.Width / 2, btnHldr.Height);
                                pstdTime.Location = new Point(icn.Right, 0);
                                pstdTime.Anchor = AnchorStyles.Top;
                                pstdTime.Invalidate();
                                btnHldr.Invalidate();
                            }//btnholder
                        }
                        else
                        {
                            {//btnHldr
                                btnHldr.Height = allwaysShown.Height;
                                btnHldr.Width = allwaysShown.Width / 6;
                                btnHldr.Location = new Point(0, 0);
                                btnHldr.Dock = DockStyle.Left;

                                icn.Size = new Size(btnHldr.Width, btnHldr.Height / 2);
                                icn.Location = new Point(0, 0);
                                icn.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                                btnHldr.ClientSizeChanged += (sendef, f) =>
                                {
                                    icn.Width = btnHldr.Width;
                                    icn.Height = btnHldr.Height / 2;
                                    icn.Location = new Point(0, 0);
                                };

                                pstdTime.Size = new Size(btnHldr.Width, btnHldr.Height / 2);
                                pstdTime.Location = new Point(0, icn.Bottom);
                                pstdTime.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                                btnHldr.ClientSizeChanged += (senderr, r) =>
                                {
                                    pstdTime.Location = new Point(0, icn.Bottom);
                                    pstdTime.Width = btnHldr.Width; pstdTime.Height = btnHldr.Height / 2;
                                };
                            }//btnHldr
                        }
                        if (Height > MSzz.Height * 3 && ShwNws == true)
                        {
                            {//btnHldr
                                btnHldr.Height = allwaysShown.Height;
                                btnHldr.Width = allwaysShown.Width / 6;
                                btnHldr.Location = new Point(0, 0);
                                btnHldr.Dock = DockStyle.Left;

                                icn.Size = new Size(btnHldr.Width, btnHldr.Height / 2);
                                icn.Location = new Point(0, 0);
                                icn.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                                btnHldr.ClientSizeChanged += (sendef, f) =>
                                {
                                    icn.Width = btnHldr.Width;
                                    icn.Height = btnHldr.Height / 2;
                                    icn.Location = new Point(0, 0);
                                };

                                pstdTime.Size = new Size(btnHldr.Width, btnHldr.Height / 2);
                                pstdTime.Location = new Point(0, icn.Bottom);
                                pstdTime.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
                                btnHldr.ClientSizeChanged += (senderr, r) =>
                                {
                                    pstdTime.Location = new Point(0, icn.Bottom);
                                    pstdTime.Width = btnHldr.Width; pstdTime.Height = btnHldr.Height / 2;
                                };
                            }//btnHldr

                        }
                        else
                        {
                            {//btnholder
                                btnHldr.Height = allwaysShown.Height / 2;
                                btnHldr.Width = allwaysShown.Width / 3;
                                btnHldr.Location = new Point(0, 0);
                                btnHldr.BackColor = ClrdBck;
                                btnHldr.Dock = DockStyle.Left;

                                //

                                icn.Size = new Size(btnHldr.Width / 2, btnHldr.Height);
                                icn.Location = new Point(0, 0);
                                icn.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                                icn.Invalidate();

                                //

                                pstdTime.Size = new Size(btnHldr.Width / 2, btnHldr.Height);
                                pstdTime.Location = new Point(icn.Right, 0);
                                pstdTime.Anchor = AnchorStyles.Top;
                                pstdTime.Invalidate();
                                btnHldr.Invalidate();
                            }//btnholder
                        }

                        Invalidate();
                        foreach (Control item in Controls)
                        {
                            item.Invalidate();
                        }

                    };
                }//DSG



                Controls.Add(Hrzntl);
                MSzz = new Size(Width, (Height));// + S.F.STATUS.Height);
                foreach (Control c in Hrzntl.Controls)
                {
                    c.KeyDown += (sender, e) =>
                    {
                        switch (e.KeyCode)
                        {
                            case Keys.PageUp:
                                FontSize = FontSize + 2;
                                break;
                            case Keys.PageDown:
                                FontSize = FontSize - 2;
                                break;
                            default:
                                break;
                        }
                    };
                    if (c.Controls.Count > 0)
                    {
                        foreach (Control cc in c.Controls)
                        {
                            cc.KeyDown += (sender, e) =>
                            {
                                switch (e.KeyCode)
                                {
                                    case Keys.PageUp:
                                        FontSize = FontSize + 2;
                                        break;
                                    case Keys.PageDown:
                                        FontSize = FontSize - 2;
                                        break;
                                    default:
                                        break;
                                }
                            };
                            if (c.Controls.Count > 0)
                            {
                                foreach (Control ccc in cc.Controls)
                                {
                                    ccc.KeyDown += (sender, e) =>
                                    {
                                        switch (e.KeyCode)
                                        {
                                            case Keys.PageUp:
                                                FontSize = FontSize + 2;
                                                break;
                                            case Keys.PageDown:
                                                FontSize = FontSize - 2;
                                                break;
                                            default:
                                                break;
                                        }
                                    };
                                }
                            }
                        }
                    }
                }
            }//Horizontal Panel -Hrzntl
        }//NEW
        public void ShowOneRSS()
        {

        }
        public void ShowOneRSSs()//(RssNews rss)
        {
            ////here goes the code that shows the stuff from one rss
            //Ttle.Text = rss.Title;
            //NNw.Text = rss.Summary;
            //UrlImg.Text = "";
            //foreach (string pctText in rss.ImageSource)
            //{
            //    UrlImg.Text += pctText + Environment.NewLine;
            //}


            ////--- "Fake is set false by default"
            //if (Fake == false)
            //{
            //    shwPicture.Load(UrlImg.Text);

            //} 
            //else
            //{
            //    UrlImg.Text = "";
            //}//here goes the code that shows the stuff from one rss

        }//NEW Inside
    }
}
