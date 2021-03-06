﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace LocalPLC.ModbusMaster
{
    public partial class modbusmasterDeviceform : Form
    {
        public modbusmasterDeviceform()
        {
            InitializeComponent();
        }

        private ModbusMasterData data_ { get; set; }

        public void getMasterData(ref ModbusMasterData data)
        {
            data_ = data;
            data_.ID = 222;
        }

        public enum COLUMNNAME :  int
            { ID, NAME, SLAVE_ADDR, REPONSE_TIMEOUT, PERMIT_TIMEOUT_COUNT, RECONNECT_INTERVAL
                                        , RESET_VARIABLE, CHANNEL};
        private string[] columnName = {"ID", "name"};
        private void modbusmasterform_Load(object sender, EventArgs e)
        {
            DataGridViewTextBoxColumn cellColumnID = new DataGridViewTextBoxColumn();
            cellColumnID.Name = "ID";
            DataGridViewTextBoxColumn cellColumnName = new DataGridViewTextBoxColumn();
            cellColumnName.Name = "名称";
            DataGridViewTextBoxColumn cellColumnSlaveAddr = new DataGridViewTextBoxColumn();
            cellColumnSlaveAddr.Name = "从站地址";
            DataGridViewTextBoxColumn cellColumnTimeout = new DataGridViewTextBoxColumn();
            cellColumnTimeout.Name = "响应超时(ms)";
            DataGridViewTextBoxColumn cellColumnTimeoutCount = new DataGridViewTextBoxColumn();
            cellColumnTimeoutCount.Name = "允许的超时次数";
            DataGridViewTextBoxColumn cellColumnReconnectInvertal = new DataGridViewTextBoxColumn();
            cellColumnReconnectInvertal.Name = "重连间隔";
            DataGridViewTextBoxColumn cellColumnResetVariable = new DataGridViewTextBoxColumn();
            cellColumnResetVariable.Name = "复位变量";
            DataGridViewDisableButtonColumn buttonColumn = new DataGridViewDisableButtonColumn();
            buttonColumn.Name = "通道";
            //列标题自适应
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;


            dataGridView1.Columns.Add(cellColumnID);
            dataGridView1.Columns.Add(cellColumnName);
            dataGridView1.Columns.Add(cellColumnSlaveAddr);
            dataGridView1.Columns.Add(cellColumnTimeout);
            dataGridView1.Columns.Add(cellColumnTimeoutCount);
            dataGridView1.Columns.Add(cellColumnReconnectInvertal);
            dataGridView1.Columns.Add(cellColumnResetVariable);
            dataGridView1.Columns.Add(buttonColumn);

            dataGridView1.RowCount = 1 + data_.modbusDeviceList.Count;

            int i = 0;
            foreach (DeviceData devData in data_.modbusDeviceList)
            {
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.ID].Value = devData.ID;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value = devData.nameDev;

                //
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.SLAVE_ADDR].Value = devData.slaveAddr;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.REPONSE_TIMEOUT].Value = devData.reponseTimeout;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.PERMIT_TIMEOUT_COUNT].Value = devData.permitTimeoutCount;
    
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RECONNECT_INTERVAL].Value = devData.reconnectInterval;
                 
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RESET_VARIABLE].Value = devData.resetVaraible;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.CHANNEL].Value = "Button " + i.ToString();

                i++;
            }

            //dataGridView1.RowCount = /*8*/ 1;
            dataGridView1.AutoSize = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleCenter;

            this.comboBox_transform_channel.Items.Add("COM1");
            this.comboBox_transform_channel.Text = data_.transformChannel;

            this.textBox_reponse_timeout.Text = data_.responseTimeout.ToString();   //ms

            if(data_.transformMode == 0)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
            }
            else if(data_.transformMode == 1)
            {
                radioButton2.Checked = true;
                radioButton1.Checked = false;
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            int row = dataGridView1.RowCount;
            dataGridView1.RowCount += 1;

            // Set the text for each button.
            int i = row;

            DeviceData data = new DeviceData();

            // for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                //dataGridView1.Rows[i].Cells["ID"].Value = row;
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.ID].Value = row;
                data.ID = row;

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value = "设备" + i.ToString();
                data.nameDev = dataGridView1.Rows[i].Cells[(int)COLUMNNAME.NAME].Value.ToString();

                //
                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.SLAVE_ADDR].Value = "";
                data.slaveAddr = dataGridView1.Rows[i].Cells[(int)COLUMNNAME.SLAVE_ADDR].Value.ToString();

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.REPONSE_TIMEOUT].Value = 1000;
                data.reponseTimeout = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.REPONSE_TIMEOUT].Value.ToString());

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.PERMIT_TIMEOUT_COUNT].Value = 5;
                data.permitTimeoutCount = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.PERMIT_TIMEOUT_COUNT].Value.ToString());

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RECONNECT_INTERVAL].Value = 1000;
                data.reconnectInterval = int.Parse(dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RECONNECT_INTERVAL].Value.ToString());

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RESET_VARIABLE].Value = "";
                data.resetVaraible =  dataGridView1.Rows[i].Cells[(int)COLUMNNAME.RESET_VARIABLE].Value.ToString();

                dataGridView1.Rows[i].Cells[(int)COLUMNNAME.CHANNEL].Value = "Button " + i.ToString();
                //data.


                //dataGridView1.Cell.Value = "Button " + i.ToString();

                data_.modbusDeviceList.Add(data);
            }
        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            // int row = dataGridView1.SelectedRows[0];
            if(dataGridView1.SelectedRows.Count <= 0)
            {
                return;
            }

            for(int i = dataGridView1.SelectedRows.Count - 1; i >= 0; i--)
            {
                int index = dataGridView1.SelectedRows[i].Index;

                dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                data_.modbusDeviceList.RemoveAt(index);
            }


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0 | e.RowIndex < 0)
            {
                return;
            }

            if (dataGridView1.Columns[e.ColumnIndex].Index == (int)COLUMNNAME.CHANNEL)
            {
                DataGridViewDisableButtonCell buttonCell =
                    (DataGridViewDisableButtonCell)dataGridView1.
                    Rows[e.RowIndex].Cells[(int)COLUMNNAME.CHANNEL];

                if (buttonCell.Enabled)
                {
                    //MessageBox.Show(dataGridView1.Rows[e.RowIndex].
                    //    Cells[e.ColumnIndex].Value.ToString() +
                    //    " is enabled");

                    modbusmasterchannel form = new modbusmasterchannel();
                    DeviceData data = data_.modbusDeviceList.ElementAt(e.RowIndex);
                    form.getDeviceData(ref data);
                    form.ShowDialog();
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string str = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (str.Equals(""))
            {

            }

            if (e.ColumnIndex == (int)COLUMNNAME.ID)
            {
                data_.modbusDeviceList.ElementAt(e.RowIndex).ID = int.Parse(str);
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.NAME)
            {
                data_.modbusDeviceList.ElementAt(e.RowIndex).nameDev = str;
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.SLAVE_ADDR)
            {
                data_.modbusDeviceList.ElementAt(e.RowIndex).slaveAddr = str;
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.REPONSE_TIMEOUT)
            {   
                int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).reponseTimeout);
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.PERMIT_TIMEOUT_COUNT)
            {
                int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).permitTimeoutCount);
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.RECONNECT_INTERVAL)
            {
                int.TryParse(str, out data_.modbusDeviceList.ElementAt(e.RowIndex).reconnectInterval);
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.RESET_VARIABLE)
            {
                data_.modbusDeviceList.ElementAt(e.RowIndex).resetVaraible = str;
            }
            else if(e.ColumnIndex == (int)COLUMNNAME.CHANNEL)
            {

            }
        }

        private void comboBox_transform_channel_SelectedIndexChanged(object sender, EventArgs e)
        {
            data_.transformChannel = comboBox_transform_channel.Text;
        }

        private void textBox_reponse_timeout_TextChanged(object sender, EventArgs e)
        {

            string str = textBox_reponse_timeout.Text;
            if(!int.TryParse(str.ToString(), out data_.responseTimeout))
            {
                textBox_reponse_timeout.Text = "1000";
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked == true)
            {
                data_.transformMode = 0;
            }
            else 
            {
                data_.transformMode = 1;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                data_.transformMode = 1;
            }
            else
            {
                data_.transformMode = 0;
            }
        }
    }
}
