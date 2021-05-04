﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaBazaarApp.Classes;
using MediaBazaarApp.Popups;

namespace MediaBazaarApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Classes.Calendar calendar;
        private Person person;
        private Company company;
        private Add_Employee addEmployeeForm;
        private EditEmployee editEmployeeForm;
        private AddAnnouncement addAnnouncementForm;
        private EditAnnouncement editAnnouncementForm;

        private List<ShopWorker> employees;
        public MainWindow(Company company, Person person)
        {
            try
            {
                Loaded += OnLoad;
                InitializeComponent();
                this.company = company;
                this.person = person;
                Loaded += OnLoad;

                this.employees = this.company.ShopWorkers.ToList();
                this.lvShopWorkers.ItemsSource = this.employees;
                this.lvMessages.ItemsSource = this.company.Messages.ToList();
                this.lblUserString.Content = $"Hello , {person.FirstName}";

                this.showAnnouncements();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnLoad(object sender, RoutedEventArgs e)
        {
            this.RefreshCalendar();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                calendar.NextMonth();
                this.lblMonthYear.Content = $"{this.calendar.Year}, {this.calendar.Month}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                calendar.PreviousMonth();
                this.lblMonthYear.Content = $"{this.calendar.Year}, {this.calendar.Month}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.addEmployeeForm = new Add_Employee(this.company);
                this.addEmployeeForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_Refresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.employees = this.company.ShopWorkers.ToList();
                this.lvShopWorkers.ItemsSource = this.employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.lvShopWorkers.SelectedItem != null)
                {
                    ShopWorker worker = ((ShopWorker)this.lvShopWorkers.SelectedItem);
                    this.editEmployeeForm = new EditEmployee(this.company, worker);
                    this.editEmployeeForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.tbNewPassRepeat.Text == this.tbNewPass.Text)
                {
                    this.company.AccountManager.ChangePassword(this.person.Username,
                                        this.tbCurrentPass.Text, this.tbNewPass.Text);
                    MessageBox.Show("Suucessfully changed");
                }
                else throw new ArgumentException("Passwords do not match");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefreshCalendar_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshCalendar();
        }

        private void btn_Sort_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.employees = this.company.ShopWorkers.ToList();
                this.employees.Sort(new EmployeeSort());
                this.lvShopWorkers.ItemsSource = this.employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random rnd = new Random();
                string firstName = tb_FirstName.Text;
                string lastName = tb_LastName.Text;
                string email = tb_Email.Text;
                string username = email;
                string password = "";
                

                AccountManager account = new AccountManager();


                if ((bool)rb_Adminstrator.IsChecked)
                {
                    password = account.Add(new Administrator(firstName, lastName, email));
                }
                else if ((bool)rb_Manager.IsChecked)
                {
                    password = account.Add(new Manager(firstName, lastName, email));
                }

                MessageBox.Show($"Username: {username}, Password: {password}" + "\n Please note them down!");

            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("All fields must be completed");
            }
        }


       

        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            string name = tb_Search.Text;
            this.employees = this.company.ShopWorkers.ToList();
            this.employees = FindPattern(name,employees);
            this.lvShopWorkers.ItemsSource = this.employees;

        }

        private List<ShopWorker> FindPattern(string name, List<ShopWorker> workers)
        {
            List<ShopWorker> temp = new List<ShopWorker>();
            foreach (ShopWorker worker in workers)
            {
                if (worker.ToString().Contains(name))
                {
                    temp.Add(worker);
                    
                }
            }
            return temp;
        }

        private void btnRefreshMessages_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.lvMessages.ItemsSource = this.company.Messages.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lvMessages_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Message message = (Message)this.lvMessages.SelectedItem;
                MessageBox.Show(message.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void RefreshCalendar()
        {
            try
            {
                calendar = new Classes.Calendar(this, company.ShiftSchedule.ToList());
                this.lblMonthYear.Content = $"{this.calendar.Year}, {this.calendar.Month}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void RefreshCalendar(DateTime date)
        {
            try
            {
                calendar = new Classes.Calendar(this, company.ShiftSchedule.ToList(), date);
                this.lblMonthYear.Content = $"{this.calendar.Year}, {this.calendar.Month}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefreshAnnouncements_Click(object sender, RoutedEventArgs e)
        {
            this.showAnnouncements();
        }

        private void btn_EditAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.lvAnnouncements.SelectedItem != null)
                {
                    Announcement announcement = ((Announcement)this.lvAnnouncements.SelectedItem);
                    this.editAnnouncementForm = new EditAnnouncement(this.company, announcement);
                    this.editAnnouncementForm.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_AddAnnouncement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.addAnnouncementForm = new AddAnnouncement(this.company);
                this.addAnnouncementForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void showAnnouncements()
        {
            try
            {
                this.lvAnnouncements.ItemsSource = this.company.Announcements.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}