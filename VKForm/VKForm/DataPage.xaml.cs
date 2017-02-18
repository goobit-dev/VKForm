using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace VKForm
{
    public partial class DataPage : ContentPage
    {
        public DataPage()
        {
            InitializeComponent();
        }

        public string FormDataText {
            get { return FormData.Text; }
            set { FormData.Text = value; }
        }
    }
}
