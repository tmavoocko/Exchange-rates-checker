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
//using System.Windows.Shapes;
//using System.Net.Mime.MediaTypeNames;
using System.Collections.ObjectModel;

namespace ExRaChe
{
    public static class Cnst
    {

        public static bool To2D = true;
        public static bool HdrToSet = true;
        public static bool To3D = false;
        //public static bool To2D = true;
        //public static bool To3D = false;
        //public static PnTxt A,A2,B,C,D ;
        public static List<Label> Mtltps = new List<Label>();
        public static String LddFileName = "";
        public static String LddFile ="";

        internal static class S
        {
            //TlStrip
            public static List<object> RuningAps = new List<object>();
            public static void addApp(object o)
            {
                RuningAps.Add(o);
            }
            public static void remApp(object o)
            {
                if (RuningAps.Contains(o)) RuningAps.Remove(o);
                if (RuningAps.Count < 1) Application.Exit();
            }
            //TlStrip

            public static double FlatZ = 1 / Math.Sqrt(2.0);
            public static Timer Ticker = new Timer();
            public static Panel scIntrBackground = new Panel();
            
            public static List<LblAnmtd> Movement2D = new List<LblAnmtd>();
            public static List<LblAnmtd> LblsAnmtdAll = new List<LblAnmtd>();
            //public static CanvasPanel Canvas;
          // //public static DsgnCnvHldr CnvsHldr = new DsgnCnvHldr(""); 
            //public static DsgnCnv Cnvs;
            //public static Hdr Hdr;
            //public static Hdr2 Hdr2;
            //public static List<MTltip> TPaint = new List<MTltip>();
        }

        public static Color MClrTrnsfr;
        public static List<Color> MClrs = new List<Color>();

        public static List<object> AllPanels = new List<object>();
        public static readonly string StartupPath = System.Windows.Forms.Application.StartupPath;
        public static Screen StartupScreen = Screen.FromPoint(Cursor.Position);
        public static Point MouseDownLoc;
        public static bool MouseIsDown = false;
        
        public static List<System.Drawing.Rectangle> MyPlgns = new List<System.Drawing.Rectangle>();
        public static List<System.Drawing.Rectangle> My2DRcsColl = new List<System.Drawing.Rectangle>();
        public static List<System.Drawing.Rectangle> My3DRcsColl = new List<System.Drawing.Rectangle>();
        public static List<Control> My3DObject = new List<Control>();

        //private static System.Collections.ObjectModel.ObservableCollection<Point> _frstPolygonP = new System.Collections.ObjectModel.ObservableCollection<Point>();
        //public static System.Collections.ObjectModel.ObservableCollection<Point> FrstPolygonP { get { return _frstPolygonP; } }

        public static List<Point> LftPolygonP = new List<Point>();
        public static List<Point> RghtPolygonP = new List<Point>();
        public static List<Point> TopPolygonP = new List<Point>();
        public static List<Point> BttmPolygonP = new List<Point>();
        public static List<Point> BckPolygonP = new List<Point>();
        public static List<Point> FrntPolygonP = new List<Point>();
        //public static List<PcBR3D> PcBr3D = new List<PcBR3D>();
        //public static Prism Prsm = new Prism();
        
        public static ObservableCollection<Control> Smthngs = new ObservableCollection<Control>();
        
        public static RichTextBox Vlues2D = new RichTextBox();

        public static RichTextBox VluesIso = new RichTextBox();
        public static Panel Hldr = new Panel();
        public static Panel MCntxMenu = new Panel();
        //D3
        public static double FlatZDigg = 1 / Math.Sqrt(2.0);
        //public static Timer Ticker = new Timer();//D3
        //D3public static CanvasPanel Canvas;//D3
        //public static DsgnCnv ClCnvs2D = new DsgnCnv("D2");
        //public static DsgnCnv ClCnvs3D = new DsgnCnv("D3");
        public static List<int> UsrValues = new List<int>();//"X:";"Y:";"Z:";"Š:";"V:";"H:"
        public static System.Drawing.Rectangle Usr3DRect1;
        public static System.Drawing.Rectangle Usr3DRect2;
        public static System.Drawing.Rectangle Usr2DRect;
        //public static List<RdmPan> ReDrw = new List<RdmPan>();
        public static int FltZ = (int)Math.Sqrt(2) * 2;//Math.Sqrt(2) is Odmocnina(squareroot) ze dvou *2(dva krat dva)
        public static Control Cller;
        //public static PcBResizable Pc = null;
        //public static PcBR3D PcBRNwUniv = null;
        public static bool CllerFilled = false;
        public static Form LstForm;
        //public static List<string> HstryTxts = new List<string>();
        
