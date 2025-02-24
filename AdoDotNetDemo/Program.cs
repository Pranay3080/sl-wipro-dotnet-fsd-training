﻿using System;
using System.Data;
using System.Data.SqlClient;
namespace AdoNetConsoleApplication
{
    class Program
    {
        string connectionString = $"data source=(local);database=student2; integrated security=SSPI";
        
        private SqlConnection GetSqlConnection()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            return sqlConnection;
        }

        private int ExecuteNonQuery(string query)
        {
            using(SqlConnection sqlConnection = GetSqlConnection())
            {
                SqlCommand cmd = new SqlCommand(query, sqlConnection);
                return cmd.ExecuteNonQuery();
            }
        }

        private SqlDataReader GetReader(string query)
        {
            SqlCommand cmd = new SqlCommand(query, GetSqlConnection());
            return cmd.ExecuteReader();
        }

        static void Main(string[] args)
        {
            //new Program().CreateTable();
            //Console.ReadLine();

            //new Program().InsertData();
            //Console.ReadLine();

            //new Program().ReadData();
            //Console.ReadLine();

            //new Program().DeleteData();
            //Console.ReadLine();

            new Program().DataSetDemo();
            Console.ReadLine();
        }

        public void CreateTable()
        {
            try
            {
                ExecuteNonQuery("create table student(id int not null, name varchar(100), email varchar(50), join_date date)");
                
                // Displaying a message  
                Console.WriteLine("Table created Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
        }

        public void InsertData()
        {
            try
            {
                ExecuteNonQuery("insert into student (id, name, email, join_date)values('101', 'Ronald Trump', 'ronald@example.com', '1/12/2017')");

                // Displaying a message  
                Console.WriteLine("Record Inserted Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong." + e);
            }
        }

        public void ReadData()
        {
            try
            {

                // Executing the SQL query  
                SqlDataReader sdr = GetReader("Select * from student");
                
                // Iterating Data  
                while (sdr.Read())
                {
                    Console.WriteLine(sdr["id"] + " " + sdr["name"] + " " + sdr["email"]); // Displaying Record  
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }
        }

        public void DeleteData()
        {
            try
            {

                ExecuteNonQuery("delete from student where id = '101'");
                Console.WriteLine("Record Deleted Successfully");
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, something went wrong.\n" + e);
            }
        }

        public void DataSetDemo()
        {
            using (SqlConnection con = GetSqlConnection())
            {
                SqlDataAdapter sde = new SqlDataAdapter("Select * from student", con);
                
                DataSet ds = new DataSet();
                sde.Fill(ds);
                
                Console.WriteLine($"No. of records in Student table = {ds.Tables[0].Rows.Count}");

                DataTable table = ds.Tables[0];

                table.Rows.Add("201", "Rameez", "rameez@example.com");
                table.Rows.Add("202", "Sam Nicolus", "sam@example.com");
                table.Rows.Add("203", "Subramanium", "subramanium@example.com");
                table.Rows.Add("204", "Ankur Kumar", "ankur@example.com");

                ds.AcceptChanges();
                
                Console.WriteLine($"No. of records in Student table = {ds.Tables[0].Rows.Count}");
            }
        }
    }
}