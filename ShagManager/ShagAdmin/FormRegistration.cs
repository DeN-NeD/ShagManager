using ShagManager;
using ShagModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShagAdmin
{
    enum RegisterValidation
    {
        LoginEmpty,
        LoginTooShort,
        PasswordEmpty,
        PasswordTooShort
    }
    public partial class FormRegistration : Form
    {
        RegisterValidation validate = RegisterValidation.LoginEmpty;
        WindowsMode mode = WindowsMode.ADD;
        Credential currentCredential=new Credential();
        public FormRegistration()
        {
            InitializeComponent();
            LoadData();
        }

        public FormRegistration(Credential credential)
        {
            InitializeComponent();
            currentCredential = credential;
            mode = WindowsMode.EDIT;
            LoadData();
        }
        private async void LoadData()
        {
            using (ManagerContext context = new ManagerContext())
            {
                var query = await (from c in context.AccessOptions
                            select c.AccessName).ToListAsync();
                comboBoxAccessList.DataSource = query;
                //Есть режим редактирования: использовать предыдущие права
                //if (mode == WindowsMode.EDIT)
                //{
                //    comboBoxAccessList.SelectedValue = currentCredential.AccessList.AccessName;
                //}
            }
        }

        private bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(textBoxLogin.Text))
            {
                
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                validate = RegisterValidation.PasswordEmpty;
                return false;
            }
            if (textBoxLogin.TextLength < 3)
            {
                validate = RegisterValidation.LoginTooShort;
                return false;
            }
            if (textBoxPassword.TextLength < 3)
            {
                validate = RegisterValidation.PasswordTooShort;
                return false;
            }
            return true;
        }
        public Credential GetResult()
        {
            return currentCredential;
        }
        //----------------------------------------------------------------
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                //isUnique
                currentCredential.Login = textBoxLogin.Text;
                currentCredential.Password = textBoxPassword.Text;

                string accessName = comboBoxAccessList.SelectedValue.ToString();
                using (ManagerContext context = new ManagerContext())
                {
                    var query = (from c in context.AccessOptions
                                 where c.AccessName == accessName
                                 select c).FirstOrDefault();
                    if (query != null)
                    {
                        currentCredential.AccessList = query;
                    }
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                switch (validate)
                {
                    case RegisterValidation.LoginEmpty: Notify("Логин не должен быть пустым!"); break;
                    case RegisterValidation.LoginTooShort: Notify("Слишком короткий логин!"); break;
                    case RegisterValidation.PasswordEmpty: Notify("Пароль не должен быть пустым!"); break;
                    case RegisterValidation.PasswordTooShort: Notify("Слишком короткий пароль!"); break;
                }
            }
        }

        private void Notify(string info)
        {
            MessageBox.Show(info, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    

    

       
    }
}
