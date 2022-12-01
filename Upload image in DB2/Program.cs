using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.IO;

namespace Upload_image_in_DB2
{
    class Program
    {
        static void Main(string[] args)
        {
                string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Picture;Trusted_Connection=True";
                string shortFileName;
                int num = 0;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = connection;
                    command.CommandText = "UPDATE Weather SET folderPath = @folderPath WHERE ID = @ID";
                    command.Parameters.Add("@folderPath", SqlDbType.Image, 1000000);
                    command.Parameters.Add("@ID", SqlDbType.Int, 10);

                    // путь к файлу для загрузки
                    string filename = "";
                    
                    byte[] imageData;
                    foreach (var a in Directory.GetFiles(filename, "*", SearchOption.AllDirectories))
                    {
                        shortFileName = a.Substring(filename.LastIndexOf('\\') + 1);
                        num += 1;
                        using (FileStream fs = new FileStream(filename + shortFileName, FileMode.Open, FileAccess.Read))
                        {
                            imageData = new byte[fs.Length];
                            fs.Read(imageData, 0, imageData.Length);
                            // передаем данные в команду через параметры
                            command.Parameters["@folderPath"].Value = imageData;
                            command.Parameters["@ID"].Value = num;
                            command.ExecuteNonQuery();
                    }
                    }
                   
                   
                }
            
        }
    }
}
