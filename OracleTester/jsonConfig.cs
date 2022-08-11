using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows.Forms;

namespace OracleTester
{
    public class config
    {
        public static string FileName = "oraConfig";
        public class ConfigMember
        {
            public string LastTNS { get; set; } = @"Provider=OraOLEDB.Oracle;
Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST =192.168.0.248)(PORT = 1521))
(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME=orcl)));
User Id=myid;
Password=mypw;
Privilege=SYSDBA;";

            public string LastQuery { get; set; } = "select * from all_tables";
        }

        public static ConfigMember instance = new ConfigMember();
        /// <summary>
        /// 설정을 불러와 구조체에 보관한다
        /// </summary>
        public static void Load()
        {
            try
            {
                if (File.Exists($@"{Application.StartupPath}\{FileName}.json") == false)
                {
                    File.WriteAllText($@"{Application.StartupPath}\{FileName}.json", "{ }");
                }
                string jsonString = File.ReadAllText($@"{Application.StartupPath}\{FileName}.json");

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                instance = JsonSerializer.Deserialize<ConfigMember>(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 로컬 변수에 저장된 구조체를 파일에 저장한다
        /// </summary>
        public static void Save()
        {
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true
                };

                string json = JsonSerializer.Serialize(instance, options);
                File.WriteAllText($@"{Application.StartupPath}\{FileName}.json", json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


}
