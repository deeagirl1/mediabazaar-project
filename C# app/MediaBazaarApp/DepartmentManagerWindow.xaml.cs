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
using System.Windows.Shapes;
using MediaBazaarApp.Classes;

namespace MediaBazaarApp
{
    /// <summary>
    /// Interaction logic for DepartmentManagerWindow.xaml
    /// </summary>
    public partial class DepartmentManagerWindow : Window
    {
        private Company company;
        private Department department;
        private List<ShopWorker> employees;
    
        public DepartmentManagerWindow(Company company, Person person)
        {
            InitializeComponent();
            this.company = company;

            this.department = this.company.Departments.GetDepartmentByManagerID(person.ID);
            this.lvShopWorkers.ItemsSource = this.company.Departments.GetEmployees(department);
        }


        
    }
}