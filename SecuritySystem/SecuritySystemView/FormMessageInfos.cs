﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecuritySystemContracts.BindingModels;
using SecuritySystemContracts.BuisnessLogicsContracts;

namespace SecuritySystemView
{
    public partial class FormMessageInfos : Form
    {
        private readonly IMessageInfoLogic logic;

        public FormMessageInfos(IMessageInfoLogic _logic)
        {
            InitializeComponent();
            logic = _logic;
        }

        private void FormMessageInfos_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = logic.Read(null);
                if (list != null)
                {
                    dataGridViewMessages.DataSource = list;
                    dataGridViewMessages.Columns[0].Visible = false;
                    dataGridViewMessages.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}