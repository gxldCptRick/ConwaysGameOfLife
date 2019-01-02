using ConsoleViewOfTheGameOfLife;
using System.Timers;
using System.Windows;
using System.Windows.Data;
using System.Windows.Shapes;

namespace ConwaysGameOfLifeGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string StartText = "Run Evolutionary Game.";
        private const string StopText = "Pause Evolutionary Game.";
        private const int MillisecondsPerSecond = 1000;

        private ConwaysGameController<ConwayCell> game;
        private int size;
        private Timer evolutionTimer;


        public MainWindow()
        {
            InitializeComponent();
            evolutionTimer = new Timer();
            evolutionTimer.Elapsed += (s, e) => game?.RunEvolution();
            UpdateTimerInterval();
        }

        private void UpdateTimerInterval()
        {
            if (evolutionTimer != null)
            {
                evolutionTimer.Interval = MillisecondsPerSecond / framesPerSecond.Value;
            }
        }

        private void ClearAndRefillGrid()
        {
            conwaySpawnGrid.Children.Clear();
            game = new ConwaysGameController<ConwayCell>(size, (cell) =>
            {
                var rect = new Rectangle()
                {
                    Margin = new Thickness(0.1)
                };

                rect.MouseDown += (s, e) => cell.IsAlive = !cell.IsAlive;

                var binding = new Binding
                {
                    Path = new PropertyPath(nameof(cell.IsAlive)),
                    Source = cell,
                    Converter = new CellColorConverter()
                };
                rect.SetBinding(Shape.FillProperty, binding);
                conwaySpawnGrid.Children.Add(rect);
            });
        }

        private void InitializeGrid()
        {
            evolutionTimer?.Stop();
            StartStopTimerBtn.Content = StartText;
            size = (int)gridSize.Value;
            ClearAndRefillGrid();
        }

        private void OnLoad_Event(object sender, RoutedEventArgs e)
        {
            InitializeGrid();
        }

        private void RebuildBoardClicked(object sender, RoutedEventArgs e)
        {
            InitializeGrid();
        }

        private void RandomzieButtonClicked(object sender, RoutedEventArgs e)
        {
            game?.RandomizeBoard();
        }

        private void RunEvolutionaryStep(object sender, RoutedEventArgs e)
        {
            game?.RunEvolution();
        }

        private void Start_StopTimerClicked(object sender, RoutedEventArgs e)
        {
            if (evolutionTimer.Enabled)
            {
                StartStopTimerBtn.Content = StartText;
                evolutionTimer.Stop();
            }
            else
            {
                StartStopTimerBtn.Content = StopText;
                evolutionTimer.Start();
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            evolutionTimer?.Stop();
        }

        private void FramesPerSecond_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            UpdateTimerInterval();
        }
    }
}