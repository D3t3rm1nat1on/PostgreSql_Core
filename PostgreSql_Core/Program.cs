using System;
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
            Insert(member);
                
            // -------------------------------------------------------------------- //

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