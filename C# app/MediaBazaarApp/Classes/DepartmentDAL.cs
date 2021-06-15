﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MediaBazaarApp.Classes
{
    public class DepartmentDAL : DBmanager, IDepartment
    {
        public void Create(Department department)
        {
            string sql = $"INSERT INTO department(ID, Name, Manager) values(@ID, @Name, @Manager)";

            MySqlParameter[] prms = new MySqlParameter[3];
            prms[0] = new MySqlParameter("@ID", department.ID);
            prms[1] = new MySqlParameter("@Name", department.Name);
            prms[2] = new MySqlParameter("@Manager", department.DepartmentManager);

            

            this.ExecuteQuery(sql, prms);
        }
        public List<Department> GetAll()
        {

            string sql = $"SELECT d.ID, d.Name, d.Manager, p.FirstName, p.LastName " +
            $"FROM department as d " +
            $"INNER JOIN  person as p ON d.Manager = p.ID";

            MySqlCommand cmd = new MySqlCommand(sql, this.GetConnection());
            MySqlDataReader reader = null;
            List<Department> departments = new List<Department>();
            try
            {
                reader = this.OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    DepartmentManager manager
                    = new DepartmentManager(Convert.ToInt32(reader["Manager"]),
                                  Convert.ToString(reader["FirstName"]),
                                  Convert.ToString(reader["LastName"]));

                    Department department
                         = new Department(Convert.ToInt32(reader["ID"]),
                               Convert.ToString(reader["Name"]),
                               manager, this.NrOfEmployees(Convert.ToInt32(reader["ID"])));
                   
                    departments.Add(department);
                   
                }
            }
            finally
            {
                this.CloseExecuteReader(reader);
            }
            return departments;
        }


        public List<ShopWorker> GetEmployees(Department department)
        {
            string sql = $"SELECT p.ID, p.FirstName, p.LastName, e.DepartmentID from person as p " +
                         $"INNER join employee as e on p.ID = e.ID "+
                         $"INNER JOIN department as d on e.DepartmentID = d.ID "+
                         $"WHERE d.ID = @ID AN";
            MySqlCommand cmd = new MySqlCommand(sql, this.GetConnection());
            MySqlDataReader reader = null;
            cmd.Parameters.AddWithValue("@ID", department.ID);
            List<ShopWorker> workers = new List<ShopWorker>();
            try
            {
                reader = this.OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    ShopWorker worker = new ShopWorker(Convert.ToInt32(reader["ID"]),
                        Convert.ToString(reader["FirstName"]),
                        Convert.ToString(reader["LastName"]));
                    workers.Add(worker);

                }
            }
            finally
            {
                this.CloseExecuteReader(reader);
            }
            return workers;
        
        }

        public List<ShopWorker> GetEmployeesByDepartment(Department department)
        {
            string sql = $"SELECT p.ID, p.FirstName, p.LastName, e.DepartmentID from person as p " +
                         $"INNER join employee as e on p.ID = e.ID " +
                         $"INNER JOIN department as d on e.DepartmentID = d.ID " +
                         $"WHERE d.ID = @ID";
            MySqlCommand cmd = new MySqlCommand(sql, this.GetConnection());
            MySqlDataReader reader = null;
            cmd.Parameters.AddWithValue("@ID", department.ID);
            List<ShopWorker> workers = new List<ShopWorker>();
            try
            {
                reader = this.OpenExecuteReader(cmd);
                while (reader.Read())
                {
                    ShopWorker worker = new ShopWorker(Convert.ToInt32(reader["ID"]),
                        Convert.ToString(reader["FirstName"]),
                        Convert.ToString(reader["LastName"]));
                    workers.Add(worker);

                }
            }
            finally
            {
                this.CloseExecuteReader(reader);
            }
            return workers;

        }


        public List<DepartmentManager> GetManagers()
        {
            string sql = $"SELECT ID,FirstName, LastName from person WHERE AccessLevel = 6";

            MySqlCommand cmd = new MySqlCommand(sql, this.GetConnection());
            MySqlDataReader reader = null;
            List<DepartmentManager> managers = new List<DepartmentManager>();
            try
            {
                reader = this.OpenExecuteReader(cmd);
                while (reader.Read())
                {

                    DepartmentManager manager
                    = new DepartmentManager(Convert.ToInt32(reader["ID"]),
                                  Convert.ToString(reader["FirstName"]),
                                  Convert.ToString(reader["LastName"]));



                    managers.Add(manager);
                }
            }
            finally
            {
                this.CloseExecuteReader(reader);
            }

            return managers;
        }

        public int NrOfEmployees(int id)
        {
            string sql = "SELECT COUNT(*) FROM employee WHERE DepartmentID = @ID";
            MySqlParameter[] prms = new MySqlParameter[1];
            prms[0] = new MySqlParameter("@ID", id);
            Object obj = this.ReadScalar(sql, prms);
            if (obj != DBNull.Value)
            {
                int amount = Convert.ToInt32(obj);
                return amount;
            }
            else throw new ArgumentException("No employees found");
        }

      

        public void Update(Department department)
        {
            string sql = $"UPDATE department SET Name = @Name, Manager = @Manager WHERE ID = @ID";

            MySqlParameter[] prms = new MySqlParameter[3];
            prms[0] = new MySqlParameter("@ID", department.ID);
            prms[1] = new MySqlParameter("@Name", department.Name);
            prms[2] = new MySqlParameter("@Manager", department.DepartmentManager);

            this.ExecuteQuery(sql, prms);
        }

        
    }

}
