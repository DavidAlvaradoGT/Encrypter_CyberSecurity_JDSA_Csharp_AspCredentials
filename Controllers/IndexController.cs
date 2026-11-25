using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Encrypter;
using Encrypter_JDSA.Models;
using MySql.Data.MySqlClient;

namespace Encrypter_JDSA.Controllers
{
    public class IndexController : Controller
    {
        private readonly string DatabaseConnection = "Server=localhost;Database=encrypter_jdsa;Uid=root;Pwd=Manzana123;";
        
        [HttpGet]
        public ActionResult Index()
        {
            DateTime netTime = NetworkTimeHelper.GetInternetDateTime();
            var model = new UserCredentialPageModel
            {
                NetworkDateUsed = netTime.ToString("yyyy-MM-dd HH:mm:ss"),
                Users = GetAllUsers()
            };
            return View("~/Views/Index/Index.cshtml", model);
        }
         
        [HttpPost]
        public ActionResult Index(string UserNameInput, string PasswordInput)
        {
            DateTime netTime = NetworkTimeHelper.GetInternetDateTime();
            if (!string.IsNullOrWhiteSpace(UserNameInput) && !string.IsNullOrWhiteSpace(PasswordInput))
            {
                string encryptedUserName = EncrypterEngine.Encrypt(UserNameInput);
                string encryptedPassword = EncrypterEngine.Encrypt(PasswordInput);

                using (var SQLDataBase = new MySqlConnection(DatabaseConnection))
                {
                    SQLDataBase.Open();
                    string sql = @"INSERT INTO Users (UserName, Password, EncryptedPassword, EncryptedUserName, UserDateCreation)
                         VALUES (@UserName, @Password, @EncryptedPassword, @EncryptedUserName, @UserDateCreation);";
            using (var data = new MySqlCommand(sql, SQLDataBase))
            {
                data.Parameters.AddWithValue("@UserName", UserNameInput);
                data.Parameters.AddWithValue("@Password", PasswordInput);
                data.Parameters.AddWithValue("@EncryptedPassword", encryptedPassword);
                data.Parameters.AddWithValue("@EncryptedUserName", encryptedUserName);
                data.Parameters.AddWithValue("@UserDateCreation", netTime);

                data.ExecuteNonQuery();
            }
        }
    }
    var model = new UserCredentialPageModel
    {
        UserNameInput=UserNameInput,
        PasswordInput=PasswordInput,
        NetworkDateUsed = netTime.ToString("yyyy-MM-dd HH:mm:ss"),
        Users = GetAllUsers()
    };
         return View("~/Views/Index/Index.cshtml", model);

        }
        private List<UserCredentialRow> GetAllUsers()
    {
    var list = new List<UserCredentialRow>();
    using (var databaseConnection = new MySqlConnection(DatabaseConnection))
    {
        databaseConnection.Open();
        string sql = @"SELECT Id, UserName, Password, EncryptedPassword, EncryptedUserName, UserDateCreation FROM Users Order BY Id;";
        using (var data = new MySqlCommand(sql, databaseConnection))
        using (var reader = data.ExecuteReader())
        {
            while (reader.Read())
            {
                string encryptUser = reader.GetString("EncryptedUserName");
                string encryptPassword = reader.GetString("EncryptedPassword");

                list.Add(new UserCredentialRow
                {
                    Id = reader.GetInt32("Id"),
                    UserName = reader.GetString("UserName"),
                    Password = reader.GetString("Password"),
                    EncryptedPassword = encryptPassword,
                    EncryptedUserName = encryptUser,
                    DecryptedUserName = EncrypterEngine.Decrypt(encryptUser),
                    DecryptedPassword = EncrypterEngine.Decrypt(encryptPassword),
                    UserDateCreation = reader["UserDateCreation"] == DBNull.Value
                    ? string.Empty
                    : Convert.ToDateTime(reader["UserDateCreation"]).ToString("yyyy-MM-dd HH:mm:ss")
                  });
                }
            }
        }
        return list;
 }

    }
        
}