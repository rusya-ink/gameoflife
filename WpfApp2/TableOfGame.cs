using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApp2
{
    class TableOfGame
    {
        //Parameters
        public int WIDTH { get; set; }
        public int HEIGHT { get; set; }
        public int CellSize = 10;
        public void SetWidthAndHeight(int w, int h)
        {
            WIDTH = w;
            HEIGHT = h;
        }
        public int NumberOfCells()
        {
            return table.Count;
        }

        //Colors
        public SolidColorBrush AliveColor { get; set; }
        public SolidColorBrush TextColor { get; set; }
        public SolidColorBrush BackgroundColor { get; set; }
        public SolidColorBrush UserAliveColor { get; set; }
        public SolidColorBrush UserTextColor { get; set; }
        public SolidColorBrush UserBackgroundColor { get; set; }
        public SolidColorBrush GetColor(int RGB)
        {
            return new SolidColorBrush(Color.FromRgb((byte)(RGB / (256 * 256)),
                (byte)(RGB % (256 * 256) / 256), (byte)(RGB % 256)));
        }

        //Canvas
        public Canvas GameCanvas;
        
        //History and Table
        private List<SortedSet<GameCell>> history;
        public int historyPointer;
        private SortedSet<GameCell> table;

        //Construct
        private TableOfGame() { }
        private static TableOfGame instance;
        public void CreateTableOfGame(Canvas GameCanvas)
        {
            BackgroundColor = new SolidColorBrush(Color.FromRgb(0xE0, 0xE0, 0xFF));
            AliveColor = new SolidColorBrush(Color.FromRgb(0x19, 0x19, 0x70));
            TextColor = new SolidColorBrush(Color.FromRgb(0,0,0));
            historyPointer = 0;
            this.GameCanvas = GameCanvas;
            history = new List<SortedSet<GameCell>>();
            table = new SortedSet<GameCell>();
        }
        public void InitializeFirstCells()
        {
            
            history.Add(table);
        }
        public static TableOfGame GetInstance()
        {
            if (instance == null)
                instance = new TableOfGame();
            return instance;
        }
        public void ClearTable()
        {
            table.Clear();
            PrintTable();
        }


        //Turns
        public void MakeTurn()
        {
            if(historyPointer!=history.Count-1)
                history = history.GetRange(0, historyPointer+1);
            
            table = new SortedSet<GameCell>();
            foreach (GameCell cell in history[history.Count - 1])
                AddEverythingInRaduisToTable(cell.X, cell.Y);

            GameCell[] extraTable = new GameCell[table.Count];
            table.CopyTo(extraTable);
            foreach (GameCell cell in extraTable)
            {
                if (!cell.Color && cell.Neihbours != 3)
                    RemoveGameCellFromTable(cell.X, cell.Y);

                if (cell.Color && (cell.Neihbours < 3 || cell.Neihbours > 4))
                    RemoveGameCellFromTable(cell.X, cell.Y);
            }
            history.Add(table);
            historyPointer++;
        }
        public void ForwardTurn()
        {
            if (historyPointer < history.Count - 1)
                historyPointer++;
            else
                MakeTurn();
        }
        public void ReverseTurn()
        {
            if (historyPointer >0)
                historyPointer--;
        }
        //public bool GetNextState(int x, int y)
        //{
        //    int c = 0;
        //    for(int i=-1; i<2; i++)
        //    {
        //        for(int j=-1; j<2; j++)
        //        {
        //            if (history[history.Count - 1].Contains(new GameCell(FormatX(x + i), FormatY(y + j))))
        //                c++;
        //        }
        //    }
        //    return c == 3 || (history[history.Count - 1].Contains(new GameCell(FormatX(x), FormatY(y))) && c == 4);
        //}

        //Adding and Removing Points
        public void AddEverythingInRaduisToTable(int x, int y, int n=1)
        {
            AddGameCellToTable(x, y);
            for (int dx = -n; dx < n+1; dx++)
            {
                for(int dy=-n; dy<n+1; dy++)
                {
                    if(dx!=0||dy!=0)
                        AddGameCellToTable(FormatX(x+dx), FormatY(y+dy), false);
                }
            }
        }
        public void RemoveGameCellFromTable(int x, int y)
        {
            table.Remove(new GameCell(FormatX(x), FormatY(y)));
        }
        public void AddGameCellToTable(int x, int y, bool color = true)
        {
            table.Add(new GameCell(FormatX(x), FormatY(y), color));
        }
        public void AddGameCellToTable(GameCell GC)
        {
            table.Add(GC);
        }

        //Rle
        public void AddRle(string rle_, int x_ = 0, int y_ = 0)
        {
            string rle = rle_ + '$';
            int x = x_, y = y_, num = 1, k = 0;
            char last = rle[0];
            for (int i = 0; i < rle.Length; i++)
            {
                if (rle[i] >= '0' && rle[i] <= '9')
                {
                    num = k * num * 10 + rle[i] - '0';
                    k = 1;
                }
                else
                {
                    if (rle[i] == 'o')
                    {
                        for(int j=0; j<num; j++)
                        {
                            AddGameCellToTable(x, y);
                            x++;
                        }
                    }
                    if (rle[i] == 'b')
                    {
                        x += num;
                    }
                    if (rle[i] == '$')
                    {
                        x = x_;
                        y += num;
                    }
                    k = 0;
                    num = 1;
                }
            }

        }

        //Formating Coordinates
        public int FormatX(int x)
        {
            return (x % WIDTH+WIDTH) % WIDTH;
        }
        public int FormatY(int y)
        {
            return (y % HEIGHT + HEIGHT) % HEIGHT;
        }
        public int GetCanvasCoordinate(int x, int y)
        {
            return x + y * WIDTH;
        }

        //Printing
        public void PrintTable()
        {
            GameCanvas.Children.Clear();
            foreach (GameCell cell in history[historyPointer])
            {
                Ellipse ell = new Ellipse
                {
                    Fill = AliveColor,
                    Height = CellSize,
                    Width = CellSize
                };
                Canvas.SetLeft(ell, cell.X * CellSize);
                Canvas.SetTop(ell, cell.Y * CellSize);
                GameCanvas.Children.Add(ell);
            }
        }
    }
}
