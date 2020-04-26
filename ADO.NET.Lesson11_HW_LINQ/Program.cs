using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ADO.NET.Lesson11_HW_LINQ
{
    public class Area
    {
        public int AreaId { get; set; }
        public int TypeArea { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public bool? NoSplit { get; set; }
        public bool? AssemblyArea { get; set; }
        public string FullName { get; set; }
        public bool? MultipleOrders { get; set; }
        public bool? HiddenArea { get; set; }
        public string IP { get; set; }
        public int PavilionId { get; set; }
        public int TypeId { get; set; }
        public int OrderExecution { get; set; }
        public int Dependence { get; set; }
        public int WorkingPeople { get; set; }
        public int ComponentTypeId { get; set; }
    }

    public class Timer
    {
        public int TimerId { get; set; }
        public int UserId { get; set; }
        public int AreaId { get; set; }
        public int DocumentId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime? DateFinish { get; set; }
        public int DurationInSeconds { get; set; }
    }

    class Program
    {
        static SqlConnection connection;
        static string connectionString;
        static SqlDataReader dataReader;
        static SqlCommand cmd;
        static List<Area> dataArea;
        static List<Timer> dataTimer;

        static void Main(string[] args)
        {
            connectionString = ConfigurationManager.ConnectionStrings["SqlDb"].ConnectionString;

            connection = new SqlConnection(connectionString);
            cmd = new SqlCommand("SELECT * FROM Area", connection);
            dataArea = GetData<Area>(cmd);

            connection = new SqlConnection(connectionString);
            cmd = new SqlCommand("SELECT * FROM Timer", connection);
            dataTimer = GetData<Timer>(cmd);

            TaskG();

            Console.ReadKey();
        }

        /// <summary>
        /// Метод получения обобщенной коллекции из таблицы в запросе к БД
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cmd"></param>
        /// <returns></returns>
        static List<T> GetData<T>(SqlCommand cmd)
        {
            List<T> collection = new List<T>();

            cmd.CommandText = string.IsNullOrWhiteSpace(cmd.CommandText)
                ? "SELECT * FROM " + typeof(T).Name
                : cmd.CommandText;

            using (connection)
            {
                // открываем соедиение
                connection.Open();
                // выполняем запрос и полученные данные записываем в dataReader
                dataReader = cmd.ExecuteReader();

                // открываем цикл для построчного считывания данных
                // критерий остановки возврат false по достижению последней строки таблицы
                while (dataReader.Read())
                {
                    // используем -рефлексию-

                    /* Создаем объект базового класса и инстанцируем его посредством метода 
                     * CreateInstance класса Activator для динамической конструирования
                     * объектов во время выполнения. */
                    Object obj = Activator.CreateInstance(typeof(T));

                    // проходим циклом по всем свойствам полученного экз объекта постр на основе данных таб БД
                    foreach (PropertyInfo property in obj.GetType().GetProperties())
                    {
                        obj.GetType()
                           .GetProperty(property.Name)                               // получаем название свойства
                           .SetValue(obj, dataReader[property.Name] != DBNull.Value  // проверяем на null данные из БД
                                     ? dataReader[property.Name]                     // данные не null присваеваем свойству
                                     : null);                                        // если ноль устанавливаем null
                    }

                    // на каждой итерации запись нового объекта в коллекцию
                    collection.Add((T)Convert.ChangeType(obj, typeof(T)));
                }
            }


            return collection;
        }

        /*3.	Используя таблицу Area, выгрузить в XML файл следующие данные:*/

        //  a.	Все зоны/участки которые принадлежат PavilionId = 1, при этом каждая
        //      зоны должна находиться в отдельном XML файле с наименованием PavilionId.
        static void TaskA()
        {
            XElement xArea = new XElement("Areas",
                dataArea.Where(w => w.PavilionId == 1)
                        .Select(s => new XElement("Area",
                                        new XElement("AreaId", s.AreaId),
                                        new XElement("AssemblyArea", s.AssemblyArea),
                                        new XElement("Name", s.Name)
                                        )));

            //xArea.Save("xDocFiles/PavilionId.xml");

            int count = 0;
            foreach (var item in xArea.Elements("Area"))
            {
                var temp = new XElement(item);
                //temp.Save(temp.Elements)
                Console.WriteLine(temp.Element("AreaId").Value);
                temp.Save($"xDocFiles/TaskA_{temp.Element("AreaId").Value}.xml");
                if (++count == 2) break;
            }
        }


        //  b.	Используя Directory класс, создать папки с название зон/участков, 
        //      в каждую папку выгрузить XML файл на основе данных их таблицы.
        static void TaskB()
        {

            var coll = dataArea.Where(s => s.ParentId == 93)
                    .Select(s => new XElement("Area",
                                        new XElement("AreaId", s.AreaId),
                                        new XElement("AssemblyArea", s.AssemblyArea),
                                        new XElement("Name", s.Name)
                            ));
            //.Select(s=> new {s, s.Save($"{Directory.GetCurrentDirectory()}/ + {s.Name}/") } );

            //Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}/xDocFiles/TaskB");

            foreach (var item in coll)
            {
                Directory.CreateDirectory($@"xDocFiles/TaskB/{item.Element("Name").Value}");
                string itemDirName = (item.Element("Name").Value); //.Replace(" ","");
                item.Save($"xDocFiles/TaskB/{itemDirName}/AreaId{item.Element("AreaId").Value}.xml");
                Console.WriteLine(item.Value);
            }

        }

        //  c.	Выгрузить XML файл только тех участков, которые не имеют дочерних
        //      элементов (ParentId = 0);
        static void TaskC()
        {
            XElement xDoc = new XElement("TaskC",
                           dataArea.Where(w => w.ParentId == 0)
                                   .Select(s => new XElement("Area",
                                            new XElement("ParentId", s.ParentId),
                                            new XElement("Name", s.Name),
                                            new XElement("IP", s.IP))));

            xDoc.Save($"xDocFiles/{xDoc.Name}.xml");
            Console.WriteLine(xDoc);
        }

        //  d.	Выгрузить из таблицы Timer, данные только для зон у которых есть IP адрес,
        //      при этом в XML файл должны войти следующие поля: UserId, Area Name 
        //      (name из Талицы Area), DateStart
        static void TaskD()
        {
            var AreaTimer = from a in dataArea
                            where a.IP != null
                            join t in dataTimer on a.AreaId equals t.AreaId
                            select new { a.AreaId, UserId = t.UserId, Name = a.Name, IP = a.IP };


            XElement xDoc = new XElement("TaskD",
                                AreaTimer.Select(s => new XElement("Selected",
                                                            new XElement("UserId", s.UserId),
                                                            new XElement("Name", s.Name),
                                                            new XElement("IP", s.IP))));
            Console.WriteLine(xDoc);
        }

        //  e.	Выгрузить в XML файл, данные из таблицы Timer, у которых нет даты завершения 
        //      работы DateFinish =null
        static void TaskE()
        {
            var AreaTimer = from a in dataArea
                            join t in dataTimer on a.AreaId equals t.AreaId
                            select new { UserId = t.UserId, Name = a.Name, t.DateFinish };

            XElement xDoc = new XElement("TaskE",
                                AreaTimer.Where(w => w.DateFinish == null)
                                         .Select(s => new XElement("Selected",
                                                            new XElement("UserId", s.UserId),
                                                            new XElement("Name", s.Name),
                                                            new XElement("DateFinish", s.DateFinish))));
            Console.WriteLine(xDoc);
        }

        //  f.	Выгрузить весь список выполненных работ из таблицы Timer
        static void TaskF()
        {
            var AreaTimer = from a in dataArea
                            join t in dataTimer on a.AreaId equals t.AreaId
                            select new { Name = a.Name, t.DateFinish };

            XElement xDoc = new XElement("TaskF",
                                AreaTimer.Where(w => w.DateFinish != null)
                                         .Select(s => new XElement("Selected",
                                                            new XElement("Name", s.Name),
                                                            new XElement("DateFinish", s.DateFinish))));

            Console.WriteLine(xDoc);
        } 

        //  g.	Выгрузить данные с таблицы Area в переменную, на основе данных в этой 
        //      переменной создать XML файл имеющим Xmlns = «area», 
        //      а также namespace - http://logbook.itstep.org/
        static void TaskG()
        {
            var editDataArea = from a in dataArea
                               select new { a.AreaId, a.ParentId, a.Name, Rate = 12 };

            XNamespace area = "area";
            XNamespace link = "http://logbook.itstep.org/";

            XElement xDoc = new XElement("TaskG",
                                    new XAttribute(XNamespace.Xmlns + "area", area.NamespaceName),
                                    new XAttribute(XNamespace.Xmlns + "link", link.NamespaceName),
                                    editDataArea.Select(s => new XElement ("area", 
                                                                new XElement("AreaId", s.AreaId),
                                                                new XElement("ParentId", s.ParentId),
                                                                new XElement("Name", s.Name),
                                                                new XElement(link + "Rate", s.Rate))));

            Console.WriteLine(xDoc);
        }


    }
}
