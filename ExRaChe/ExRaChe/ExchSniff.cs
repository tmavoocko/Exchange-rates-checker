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

namespace ExRaChe
{
    public partial class ExchSniff : Form
    {
        public int TmrCnvertToInt = 0;
        public int TmrCnvertToIntEnd = 0;
        public int ClctIntrval = 0;
        public DateTime TmerStart = new DateTime();
        public DateTime TmerEnd = new DateTime();
        public DateTime TmOfClick = new DateTime();
        List<string> IntroStrngs = new List<string>();
        private Point mouseDownLoc;
        public Panel DrwCore = new Panel();
        //public Panel scIntrBackground = new Panel();
        public Point SpwLc = new Point();
        public Timer Tckr1 = new Timer();
        public Font MFnt = new Font("Litograph", 15, FontStyle.Bold);
        public Font mFntCh = new Font("Litograph", 10, FontStyle.Regular);
        public Rectangle DsgRct = new Rectangle();
        //public DsgInsHrz Hrzntl = new DsgInsHrz();
        public Panel VrtLft, HrzLftTop, HrzLftDwn, VrtRght, HrzRghtTop, HrzRghtDwn;
        public Panel Hrzntl = new Panel();
        public Panel Prgrm = new Panel();
        public Color ClrBasePenelHolder = Color.FromArgb(255, Color.WhiteSmoke);

        public Color ClrdBck = Color.FromArgb(132, 45, 37);
        public Color ClrdFrnt = Color.FromArgb(232, 145, 137);

        public Color ClrInver;
        public Control LstCntrl = new Control();

        public int SiModifier = 1;

        //NEW -Hrzntl object declaration
        private List<string> NwsAll = new List<string>();//
        private List<RssNews> NwsAllRss = new List<RssNews>();//
        private RssNews NwsActualRssShw;
        private string Pstn = "";
        public MainStatus Sttus = new MainStatus();
        private int NwsCount = 0;
        private List<RssNews> news = new List<RssNews>();//
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

        //public List<string> NwsAll = new List<string>();
        public Panel smtimsShown = new Panel();
        public bool ShwNws = false;
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
        //NEW -Hrzntl object declaration

        //TlStrip
        private ToolStrip menuStrip;
        private StatusStrip statusStrip;
        private ContextMenuStrip fileMenuStrip;
        private ContextMenuStrip optionsMenuStrip;
        private bool inProgres = false;
        private static Timer ticker = new Timer();
        ToolStripProgressBar progress;
        ToolStripLabel label;
        //TlStrip

        private Timer tcker = new Timer() { Interval = 100 };
        private int tickCounter = 0;
        private bool updateWatcher = false;
        public ExchSniff()
        {
            SniffIt();
            tcker.Tick += (sender, e) =>
            {
                DateTime now = DateTime.Now;
                Text = now.ToLongTimeString();

                if (!updateWatcher)
                {
                    updateWatcher = true;
                    if (tickCounter == 200)
                    {
                        tickCounter = 0;
                        SniffIt();
                    }
                    else tickCounter++;
                    updateWatcher = false;
                }
            };
            tcker.Start();




            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            DoubleBuffered = true;
            //AutoScroll = true;           
            //InitializeComponent();
            //Text = "TxtSpltr";
            Text = "Exchange rate checker - D.Konicek - zadani pate";
            MouseDown += (sender, e) =>
            {
                mouseDownLoc = e.Location;

            };

            MouseUp += (sender, e) =>
            {

            };
            MouseWheel += (sender, e) =>
            {
                //int oldZoom = 100;
                //int newZoom = e.Delta * SystemInformation.MouseWheelScrollLines / 30 + ZoomPerc;
                //ZoomPerc += e.Delta * SystemInformation.MouseWheelScrollLines / 120;
                //if (newZoom < minZoom) newZoom = minZoom;
                //if (newZoom > maxZoom) newZoom = maxZoom;
                //foreach (Control p in Controls)
                //{
                //    p.Location = new Point(p.Location.X * newZoom / oldZoom, p.Location.Y * newZoom / oldZoom);
                //    p.Size = new Size(p.Size.Width * newZoom / oldZoom, p.Size.Height * newZoom / oldZoom);
                //    //MessageBox.Show(p.Location.ToString() +Environment.NewLine + p.Size.ToString());
                //}
            };
            MouseHover += (sender, e) =>
            {
                ToolTip hint = new ToolTip();
                hint.SetToolTip(this, Text);
                foreach (Control c in Controls)
                {
                    c.MouseHover += (senderf, f) =>
                    {
                        ToolTip hint2 = new ToolTip();
                        hint2.SetToolTip(c, c.ToString());
                    };
                    if (c.Controls.Count > 0)
                    {
                        foreach (Control Cc in Controls)
                        {

                            Cc.MouseHover += (senderf, f) =>
                            {
                                ToolTip hint2 = new ToolTip();
                                hint2.SetToolTip(Cc, Cc.ToString());
                            };
                            if (Cc.Controls.Count > 0)
                            {
                                foreach (Control Ccc in Controls)
                                {
                                    Ccc.MouseHover += (senderf, f) =>
                                    {
                                        ToolTip hint2 = new ToolTip();
                                        hint2.SetToolTip(Ccc, Ccc.ToString());
                                    };
                                }
                            }
                        }

                    }
                }
               
            };
            MouseLeave += (sender, e) =>
            {

                {
                    ////No mdFrameLoc - because it is on Form1                        
                    //Point parentLoc = new Point(0, 0);
                    //try { Cnst.jstTry.Dispose(); } catch (Exception) { }
                    //try { Cnst.justTwo.Dispose(); } catch (Exception) { }
                    //try { Cnst.justOne.Dispose(); } catch (Exception) { }
                    //BackColor = ClrdBck;
                    //Enabled = true;
                    ////Cnst.jstTry = new MTltip(this, this, "Pokus!!!!", 70);
                    ////Cnst.jstTry.BringToFront();
                }
            };
            KeyUp += (sender, e) =>
            {
                //if (e.KeyData == Keys.Enter)
                //{
                //    //No mdFrameLoc - because it is on Form1                        
                //    Point parentLoc = new Point(0, 0);
                //    try { Cnst.justTwo.Dispose(); } catch (Exception) { }
                //    try { Cnst.justOne.Dispose(); } catch (Exception) { }
                //    Cnst.jstTry = new MTltip(parentLoc, this);
                //    Cnst.jstTry.BringToFront();
                //}
            };
            FormClosing += (sender, e) =>
            {
                {//Xml create & save
                    XmlDocument docxml = new XmlDocument();
                    //xmlDoc.CreateNode(XmlNodeType.Element, "Records", null);
                    //XElement n = new XElement("Customer", "Adventure Works");
                    XmlDocument doc = new XmlDocument();
                    //doc.AppendChild(n);
                    //doc.CreateElement("Customer", "Adventure Works");
                    //StartingNode
                    XmlElement prgr = (XmlElement)doc.AppendChild(doc.CreateElement("prgrm"));
                    //el.SetAttribute("Bar", "some & value");
                    prgr.SetAttribute("ExchangeRateChecker", this.Text);
                    {//aBlok-InnerNode 
                        XmlElement aBlock = (XmlElement)prgr.AppendChild(doc.CreateElement("ABlock"));
                        aBlock.InnerText = "ABlock-InnerNode";
                        //aBlock.SetAttribute("aBlock", Cnst.C.UsVl.Text);
                    }//aBlok-InnerNode
                    {//cBlok-InnerNode 
                        XmlElement cBlock = (XmlElement)prgr.AppendChild(doc.CreateElement("CBlok"));
                        cBlock.InnerText = "CBlock-InnerNode";
                        //cBlock.SetAttribute("cBlock", Cnst.C.UsVl.Text);
                    }//cBlok-InnerNode
                    //MessageBox.Show(doc.OuterXml);
                    
                    // Save the document to a file. White space is
                    // preserved (no white space).
                    doc.PreserveWhitespace = true;
                    doc.Save("ExchangeRateChecker.xml");
                }//Xml create & save
            };
            {//XmlDocument load
                //XmlDocument dcLoad = new XmlDocument();
                //dcLoad.Load("IndexOfWords.xml");

                //XmlNode node = dcLoad.SelectSingleNode("ABlock");

                ////XmlNodeList prop = node.SelectNodes("CBlock");

                ////foreach (XmlNode item in prop)
                //{
                //    //items Temp = new items();
                //    //Temp.AssignInfo(item);
                //    //lstitems.Add(Temp);
                //}
                ////MessageBox.Show(dcLoad.OuterXml);
            }//XmlDocument load
            ScSize();
            IntrScr();
            //SizeChanged += (sender, e) => { RposCntrols(); };
            //ClientSizeChanged += (sender, e) => { RposCntrols(); };
        }
        //FUNCTION - TlStrip-------------------
        void onOpenFileClick(object sender, EventArgs e)
        {
            MessageBox.Show("Abra kadabra, Open Sezame!");
        }

