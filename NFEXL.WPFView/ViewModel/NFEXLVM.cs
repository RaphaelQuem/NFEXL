using System.Collections.Generic;
using System.ComponentModel;
using NFEXL.Controller;
using System.Windows.Input;
using NFEXL.View.Infra;
using System;
using System.Windows;

namespace NFEXL.View.ViewModel
{
    public class NFEXLVM : INotifyPropertyChanged
    {

        #region Command
        public ICommand OkCommand { get { return okCommand; } }
        private ICommand okCommand;
        #endregion
        #region Controllers
        IOController iocontroller;
        NFEXLController controller;
        #endregion
        #region Interface implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        #region private variables

        private List<string> inputPaths;
        private string inputPath;
        private List<string> outputPaths;
        private string outputPath;
        private Visibility isExporting;
        #endregion
        #region  Properties
        public List<string> InputPaths { get { return iocontroller.GetPaths("INPUT"); } set { inputPaths = value; NotifyPropertyChanged("InputPaths"); } }
        public string InputPath { get { return inputPath; } set { inputPath = value; NotifyPropertyChanged("InputPath"); NotifyPropertyChanged("InputPaths"); } }
        public List<string> OutputPaths { get { return iocontroller.GetPaths("OUTPUT"); } set { outputPaths = value; NotifyPropertyChanged("OutputPaths"); } }
        public string OutputPath { get { return outputPath; } set { outputPath = value; NotifyPropertyChanged("OutputPath"); NotifyPropertyChanged("OutputPaths"); } }
        public Visibility IsExporting { get { return isExporting; } set { isExporting = value; NotifyPropertyChanged("IsExporting"); } }
        #endregion
        #region Methods
        public NFEXLVM()
        {
            iocontroller =  new IOController();
            controller = new NFEXLController();
            okCommand = new CommandHandler(Ok);
            if(InputPaths.Count > 0)
                InputPath = InputPaths[InputPaths.Count - 1];
            if (OutputPaths.Count > 0)
                OutputPath = OutputPaths[OutputPaths.Count - 1];
            IsExporting = Visibility.Hidden;
        }
        private void Ok(object parameter)
        {
            try
            {
                Mouse.OverrideCursor = Cursors.Wait;
                controller.ExportXML(OutputPath, InputPath);
                Mouse.OverrideCursor = Cursors.Arrow;
            }
            catch(Exception ex)
            {
                Mouse.OverrideCursor = Cursors.Arrow;
            }
        }

        #endregion
    }
}
