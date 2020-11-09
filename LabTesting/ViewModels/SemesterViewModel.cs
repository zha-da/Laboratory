using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LabTesting.ViewModels
{
    class SemesterViewModel : Screen
    {
        private int _take = 0;

        public int Take
        {
            get { return _take; }
            set
            {
                _take = value;
                NotifyOfPropertyChange(() => Take);
                NotifyOfPropertyChange(() => CanShowMessage);
            }
        }

        public bool CanShowMessage
        {
            get => Take == 0;
        }
        public void ShowMessage()
        {
            MessageBox.Show("f");
            Take++;
        }
    }
}