        void onAnimateOptionClick(object sender, EventArgs e)
        {
            if (inProgres)
            {
                ticker.Stop();
                inProgres = false;
            }
            else
            {
                ticker.Start();
                inProgres = true;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            //Cnst.S.remApp(this);
            base.OnClosed(e);
        }//FUNCTION - TlStrip-------------------


        //FUNCTION - private-------------------
        private void ScSize()
        {
            //{
            //    //SetBounds((Screen.GetBounds(this).Width / 2) - (Width / 2), (Screen.GetBounds(this).Height / 2) - (Height / 2), Width, Height, BoundsSpecified.Location);
            //    //Set Form1(Window) fit on any Monitor             
            //    Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            //    int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            //    int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            //    Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            //    //thiCnst.S.Size = new Size(w, h);
            //    Size = new Size(w - ((1 / 2) / 2), h - ((1 / 2) / 2));
            //    //-----------------Set Form1(Window) fit on any Monitor 
            //}





        }
        
        private void IntrScr()
        {
            ExRaCheDsgn();
            
            Prgrm.Visible = true;
            Prgrm.BringToFront();
            
            try
            {
                //Intro Screen

                Cnst.S.scIntrBackground.Location = new Point(0, 0);
                Cnst.S.scIntrBackground.BackColor = Color.Black;
                Cnst.S.scIntrBackground.Size = new Size(ClientSize.Width, ClientSize.Height);
                Cnst.S.scIntrBackground.Dock = DockStyle.Fill;
                //Cnst.S.scIntrBackground.BringToFront();
                DrwCore.Location = new Point(19, 16);
                DrwCore.Height = ClientSize.Height - 17 - 20;
                DrwCore.Width = ClientSize.Width - 17 - 20;
                DrwCore.BackColor = ClrdBck;
                DrwCore.Dock = DockStyle.Fill;
                DrwCore.MouseUp += (sender, e) =>
                {
                    if (e.Button == MouseButtons.Right && Tckr1.Enabled == true)
                    {
                        TmOfClick = DateTime.Now;
                        if (TmrCnvertToIntEnd>(int)TmOfClick.Ticks)
                        {
                            //DrwCore.Dispose();
                            //Prgrm.Visible = true;
                            Prgrm.BringToFront();
                            //Cnst.S.scIntrBackground.SendToBack();
                            //Prgrm.BringToFront();
                            Cnst.S.scIntrBackground.Hide();
                        }
                        //MessageBox.Show(TmerStart.ToString());
                        //MessageBox.Show(TmerEnd.ToString());
                        //MessageBox.Show(TmrCnvertToIntEnd.ToString());
                    }


                };
                Cnst.S.scIntrBackground.Controls.Add(DrwCore);
                



                Controls.Add(Cnst.S.scIntrBackground);
                Cnst.S.scIntrBackground.BringToFront();
                Prgrm.SendToBack();
                //NEW CHANGE
                {//Draw some Controls


                    Random mManager = new Random();
                    int fnt = 17;
                    Font fntMine = new Font("Litograph", fnt, FontStyle.Bold);
                    string[] fill = { "Exchange", "rate", "checker", "Exchange", "rate", "checker" , "Exchange", "rate", "checker" , "Exchange", "rate", "checker" };
                    
                    int rndCreate = mManager.Next(0, (fill.Length - 1));
                    for (int i = 0; i < rndCreate; i++)
                    {
                        string tXtI=fill[mManager.Next(rndCreate)];
                        IntroStrngs.Add(tXtI);
                    }
                    
                    
                    for (int i = 0; i < IntroStrngs.Count; i++)
                    {




                        Random rndm = new Random();
                        


                        LblAnmtd sssAnmtd = new LblAnmtd(new Point(i*30, i*30), DrwCore, IntroStrngs[i]);
                        
                        sssAnmtd.Font = fntMine;
                        DrwCore.Controls.Add(sssAnmtd);
                        {//Create new Move2D for later execution
                            { //Create new Move2D for later execution

                                LblAnmtd.Move2D animationON = new LblAnmtd.Move2D(rndm.Next(23, 45), rndm.Next(23, 45));
                                sssAnmtd.ReceiveMove(animationON,33);
                                //e.Graphics.DrawString(sssAnmtd.Text, mgnr.Font, new SolidBrush(Color.FromArgb(rndm.Next(50, 255), rndm.Next(0, 255), rndm.Next(25, 255))), sssAnmtd.Location.X + (txtSz2.Width / 2) + i * 10, sssAnmtd.Location.Y + (txtSz2.Height / 2) + i * 10);

                            }//Create new Move2D for later execution
                            
                        }//Create new Move2D for later execution
                    }
                }//Draw some Controls





                Cnst.S.scIntrBackground.Paint += (sender, e) =>
                {
                    //{////ClientResultsDraw
                    //    GraphicsContainer Brdr = e.Graphics.BeginContainer();//BaseLines Container
                    //    int w = ClientSize.Width-28, h = ClientSize.Height - 28;
                    //    Pen pncl = new Pen(Color.DarkOrange, 7f);
                    //    Pen pnclDash = new Pen(ForeColor, 3f);
                        
                    //    pnclDash.DashPattern = new float[] { 4.0F, 2.0F, 1.0F, 3.0F };
                    //    SpwLc = new Point(0, 0);
                    //    e.Graphics.ScaleTransform(1.0f, -1.0f);
                    //    e.Graphics.TranslateTransform(14, 56 - Height);
                    //    Rectangle rmck = new Rectangle();
                    //    rmck.Height = h;
                    //    rmck.Width = w;//funguje 
                    //    e.Graphics.DrawRectangle(pncl, rmck);
                        
                        
                    //    e.Graphics.EndContainer(Brdr);//ContainerEnd////BaseLines Container
                        

                    //}//ClientResultsDraw
                    


                };
                
                //Draw some Controls

                {//Print
                    // FINALLY PRINTSCR All spawned Items DONE!!!!!!!! BECOUSE I AM PRINTING A JUST PANEL NOT FORM1 (remeber this!) $$
                    Bitmap smaBmp = new Bitmap(DrwCore.Width, DrwCore.Height);
                    DrwCore.DrawToBitmap(smaBmp, new Rectangle(Point.Empty, smaBmp.Size));

                    string myPath = Application.StartupPath;
                    myPath += "\\Intro Screen.jpg";
                    //MessageBox.Show(myPath);
                    try//Save print
                    { smaBmp.Save(myPath, ImageFormat.Jpeg); }
                    catch (Exception) { }//Save print
                }//Print+Save
                //NEW CHANGE----

                {//PictureBox
                    PictureBox scIntr = new PictureBox();
                    scIntr.Location = new Point(0, 0);
                    if (ClientSize.Width > ClientSize.Height)
                    {
                        scIntr.Size = new Size(ClientSize.Height, ClientSize.Height);
                        scIntr.Location = new Point((ClientSize.Width - scIntr.Width) / 2, 0);
                    }
                    else
                    {
                        scIntr.Size = new Size(ClientSize.Width, ClientSize.Width);
                    }
                    scIntr.BackColor = Color.BlueViolet;
                    scIntr.SizeMode = PictureBoxSizeMode.StretchImage;
                    string autoPathScr = Application.StartupPath;
                    //---Some Changes---------
                    autoPathScr += "\\Intro Screen.jpg";//New Load
                                                        //autoPathScr += "\\IntrScr\\IntrScrZeuCnst.S.jpg";//Old Load
                                                        //MessageBox.Show(autoPathScr);
                    try
                    {
                        scIntr.Image = Image.FromFile(autoPathScr);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }//Load the picture
                    scIntr.BringToFront();
                    //Controls.Add(scIntr);

                }//PictureBox



                
                //scIntr.Hide();
                Tckr1.Interval = 3500;
                Tckr1.Start();
                TmerStart = DateTime.Now;
                TmrCnvertToInt = (int)TmerStart.Ticks;
                TmerEnd = TmerStart.AddSeconds(3).AddMilliseconds(500);
                TmrCnvertToIntEnd= (int)TmerEnd.Ticks;
                ClctIntrval = ((int)TmerEnd.Ticks - (int)TmerStart.Ticks) / 10000;
                {//Execute && Update Move2D
                    Cnst.S.Ticker.Interval = 833;
                    Cnst.S.Ticker.Tick += (senderr, r) =>
                    {
                        

                        foreach (LblAnmtd lbl in Cnst.S.Movement2D)
                        {
                            lbl.Ticker_Tick2();
                        }
                        Cnst.S.Movement2D.RemoveAll(lbl => lbl.isMoving == false);
                        if (Cnst.S.Movement2D.Count == 0) Cnst.S.Ticker.Stop();
                        //IntersectBlocks();
                        //Cnst.S.scIntrBackground.Refresh();
                        DrwCore.Refresh();
                    };
                }//Execute && Update Move2D
                
                Tckr1.Tick += (sender, e) =>
                {
                    
                    //scIntr.Hide();

                    //Prgrm.Visible = true;
                    Prgrm.BringToFront();
                    //Cnst.S.scIntrBackground.SendToBack();
                    
                    Cnst.S.scIntrBackground.Hide();

                    Tckr1.Stop();
                    
                    foreach (Control t in Controls)
                    {
                        //Font = new Font("Cambria", FntSize, FontStyle.Bold);
                        BackColor = ClrdFrnt;//ClrdBck;
                        ForeColor = ClrdBck;//ClrdFrnt;
                    }
                    
                    {//DirectoryCreation
                     // Specify the directory you want to manipulate.
                        string myPath = Application.StartupPath;
                        myPath += "\\User save";
                        try
                        {
                            // Determine whether the directory existCnst.S.
                            if (Directory.Exists(myPath))
                            {
                                //MessageBox.Show("That path exists already.");
                                return;
                            }

                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(myPath);
                            //MessageBox.Show("The directory was created successfully at {0}.", Directory.GetCreationTime(myPath).ToLongDateString());

                            // Delete the directory.
                            //di.Delete();
                            //Console.WriteLine("The directory was deleted successfully.");
                        }
                        catch (Exception z)
                        {
                            MessageBox.Show("The process failed: {0}", z.ToString());
                        }
                        finally { }

                    }//DirectoryCreation
                    
                };//Intro Screen
            }
            catch (Exception) { }//IntroScr
        }//Startup seting for window
        public void SniffIt()
        {
            {//FakeNews
                Fake = true;
                string ttlText = "Attempt for internet conection failed.", smmryText = "Nothing to display", dcmntTex = "Nothing to display", lnks = "Url:https://www.aktualne.cz/rss/ can not be loaded", pblshDate = DateTime.Now.ToString(), id = "0";
                List<string> imgSrcFrmFile = (Application.StartupPath + "/Intro Screen.jpg").Split(' ').ToList();
                news.Add(new RssNews(ttlText, smmryText, dcmntTex, lnks, pblshDate, id, imgSrcFrmFile));
                id = "1";
                news.Add(new RssNews(ttlText, smmryText, dcmntTex, lnks, pblshDate, id, imgSrcFrmFile));
                id = "2";
                news.Add(new RssNews(ttlText, smmryText, dcmntTex, lnks, pblshDate, id, imgSrcFrmFile));
                List<RssNews> RssList = news.Where(rss => rss.Id == Sttus.POSITION.StrValue).ToList();
                if (Sttus.POSITION.StrValue != "" && Sttus.POSITION.BoolValue && RssList.Count > 0)
                {   //check empty id on start      //locked on id               //id found
                    ShowOneRSS(RssList.First());
                }
                else
                {
                    Sttus.POSITION.StrValue = news[0].Id;//first item
                    ShowOneRSS(news[0]);
                }
                NwsCount = news.Count;
            }//FakeNews
            try
            {

                //http://www.cnb.cz/cs/financni_trhy/devizovy_trh/kurzy_devizoveho_trhu/denni_kurz.jsp
                //https://www.ecb.europa.eu/stats/policy_and_exchange_rates/euro_reference_exchange_rates/html/index.en.html
                //http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml

                XmlReader rdr = XmlReader.Create("https://www.aktualne.cz/rss/");
                SyndicationFeed snFeed = SyndicationFeed.Load(rdr);
                rdr.Close();
                int snFeedCount = 0;
                foreach (SyndicationItem item in snFeed.Items)
                { snFeedCount++; }
                if (snFeedCount > 0)
                {
                    Fake = false;
                    news.Clear();
                    foreach (SyndicationItem item in snFeed.Items)
                    {
                        WebBrowser browser = new WebBrowser();
                        List<string> imgSrc = new List<string>();
                        foreach (SyndicationElementExtension extension in item.ElementExtensions)
                        {
                            XElement ele = extension.GetObject<XElement>();
                            if (ele.Name.LocalName == "encoded" && ele.Name.Namespace.ToString().Contains("content"))
                            {
                                browser.ScriptErrorsSuppressed = true;
                                browser.DocumentText = ele.Value;
                                browser.Document.OpenNew(true);
                                browser.Document.Write(ele.Value);
                                browser.Refresh();
                                foreach (HtmlElement image in browser.Document.Images) imgSrc.Add(image.GetAttribute("src"));
                            }
                        }
                        news.Add(new RssNews(item.Title.Text, item.Summary.Text, browser.DocumentText, item.Links.FirstOrDefault().Uri.ToString(), item.PublishDate.ToString(), item.Id, imgSrc));
                    }

                    if (news.Count > 0)
                    {
                        NwsAllRss = news;
                        List<RssNews> RssList = news.Where(rss => rss.Id == Sttus.POSITION.StrValue).ToList();
                        if (Sttus.POSITION.StrValue != "" && Sttus.POSITION.BoolValue && RssList.Count > 0)
                        {   //check empty id on start      //locked on id               //id found
                            NwsActualRssShw = RssList.First();
                            Pstn = NwsActualRssShw.Id;
                            ShowOneRSS(RssList.First());
                        }
                        else
                        {
                            Sttus.POSITION.StrValue = news[0].Id;//first item
                            ShowOneRSS(news[0]);
                            NwsActualRssShw = news[0];
                            Pstn = news[0].Id;
                        }
                        NwsCount = news.Count;
                        for (int i = 0; i < news.Count; i++)
                        {
                            string rssOneByOne = news[i].ToString();
                            NwsAll.Add(rssOneByOne);
                        }


                    }
                }

            }
            catch (Exception)
            {

                //throw;
            }

        }

        private void ShowOneRSS(RssNews rss)
        {
            //here goes the code that shows the stuff from one rss
            Ttle.Text = rss.Title;
            NNw.Text = rss.Summary;
            UrlImg.Text = "";
            foreach (string pctText in rss.ImageSource)
            {
                UrlImg.Text += pctText + Environment.NewLine;
            }
            if (Fake == false)
            {
                shwPicture.Load(UrlImg.Text);

            }
            else
            {
                UrlImg.Text = "";
            }//here goes the code that shows the stuff from one rss


            //use S.F.STATUS to acces Status Bar
            // S.F.PANEL is used to acces this panel
        }

        



        private void ExRaCheDsgn()
        {
            //ScSize();
            Size = new Size(310, 100);
            {//Prgrm
                Prgrm.Width = ClientSize.Width;
                Prgrm.Height = ClientSize.Height;
                Prgrm.Location = new Point(0, 0);
                Prgrm.Dock = DockStyle.Fill;
                Prgrm.Visible = false;
                Controls.Add(Prgrm);
            }//Prgrm
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



                Prgrm.Controls.Add(Hrzntl);
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









            ClientSizeChanged += (sender, e) =>
            {
                Prgrm.Invalidate();
            };
            {
                //{
                //    {//Hdr2
                //        Cnst.WnOrigArea = new Size(ClientRectangle.Width, ClientRectangle.Height);

                //        MyClrInvrt(ClrdBck);
                //        //DsgnFrColor = ForeColor;
                //        //Point spwnLoc = new Point(ClientSize.Width / 80, ClientSize.Height / 80);
                //        //Size szz = new Size(75, 45); int fnt = 21;
                //        //Font myFont = new Font("Cambria", fnt, FontStyle.Bold);

                //        //Cnst.S.Hdr2 = new Hdr2(Prgrm); Cnst.S.Hdr2.Location = new Point(0, 0);
                //        //Cnst.ScreeLftOver = new Rectangle(Cnst.S.Hdr2.Left, Cnst.S.Hdr2.Bottom, Prgrm.ClientRectangle.Width, Prgrm.ClientRectangle.Height - Cnst.S.Hdr2.Height);
                //        //LstCntrl = Cnst.S.Hdr2;
                //    }//Hdr2
                //    {//newDSG

                //        Prgrm.ClientSizeChanged += (sender, e) =>
                //        {

                //            //Visible = false;
                //            Hrzntl.Width = ClientSize.Width;
                //            Hrzntl.Height = ClientSize.Height;// - Cnst.S.Hdr2.Height;

                //            VrtLft.Width = Hrzntl.Width / 5 * 4;//Left side
                //            VrtLft.Height = Hrzntl.Height;//Left side

                //            HrzLftTop.Location = new Point(0, 0);//A1
                //            HrzLftTop.Width = VrtLft.Width;
                //            HrzLftTop.Height = VrtLft.Height / 2;//A1

                //            HrzLftDwn.Width = VrtLft.Width;//B1
                //            HrzLftDwn.Height = VrtLft.Height / 2;
                //            HrzLftDwn.Left = HrzLftTop.Left;
                //            HrzLftDwn.Top = HrzLftTop.Bottom;//B1

                //            VrtRght.Width = Hrzntl.Width - VrtLft.Right; //Right side
                //            VrtRght.Height = Hrzntl.Height;

                //            HrzRghtTop.Location = new Point(0, 0);//C1
                //            HrzRghtTop.Width = VrtRght.Width;
                //            HrzRghtTop.Height = VrtRght.Height / 2;//C1

                //            HrzRghtDwn.Width = VrtRght.Width;//D1
                //            HrzRghtDwn.Height = VrtRght.Height / 2;
                //            HrzRghtDwn.Left = HrzRghtTop.Left;
                //            HrzRghtDwn.Top = HrzRghtTop.Bottom;//D1

                //            foreach (Control c in Hrzntl.Controls)
                //            {


                //                c.Invalidate();
                //                if (c.Controls.Count > 0)
                //                {
                //                    foreach (Control Cc in Controls)
                //                    {

                //                        Cc.Invalidate();
                //                        if (Cc.Controls.Count > 0)
                //                        {
                //                            foreach (Control Ccc in Controls)
                //                            {

                //                                Ccc.Invalidate();
                //                                if (Ccc.Controls.Count > 0)
                //                                {
                //                                    foreach (Control Cccc in Controls)
                //                                    {

                //                                        Cccc.Invalidate();
                //                                    }
                //                                }
                //                            }
                //                        }
                //                    }

                //                }
                //            }
                //            Visible = true;
                //        };

                //        {//Hrzntl
                //            Hrzntl.Width = ClientSize.Width;
                //            Hrzntl.Height = ClientSize.Height - LstCntrl.Height;
                //            Hrzntl.Left = LstCntrl.Left;
                //            Hrzntl.Top = LstCntrl.Bottom;
                //            Hrzntl.BackColor = ClrdBck;// Cnst.S.Hdr2.BackColor;
                //                                       //Hrzntl.Dock = DockStyle.Fill;
                //            Hrzntl.BringToFront();
                //            //Hrzntl.Width = Cnst.ScreeLftOver.Width;
                //            //Hrzntl.Height = Cnst.ScreeLftOver.Height;
                //            Hrzntl.Controls.Clear();

                //            //Rectangle pnlScreen = Cnst.ScreeLftOver;//new Rectangle(Location.X, Location.Y, Cnst.ScreeLftOver.Width, Cnst.ScreeLftOver.Height);
                //            //int w = Width >= pnlScreen.Width ? pnlScreen.Width : (pnlScreen.Width + Hrzntl.Width) / 2;
                //            //int h = Height >= pnlScreen.Height ? pnlScreen.Height : (pnlScreen.Height + Hrzntl.Height) / 2;
                //            //Size = new Size(w, h);
                //            //
                //            //Hrzntl.Size = new Size(w / 10 * 9, h / 10 * 9);
                //            //Hrzntl.Location = new Point((Width - Hrzntl.Width) / 2, (Height - Hrzntl.Height) / 2);
                //            Hrzntl.BringToFront();

                //            {//VrtLft
                //                VrtLft = new Panel();

                //                VrtLft.Width = Hrzntl.Width / 5 * 4;
                //                VrtLft.Height = Hrzntl.Height;
                //                //VrtLft.Dock = DockStyle.Left;
                //                VrtLft.BackColor = ClrBasePenelHolder;


                //                {//Panel A1
                //                    HrzLftTop = new Panel();
                //                    HrzLftTop.Location = new Point(0, 0);
                //                    HrzLftTop.Width = VrtLft.Width;
                //                    HrzLftTop.Height = VrtLft.Height / 2;
                //                    //HrzLftTop.Dock = DockStyle.Top;
                //                    HrzLftTop.BackColor = ClrBasePenelHolder;



                //                    //RichTextBox usrText = new RichTextBox(); usrText.Font = MFnt; 
                //                    //usrText.Font = MFnt;

                //                    PnTxt A = new PnTxt(HrzLftTop, new Point(2, 2), new Size(HrzLftTop.Width, HrzLftTop.Height), ClrdBck, MFnt, false);//, "PnTxt")
                //                    A.BorderStyle = BorderStyle.FixedSingle;
                //                    //A.BorderStyle = BorderStyle.None;


                //                    A.ForeColor = ClrdFrnt;
                //                    //usrText.Dock = DockStyle.Fill;
                //                    A.Size = new Size(HrzLftTop.Width, HrzLftTop.Height / 2);
                //                    A.Dock = DockStyle.Fill;
                //                    A.Location = new Point(2, 2);
                //                    Cnst.A = A;
                //                    //Cnst.A.Enabled = false;
                //                    Cnst.A.BackColor = ClrdBck;
                //                    Cnst.A.ForeColor = ClrdFrnt;

                //                    if (Cnst.A.WrtnTxt == true)
                //                    {
                //                        Cnst.A.UsVl.BackColor = Color.DarkOrange;
                //                        //Cnst.A.UsVl.SelectionProtected = false;
                //                    }
                //                    else
                //                    {
                //                        //Cnst.A.UsVl.SelectionProtected = true;
                //                        Cnst.A.UsVl.BackColor = ClrdBck;
                //                    }
                //                    HrzLftTop.Controls.Add(Cnst.A);

                //                    //Cnst.A
                //                    //HrzLftTop.Controls.Add(dntTouchMe);
                //                }//Panel A1
                //                {//Panel B1
                //                    HrzLftDwn = new Panel();
                //                    HrzLftDwn.Width = VrtLft.Width;
                //                    HrzLftDwn.Height = VrtLft.Height / 2;
                //                    HrzLftDwn.Left = HrzLftTop.Left;
                //                    HrzLftDwn.Top = HrzLftTop.Bottom;
                //                    //HrzLftDwn.Dock = DockStyle.Bottom;
                //                    HrzLftDwn.BackColor = ClrBasePenelHolder;
                //                    //HrzLftDwn.AutoScroll = true;


                //                    //RichTextBox usrText = new RichTextBox(); usrText.Font = MFnt;


                //                    PnTxt B = new PnTxt(HrzLftDwn, new Point(0, 0), new Size(HrzLftDwn.Width, HrzLftDwn.Height), ClrdBck, MFnt, false);//, "PnTxt")
                //                    B.BorderStyle = BorderStyle.FixedSingle;
                //                    //B.BackColor = ClrdBck;
                //                    //B.ForeColor = ClrdFrnt;
                //                    //B.BorderStyle = BorderStyle.None;
                //                    //usrText.BackColor = ClrdBck;
                //                    //usrText.ForeColor = ClrdFrnt;
                //                    //usrText.Size = new Size(HrzLftDwn.Width, HrzLftDwn.Height / 3);
                //                    B.Dock = DockStyle.Fill;
                //                    //usrText.Location = new Point(2, 2);
                //                    Cnst.B = B;
                //                    HrzLftDwn.Controls.Add(Cnst.B);
                //                }//Panel B1

                //                VrtLft.Controls.Add(HrzLftDwn);
                //                VrtLft.Controls.Add(HrzLftTop);
                //            }//VrtLft
                //            LstCntrl = VrtLft;

                //            {//VrtRght
                //                VrtRght = new Panel();

                //                VrtRght.Width = Hrzntl.Width - VrtLft.Right;
                //                VrtRght.Height = Hrzntl.Height;
                //                VrtRght.Dock = DockStyle.Right;
                //                VrtRght.BackColor = ClrBasePenelHolder;


                //                {//Panel C1
                //                    HrzRghtTop = new Panel();
                //                    HrzRghtTop.Location = new Point(0, 0);
                //                    HrzRghtTop.Width = VrtRght.Width;
                //                    HrzRghtTop.Height = VrtRght.Height / 2;
                //                    //HrzRghtTop.Dock = DockStyle.Top;
                //                    HrzRghtTop.BackColor = ClrBasePenelHolder;



                //                    //RichTextBox usrText = new RichTextBox(); usrText.Font = MFnt;
                //                    //usrText.BorderStyle = BorderStyle.None;
                //                    //usrText.BackColor = ClrdBck;
                //                    //usrText.ForeColor = ClrdFrnt;
                //                    PnTxt C = new PnTxt(HrzRghtTop, new Point(2, 2), new Size(HrzRghtTop.Width, HrzRghtTop.Height), ClrdBck, mFntCh, false);//, "PnTxt")
                //                    C.BorderStyle = BorderStyle.FixedSingle;
                //                    //usrText.BackColor = ClrdBck;
                //                    //usrText.ForeColor = ClrdFrnt;
                //                    //usrText.Size = new Size(HrzLftDwn.Width, HrzLftDwn.Height / 3);
                //                    C.Dock = DockStyle.Fill;
                //                    C.Size = new Size(HrzRghtTop.Width, HrzRghtTop.Height / 2);
                //                    C.Dock = DockStyle.Fill;
                //                    C.Location = new Point(2, 2);
                //                    Cnst.C = C;
                //                    HrzRghtTop.Controls.Add(Cnst.C);
                //                }//Panel C1
                //                {//Panel D1
                //                    HrzRghtDwn = new Panel();
                //                    HrzRghtDwn.Width = VrtRght.Width;
                //                    HrzRghtDwn.Height = VrtRght.Height / 2;
                //                    HrzRghtDwn.Left = HrzRghtTop.Left;
                //                    HrzRghtDwn.Top = HrzRghtTop.Bottom;
                //                    //HrzRghtDwn.Dock = DockStyle.Bottom;
                //                    HrzRghtDwn.BackColor = ClrBasePenelHolder;
                //                    //HrzRghtDwn.AutoScroll = true;

                //                    //RichTextBox usrText = new RichTextBox(); usrText.Font = MFnt;
                //                    //
                //                    //usrText.BorderStyle = BorderStyle.None;
                //                    //usrText.BackColor = ClrdBck;
                //                    //usrText.ForeColor = ClrdFrnt;
                //                    PnTxt D = new PnTxt(HrzRghtDwn, new Point(2, 2), new Size(HrzRghtDwn.Width, HrzRghtDwn.Height), ClrdBck, mFntCh, false);//, "PnTxt")
                //                    D.BorderStyle = BorderStyle.FixedSingle;
                //                    //usrText.BackColor = ClrdBck;
                //                    //usrText.ForeColor = ClrdFrnt;
                //                    //usrText.Size = new Size(HrzLftDwn.Width, HrzLftDwn.Height / 3);
                //                    D.Dock = DockStyle.Fill;
                //                    //usrText.Location = new Point(2, 2);
                //                    Cnst.D = D;
                //                    HrzRghtDwn.Controls.Add(Cnst.D);
                //                }//Panel D1

                //                VrtRght.Controls.Add(HrzRghtDwn);
                //                VrtRght.Controls.Add(HrzRghtTop);
                //            }//VrtRght
                //            Hrzntl.Controls.Add(VrtLft);
                //            Hrzntl.Controls.Add(VrtRght);

                //            foreach (Control c in Hrzntl.Controls)
                //            {


                //                //foreach (Control c in Controls)
                //                {
                //                    //c.MouseHover += (senderf, f) =>
                //                    //{
                //                    //    ToolTip hint2 = new ToolTip();
                //                    //    hint2.SetToolTip(c, c.ToString());
                //                    //};
                //                    //if (c.Controls.Count > 0)
                //                    //{
                //                    //    foreach (Control Cc in Controls)
                //                    //    {

                //                    //        Cc.MouseHover += (senderf, f) =>
                //                    //        {
                //                    //            ToolTip hint2 = new ToolTip();
                //                    //            hint2.SetToolTip(Cc, Cc.ToString());
                //                    //        };
                //                    //        if (Cc.Controls.Count > 0)
                //                    //        {
                //                    //            foreach (Control Ccc in Controls)
                //                    //            {
                //                    //                Ccc.MouseHover += (senderf, f) =>
                //                    //                {
                //                    //                    ToolTip hint2 = new ToolTip();
                //                    //                    hint2.SetToolTip(Ccc, Ccc.ToString());
                //                    //                };
                //                    //            }
                //                    //        }
                //                    //    }

                //                    //}
                //                }
                //                c.MouseLeave += (sender, e) =>
                //                {

                //                    {

                //                    }
                //                };





                //                c.ForeColor = Color.FromArgb(32, 78, 70);
                //                c.Invalidate();
                //                if (c.Controls.Count > 0)
                //                {
                //                    foreach (Control Cc in Controls)
                //                    {
                //                        Cc.MouseHover += (sender, e) =>
                //                        {
                //                            ToolTip hint = new ToolTip();
                //                            hint.SetToolTip(c, "Pokus");

                //                        };
                //                        Cc.ForeColor = Color.FromArgb(32, 78, 70);
                //                        Cc.Invalidate();
                //                        if (Cc.Controls.Count > 0)
                //                        {
                //                            foreach (Control Ccc in Controls)
                //                            {
                //                                Ccc.MouseHover += (sender, e) =>
                //                                {
                //                                    ToolTip hint = new ToolTip();
                //                                    hint.SetToolTip(c, "Pokus");

                //                                };
                //                                Ccc.ForeColor = Color.FromArgb(32, 78, 70);
                //                                Ccc.Invalidate();
                //                            }
                //                        }
                //                    }

                //                }
                //            }

                //        }//Hrzntl
                //         //Hrzntl.Visible = false;
                //        Prgrm.Controls.Add(Hrzntl);

                //    }//newDSG




                //    {//internal MainForm   

                //        // Form Location and Position
                //        Screen activeScreen = Screen.FromPoint(MousePosition);
                //        Cnst.S.addApp(this);
                //        StartPosition = FormStartPosition.Manual;
                //        Location = activeScreen.WorkingArea.Location;
                //        Size = activeScreen.WorkingArea.Size;

                //        //Menu Instancing
                //        menuStrip = new ToolStrip();
                //        menuStrip.Dock = DockStyle.Top;
                //        statusStrip = new StatusStrip();
                //        statusStrip.Dock = DockStyle.Bottom;
                //        fileMenuStrip = new ContextMenuStrip();
                //        fileMenuStrip.Opening += (sender, e) =>
                //        {
                //            //FILL FILE MENU each time it is opened
                //            //clear old items
                //            fileMenuStrip.Items.Clear();
                //            //decide what to show

                //            //show it                                      Title, Icon,   click handler, name of control                
                //            fileMenuStrip.Items.Add(new ToolStripMenuItem("Open", null, onOpenFileClick, "openFileButton"));
                //            // EventArgs.Cancel default is true (i have no idea what it does :)
                //            e.Cancel = false;
                //        };
                //        optionsMenuStrip = new ContextMenuStrip();
                //        optionsMenuStrip.Opening += (sender, e) =>
                //        {
                //            /*FILL OPTIONS MENU*/
                //            optionsMenuStrip.Items.Clear();
                //            optionsMenuStrip.Items.Add(new ToolStripMenuItem("Animate", null, onAnimateOptionClick, "animateButton"));
                //            e.Cancel = false;
                //        };

                //        // file button in menu
                //        ToolStripDropDownButton file = new ToolStripDropDownButton("File", null, null, "File");
                //        menuStrip.Items.Add(file);
                //        file.DropDown = fileMenuStrip;

                //        // options button in menu
                //        ToolStripDropDownButton options = new ToolStripDropDownButton("Options", null, null, "Options");
                //        menuStrip.Items.Add(options);
                //        options.DropDown = optionsMenuStrip;

                //        Controls.Add(menuStrip);

                //        //ticker instancing
                //        ticker.Tick += (sender, e) =>
                //        {
                //            progress.PerformStep();
                //            label.Text = progress.Value.ToString() + "% Complete";
                //            if (progress.Value == 100)
                //            {
                //                progress.Value = 0;
                //            }
                //        };
                //        //progress bar in status strip
                //        progress = new ToolStripProgressBar();
                //        progress.Minimum = 0;
                //        progress.Maximum = 100;
                //        progress.Step = 1;
                //        progress.Width = 500;
                //        progress.Value = 45;
                //        statusStrip.Items.Add(progress);

                //        // label in status
                //        label = new ToolStripLabel();
                //        label.Text = progress.Value.ToString() + "% Complete";
                //        statusStrip.Items.Add(label);

                //        Controls.Add(statusStrip);

                //        Show(); // form
                //    }//internal MainForm
                //}
            }
        }//FUNCTION - private----

        //FUNCTION PUBLIC----------------------------------------------    
        
        public void MyClrInvrt(Color cl)
        {
            ClrInver = Color.FromArgb(cl.ToArgb() ^ 0xffffff);
            ForeColor = ClrInver;
        }
        public void DbgTltps()
        {//Position of added controls

            {//Draw over Time
                {//Create new Draw over Time for later execution
                    //MTltip.DrwOverTime drwot = new MTltip.DrwOverTime(50);
                    
                    //PrsmSlct.ReceiveMove(prsmSlctUsrVal, 43);
                }//Create new Draw over Time for later execution
                {//asign whoo will be drawed
                }//asign whoo will be drawed
                {//Execute && DRAW Move3d
                    Cnst.S.Ticker.Interval = 33;

                    Cnst.S.Ticker.Tick += (senderf, f) =>
                    {
                        //foreach (MTltip q in Cnst.S.TPaint)
                        //{
                        //    q.Ticker_Tick2();
                        //    //q.Ticker_Tick();

                        //}
                        //Cnst.S.TPaint.RemoveAll(q => q.isDrwing == false);
                        //if (Cnst.S.TPaint.Count == 0) Cnst.S.Ticker.Stop();

                        ////S.Cnvs.Refresh();

                    };
                }//Execute && DRAW Move3d
            }



                Invalidate();
            //MessageBox.Show(Cnst.Source.ClientRectangle.Size.ToString());

            foreach (Control c in Controls)
            {

                c.ForeColor = Color.FromArgb(32, 78, 70);
                c.Invalidate();
                {
                }
                if (c.Controls.Count > 0)
                {
                    foreach (Control Cc in Controls)
                    {
                        Cc.ForeColor = Color.FromArgb(32, 78, 70);
                        Cc.Invalidate();
                        if (Cc.Controls.Count > 0)
                        {
                            foreach (Control Ccc in Controls)
                            {
                                Ccc.ForeColor = Color.FromArgb(32, 78, 70);
                                Ccc.Invalidate();
                            }
                        }
                    }

                }
            }
            //Controls.Clear();
            //HdrDsgn();
        }
        public void RposCntrols()
        {//Position of added controls
            Invalidate();
            //MessageBox.Show(Cnst.Source.ClientRectangle.Size.ToString());

            foreach (Control c in Controls)
            {
                //c.ForeColor = Color.FromArgb(32, 78, 70);
                c.Invalidate();
                if (c.Controls.Count > 0)
                {
                    foreach (Control Cc in Controls)
                    {
                        //Cc.ForeColor = Color.FromArgb(32, 78, 70);
                        Cc.Invalidate();
                        if (Cc.Controls.Count > 0)
                        {
                            foreach (Control Ccc in Controls)
                            {
                                //Ccc.ForeColor = Color.FromArgb(32, 78, 70);
                                Ccc.Invalidate();
                            }
                        }
                    }

                }
            }
            //Controls.Clear();
            //HdrDsgn();
            {//Hldr
                //Hldr.Size = Cnst.ScreeLftOver.Size;
                //Hldr.Location = Cnst.ScreeLftOver.Location;
                //Hldr.BackColor = Color.Transparent;
                //Hldr.BackColor = Color.FromArgb(70, Color.Violet);
                //Hldr.BringToFront();
                //Hldr.Invalidate();
                //Controls.Add(Hldr);
            }//Hldr
             //Hrzntl.Location = Cnst.ScreeLftOver.Location;
            {//Hrzntl
             //Hrzntl.Location = Cnst.ScreeLftOver.Location;
             //Hrzntl.BackColor = Color.Blue;
             ////Hrzntl.Dock = DockStyle.Bottom;
             //Hrzntl.BringToFront();
             ////Hrzntl.Width = Cnst.ScreeLftOver.Width;
             ////Hrzntl.Height = Cnst.ScreeLftOver.Height;
             //Hrzntl.Controls.Clear();

                //Rectangle pnlScreen = Cnst.ScreeLftOver;//new Rectangle(Location.X, Location.Y, Cnst.ScreeLftOver.Width, Cnst.ScreeLftOver.Height);
                //int w = Width >= pnlScreen.Width ? pnlScreen.Width : (pnlScreen.Width + Hrzntl.Width) / 2;
                //int h = Height >= pnlScreen.Height ? pnlScreen.Height : (pnlScreen.Height + Hrzntl.Height) / 2;
                //Size = new Size(w, h);

                //Hrzntl.Size = new Size(w / 10 * 9, h / 10 * 9);
                //Hrzntl.Location = new Point((Width - Hrzntl.Width) / 2, (Height - Hrzntl.Height) / 2);
                //Hrzntl.BringToFront();

                //{//VrtLft
                //    VrtLft = new Panel();

                //    VrtLft.Width = Hrzntl.Width / 2;
                //    VrtLft.Height = Hrzntl.Height;
                //    VrtLft.Dock = DockStyle.Left;
                //    VrtLft.BackColor = Color.FromArgb(90, Color.WhiteSmoke);


                //    {//Panel A1
                //        HrzLftTop = new Panel();
                //        HrzLftTop.Width = VrtLft.Width;
                //        HrzLftTop.Height = VrtLft.Height / 2;
                //        HrzLftTop.Dock = DockStyle.Top;
                //        HrzLftTop.BackColor = Color.FromArgb(90, Color.Yellow);



                //        RichTextBox usrText = new RichTextBox(); usrText.Font = MFnt;

                //        usrText.BorderStyle = BorderStyle.None;
                //        usrText.BackColor = ClrdBck;
                //        usrText.ForeColor = ClrdFrnt;
                //        usrText.Size = new Size(HrzLftTop.Width, HrzLftTop.Height / 3);
                //        usrText.Dock = DockStyle.Fill;
                //        usrText.Location = new Point(2, 2);
                //        HrzLftTop.Controls.Add(usrText);
                //    }//Panel A1
                //    {//Panel B1
                //        HrzLftDwn = new Panel();
                //        HrzLftDwn.Width = VrtLft.Width;
                //        HrzLftDwn.Height = VrtLft.Height / 2;
                //        HrzLftDwn.Dock = DockStyle.Bottom;
                //        HrzLftDwn.BackColor = Color.FromArgb(90, Color.LimeGreen);
                //        //HrzLftDwn.AutoScroll = true;

                //        PnTxt usrText = new PnTxt(this,new Point(2, 2), new Size(HrzLftDwn.Width, HrzLftDwn.Height), ClrdFrnt, MFnt);
                //        usrText.BorderStyle = BorderStyle.None;
                //        //usrText.BackColor = ClrdBck;
                //        //usrText.ForeColor = ClrdFrnt;
                //        //usrText.Size = new Size(HrzLftDwn.Width, HrzLftDwn.Height / 3);
                //        usrText.Dock = DockStyle.Fill;
                //        //usrText.Location = new Point(2, 2);

                //        HrzLftDwn.Controls.Add(usrText);
                //    }//Panel B1

                //    VrtLft.Controls.Add(HrzLftDwn);
                //    VrtLft.Controls.Add(HrzLftTop);
                //}//VrtLft



                //Hrzntl.Controls.Add(VrtLft);


            }//Hrzntl
             //Controls.Add(Hrzntl);
        }
        public void PrintScreen()
        {
            //!!SAVE PICTURE FILE ON DISC!!---------------------------------
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            this.DrawToBitmap(bmp, new Rectangle(Point.Empty, bmp.Size));
            string myPath = Application.StartupPath;//Setting HomeFolder for saving future PrtScr.jpg
            myPath += "\\SpliterScreen.jpg"; //Adding Name for PrtScr.jpg
            try
            {
                bmp.Save(myPath, ImageFormat.Jpeg);//SAVE!!
            }
            catch (Exception) { }
            //!!SAVE PICTURE FILE ON DISC!!---------------------------------
        }
        
    }

    //Classes
    
    internal class RssNews
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
        public string Id { get; set; }
        public List<string> ImageSource { get; set; }

        public RssNews(string title, string summary, string content, string link, string date, string id, List<string> imageSource)
        {
            Title = title;
            Summary = summary;
            Content = content;
            Link = link;
            Date = date;
            Id = id;
            ImageSource = imageSource;
        }
    }

    //context menu for Textures

    //context menu for Panels




}
