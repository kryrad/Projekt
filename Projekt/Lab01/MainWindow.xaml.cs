using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Net;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.IO;
using System.Timers;

namespace Lab01
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();

        async Task<int> GetNumberAsync(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException("number", number, "The number must be greater or equal zero");
            int result = 0;
            while (result < number)
            {
                result++;
                await Task.Delay(100);
            }
            return number;
        }

        protected void UpdateProgressBlock(string text, TextBlock textBlock)
        {
            try
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    textBlock.Text = text;
                });
            }
            catch { } 
        }

        class WaitingAnimation
        {
            private readonly int maxNumberOfDots;
            private int currentDots;
            private MainWindow sender;
            

            public WaitingAnimation(int maxNumberOfDots, MainWindow sender)
            {
                this.maxNumberOfDots = maxNumberOfDots;
                this.sender = sender;
                currentDots = 0;
            }

            public void CheckStatus(Object stateInfo)
            {
                sender.UpdateProgressBlock(
                    "Processing" + 
                    new Func<string>(() => {
                        StringBuilder strBuilder = new StringBuilder(string.Empty);
                        for (int i = 0; i < currentDots; i++)
                            strBuilder.Append(".");
                        return strBuilder.ToString();
                    })(), sender.progressTextBlock
                );
                if (currentDots == maxNumberOfDots)
                    currentDots = 0;
                else
                    currentDots++;
            }
        }

        ObservableCollection<Person> people = new ObservableCollection<Person>
        {
        };

        public ObservableCollection<Person> Items
        {
            get => people;
        }

        private static System.Timers.Timer aTimer;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
      
            aTimer = new System.Timers.Timer();

            aTimer.Elapsed += new ElapsedEventHandler(OnTimeWorker);
            aTimer.Interval = 15000;
            aTimer.Enabled = true;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += Worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;
        }

        private void OpenImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg;*.png;*.jpeg)|*.jpg;*.png;*.jpeg"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(openFileDialog.FileName));
            }
        }
        public uint Number { get; set; }

        public byte[] GetJPGFromImageControl(BitmapImage image)
        {
            MemoryStream memStream = new MemoryStream();
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            encoder.Save(memStream);
            return memStream.ToArray();
        }

        private void AddNewPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(nameTextBox.Text)  && image.Source != null)
            {
                people.Add(new Person {Name = nameTextBox.Text, Picture = GetJPGFromImageControl((BitmapImage)image.Source) });
                nameTextBox.Text = String.Empty;
                image.Source = new BitmapImage();
            }
            else
            {
                MessageBox.Show("Nie wszystkie pola wypełnione!");
            }
        }

        public byte[] GetJPGFromUrl(string image)
        {
            byte[] imageBytes;
            using (var webClient = new WebClient())
            {
                imageBytes = webClient.DownloadData(new Uri(image)); 
            }
            return imageBytes;
        }

        private void AddPerson(string name,String image)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                people.Add(new Person { Name = name, Picture = GetJPGFromUrl(image) });
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (worker.IsBusy != true)
                worker.RunWorkerAsync();
        }

        private void OnTimeWorker(object sender, ElapsedEventArgs e)
        {
            aTimer.Stop();
            if (worker.IsBusy != true)
                worker.RunWorkerAsync();
            aTimer.Start();
        }       
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                progressBar.Value = e.ProgressPercentage;
                progressTextBlock2.Text = e.UserState as string;
            });

        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 0; i < Number; i++)
            {
                if (worker.CancellationPending == true)
                {
                    worker.ReportProgress(0, "Cancelled");
                    e.Cancel = true;
                    return;
                }
                else
                {
                    worker.ReportProgress(
                        (int)Math.Round((float)i * 100.0 / (float)Number),
                        "Loading...");

                    string response = WikiConnection.LoadDataAsync().Result;
                    WikiDataEntry result;
                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(response)))
                    {
                        result = WikiParser.Parse(response);
                        AddPerson(result.Title, result.Picture);
                    }
                
                    Thread.Sleep(2000);
                }
            }
            worker.ReportProgress(100, "Done");
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (worker.WorkerSupportsCancellation == true)
            {
                progressTextBlock2.Text = "Cancelling...";
                worker.CancelAsync();
            }
        }
    }
}