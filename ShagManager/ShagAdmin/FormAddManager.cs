using DataValidate;
using ShagManager;
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
    enum ManagerValidation
    {
        NameEmpty,
        SecondNameEmpty,
        LastNameEmpty,
        SexNotCorrect,
        PassportNumberEmpty,
        PassportNumberNotcorrect,
        BirthDayNotCorrect,
        PassportGettingTimeNotCorrect,
        PassportGettingPlaceEmpty,
        IINNotCorrect,
        ICNumberNotCorrect,
        IcNumberGettingDateNotCorrect,
        ICNumberGettingPlaceNotEmpty,
        CredentialNotAdded,
        ContactNotAdded
    }
    public partial class FormAddManager : Form
    {
        WindowsMode mode = WindowsMode.ADD;
        ManagerValidation validate = ManagerValidation.NameEmpty;
        Manager manager = new Manager();
        ContactInfo info;
        Credential credential;
        bool isContactAdded = false;
        bool isCredentialAdded = false;

        public FormAddManager()
        {
            InitializeComponent();
        }
        public FormAddManager(Manager manager)
        {
            InitializeComponent();
            this.manager = manager;
            mode = WindowsMode.EDIT;
            LoadData();
        }

        private void LoadData()
        {
            textBoxFirstName.Text = manager.Person.FirstName;
            textBoxSecondName.Text = manager.Person.SecondName;
            textBoxLastName.Text = manager.Person.LastName;
            textBoxPassportNumber.Text = manager.Person.PassportNumber;
            textBoxPassportGettingPlace.Text = manager.Person.PassportGettingPlace;
            textBoxIIN.Text = manager.Person.IIN;
            textBoxICGettingPlace.Text = manager.Person.ICGettingPlace;
            textBoxICNumber.Text = manager.Person.ICNumber;
            dateTimePickerBirthDay.Value = manager.Person.Birthday;
            dateTimePickerICGettingDate.Value = manager.Person.ICGettingDay;
            dateTimePickerPassportGettingDate.Value = manager.Person.PassportGettingDay;
            radioButtonMan.Checked = manager.Person.Sex;            
            
        }
        private bool CheckData()
        {   
            //Настоящее время
            DateTime current = DateTime.Now;
            //имя
            if (string.IsNullOrWhiteSpace(textBoxFirstName.Text))
            {
                validate = ManagerValidation.NameEmpty;
                return false;
            }
            //фамилия
            if (string.IsNullOrWhiteSpace(textBoxSecondName.Text))
            {
                validate = ManagerValidation.SecondNameEmpty;
                return false;
            }
            //Очество
            if (string.IsNullOrWhiteSpace(textBoxLastName.Text))
            {
                validate = ManagerValidation.LastNameEmpty;
                return false;
            }
            //ДэйтПикер День Рождения
            if (current.Year - dateTimePickerBirthDay.Value.Year > 18 && current.Year - dateTimePickerBirthDay.Value.Year < 60)
            {
                validate = ManagerValidation.BirthDayNotCorrect;
                return false;
            }
            //Номер пасспорта
            if (string.IsNullOrWhiteSpace(textBoxPassportNumber.Text) || textBoxPassportNumber.Text.Trim().Length != 10)
            {
                validate = ManagerValidation.PassportNumberNotcorrect;
                return false;
            }
            //ДэйтПикер Дата получения пасспорта
            if (current.Year - dateTimePickerPassportGettingDate.Value.Year > 18 && current.Year - dateTimePickerPassportGettingDate.Value.Year < 60)
            {
                validate = ManagerValidation.PassportGettingTimeNotCorrect;
                return false;
            }
            //Место получения пасспорта
            if (string.IsNullOrWhiteSpace(textBoxPassportGettingPlace.Text))
            {
                validate = ManagerValidation.PassportGettingPlaceEmpty;
                return false;
            }
           // иин
            if (string.IsNullOrWhiteSpace(textBoxIIN.Text) || textBoxIIN.Text.Trim().Length!=12)
            {
                validate = ManagerValidation.IINNotCorrect;
                return false;
            }
            //Номер удостоверения 
            if (string.IsNullOrWhiteSpace(textBoxICNumber.Text) || textBoxICNumber.Text.Trim().Length != 12)
            {
                validate = ManagerValidation.ICNumberNotCorrect;
                return false;
            }
            //ДэйтПикер Дата получения удостовирения личности
            if (current.Year - dateTimePickerICGettingDate.Value.Year > 16 && current.Year - dateTimePickerICGettingDate.Value.Year < 60)
            {
                validate = ManagerValidation.IcNumberGettingDateNotCorrect;
                return false;
            }
            //Место получения 
            if(string.IsNullOrWhiteSpace(textBoxICGettingPlace.Text))
            {
                validate = ManagerValidation.ICNumberGettingPlaceNotEmpty;
                return false;
            }
            //проверка

            if (!isContactAdded)
            {
                validate = ManagerValidation.ContactNotAdded;
                return false;
            }
            if (!isCredentialAdded)
            {
                validate = ManagerValidation.CredentialNotAdded;
                return false;
            }
            //финальная проверка корректности данных
            PassportValidate pass = new PassportValidate();
            bool sex;
            if (radioButtonMan.Checked)
                sex = true;
            else
                sex = false;
            DateTime birthday = dateTimePickerBirthDay.Value;
            bool isIndividual;
            if (radioButtonIsInd.Checked)
                isIndividual = true;
            else
                isIndividual = false;
            bool isResident=info.IsResident;

            if (!pass.CodeValidate(textBoxIIN.Text.Trim(), isIndividual, birthday, sex, isResident))
                return false;
            return true;
        }
        private void buttonAddContact_Click(object sender, EventArgs e)
        {
            ContactForm frm;
            DialogResult result;
            switch (mode)
            {
                case WindowsMode.ADD: 
                    frm = new ContactForm();
                    result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        info = frm.GetResult();
                        if (info != null)
                        {
                            isContactAdded = true;
                        }
                    }
                    break;
                case WindowsMode.EDIT:
                   frm = new ContactForm(manager);
                    result = frm.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        info = frm.GetResult();
                        if (info != null)
                        {
                            isContactAdded = true;
                        }
                    }
                    break;
            }

        }

        private void buttonAddManager_Click(object sender, EventArgs e)
        {
           FormRegistration frm;
           DialogResult result;
           switch (mode)
           {
               case WindowsMode.ADD:
                   if (isCredentialAdded)
                   {                      
                       frm = new FormRegistration(credential);
                       result = frm.ShowDialog();
                       if (result == DialogResult.OK)
                       {
                           credential = frm.GetResult();                         
                       }
                   }
                   else
                   {
                       frm = new FormRegistration();
                       result = frm.ShowDialog();
                       if (result == DialogResult.OK)
                       {
                           credential = frm.GetResult();                      
                           if (credential!= null)
                           {
                               isCredentialAdded = true;
                             //  MessageBox.Show(credential.Login);
                           }
                       }
                   }
                   break;
               case WindowsMode.EDIT:
                   frm = new FormRegistration(manager.Credential);
                   result = frm.ShowDialog();
                   if (result == DialogResult.OK)
                   {
                       credential = frm.GetResult();
                       if (credential != null)
                       {
                           isCredentialAdded = true;
                       }
                   }
                   break;
           }
        }
        private void AddManager()
        {
            using (ManagerContext context = new ManagerContext())
            {
                manager.Credential = credential;
                manager.Person.Info=info;
                if (radioButtonMan.Checked)
                {
                    manager.Person.Sex = true;
                }
                else
                {
                    manager.Person.Sex = false;
                }
                manager.Person.ICNumber = textBoxICNumber.Text.Trim();
                manager.Person.ICGettingPlace = textBoxICGettingPlace.Text.Trim();
                manager.Person.PassportGettingPlace = textBoxPassportGettingPlace.Text.Trim();
                manager.Person.PassportNumber = textBoxPassportNumber.Text.Trim();
                manager.Person.IIN = textBoxIIN.Text.Trim();
                manager.Person.LastName = textBoxLastName.Text.Trim();
                manager.Person.SecondName = textBoxSecondName.Text.Trim();
                manager.Person.FirstName = textBoxFirstName.Text.Trim();               

                //manager info date
                manager.Person.Birthday = dateTimePickerBirthDay.Value;
                manager.Person.ICGettingDay = dateTimePickerICGettingDate.Value;
                manager.Person.PassportGettingDay = dateTimePickerPassportGettingDate.Value;
                //сохраняем данные
                context.Managers.Add(manager);
               context.SaveChanges(); 
            }
        }
        private void EditManager()
        {
            using (ManagerContext context = new ManagerContext())
            {
                //фиксируем менеджера
                context.Managers.Attach(manager);

                manager.Credential = credential;
                manager.Person.Info = info;
                if (radioButtonMan.Checked)
                {
                    manager.Person.Sex = true;
                }
                else
                {
                    manager.Person.Sex = false;
                }
                manager.Person.ICNumber = textBoxICNumber.Text.Trim();
                manager.Person.ICGettingPlace = textBoxICGettingPlace.Text.Trim();
                manager.Person.PassportGettingPlace = textBoxPassportGettingPlace.Text.Trim();
                manager.Person.PassportNumber = textBoxPassportNumber.Text.Trim();
                manager.Person.IIN = textBoxIIN.Text.Trim();
                manager.Person.LastName = textBoxLastName.Text.Trim();
                manager.Person.SecondName = textBoxSecondName.Text.Trim();
                manager.Person.FirstName = textBoxFirstName.Text.Trim();

                //manager info date
                manager.Person.Birthday = dateTimePickerBirthDay.Value;
                manager.Person.ICGettingDay = dateTimePickerICGettingDate.Value;
                manager.Person.PassportGettingDay = dateTimePickerPassportGettingDate.Value;
                //сохраняем данные
                context.Entry(manager).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (CheckData())
            {
                switch (mode)
                {
                    case WindowsMode.ADD: AddManager(); break;
                    case WindowsMode.EDIT: EditManager();  break;
                }
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                switch (validate)
                {
                    case ManagerValidation.BirthDayNotCorrect: Notify("Указанная дата рождения не корректна!"); break;
                    case ManagerValidation.ContactNotAdded: Notify("Контактные данные не заполнены!"); break;
                    case ManagerValidation.CredentialNotAdded: Notify("Данные в Учетной записи не верны!"); break;
                    case ManagerValidation.IcNumberGettingDateNotCorrect: Notify("Указанная дата получения удостоверения не корректна!"); break;
                    case ManagerValidation.ICNumberGettingPlaceNotEmpty: Notify("Место полученя удостоверения не может быть пустым!"); break;
                    case ManagerValidation.ICNumberNotCorrect: Notify("Указанный номер удостоверения не верный!"); break;
                    case ManagerValidation.IINNotCorrect: Notify("Не верный И И Н!"); break;
                    case ManagerValidation.LastNameEmpty: Notify("Очество не заполнено!"); break;
                    case ManagerValidation.NameEmpty: Notify("Имя не заполнено!"); break;
                    case ManagerValidation.PassportGettingPlaceEmpty: Notify("Место получения пасспорта не может быть пустым!"); break;
                    case ManagerValidation.PassportGettingTimeNotCorrect: Notify("Указанная дата получения пасспорта не корректна!"); break;
                    case ManagerValidation.PassportNumberEmpty: Notify("Номер пасспорта не может быть пустым!"); break;
                    case ManagerValidation.PassportNumberNotcorrect: Notify("Номер пасспорта не корректный!"); break;
                    case ManagerValidation.SecondNameEmpty: Notify("Фамилия не заполнена!"); break;
                    case ManagerValidation.SexNotCorrect: Notify("Пол или данные ИИН указанны не верно! "); break;     
                }
            }

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
        private void Notify(string info)
        {
            MessageBox.Show(info, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FormAddManager_Load(object sender, EventArgs e)
        {

        }

       
      
    }
}
