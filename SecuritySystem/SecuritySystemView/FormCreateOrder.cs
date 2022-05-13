using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SecuritySystemContracts.ViewModels;
using SecuritySystemContracts.BuisnessLogicsContracts;
using SecuritySystemContracts.BindingModels;

namespace SecuritySystemView
{
    public partial class FormCreateOrder : Form
    {
        private readonly ISecureLogic _logicP;
        private readonly IOrderLogic _logicO;
        private readonly IClientLogic _logicC;
        public FormCreateOrder(ISecureLogic logicP, IOrderLogic logicO, IClientLogic logicC)
        {
            InitializeComponent();
            _logicP = logicP;
            _logicO = logicO;
            _logicC = logicC;
        }
        public void FormCreateOrder_Load(object sender, EventArgs e)
        {
            List<SecureViewModel> list = _logicP.Read(null);
            if (list != null)
            {
                comboBoxSecure.DisplayMember = "SecureName";
                comboBoxSecure.ValueMember = "Id";
                comboBoxSecure.DataSource = list;
                comboBoxSecure.SelectedItem = null;
            }
            List<ClientViewModel> listC = _logicC.Read(null);
            if (listC != null)
            {
                comboBoxClient.DisplayMember = "ClientFLM";
                comboBoxClient.ValueMember = "Id";
                comboBoxClient.DataSource = listC;
                comboBoxClient.SelectedItem = null;
            }
        }
        private void CalcSum()
        {
            if (comboBoxSecure.SelectedValue != null &&
           !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxSecure.SelectedValue);
                    SecureViewModel secure = _logicP.Read(new SecureBindingModel
                    {
                        Id
                    = id
                    })?[0];
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * secure?.Price ?? 0).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void TextBoxCount_TextChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ComboBoxSecure_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка",
               MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxSecure.SelectedValue == null)
            {
                MessageBox.Show("Выберите изделие", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                _logicO.CreateOrder(new CreateOrderBindingModel
                {
                    ClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    SecureId = Convert.ToInt32(comboBoxSecure.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void ButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}
