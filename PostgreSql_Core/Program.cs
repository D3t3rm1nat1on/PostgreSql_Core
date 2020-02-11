using System;
using System.Data;
using System.Linq;
using Npgsql;

namespace PostgreSql_Core
{
    class Program
    {
        private const int TableWidth = 199;
        private static NpgsqlConnection _connection;
        private static string _connString;
        private static ConsoleColor queryColor = ConsoleColor.Yellow;
        private static ConsoleColor tableColor = ConsoleColor.DarkGray;
        private static ConsoleColor columnNameColor = ConsoleColor.DarkBlue;
        private static ConsoleColor cellColor = ConsoleColor.Blue;

        static void Main(string[] args)
        {

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
            query = "select * from cd.members where recommendedby is not null AND length(firstname) < 5";

            PrintResultQuery(query);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ВЫХОД");
        }

        public static void Insert(Member m)
        {
            // поменять
            string sql =
                "insert into cd.members VALUES (39, 'Surname', 'Firstname', 'Address', 123432, 'telephone', NULL, '2019-07-01 00:00:00')";
            NpgsqlCommand command = new NpgsqlCommand(sql, _connection);
            command.ExecuteNonQuery();
            Console.WriteLine("вставка завершена");
        }

        static void PrintResultQuery(string query)
        {
            Console.ForegroundColor = queryColor;
            Console.WriteLine(query);
            Console.ForegroundColor = default;

            var command = new NpgsqlCommand(query, _connection);
            var reader = command.ExecuteReader();

            int count = reader.FieldCount;

            string[] columnNames = new string[count];
            for (int i = 0; i < count; i++)
            {
                columnNames[i] = reader.GetName(i);
            }

            PrintLine(count);
            PrintRow(columnNameColor, columnNames);
            PrintLine(count);

            while (reader.Read())
            {
                string[] values = new string [8];

                for (int i = 0; i < count; i++)
                {
                    values[i] = reader.GetValue(i).ToString();
                }

                PrintRow(cellColor, values);
                PrintLine(count);
            }
        }

        static void PrintLine(int countOfRows)
        {
            int width = (TableWidth - countOfRows) / countOfRows;
            string line = "+";
            ;
            for (int i = 0; i < countOfRows; i++)
            {
                line += new string('-', width) + '+';
            }

            Console.ForegroundColor = tableColor;
            Console.WriteLine(line);
            Console.ForegroundColor = default;
        }

        static void PrintRow(ConsoleColor color, params string[] columns)
        {
            int width = (TableWidth - columns.Length) / columns.Length;
            string row;

            Console.ForegroundColor = tableColor;
            Console.Write("|");
            foreach (string column in columns)
            {
                row = AlignCentre(column, width);
                Console.ForegroundColor = color;
                Console.Write(row);
                Console.ForegroundColor = tableColor;
                Console.Write("|");
            }

            Console.WriteLine();
            Console.ForegroundColor = default;
        }

        static string AlignCentre(string text, int width)
        {
            text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

            if (string.IsNullOrEmpty(text))
            {
                return new string(' ', width);
            }
            else
            {
                return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
            }
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