using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace KomlevIntertaiment
{
    internal class Config
    {
        public static string DBConnection =
        "server=127.0.0.1;" +
        "port=3306;" +
        "user=root;" +
        "password=root;" +
        "database=variant6KomlevYuri";

        public static string GetAllClients =
            "SELECT * FROM clients";
        public static string CreateClients
        (string firstName, string lastName, string gender, string address, string city, string phone, string email, string status)
        {
            return "INSERT INTO clients (firstName, lastName, gender, address, city, phone, email, status, createdDate)" +
                $" VALUE ('{firstName}', '{lastName}', '{gender}', '{address}', '{city}', '{phone}', '{email}', '{status}', NOW());";
        }
        public static string CheckClientByID
        (string id, string ColumnName)
        {
            return $"SELECT id FROM clients " +
                $"WHERE id = '{id}';";
        }
        public static string EditClientByID
        (string id, string editParameter, string newParameter)
        {
            return "UPDATE clients " +
                $"SET {editParameter} = '{newParameter}'" +
                $"WHERE id = '{id}';";
        }
    }
}
