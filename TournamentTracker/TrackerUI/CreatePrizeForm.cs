using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form
    {
        IPrizeRequester callingForm;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                PrizeModel model = new PrizeModel(
                    placeNumberValue.Text,
                    placeNameValue.Text,      
                    prizeAmountValue.Text, 
                    prizePercentageValue.Text);

                GlobalConfig.Connection.CreatePrize(model);

                callingForm.PrizeComplete(model);
                this.Close();             
            }

            else
            {
                MessageBox.Show("Form is not valid!");
            }
        }

        private bool ValidateForm()
        {
            bool output = true;
            int placeNumber = 0;
            decimal prizeAmount = 0;
            double prizePercentage = 0;

            if (!int.TryParse(placeNumberValue.Text, out placeNumber))
            {
                // TODO - Add some message
                output = false;
            }

            if (placeNumber < 1)
            {
                output = false;
            }

            if (placeNameValue.Text.Length == 0)
            {
                output = false;
            }

            if (!double.TryParse(prizePercentageValue.Text, out prizePercentage) || !decimal.TryParse(prizeAmountValue.Text, out prizeAmount))
            {
                output = false;
            }

            if(prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }
            
            if(prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }

            return output;
        }        
    }
}
