using System.Windows.Forms;

namespace ExRaChe
{
    public class MainStatus : StatusStrip
    {
        public MultiButton FFWD = new MultiButton("|<<");
        public MultiButton FWD = new MultiButton("|<");
        public MultiButton POSITION = new MultiButton(@"0/0") { IsBool = true };
        public MultiButton BCK = new MultiButton(">|");
        public MultiButton REW = new MultiButton(">>|");

        public MainStatus()
        {
            Dock = DockStyle.Bottom;

            Items.Add(FFWD.TsItem);
            FFWD.ClickOnly += (sender, e) => { }; // ffwd click
            Items.Add(FWD.TsItem);
            FWD.ClickOnly += (sender, e) => { }; // fwd click
            Items.Add(POSITION.TsItem);
            POSITION.BoolChange += (sender, e) => { }; // ID LOCK CHANGE
            POSITION.IntChange += (sender, e) => { }; // Actual position CHANGE
            POSITION.StrChange += (sender, e) => { }; //focused ID CHANGE
            Items.Add(BCK.TsItem);
            BCK.ClickOnly += (sender, e) => { }; // back click
            Items.Add(REW.TsItem);
            REW.ClickOnly += (sender, e) => { }; // rewind click
        }
    }
}