        public static List<System.Drawing.Rectangle> MyRsltRecs = new List<System.Drawing.Rectangle>();
        public static List<System.Drawing.Rectangle> MyRecs = new List<System.Drawing.Rectangle>();
        public static List<System.Drawing.Rectangle> RcIntersect = new List<System.Drawing.Rectangle>();
        public static Region Src = new Region();
        public static Size WnOrigArea;
        
        public static System.Drawing.Rectangle ScreeLftOver = new System.Drawing.Rectangle(0, 0, 1, 1);
        public static Label VlShow = new Label();
        //public static CntxtMenu justOne;
        //public static MTltip jstTry;
        //public static BtnCntxtMenu justTwo;
        public static Form1 Source;
        public static string SaveIt = "";
        public static void RegInstance(Form1 source)
        {
            Source = source;
            Ticker.Interval = 2250;
            //Save info
            Source.FormClosing += (sender, e) =>
            {
                //My own working Save for all controls!
                string saveIt = "";
                string autoPath = System.Windows.Forms.Application.StartupPath;
                //foreach (HookZoom p in Cnst.PcBR)
                //{//pores jeste homepath obrazku - pri presunu programu z discu na disk budou jinak komplikace
                //    //string ks = p.FrontTexturePath.Remove(0,autoPath.Length);
                //    //saveIt = +'/' + p.Class + '/' + p.ChClass + '/' + p.Counter + '/' + p.MyShape + '/' + (p.FrontTexturePath) + '/' + p.BackTexturePath + '/' + p.OriLoca.X + '/' + p.OriLoca.Y + '/' + p.OriginSize.Width + '/' + p.OriginSize.Height + '/' + p.Facing + '%';

                //    SaveIt += saveIt;

                //}

                autoPath += "//DefaultSave.tod";
                System.IO.File.WriteAllText(autoPath, SaveIt.ToString());
                //My own working Save for all controls!
                //MessageBox.Show(SaveIt.ToString());
            };//Save info

            //Load info

            //Load info



        }
        public static List<Control> DrwClnt = new List<Control>();
        public static List<Rectangle> RmckClnt = new List<Rectangle>();
        //public static List<PcBR3D> PcBRUniversals = new List<PcBR3D>();
        public static List<Control> Slctd = new List<Control>();
        public static List<Control> AllFrZoom = new List<Control>();
        public static List<Control> TmpProperties = new List<Control>();
        
        public static int ItemCoun = 0;
        public static int DiceCoun = 0;
        public static int MonsterCoun = 0;
        public static int RoomCoun = 0;
        public static int TxtBCoun = 0;
        public static int QuestiCoun = 0;
        public static int WallCoun = 0;
        public static int TokenCoun = 0;
        public static int UsePanCoun = 0;
        public static Timer Ticker = new Timer();

        public static Dictionary<string, string> GetProps(string s)
        {
            Dictionary<string, string> myD = new Dictionary<string, string>();
            string[] splitS = s.Split('&');
            foreach (string st in splitS)
            {
                string[] splitSt = st.Split('=');
                if (splitSt.Count() > 0)
                {
                    if (splitSt.Count() > 1)
                    {
                        myD.Add(splitSt[0], splitSt[1]);
                    }
                    else
                    {
                        myD.Add(splitSt[0], "");
                    }
                }
            }
            return myD;
        }

        public static bool LoadContent()
        {
            bool scc = false;
            try
            {
                if (File.Exists(StartupPath + @"/Default.ini"))
                {
                    using (StreamReader sr = new StreamReader(StartupPath + @"/Default.ini"))
                    {
                        while (sr.Peek() >= 0)
                        {
                            string innerData = sr.ReadLine();
                            string spawnData = "";
                            while (innerData.Length > 0 && innerData[0] != '@')
                            {
                                spawnData += innerData[0];
                                innerData = innerData.Remove(0, 1);
                            }
                            if (innerData != "") innerData = innerData.Remove(0, 1);

                            Dictionary<string, string> myProps = GetProps(spawnData);

                            string AssName = "";
                            string ClassName = "";
                            if (myProps.TryGetValue("AssName", out AssName) && myProps.TryGetValue("ClassName", out ClassName))
                            {
                                string fullyQualifiedAssemblyName = AssName + "." + ClassName;
                                object myO = GetInstance(fullyQualifiedAssemblyName, innerData);
                                scc = myO != null ? true : false;
                            }
                        }
                    }
                }
            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }
            return scc;
        }
        public static void SaveContent()
        {
            string output = "";
            foreach (object item in AllPanels)
            {
                if (output != "") output += "\r\n";
                output += item.ToString();
            }
            File.WriteAllText(StartupPath + @"/Default.ini", output);
        }
        public static void CloseApp()
        {
            //save content
            SaveContent();
            //exit
            System.Windows.Forms.Application.Exit();
        }

