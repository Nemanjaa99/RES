using Common.FileUpload;
using Common.FileUpload.Interfaces;
using Common.Helper;
using Common.Helper.Interfaces;
using Common.Logging;
using Common.Model;
using DatabaseAccess;
using DatabaseAccess.Interfaces;
using ServiceEngine;
using System;
using System.Collections.Generic;
using System.Windows;

namespace UIApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private string message;

		private OutputModel outputModel = new OutputModel();
		private List<string> regions = new List<string>();

		private IDataTypeParser dataTypeParser;
		private IMathHelper mathHelper;
		private ISqliteDataAccess sqliteDataAccess;
		private IFileDialog fileDialog;
		private PowerImporter powerImporter;
		private PowerConsumptionCalculator powerConsumptionCalculator;

		public MainWindow()
		{
			InitializeComponent();

			dataTypeParser = new DataTypeParser();
			mathHelper = new MathHelper();
			sqliteDataAccess = new SqliteDataAccess();
			fileDialog = new FileDialog(dataTypeParser);
			powerImporter = new PowerImporter(fileDialog, sqliteDataAccess);
			powerConsumptionCalculator = new PowerConsumptionCalculator(sqliteDataAccess, dataTypeParser, mathHelper);
		}

		
		private void browseExpetedPower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();

			Logger.Log(Operation.Info, "Browsing for expeted power consumption .csv file");
			expetedPowerTextBlock.Text = fileDialog.GetDialogPathName();
		}

		
		private void browseActualPower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();

			Logger.Log(Operation.Info, "Browsing for actual power consumption .csv file");
			actualPowerTextBlock.Text = fileDialog.GetDialogPathName();
		}

		
		private void importButton_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			Logger.Log(Operation.Info, "Validating input .csv files");

			if (!ValidateImportFiles())
			{
				return;
			}

			try
			{
				powerImporter.CollectData(expetedPowerTextBlock.Text, actualPowerTextBlock.Text);
			}
			catch (Exception ex)
			{
				errorTextBlock.Text = ex.Message;
				Logger.Log(Operation.Error, ex.Message);
				return;
			}

			message = "You have successfully imported files in db!";
			Logger.Log(Operation.Info, message);
			WriteSuccessfullyMessage(message);
		}

		
		private void calculatePower_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			Logger.Log(Operation.Info, "Calculating power consumption started!");

			if (!ValidatePowerConsumptionInputs())
			{
				return;
			}

			DateTime fromDate = dataTypeParser.ConvertDateTimeFromString(fromDatePicker.Text);
			DateTime toDate = dataTypeParser.ConvertDateTimeFromString(toDatePicker.Text);

			if (!CheckDatesValues(fromDate, toDate))
			{
				return;
			}

			try
			{
				outputModel = powerConsumptionCalculator.CalculatePower(fromDate, toDate, regionComboBox.Text);
			}
			catch (Exception ex)
			{
				errorTextBlock.Text = ex.Message;
				Logger.Log(Operation.Error, ex.Message);
				return;
			}

			WriteOutputModelOnScreen();

			message = "Calculating power consumption ended!";
			Logger.Log(Operation.Info, message);
			WriteSuccessfullyMessage(message);
		}

		
		private void exportToXML_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			Logger.Log(Operation.Info, "Exporting result to XML!");

			if (!ValidateOutputTextBlock())
			{
				return;
			}

			try
			{
				fileDialog.SaveToXML(outputModel, "../../../../output.xml");
			}
			catch (Exception ex)
			{
				errorTextBlock.Text = ex.Message;
				Logger.Log(Operation.Error, ex.Message);
				return;
			}

			message = "You have successfully exported files to XML!";
			Logger.Log(Operation.Info, message);
			WriteSuccessfullyMessage(message);
		}

		
		private void refreshRegions_Click(object sender, RoutedEventArgs e)
		{
			ResetErrorText();
			Logger.Log(Operation.Info, "Refreshing regions combobox started!");

			regions.Clear();
			regions = sqliteDataAccess.LoadRegions("ExpetedConsumption");

			regionComboBox.ItemsSource = regions;
			Logger.Log(Operation.Info, "Refreshing regions combobox ended!");
		}

		
		private bool ValidateImportFiles()
		{
			if (expetedPowerTextBlock.Text == string.Empty || actualPowerTextBlock.Text == string.Empty)
			{
				message = "Please select excel files first!";
				errorTextBlock.Text = message;
				Logger.Log(Operation.Error, message);

				return false;
			}

			return true;
		}

		
		private bool ValidatePowerConsumptionInputs()
		{
			if (fromDatePicker.Text == string.Empty || toDatePicker.Text == string.Empty || regionComboBox.Text == string.Empty)
			{
				message = "Please select necessary inputs first!";
				errorTextBlock.Text = message;
				Logger.Log(Operation.Error, message);

				return false;
			}

			return true;
		}

		private bool CheckDatesValues(DateTime from, DateTime to)
		{
			if (from > to)
			{
				message = "Starting date needs to be lower then finished date!";
				errorTextBlock.Text = message;
				Logger.Log(Operation.Error, message);

				return false;
			}

			return true;
		}

	
		private bool ValidateOutputTextBlock()
		{
			if (outputTextBlock.Text == string.Empty)
			{
				message = "Output screen is empty, please calculate first power consumption then export it to XML!";
				errorTextBlock.Text = message;
				Logger.Log(Operation.Error, message);

				return false;
			}

			return true;
		}

		
		private void ResetErrorText()
		{
			errorTextBlock.Text = string.Empty;
		}

		
		private void WriteSuccessfullyMessage(string message)
		{
			MessageBox.Show(message);
		}

		
		private void WriteOutputModelOnScreen()
		{
			outputTextBlock.Text = string.Empty;

			outputTextBlock.Text += "Result:\n";
			outputTextBlock.Text += $"Mean deviation is: {outputModel.MeanDeviation}\n";
			outputTextBlock.Text += $"Square deviation is: {outputModel.SquareDeviation}\n";

			foreach (var calculatedPower in outputModel.CalculatedPowers)
			{
				outputTextBlock.Text += $"Calculated absolute average power consumption for date: {calculatedPower.Date.ToString("dd/MM/yyyy")}\n";
				foreach (var value in calculatedPower.AverageRecords)
				{
					outputTextBlock.Text += $"Hour {value.Hour}, value: {value.AbsoluteValue}\n";
				}
			}
		}
	}
}