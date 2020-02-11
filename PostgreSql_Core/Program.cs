using System;
using System.Data;
using System.Linq;
using Npgsql;

namespace PostgreSql_Core
{
    class Program
    {

        static NpgsqlConnection _connection;
        static string _connString;

        static void Main(string[] args)
        {
            // bool exit = false;
            // string command;
            //
            // while (!exit)
            // {
            //     command = Console.ReadLine();
            //     switch (command)
            //     {
            //         case "exit":
            //             exit = true;
            //             break;
            //         case "select":
            //             var a = from ch in command
            //                 join ch1 in "XXXect" on ch equals ch1
            //                 select new {ch = ' '};
            //             break;
            //     }
            // }

            Console.WriteLine();

            // ------------------------- ПОДКЛЮЧЕНИЕ К БД ------------------------- //

            try
            {
                _connString = "Server=localhost;Port=5432;User Id=postgres;Password=12345;Database=exercises";
                _connection = new NpgsqlConnection(_connString);
                _connection.Open();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Удачное подключение");
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Не удалось подключиться");
                return;
            }

            Console.ForegroundColor = default;


            // ------------------------- ВСТАВКА К БД ----------------------------- //

            Member member = new Member();
            //  Insert(member);

            // --------------------------ЧТЕНИЕ ИЗ БД ----------------------------- //

            string query = "select * from cd.members";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(query);
            Console.ForegroundColor = default;
            var command = new NpgsqlCommand(query, _connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var output =
                    $"{reader.GetInt32(0)}\t" +
                    $"{reader.GetString(1)}\t\t" +
                    $"{reader.GetString(2)}\t\t" +
                    $"{reader.GetString(3)}\t\t\t" +
                    $"{reader.GetInt32(4)}\t\t\t" +
                    $"{reader.GetString(5)}\t\t\t" +
                    $"{GetSaveInt32(reader,6)}\t\t\t" +
                    $"{reader.GetDateTime(7)}";
                Console.WriteLine(output);
            }


            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ВЫХОД");
        }

        public static void Insert(Member m)
        {
            // поменять
            string sql = "insert into cd.members VALUES (39, 'Surname', 'Firstname', 'Address', 123432, 'telephone', NULL, '2019-07-01 00:00:00')";
            NpgsqlCommand command = new NpgsqlCommand(sql, _connection);
            command.ExecuteNonQuery();
            Console.WriteLine("вставка завершена");
        }

        public static string GetSaveInt32(NpgsqlDataReader reader, int idx)
        {
            return reader.IsDBNull(idx) ? "" : reader.GetInt32(idx).ToString();
        }
    }

    public class Member
    {
        public int MemId { get; set; }
        public string Surname { get; set; } = "";
        public string Firstname { get; set; } = "";
        public string Address { get; set; } = "";
        public int Zipcode { get; set; } = 0;
        public string Telephone { get; set; } = "";
        public int RecommendedBy { get; set; }
        public DateTime JoinDate { get; set; } = DateTime.Now;
    }




}