        public static string GetValues(object sender)
        {
            string output = "";
            try
            {
                Type myType = sender.GetType();
                //PropertyInfo[] myPI = myType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                //foreach (PropertyInfo pi in myPI)
                //{
                //    if (pi.CanWrite && pi.CanRead)
                //    {
                //        MethodInfo mget = pi.GetGetMethod(false);
                //        MethodInfo mset = pi.GetSetMethod(false);
                //        if (mget != null && mset != null)
                //        {
                //            if (pi.PropertyType.Equals(typeof(String)))
                //            {
                //                var str = pi.GetValue(sender);

                //                if (str != null && str.ToString() != "")
                //                {
                //                    if (output != "") output += "&";
                //                    output += pi.Name + "=" + str.ToString();
                //                }
                //            }
                //            if (pi.PropertyType.Equals(typeof(int)))
                //            {
                //                var vAr = pi.GetValue(sender);
                //                if (vAr != null)
                //                {
                //                    if (output != "") output += "&";
                //                    output += pi.Name + "=" + vAr.ToString();
                //                }
                //            }
                //        }
                //    }
                //}
                //output = "AssName=" + Assembly.GetAssembly(myType).GetName().Name + "&ClassName=" + myType.Name + "$" + output;

            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }

            return output;
        }

        public static bool SetValue(object sender, string propName, string propValue)
        {
            try
            {
                Type myType = sender.GetType();
                //PropertyInfo myFI = myType.GetProperty(propName, BindingFlags.Instance | BindingFlags.Public);
                //if (myFI != null && myFI.PropertyType.Equals(typeof(String)))
                //{
                //    myFI.SetValue(sender, propValue);
                //    return true;
                //}
                //if (myFI != null && myFI.PropertyType.Equals(typeof(int)))
                //{
                //    int myI;
                //    if (int.TryParse(propValue, out myI))
                //    {
                //        myFI.SetValue(sender, myI);
                //        return true;
                //    }
                //}

            }
            catch (Exception) { }
            return false;
        }

        public static void SetValues(object sender, Dictionary<string, string> AllValues)
        {
            List<KeyValuePair<string, string>> myStrValues = new List<KeyValuePair<string, string>>();
            List<KeyValuePair<string, int>> myIntValues = new List<KeyValuePair<string, int>>();
            //List<KeyValuePair<string, bool>> myBoolValues = new List<KeyValuePair<string, bool>>();
            try
            {
                //foreach (KeyValuePair<string, string> kv in AllValues)
                //{
                //    Type myType = sender.GetType();
                //    PropertyInfo myFI = myType.GetProperty(kv.Key, BindingFlags.Instance | BindingFlags.Public);
                //    if (myFI != null && myFI.PropertyType.Equals(typeof(String)))
                //    {
                //        myStrValues.Add(new KeyValuePair<string, string>(kv.Key, kv.Value));
                //    }
                //    if (myFI != null && myFI.PropertyType.Equals(typeof(int)))
                //    {
                //        int myI;
                //        if (int.TryParse(kv.Value, out myI))
                //        {
                //            myIntValues.Add(new KeyValuePair<string, int>(kv.Key, myI));
                //        }
                //    }
                //    //if (myFI != null && myFI.PropertyType.Equals(typeof(bool)))
                //    //{
                //    //    bool myI;
                //    //    if (bool.TryParse(kv.Value, out myI))
                //    //    {
                //    //        myBoolValues.Add(new KeyValuePair<string, bool>(kv.Key, myI));
                //    //    }
                //    //}
                //}
                //foreach (KeyValuePair<string, string> kv in myStrValues)
                //{
                //    PropertyInfo myFI = sender.GetType().GetProperty(kv.Key);
                //    if (myFI != null)
                //        myFI.SetValue(sender, kv.Value);
                //}
                //foreach (KeyValuePair<string, int> kv in myIntValues)
                //{
                //    PropertyInfo myFI = sender.GetType().GetProperty(kv.Key);
                //    if (myFI != null)
                //        myFI.SetValue(sender, kv.Value);
                //}
                ////foreach (KeyValuePair<string, bool> kv in myBoolValues)
                //{
                //    PropertyInfo myFI = sender.GetType().GetProperty(kv.Key);
                //    if (myFI != null)
                //        myFI.SetValue(sender, kv.Value);
                //}

            }
            catch (Exception e) { MessageBox.Show(e.ToString()); }

        }
        public static object GetInstance(string strFullyQualifiedName, string strInnerData)
        {
            try
            {
                Type t = Type.GetType(strFullyQualifiedName);
                return Activator.CreateInstance(t, strInnerData);
            }
            catch (Exception) { }
            return null;
        }
    
        public struct MoveEventArgs
        {
            private static int deltaX = 0;
            public int DeltaX { get { return deltaX; } }
            private static int deltaY = 0;
            public int DeltaY { get { return deltaY; } }

            public MoveEventArgs(int DeltaX, int DeltaY)
            {
                deltaX = DeltaX;
                deltaY = DeltaY;
            }
        }


    }
    
}
