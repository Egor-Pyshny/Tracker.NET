using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracker.Controllers.DataBaseController.Models;

namespace Tracker.Controllers.DataBaseController
{
    public static class MyDBController
    {
        private static SQLiteConnection connection = null;
        private static string path = "db.sqlite";
        public static void Start(string dbPath)
        {
            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<UserModel>();
        }

        public static bool CheckUserExists(UserModel u) {
            if(connection == null) Start(path);
            var user = connection.Table<UserModel>().Where(
                cond => cond.FirstName == u.FirstName &&
                cond.SecondName == u.SecondName &&
                cond.ThirdName == u.ThirdName);
            return user.Any();
        }

        public static bool RegisterNewUser(UserModel u) {

            if (!(CheckUserExists(u)))
            {
                connection.Insert(u);
            } else return false;
            return true;
        }

        public static void Stop()
        {
            if(connection != null) connection.Close();
        }
    }
}
