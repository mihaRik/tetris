using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public delegate void CreateFigureHandler();
    public partial class Form1 : Form
    {
        List<Label> squares = new List<Label>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            graphics.BeginContainer();
            RectangleF container = new RectangleF(0, 0, 300, 600);
            Region region = new Region(container);
            graphics.Clip = region;
            SolidBrush containerBrush = new SolidBrush(Color.LightSeaGreen);
            graphics.FillRectangle(containerBrush, container);
            Pen pen = new Pen(Color.Gray, 2);

            //horizontal lines

            int linePosX = 0;
            for (int i = 0; i < 20; i++)
            {
                Point startPoint = new Point(0, linePosX);
                Point endPoint = new Point(300, linePosX);
                graphics.DrawLine(pen, startPoint, endPoint);
                linePosX += 30;
            }

            //vertical lines

            int linePosY = 0;
            for (int i = 0; i < 10; i++)
            {
                Point startPoint = new Point(linePosY, 0);
                Point endPoint = new Point(linePosY, 600);
                graphics.DrawLine(pen, startPoint, endPoint);
                linePosY += 30;
            }
        }


        private void CreateObj()
        {
            Label square = new Label()
            {
                Top = 0,
                Left = 150,
                Width = 30,
                Height = 30,
                BackColor = Color.Yellow
            };
            Controls.Add(square);
            squares.Add(square);
        }

        Delegate[] figuresCreator;

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateFigureHandler figureHandler = CreateSquare;
            figureHandler += CreateLine;
            figureHandler += CreateHorse;
            figuresCreator = figureHandler.GetInvocationList();
            figuresCreator[2].DynamicInvoke();
        }

        //private void timer1_Tick(object sender, EventArgs e)
        //{
        //    Label square = squares[squares.Count - 1];

        //    if (square.Bottom < 639 - square.Height * 2 && !CheckCollasion(square))
        //    {
        //        square.Top += 30;
        //    }
        //    else
        //    {
        //        CreateObj();
        //    }

        //    int linePos = 0;
        //    while (linePos <= 600)
        //    {
        //        var line = squares.Where(_square => _square.Bottom == linePos);
        //        line = LineDetector(line, linePos);
        //        linePos += 30;
        //    }

        //}

        //private IEnumerable<Label> LineDetector(IEnumerable<Label> line, int _linePos)
        //{
        //    if (line.Count() == 10)
        //    {
        //        foreach (var item in line)
        //        {
        //            if (Controls.Contains(item) && item is Label)
        //            {
        //                var lbl = (Control)item;
        //                Controls.Remove(lbl);
        //            }
        //        }
        //        squares.RemoveAll(_squar => _squar.Bottom == _linePos);
        //        squares.Where(x => x.Top < _linePos)
        //            .ToList()
        //            .ForEach(x => x.Top += 30);
        //        line = null;
        //    }

        //    return line;
        //}

        //private void Form1_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Label square = squares[squares.Count - 1];
        //    if (square.Bottom < 600 && square.Left >= 0 && square.Left <= 300 - square.Width)
        //    {
        //        switch (e.KeyCode)
        //        {
        //            case Keys.Left:
        //                if (square.Left != 0 &&
        //                    (CheckHorizontalCollasion(square) == "left" || CheckHorizontalCollasion(square) == null))
        //                { square.Left -= 30; }
        //                break;

        //            case Keys.Right:
        //                if (square.Left != 300 - square.Width &&
        //                    (CheckHorizontalCollasion(square) == "right" || CheckHorizontalCollasion(square) == null))
        //                { square.Left += 30; }
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

        //private string CheckHorizontalCollasion(Label _lbl)
        //{
        //    string collasionSide = null;

        //    foreach (Label square in squares)
        //    {
        //        if (
        //            (square != _lbl && _lbl.Left + _lbl.Width == square.Left && _lbl.Bottom == square.Bottom)
        //            ||
        //           (square != _lbl && _lbl.Left == square.Left + square.Width && _lbl.Bottom == square.Bottom)
        //           )
        //        {
        //            collasionSide = square != _lbl && _lbl.Left + _lbl.Width
        //                == square.Left && _lbl.Bottom == square.Bottom
        //                ? "left" : "right";
        //            break;
        //        }
        //        else
        //        {
        //            collasionSide = null;
        //        }
        //    }
        //    return collasionSide;
        //}

        //private bool CheckCollasion(Label _lbl)
        //{
        //    bool result = false;
        //    foreach (Label square in squares)
        //    {
        //        if (square != _lbl && _lbl.Bottom == square.Top && _lbl.Left == square.Left)
        //        {
        //            result = true;
        //            break;
        //        }
        //        else
        //        {
        //            result = false;
        //        }
        //    }
        //    return result;
        //}



        const int _length = 30;
        List<Label> bigSquare = new List<Label>();

        private void MoveFigureY(List<Label> _figure, int _nav)
        {
            _figure.ForEach(x => x.Top = x.Top + _nav);
        }

        private void MoveFigureX(List<Label> _figure, int _nav)
        {
            _figure.ForEach(x => x.Left = x.Left + _nav);
        }

        private void CreateSquare()
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Label label = new Label()
                    {
                        Top = _length * i,
                        Left = _length * j,
                        Width = 30,
                        Height = 30,
                        BackColor = Color.Yellow,
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    Controls.Add(label);
                    bigSquare.Add(label);
                }
            }
            MoveFigureX(bigSquare, 90);
        }

        private void CreateLine()
        {
            for (int i = 0; i < 4; i++)
            {
                Label label = new Label()
                {
                    Top = 0,
                    Left = _length * i,
                    Width = 30,
                    Height = 30,
                    BackColor = Color.Green,
                    BorderStyle = BorderStyle.FixedSingle
                };
                Controls.Add(label);
                bigSquare.Add(label);
            }
            MoveFigureX(bigSquare, 90);
        }

        private void CreateHorse()
        {
            for (int i = 0; i < 4; i++)
            {
                int test = _length;
                int j = i;
                if (i == 0)
                {
                    test = 0;
                    j = i + 1;
                }
                Label label = new Label()
                {
                    Top = test,
                    Left = _length * j,
                    Width = 30,
                    Height = 30,
                    BackColor = Color.Purple,
                    BorderStyle = BorderStyle.FixedSingle
                };
                Controls.Add(label);
                bigSquare.Add(label);
            }
            MoveFigureX(bigSquare, 90);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!bigSquare.Any(x => x.Bottom == 600) && !bigSquare.Any(x => CheckCollasion(x)))
            {
                MoveFigureY(bigSquare, 30);
            }
            else
            {
                bigSquare.ForEach(x => squares.Add(x));
                bigSquare.Clear();
                Random rnd = new Random();
                figuresCreator[rnd.Next(3)].DynamicInvoke();
            }

            int linePos = 0;
            while (linePos <= 600)
            {
                var line = squares.Where(_square => _square.Bottom == linePos);
                line = LineDetector(line, linePos);
                linePos += 30;
            }

            if (bigSquare.Any(x => CheckCollasion(x)) && bigSquare.Any(x => x.Top == 0))
            {
                timer1.Stop();
                timer1.Dispose();
                MessageBox.Show("Score: ", "Game over!", MessageBoxButtons.OK);
            }

        }

        private IEnumerable<Label> LineDetector(IEnumerable<Label> line, int _linePos)
        {
            if (line.Count() == 10)
            {
                foreach (var item in line)
                {
                    if (Controls.Contains(item) && item is Label)
                    {
                        var lbl = (Control)item;
                        Controls.Remove(lbl);
                    }
                }
                squares.RemoveAll(_squar => _squar.Bottom == _linePos);
                squares.Where(x => x.Top < _linePos)
                    .ToList()
                    .ForEach(x => x.Top += 30);
                line = null;
            }

            return line;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (bigSquare.Any(x => x.Bottom < 600) &&
                bigSquare.Any(x => x.Left >= 0) &&
                bigSquare.Any(x => x.Left <= 300 - x.Width))
            {
                switch (e.KeyCode)
                {
                    case Keys.Left:
                        if (!bigSquare.Any(x => x.Left == 0))
                        {
                            MoveFigureX(bigSquare, -30);
                        }
                        break;

                    case Keys.Right:
                        if (!bigSquare.Any(x => x.Left == 300 - x.Width))
                        {
                            MoveFigureX(bigSquare, 30);
                        }
                        break;

                    case Keys.Up:
                        RotateFigure(bigSquare);
                        break;

                    case Keys.Down:
                        if (!bigSquare.Any(x => CheckCollasion(x)) &&
                            bigSquare.Any(x => x.Bottom < 570))
                        {
                            bigSquare.ForEach(x => x.Top = x.Bottom);
                        }
                        break;
                    case Keys.Space:
                        //if (!bigSquare.Any(x => CheckCollasion(x)) &&
                        //    bigSquare.Any(x => x.Bottom < 570))
                        //{
                        //    bigSquare.ForEach(x => {
                        //        if (!bigSquare.Any(y => CheckCollasion(y)))
                        //        {
                        //            x.Top = 570;
                        //        }
                        //         });
                        //}
                        break;
                    default:
                        break;
                }
            }
        }

        //private string CheckHorizontalCollasion(Label _lbl)
        //{
        //    string collasionSide = null;

        //    foreach (Label square in squares)
        //    {
        //        if (
        //            (square != _lbl && _lbl.Left + _lbl.Width == square.Left && _lbl.Bottom == square.Bottom)
        //            ||
        //           (square != _lbl && _lbl.Left == square.Left + square.Width && _lbl.Bottom == square.Bottom)
        //           )
        //        {
        //            collasionSide = square != _lbl && _lbl.Left + _lbl.Width
        //                == square.Left && _lbl.Bottom == square.Bottom
        //                ? "left" : "right";
        //            break;
        //        }
        //        else
        //        {
        //            collasionSide = null;
        //        }
        //    }
        //    return collasionSide;
        //}

        private bool CheckCollasion(Label _lbl)
        {
            bool result = false;
            foreach (Label square in squares)
            {
                if (square != _lbl && _lbl.Bottom == square.Top && _lbl.Left == square.Left)
                {
                    result = true;
                    break;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        private void RotateFigure(List<Label> _figure)
        {
            int dx = _figure[0].Left;
            int dy = _figure[0].Top;
            MoveFigureX(bigSquare, -(dx));
            MoveFigureY(bigSquare, -(dy + 30));
            foreach (Label _lbl in _figure)
            {
                int x = _lbl.Left;
                int y = _lbl.Top;
                _lbl.Left = Convert.ToInt32(x * 0 - y * (-1));
                _lbl.Top = Convert.ToInt32(x * (-1) + y * 0);
            }
            MoveFigureX(bigSquare, dx + 30);
            MoveFigureY(bigSquare, (dy));
        }
    }
}
