
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutomatedDiskCleaner
{
    public partial class FrmScheduler : Form
    {
        public FrmScheduler()
        {
            InitializeComponent();
        }

        private void FrmScheduler_Load(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void ResetControl()
        {
            // Reset controls
            this.labelDay.Visible = false;
            this.labelDate.Visible = false;
            this.labelTime.Visible = false;
            this.dateTimePickerSched.Visible = false;
            this.comboBoxDate.Visible = false;
            this.comboBoxDay.Visible = false;
        }
        
        private void radioButtonNever_CheckedChanged(object sender, EventArgs e)
        {
            ResetControl();
            if (this.radioButtonNever.Checked)
            {
                this.groupBoxSchedule.Visible = false;
                this.groupBoxDesc.Visible = false;
                this.labelDescription.Text = "";
            }
        }

        private void radioButtonDaily_CheckedChanged(object sender, EventArgs e)
        {
            ResetControl();
            if (this.radioButtonDaily.Checked)
            {
                this.groupBoxSchedule.Visible = true;
                this.groupBoxDesc.Visible = true;
                this.labelTime.Visible = true;
                this.dateTimePickerSched.Visible = true;
                this.labelDescription.Text = string.Format("A disk scan will be run every day at {0}", this.dateTimePickerSched.Value.ToShortTimeString());
            }
        }

        private void radioButtonWeekly_CheckedChanged(object sender, EventArgs e)
        {
            ResetControl();
            if (this.radioButtonWeekly.Checked)
            {
                this.groupBoxSchedule.Visible = true;
                this.labelTime.Visible = true;
                this.dateTimePickerSched.Visible = true;
                this.labelDay.Visible = true;
                this.comboBoxDay.Visible = true;
                this.groupBoxDesc.Visible = true;

                this.labelDescription.Text = string.Format("A disk scan will be run every week on {0} at {1}", this.comboBoxDay.SelectedItem, this.dateTimePickerSched.Value.ToShortTimeString());
            }
        }

        private void radioButtonMonthly_CheckedChanged(object sender, EventArgs e)
        {
            ResetControl();
            if (this.radioButtonMonthly.Checked)
            {
                this.groupBoxSchedule.Visible = true;
                this.labelTime.Visible = true;
                this.dateTimePickerSched.Visible = true;
                this.labelDate.Visible = true;
                this.comboBoxDate.Visible = true;
                this.groupBoxDesc.Visible = true;
                this.labelDescription.Text = string.Format("A disk scan will be run every month on the {0} day at {1}", this.comboBoxDate.SelectedItem, this.dateTimePickerSched.Value.ToShortTimeString());
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
