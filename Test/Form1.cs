using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //List<Button> figur = new List<Button>();
            //var figurTop = 0;
            //var btnTop = 0;
            //for (int i = 0; i < 4; i++)
            //{
            //    var btn = new Button();
            //    btn.Width = 30;
            //    btn.Height = 30;
            //    btn.Top =btnTop;
            //    btnTop += 30;
            //    Controls.Add(btn);
            //    figur.Add(btn);
            //}

            //void MoveFigur(List<Button> list)
            //{
            //    foreach (var item in list)
            //    {
            //        item.Top += figurTop;
            //    }
            //    figurTop += 30;

            //}
        }

        bool check = false;
        const int boxLength = 50;
        List<Button> buttons = new List<Button>();

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button button = new Button()
                    {
                        Height = boxLength,
                        Width = boxLength,
                        BackColor = Color.LightSeaGreen,
                        Top = boxLength * i,
                        Left = boxLength * j,
                        Font = new Font(FontFamily.GenericMonospace, 36)
                    };
                    Controls.Add(button);
                    button.Click += Button_Click;
                    buttons.Add(button);
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (check)
            {
                button.Text = "x";
                check = false;
            }
            else
            {
                button.Text = "o";
                check = true;
            }

            var a = buttons
                .Where(_btn => _btn.Location.X == 0 && !String.IsNullOrEmpty(_btn.Text));
            buttons.ForEach(_btn => )
            var b = buttons
                .Where(_btn => _btn.Location.X == boxLength && !String.IsNullOrEmpty(_btn.Text));

            var c = buttons
                .Where(_btn => _btn.Location.X == boxLength * 2 && !String.IsNullOrEmpty(_btn.Text));

            var verticalLine = c.Count() == 3 ? c : (b.Count() == 3 ? b : a.Count() == 3 ? a : null);

            if (verticalLine != null && verticalLine.Count(_btn => _btn.Text == "o" || _btn.Text == "x") == 3)
            {
                MessageBox.Show("sa");
            }
        }

        private void CheckEqualLocation(Button btnSender)
        {

        }
    }
}
