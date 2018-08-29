using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ExRaChe
{
    public class MultiButton : ToolStripItem
    {
        private ToolStripMenuItem menuItem = new ToolStripMenuItem(); //click and bool
        public ToolStripDropDownButton MenuButton = new ToolStripDropDownButton();
        public ContextMenuStrip DropDownMenu = new ContextMenuStrip();
        private List<MultiButton> multiButtons = new List<MultiButton>();
        public ToolStripMenuItem SubMenuButton = new ToolStripMenuItem();
        public ContextMenuStrip DropDownSubMenu = new ContextMenuStrip() { DefaultDropDownDirection = ToolStripDropDownDirection.Right };

        private Label intLabel = new Label();//screen name of int and string controls
        private TrackBar intBar = new TrackBar();// "int" value
        private int lastInt = 0;
        private Panel intPanel = new Panel();

        private Label strLabel = new Label();
        private TextBox strBox = new TextBox();// "str" value
        private string lastStr = "";
        private Panel strPanel = new Panel();

        private bool isMainMenu = false;
        public bool IsMainMenu
        {
            get { return isMainMenu; }
            set
            {
                isMainMenu = value;
                if (isMainMenu)
                {
                    IsSubMenu = false;
                    IsString = false;
                    IsInt = false;
                    IsBool = false;
                }
            }
        }
        private bool isSubMenu = false;
        public bool IsSubMenu
        {
            get { return isSubMenu; }
            set
            {
                isSubMenu = value;
                if (isSubMenu)
                {
                    IsMainMenu = false;
                    IsString = false;
                    IsInt = false;
                    IsBool = false;
                }
            }
        }
        private bool isString = false;
        public bool IsString
        {
            get { return isString; }
            set
            {
                isString = value;
                if (isString) // when turned on, it turns off the others
                {
                    IsMainMenu = false;
                    IsSubMenu = false;
                    IsInt = false;
                    IsBool = false;
                }
            }
        }
        private bool isInt = false;
        public bool IsInt
        {
            get { return isInt; }
            set
            {
                isInt = value;
                if (isInt)
                {
                    IsMainMenu = false;
                    IsSubMenu = false;
                    IsString = false;
                    IsBool = false;
                }
            }
        }
        private bool isBool = false;
        public bool IsBool
        {
            get { return isBool; }
            set
            {
                isBool = value;
                if (isBool)
                {
                    IsMainMenu = false;
                    IsSubMenu = false;
                    IsString = false;
                    IsInt = false;
                }
            }
        }
        public bool IsVisible { get; set; } = true;

        public event StrChangeHandler StrChange;// handlers that spread the events
        public event IntChangeHandler IntChange;
        public event BoolChangeHandler BoolChange;
        public event ClickOnlyHandler ClickOnly;

        public ToolStripItem TsItem
        {//user iterface provider
            get
            {
                if (IsMainMenu) return MenuButton;
                if (IsSubMenu) return SubMenuButton;
                if (IsString) return new ToolStripControlHost(strPanel);
                if (IsInt) return new ToolStripControlHost(intPanel);
                return menuItem;
            }
        }
        public override string Text
        {
            get { return intLabel.Text; }
            set { MenuButton.Text = SubMenuButton.Text = menuItem.Text = strLabel.Text = intLabel.Text = value; }
        }
        public bool BoolValue
        {
            get { return menuItem.Checked; }
            set { menuItem.Checked = value; }
        }
        public int IntValue
        {
            get { return intBar.Value; }
            set { intBar.Value = value; }
        }
        public string StrValue
        {
            get { return strBox.Text; }
            set { strBox.Text = value; }
        }

        public MultiButton(string screenName, string strValue = "", bool boolValue = false, int intValue = 0, int minVal = 0, int maxVal = 0)
        {
            MenuButton.Text = SubMenuButton.Text = menuItem.Text = strLabel.Text = intLabel.Text = screenName;
            menuItem.Checked = boolValue;
            menuItem.Click += ClickOnlyEvent;
            //int panel design
            intBar.Minimum = minVal;
            intBar.Maximum = maxVal;
            intBar.Value = intValue;
            lastInt = intValue;
            intPanel.Controls.Add(intLabel);
            intPanel.Controls.Add(intBar);
            intPanel.AutoSize = true;
            intLabel.Location = new Point(0, 0);
            intBar.Location = new Point(intLabel.Right + 1, 0);
            intLabel.SizeChanged += (sender, e) =>
            {
                intBar.Location = new Point(intLabel.Right + 1, 0);
            };
            intBar.ValueChanged += IntValChange;
            //str panel design
            strBox.Text = strValue;
            lastStr = strValue;
            strPanel.Controls.Add(strLabel);
            strPanel.Controls.Add(strBox);
            strBox.Location = new Point(strLabel.Right + 1, 0);
            strBox.TextChanged += StrValChange;
            strLabel.SizeChanged += (sender, e) =>
            {
                strBox.Location = new Point(strLabel.Right + 1, 0);
            };
            //submenu design
            MenuButton.DropDown = DropDownMenu;
            DropDownMenu.Opening += (sender, e) =>
            {
                DropDownMenu.Items.Clear();
                foreach (MultiButton mb in multiButtons)
                {
                    if (mb.IsVisible)
                    {
                        DropDownMenu.Items.Add(mb.TsItem);
                    }
                }
                e.Cancel = false;
            };
            SubMenuButton.DropDown = DropDownSubMenu;
            DropDownSubMenu.Opening += (sender, e) =>
            {
                DropDownSubMenu.Items.Clear();
                foreach (MultiButton mb in multiButtons)
                {
                    if (mb.IsVisible)
                    {
                        DropDownSubMenu.Items.Add(mb.TsItem);
                    }
                }
                e.Cancel = false;
            };
        }

        public void AddMultiButton(MultiButton multiButton)
        {
            if (!multiButtons.Contains(multiButton)) multiButtons.Add(multiButton);
        }
        public void RemMutiButton(MultiButton multiButton)
        {
            if (multiButtons.Contains(multiButton)) multiButtons.Remove(multiButton);
        }

        private void StrValChange(object sender, EventArgs e)
        {//this prepares the data
            StrChangeEvArgs args = new StrChangeEvArgs(lastStr, StrValue);
            lastStr = StrValue;
            OnStrChange(args);
        }
        protected void OnStrChange(StrChangeEvArgs e)
        {//this rises the event
            StrChange(this, e);
        }

        private void IntValChange(object sender, EventArgs e)
        {
            IntChangeEvArgs args = new IntChangeEvArgs(lastInt, IntValue);
            lastInt = IntValue;
            OnIntChange(args);
        }
        protected void OnIntChange(IntChangeEvArgs e)
        {
            IntChange(this, e);
        }

        private void ClickOnlyEvent(object sender, EventArgs e)
        {
            if (IsBool)
            {
                BoolValue = !BoolValue;
                BoolChangeEvArgs args = new BoolChangeEvArgs(BoolValue);
                OnBoolChange(args);
            }
            else
            {
                OnClickOnly();
            }
        }
        protected void OnBoolChange(BoolChangeEvArgs e)
        {
            BoolChange(this, e);
        }
        protected void OnClickOnly()
        {
            ClickOnly(this, new ClickOnlyEvArgs());
        }        
    }

    public delegate void StrChangeHandler(MultiButton sender, StrChangeEvArgs e);// delegates - event description
    public delegate void IntChangeHandler(MultiButton sender, IntChangeEvArgs e);
    public delegate void BoolChangeHandler(MultiButton sender, BoolChangeEvArgs e);
    public delegate void ClickOnlyHandler(MultiButton sender, ClickOnlyEvArgs e);

    public class StrChangeEvArgs : EventArgs
    {// event args - data transfered
        public string OldStr { get; private set; } = "";
        public string NewStr { get; private set; } = "";
        public StrChangeEvArgs(string oldStr, string newStr)
        {
            OldStr = oldStr;
            NewStr = newStr;
        }
    }
    public class IntChangeEvArgs : EventArgs
    {
        public int OldInt { get; private set; } = 0;
        public int NewInt { get; private set; } = 0;
        public IntChangeEvArgs(int oldInt, int newInt)
        {
            OldInt = oldInt;
            NewInt = newInt;
        }
    }
    public class BoolChangeEvArgs : EventArgs
    {
        public bool NewBool { get; private set; } = false;
        public BoolChangeEvArgs(bool newBool)
        {
            NewBool = newBool;
        }
    }
    public class ClickOnlyEvArgs : EventArgs
    {//empty data
        public ClickOnlyEvArgs() { }
    }
}

