using ShagModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShagAdmin
{
    enum ContactValidation
    {
        AddressEmpty,
        CountryEmpty,
        EmailEmpty,
        PhoneEmpty,
        Phone2Empty,
        EmailNotCorrect,
        PhoneNotCorrect,
        Phone2NotCorrect
            
    }
    public partial class ContactForm : Form
    {
        WindowsMode mode = WindowsMode.ADD;
        ContactValidation validate = ContactValidation.AddressEmpty;
        ContactInfo info;
        Manager currentManager;
        public ContactForm()
        {
            InitializeComponent();
        }
        public ContactForm(Manager manager)
        {
            InitializeComponent();
            mode = WindowsMode.EDIT;
            currentManager = manager;
            LoadManager();
        }
        private void LoadManager()
        {
            textBoxAdress.Text = currentManager.Person.Info.Address;
            textBoxCountry.Text = currentManager.Person.Info.Country;
            textBoxEmail.Text = currentManager.Person.Info.Email;
            textBoxPhone1.Text = currentManager.Person.Info.Phone1;
            textBoxPhone2.Text = currentManager.Person.Info.Phone2;                
        }
        public ContactInfo GetResult()
        {
            return info;
        }
        private bool CheckData()
        { //Личный телефон
            if (string.IsNullOrWhiteSpace(textBoxPhone1.Text) && string.IsNullOrWhiteSpace(textBoxPhone2.Text))
            {
                validate = ContactValidation.PhoneEmpty;
                return false;
            }
        //Домашний телефон
            if (string.IsNullOrWhiteSpace(textBoxPhone2.Text) && string.IsNullOrWhiteSpace(textBoxPhone2.Text))
            {
                validate = ContactValidation.Phone2Empty;
                return false;
            }
        //Почтовый адресс
            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                validate = ContactValidation.EmailEmpty;
                return false;

            }
        //Адресс
            if (string.IsNullOrWhiteSpace(textBoxAdress.Text))
            {
                validate = ContactValidation.AddressEmpty;
                return false;
            }
            //Страна
            if (string.IsNullOrWhiteSpace(textBoxCountry.Text))
            {
                validate = ContactValidation.CountryEmpty;
                return false;
            }
            
                   
            return true;
            
        }
        private void AddData()
        {
            info = new ContactInfo();
            info.Address = textBoxAdress.Text;
            info.Country = textBoxCountry.Text;
            info.Email = textBoxEmail.Text;
            info.Phone1 = textBoxPhone1.Text;
            info.Phone2 = textBoxPhone2.Text;
            if (checkBoxIsResident.CheckState == CheckState.Checked)
            {
                info.IsResident = true;
            }
            else
            {
                info.IsResident = false;
            }
            // info.IsResident=
        }
        //-------------------------------------------------------------------------
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                AddData();
                DialogResult = DialogResult.OK;
            }
            else
            {
                switch (validate)
                {
                    case ContactValidation.AddressEmpty: Notify("Адресс не должен быть пуст!"); break;
                    case ContactValidation.CountryEmpty: Notify("Страна не выбрана!"); break;
                    case ContactValidation.EmailEmpty: Notify("Почтовый адресс не должен быть пуст!"); break;
                    case ContactValidation.EmailNotCorrect: Notify("Некорректный почтовый адресс!"); break;
                    case ContactValidation.PhoneEmpty: Notify("Не указан номер личного телефона!"); break;
                    case ContactValidation.PhoneNotCorrect: Notify("Некорректный номер личного телефона!"); break;
                    case ContactValidation.Phone2Empty: Notify("Не указан номер домашнего телефона!"); break;
                    case ContactValidation.Phone2NotCorrect: Notify("Некорректный номер домашнего телефона!"); break;
                }
            }
        }

        private void Notify(string info)
        {
            MessageBox.Show(info,"Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }       

     
    }
}
