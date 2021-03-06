﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LocalPLC.ModbusSlave
{
    public partial class modbusslaveform : Form
    {

        private DataManager dataManager = null;
        ModbusSlaveData data_;
        public modbusslaveform(int index)
        {
            InitializeComponent();

            dataManager = DataManager.GetInstance();
        }


        public void getSlaveData(ref ModbusSlaveData data)
        {
            data_ = data;
        }

        private void textBox21_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_coil_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_coil.Text, out data_.dataDevice_.coilCount);
            textBox_coil_start.Text = textBox_coil.Text;
        }


        private void textBox_holding_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_holding.Text, out data_.dataDevice_.holdingCount);
        }



        private void textBox_lisan_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_lisan.Text, out data_.dataDevice_.decreteCount);
        }


        private void textBox_status_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox_status.Text, out data_.dataDevice_.statusCount);
        }

        private void textBox_coil_start_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                data_.dataDevice_.transformMode = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked == true)
            {
                data_.dataDevice_.transformMode = true;
            }
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            int.TryParse(textBox23.Text, out data_.dataDevice_.deviceAddr);
        }

        private void modbusslaveform_Load(object sender, EventArgs e)
        {
            textBox_coil.Text = data_.dataDevice_.coilCount.ToString();
            textBox_holding.Text = data_.dataDevice_.holdingCount.ToString();
            textBox_lisan.Text = data_.dataDevice_.decreteCount.ToString();
            textBox_status.Text = data_.dataDevice_.statusCount.ToString();

            if(data_.dataDevice_.transformMode == false)
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = true;
            }

            textBox23.Text = data_.dataDevice_.deviceAddr.ToString();
        }
    }
}
