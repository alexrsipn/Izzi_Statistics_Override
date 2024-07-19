using Izzi_Statistics_Override_WPF.Controller;
using Izzi_Statistics_Override_WPF.Model;
using Izzi_Statistics_Override_WPF.Util;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Izzi_Statistics_Override_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CSVController csv = new CSVController();
        LoggerController logger = new LoggerController();
        UtilWebRequest webRequest = new UtilWebRequest();
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_UploadFile_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog file = new Microsoft.Win32.OpenFileDialog();
            file.InitialDirectory = "c:\\Users\\ALEXRUIZ\\DEV\\Izzi_Statistics_Override_WPF\\Layout";
            file.Filter = "Archivos CSV (*.csv)|*.csv";
            file.FilterIndex = 1;
            file.RestoreDirectory = true;
            bool? result = file.ShowDialog();
            try
            {
                if (result == true)
                {
                    TextBox_FileName.Text = file.FileName;
                    logger.logPath = file.InitialDirectory;
                    DataTable dt = new DataTable();
                    dt = await csv.ReadCSV(file.FileName);
                    csv.DTable = dt;
                    DG_FileContent.ItemsSource = dt.AsDataView();
                    DG_FileContent.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);
                    SetVisibilityOn();
                }
                else
                {
                    MessageBox.Show("Archivo no cargado");
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show(string.Format("Error: {0} | Detalle: {1}", ex.Message, ex.InnerException.Message));
            }

        }
        private void SetVisibilityOn()
        {
            Button_Execute.Visibility = Visibility.Visible;
            Button_CleanApp.Visibility = Visibility.Visible;
            Button_UploadFile.IsDefault = false;
            Button_Execute.IsDefault = true;
        }
        private DataTable FormatDT (DataTable dt)
        {
            DataTable dtCloned = dt.Clone();
            dtCloned.Columns["override"].DataType = typeof(int);
            foreach (DataRow row in dt.Rows)
            {
                dtCloned.ImportRow(row);
            }
            return dtCloned;
        }
        private string ToJSON(DataTable dt)
        {
            string jsonString = string.Empty;
            jsonString = JsonConvert.SerializeObject(dt, Formatting.Indented);
            return jsonString;
        }
        private async void Button_Execute_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1.IsEnabled = false;
            PB_Loading.Visibility = Visibility.Visible;
            DataTable dt = new DataTable();
            dt = csv.DTable;
            dt = csv.ValidateCSV(dt);
            DG_FileContent.ItemsSource = dt.AsDataView();
            if (dt != null)
            {
                string json = ToJSON(FormatDT(dt));
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                logger.Logger("-------------------");
                logger.Logger($"Inicio de proceso para instancia {Properties.Settings.Default.key_url}");
                OFSC_Response resultado = await webRequest.UpdateActivityDurationStatistics(json);
                if (resultado.statusCode >= 200 || resultado.statusCode < 299)
                {
                    dynamic jsonResult = JsonConvert.DeserializeObject(resultado.content);
                    logger.Logger($"Código HTTP {jsonResult.status} con {jsonResult.updatedRecords} registros actualizados.", LoggerController.LoggerOption.OK);
                    MessageBox.Show($"Proceso finalizado con éxito. \nCampos cargados en OFSC: {jsonResult.updatedRecords} de {dt.Rows.Count}");
                }
                else
                {
                    MessageBox.Show($"Error, revisar el log: {resultado.content}");
                }
                stopwatch.Stop();
                logger.Logger("Milisegundos " + stopwatch.Elapsed.TotalMilliseconds.ToString());
                logger.Logger("Segundos " + stopwatch.Elapsed.TotalSeconds.ToString());
                logger.Logger("Minutos " + stopwatch.Elapsed.TotalMinutes.ToString());
                logger.Logger(DateTime.Now.ToString());
                logger.Logger(" End ");
                logger.Logger("----------------------------------------------------------------------------------");
            }
            else
            {
                MessageBox.Show("Error en obtención de datos");
            }
            PB_Loading.Visibility = Visibility.Collapsed;
            PB_Loading.Value = PB_Loading.Minimum;
            MainWindow1.IsEnabled = true;
        }

        private void Button_CleanApp_Click(object sender, RoutedEventArgs e)
        {
            csv.DTable = new DataTable();
            TextBox_FileName.Text = "";
            Button_Execute.Visibility = Visibility.Hidden;
            Button_CleanApp.Visibility = Visibility.Hidden;
            DG_FileContent.ItemsSource = new DataTable().AsDataView();
            PB_Loading.Visibility = Visibility.Hidden;
            MessageBox.Show("Proceso reiniciado");
        }
        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
