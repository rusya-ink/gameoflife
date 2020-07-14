using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WpfApp2
{
    public partial class GameWindow : Window
    {
        //Variables
        private static int ScreenWidth = (int)SystemParameters.PrimaryScreenWidth;
        private static int ScreenHeight = (int)SystemParameters.PrimaryScreenHeight;

        //Construct
        public GameWindow()
        {
            this.WindowState = WindowState.Maximized;
            InitializeComponent();
            TableOfGame.GetInstance().CreateTableOfGame(this.TableCanvas);
            this.WindowBorder.Background = TableOfGame.GetInstance().BackgroundColor;
            GameTextBlocks = new List<TextBlock>();
            GameCheckBoxes = new List<CheckBox>();
            AddTextsToList();
        }
        private void TableCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            TableOfGame.GetInstance().SetWidthAndHeight((int)(this.TableCanvas.ActualWidth / TableOfGame.GetInstance().CellSize),
                (int)(this.TableCanvas.ActualHeight / TableOfGame.GetInstance().CellSize));
            TableOfGame.GetInstance().InitializeFirstCells();
            this.WindowBorder.Background = TableOfGame.GetInstance().BackgroundColor;
            SetTextColor(TableOfGame.GetInstance().TextColor);
            TableOfGame.GetInstance().PrintTable();

            stopped = true;
            SetGameTimer();
        }

        //Lists of Elements
        public List<TextBlock> GameTextBlocks;
        public List<CheckBox> GameCheckBoxes;
        public List<Button> GameButtons;
        public void AddTextsToList()
        {
            GameTextBlocks.Add(CommandsText);
            GameTextBlocks.Add(OptionsText);
            GameTextBlocks.Add(ThemeText);
            GameTextBlocks.Add(ColoursText);
            GameTextBlocks.Add(CompleteSetsText);
            GameTextBlocks.Add(SpeedModifierText);
            GameCheckBoxes.Add(PatternsText);
        }

        //Colors
        public void SetTextColor(int RGB)
        {
            foreach (TextBlock text in GameTextBlocks)
                text.Foreground = GetColor(RGB);

            foreach (CheckBox text in GameCheckBoxes)
                text.Foreground = GetColor(RGB);
        }
        public void SetTextColor(SolidColorBrush Color)
        {
            foreach (TextBlock text in GameTextBlocks)
                text.Foreground = Color;

            foreach (CheckBox text in GameCheckBoxes)
                text.Foreground = Color;
        }
        public SolidColorBrush GetColor(int RGB)
        {
            return new SolidColorBrush(Color.FromRgb((byte)(RGB / (256 * 256)),
                (byte)(RGB % (256 * 256) / 256), (byte)(RGB % 256)));
        }

        //Timer and Speed
        private System.Timers.Timer GameTimer;
        public int zeroSpeed = 100;
        public bool stopped;
        public double speedModifier = 1;
        public int numberSteps = 1;
        public void SetGameTimer()
        {
            if(GameTimer!=null)
                GameTimer.Dispose();

            GameTimer = new System.Timers.Timer();
            GameTimer.Interval = zeroSpeed/speedModifier;
            GameTimer.Elapsed += GameTimer_Tick;
            GameTimer.AutoReset=true;
            GameTimer.Enabled = true;
            GameTimer.Start();
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!stopped)
            {
                GameTimer.Stop();
                long delta = DateTime.Now.Millisecond;
                Dispatcher.Invoke((Action)delegate ()
                {
                    for(int i=0; i<numberSteps; i++)
                        TableOfGame.GetInstance().MakeTurn();
                    TableOfGame.GetInstance().PrintTable();
                });
                delta = DateTime.Now.Millisecond - delta;
                Console.WriteLine(delta+" "+TableOfGame.GetInstance().NumberOfCells());
                GameTimer.Interval = Math.Max(0.1,zeroSpeed/speedModifier-(delta+1000)%1000);
                GameTimer.Start();
            }
        }

        //Buttons
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            GameTimer.Dispose();
            this.Close();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            stopped = false;
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            stopped = true;
        }
        private void NextStepButton_Click(object sender, RoutedEventArgs e)
        {
            stopped = true;
            TableOfGame.GetInstance().ForwardTurn();
            TableOfGame.GetInstance().PrintTable();
        }
        private void PrevStepButton_Click(object sender, RoutedEventArgs e)
        {
            stopped = true;
            TableOfGame.GetInstance().ReverseTurn();
            TableOfGame.GetInstance().PrintTable();
        }
        private void AddRleButton_Click(object sender, RoutedEventArgs e)
        {
            string rle = this.RleText.Text, coords = this.CoordsText.Text;
            int z = coords.IndexOf(',');
            if (z > 0)
            {
                int x = StringToInt(coords.Substring(0, z));
                int y = StringToInt(coords.Substring(z + 1));
                TableOfGame.GetInstance().AddRle(rle, x, y);
            }
            else
            {
                TableOfGame.GetInstance().AddRle(rle);
            }
            TableOfGame.GetInstance().PrintTable();


        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            stopped = true;
            TableOfGame.GetInstance().ClearTable();
        }

        //ComboBoxes
        private void SpeedComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                string item = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
                if (item.Equals("0.15x"))
                {
                    speedModifier = 0.15;
                }
                if (item.Equals("0.5x"))
                {
                    speedModifier = 0.5;
                }
                if (item.Equals("1x"))
                {
                    speedModifier = 1;
                }
                if (item.Equals("2x"))
                {
                    speedModifier = 2;
                }
                if (item.Equals("4x"))
                {
                    speedModifier = 4;
                }
                
                SetGameTimer();
            }
        }
        private void ThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                string item = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
                if (item.Equals("Light Blue"))
                {
                    TableOfGame.GetInstance().BackgroundColor = GetColor(0xE0E0FF);
                    TableOfGame.GetInstance().AliveColor = GetColor(0x191970);
                    TableOfGame.GetInstance().TextColor = GetColor(0x000000);
                }
                if (item.Equals("Light Red"))
                {
                    TableOfGame.GetInstance().BackgroundColor = GetColor(0xFFE0E0);
                    TableOfGame.GetInstance().AliveColor = GetColor(0x701919);
                    TableOfGame.GetInstance().TextColor = GetColor(0x000000);
                }
                if (item.Equals("Light Green"))
                {
                    TableOfGame.GetInstance().BackgroundColor = GetColor(0xE0FFE0);
                    TableOfGame.GetInstance().AliveColor = GetColor(0x197019);
                    TableOfGame.GetInstance().TextColor = GetColor(0x000000);
                }
                if (item.Equals("Dark Blue"))
                {
                    TableOfGame.GetInstance().AliveColor = GetColor(0xE0E0FF);
                    TableOfGame.GetInstance().BackgroundColor = GetColor(0x191970);
                    TableOfGame.GetInstance().TextColor = GetColor(0xFFFFFF);
                    //StartButton.Background = TableOfGame.GetInstance().BackgroundColor;
                    //StartButton.Foreground = TableOfGame.GetInstance().AliveColor;
                }
                if (item.Equals("Dark Red"))
                {
                    TableOfGame.GetInstance().AliveColor = GetColor(0xFFE0E0);
                    TableOfGame.GetInstance().BackgroundColor = GetColor(0x701919);
                    TableOfGame.GetInstance().TextColor = GetColor(0xFFFFFF);
                }
                if (item.Equals("Dark Green"))
                {
                    TableOfGame.GetInstance().AliveColor = GetColor(0xE0FFE0);
                    TableOfGame.GetInstance().BackgroundColor = GetColor(0x197019);
                    TableOfGame.GetInstance().TextColor = GetColor(0xFFFFFF);
                }
                if (item.Equals("User Theme"))
                {
                    this.BackgroundColorComboBox.IsEnabled = true;
                    this.AliveColorComboBox.IsEnabled = true;
                    this.ChangeUserThemeButton.IsEnabled = true;
                    this.WindowBorder.Background = TableOfGame.GetInstance().UserBackgroundColor;
                    SetTextColor(TableOfGame.GetInstance().UserTextColor);
                    TableOfGame.GetInstance().AliveColor = TableOfGame.GetInstance().UserAliveColor;
                }
                else
                {
                    this.BackgroundColorComboBox.IsEnabled = false;
                    this.AliveColorComboBox.IsEnabled = false;
                    this.ChangeUserThemeButton.IsEnabled = false;
                    this.WindowBorder.Background = TableOfGame.GetInstance().BackgroundColor;
                    SetTextColor(TableOfGame.GetInstance().TextColor);
                }
                

                
                TableOfGame.GetInstance().PrintTable();
            }
        }
        private void AliveColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
            if (item.Equals("Light Blue"))
            {
                TableOfGame.GetInstance().UserAliveColor = GetColor(0xE0E0FF);
                TableOfGame.GetInstance().UserTextColor = GetColor(0xE0E0FF);
            }
            if (item.Equals("Dark Blue"))
            {
                TableOfGame.GetInstance().UserAliveColor = GetColor(0x191970);
                TableOfGame.GetInstance().UserTextColor = GetColor(0x191970);
            }
        }
        private void BackgroundColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string item = (string)((ComboBoxItem)((ComboBox)sender).SelectedValue).Content;
            if (item.Equals("Light Blue"))
            {
                TableOfGame.GetInstance().UserBackgroundColor = GetColor(0xE0E0FF);
            }
            if (item.Equals("Dark Blue"))
            {
                TableOfGame.GetInstance().UserBackgroundColor = GetColor(0x191970);
            }
        }
        private void ChangeUserThemeButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowBorder.Background = TableOfGame.GetInstance().UserBackgroundColor;
            SetTextColor(TableOfGame.GetInstance().UserTextColor);
            TableOfGame.GetInstance().AliveColor = TableOfGame.GetInstance().UserAliveColor;
        }

        //Common functions
        public int StringToInt(string s)
        {
            int num = 0;
            for (int i = 0; i < s.Length; i++)
            {
                num = num * 10 + s[i] - '0';
            }
            return num;
        }
    }
}
