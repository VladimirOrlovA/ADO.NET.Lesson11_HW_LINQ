using System;
using System.Text;
using System.Threading.Tasks;

//Необходимые пространства имен
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace module05_4
{
    public enum ParticipantTypes { Author = 0, Editor }

    class Program
    {
        static void CreateXml()
        {



            /*
             <BookParticipants>  
                 <BookParticipant type="Author">    
                     <FirstName>Joe</FirstName>    
                     <LastName>Rattz</LastName>  
                 </BookParticipant>  
                 <BookParticipant type="Editor">    
                     <FirstName>Ewan</FirstName>    
                     <LastName>Buckingham</LastName>  
                 </BookParticipant> 
             </BookParticipants> 
             
             */
            // Объявление переменных, которые будут использоваться повторно. 
            XmlElement xmlBookParticipant;
            XmlAttribute xmlParticipantType;
            XmlElement xmlFirstName; XmlElement xmlLastName;

            // Сначала необходимо построить документ XML. 
            XmlDocument xmlDoc = new XmlDocument();

            // Создать корневой элемент и добавить его в документ. 
            XmlElement xmlBookParticipants = xmlDoc.CreateElement("BookParticipants");
            xmlDoc.AppendChild(xmlBookParticipants);

            // Создать список участников и добавить в список несколько участников подготовки книги. 
            xmlBookParticipant = xmlDoc.CreateElement("BookParticipant");
            xmlParticipantType = xmlDoc.CreateAttribute("type");
            xmlParticipantType.InnerText = "Author";
            xmlBookParticipant.Attributes.Append(xmlParticipantType);
            xmlFirstName = xmlDoc.CreateElement("FirstName");
            xmlFirstName.InnerText = "Joe";
            xmlBookParticipant.AppendChild(xmlFirstName);
            xmlLastName = xmlDoc.CreateElement("LastName");
            xmlLastName.InnerText = "Rattz";
            xmlBookParticipant.AppendChild(xmlLastName);
            xmlBookParticipants.AppendChild(xmlBookParticipant);

            // Создать еще одного участника и добавить в список участников подготовки книги. 
            xmlBookParticipant = xmlDoc.CreateElement("BookParticipant");
            xmlParticipantType = xmlDoc.CreateAttribute("type");
            xmlParticipantType.InnerText = "Editor";
            xmlBookParticipant.Attributes.Append(xmlParticipantType);
            xmlFirstName = xmlDoc.CreateElement("FirstName");
            xmlFirstName.InnerText = "Ewan";
            xmlBookParticipant.AppendChild(xmlFirstName);
            xmlLastName = xmlDoc.CreateElement("LastName");
            xmlLastName.InnerText = "Buckingham";

            xmlBookParticipant.AppendChild(xmlLastName);
            xmlBookParticipants.AppendChild(xmlBookParticipant);

            // Найти авторов и отобразить их имена и фамилии. 
            XmlNodeList authorsList = xmlDoc.SelectNodes("BookParticipants/BookParticipant[@type=\"Author\"]");
            foreach (XmlNode node in authorsList)
            {
                XmlNode firstName = node.SelectSingleNode("FirstName");
                XmlNode lastName = node.SelectSingleNode("LastName");
                //Console.WriteLine("{0} {1}", firstName, lastName);

                Console.WriteLine("{0} {1}", firstName.ToString(), lastName.ToString());
            }
        }

        static void Exmpl_CreateXml()
        {
            XElement xBookParticipants = 
                new XElement("BookParticipants",
                    new XElement("BookParticipant",
                        new XAttribute("type", "Author"),
                        new XElement("FirstName", "Joe"),
                        new XElement("LastName", "Rattz")),

                new XElement("BookParticipant",
                        new XAttribute("type", "Editor"),
                        new XElement("FirstName", "Ewan"),
                        new XElement("LastName", "Buckingham"))
                );

            Console.WriteLine(xBookParticipants.ToString());

        }

        static void Exmpl01()
        {
            XElement xBookParticipant = new XElement("BookParticipant",
                new XElement("FirstName", "Joe"),
                new XElement("LastName", "Rattz")
                );

            Console.WriteLine(xBookParticipant.ToString());
        }

      

        static void Exmpl02()
        {
            XDocument xDocument = new XDocument(
                new XElement("BookParticipants",
                                new XElement("BookParticipant",
                                                new XAttribute("type", "Author"),
                                                        new XElement("FirstName", "Joe"),
                                                        new XElement("LastName", "Rattz")
                                            )
                            )
                );
            Console.WriteLine(xDocument.ToString());
        }

        static void Exmpl02_2()
        {
            XElement xElement = new XElement("BookParticipants",
                new XElement("BookParticipant",
                new XAttribute("type", "Author"),
                new XElement("FirstName", "Joe"),
                new XElement("LastName", "Rattz")));

            Console.WriteLine(xElement.ToString());

        }

        static void Exmpl03_1()
        {
            /*
            <BookParticipants>  
                <BookParticipant type="Author">    
                    <FirstName>Joe</FirstName>    
                    <LastName>Rattz</LastName>  
                </BookParticipant>  
                <BookParticipant type="Editor">    
                    <FirstName>Ewan</FirstName>    
                    <LastName>Buckingham</LastName>  
                </BookParticipant> 
            </BookParticipants> 

            */

            //1
            XElement xBookParticipants = new XElement("BookParticipants");

            //2
            XNamespace nameSpace = "http://www.linqdev.com";
            XElement xBookParticipants2 = 
                new XElement(nameSpace + "BookParticipants");

            //3
            //Обратите внимание, что пространство имен заключено в фигурные скобки. 
            //Это указывает конструктору XElement на тот факт, что данная часть означает пространство имен. 
            XElement xBookParticipants3 = 
                new XElement("{http://www.linqdev.com}" + "BookParticipants");
        }

        static void Exmpl03_02()
        {
            XNamespace nameSpace = "http://www.linqdev.com";
            XElement xBookParticipants = new XElement("BookParticipants",
                new XElement("BookParticipant",
                new XAttribute("type", "Author"),
                new XElement("FirstName", "Joe"),
                new XElement("LastName", "Rattz")),

                new XElement("BookParticipant",
                new XAttribute("type", "Editor"),
                new XElement("FirstName", "Ewan"),
                new XElement("LastName", "Buckingham")));

            Console.WriteLine(xBookParticipants.ToString());

        }


        static void Main(string[] args)
        {
            Exmpl05_01();
            var test = "";
        }
        static void Exmpl03_03()
        {
            XNamespace nameSpace = "http://www.linqdev.com";
            XElement xBookParticipants = new XElement(nameSpace + "BookParticipants",
                new XAttribute(XNamespace.Xmlns + "linqdev", nameSpace),
                new XElement(nameSpace + "BookParticipant"));

            Console.WriteLine(xBookParticipants.ToString());
            Console.WriteLine("=============================================");

            XElement xBookParticipants2 = new XElement(nameSpace + "BookParticipants",
                new XAttribute(XNamespace.Xmlns + "linqdev", nameSpace),

                new XElement(nameSpace + "BookParticipant",
                new XAttribute("type", "Author"),
                new XElement(nameSpace + "FirstName", "Joe"),
                new XElement(nameSpace + "LastName", "Rattz")),

                new XElement(nameSpace + "BookParticipant",
                new XAttribute("type", "Editor"),
                new XElement(nameSpace + "FirstName", "Ewan"),
                new XElement(nameSpace + "LastName", "Buckingham")));

            Console.WriteLine(xBookParticipants2.ToString());

        }

        static void Exmpl04_01()
        {
            XElement name = new XElement("Name", "Joe");
            Console.WriteLine(name.ToString());

            Console.WriteLine("=============================================");
            //2
            XElement name2 = new XElement("Person",
                new XElement("FirstName", "Joe"),
                new XElement("LastName", "Rattz"));
            Console.WriteLine(name2);

            Console.WriteLine("=============================================");
            //3
            XElement name3 = new XElement("Name", "Joe");
            Console.WriteLine(name3);
            Console.WriteLine((string)name3);

        }

        static void Exmpl04_02()
        {
            XElement count = new XElement("Count", 12);
            Console.WriteLine(count);
            Console.WriteLine((int)count);

            XElement smoker = new XElement("Smoker", false);
            Console.WriteLine(smoker);
            Console.WriteLine((bool)smoker);

            XElement pi = new XElement("Pi", 3.1415926535);
            Console.WriteLine(pi);
            Console.WriteLine((double)pi);

        }

        static void Exmpl04_03()
        {
            XElement smoker = new XElement("Smoker", "true");
            Console.WriteLine(smoker);
            Console.WriteLine((bool)smoker);
        }

        static void Exmpl04_04()
        {
            try
            {
                XElement smoker = new XElement("Smoker", "Tue");
                Console.WriteLine(smoker);
                Console.WriteLine((bool) smoker);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


        static void Exmpl05_01()
        {
            XDocument xDocument = new XDocument(new XElement("BookParticipants",
                new XElement("BookParticipant",
                new XAttribute("type", "Author"),
                new XElement("FirstName", "Joe"),
                new XElement("LastName", "Rattz")),

                new XElement("BookParticipant",
                new XAttribute("type", "Editor"),
                new XElement("FirstName", "Ewan"),
                new XElement("LastName", "Buckingham")))
                );

            IEnumerable<XElement> elements =
                xDocument.Element("BookParticipants").
                Elements("BookParticipant");

            foreach (XElement element in elements)
            {
                Console.WriteLine("Исходный элемент: {0} : значение = {1}",
                    element.Name, element.Value);
            }

            foreach (XElement element in elements)
            {
                Console.WriteLine("Удаление {0} = {1} ...", element.Name, element.Value);
                element.Remove();
            }

            Console.WriteLine(xDocument);

        }

        static void Exmpl05_02()
        {
            XDocument xDocument = new XDocument(new XElement("BookParticipants",
                new XElement("BookParticipant",
                new XAttribute("type", "Author"),
                new XElement("FirstName", "Joe"),
                new XElement("LastName", "Rattz")),

                new XElement("BookParticipant",
                new XAttribute("type", "Editor"),
                new XElement("FirstName", "Ewan"),
                new XElement("LastName", "Buckingham")))
                );

            IEnumerable<XElement> elements = xDocument
                .Element("BookParticipants")
                .Elements("BookParticipant");

            foreach (XElement element in elements)
            {
                Console.WriteLine("Исходный элемент: {0} : значение = {1}", element.Name, element.Value);
            }

            // вызова операции ToArray в финальном перечислении, где удаляются элементы
            foreach (XElement element in elements.ToArray())
            {
                Console.WriteLine("Удаление {0} = {1} ...", element.Name, element.Value);
                element.Remove();
            }

            Console.WriteLine(xDocument);
        }

        static void Exmpl06_01()
        {
            XElement firstName = new XElement("FirstName", "Joe");
            Console.WriteLine((string)firstName);
        }

        static void Exmpl06_02()
        {
            BookParticipant[] bookParticipants = new[]
            {
                new BookParticipant { FirstName = "Joe", LastName = "Rattz", ParticipantType = ParticipantTypes.Author },
                new BookParticipant { FirstName = "Ewan", LastName = "Buckingham", ParticipantType = ParticipantTypes.Editor }
            };

            XElement xBookParticipants = new XElement("BookParticipants",
                bookParticipants.Select(p =>
                new XElement("BookParticipant",
                    new XAttribute("type", p.ParticipantType),
                    new XElement("FirstName", p.FirstName),
                    new XElement("LastName", p.LastName)))
                );

            Console.WriteLine(xBookParticipants);
        }

        static void Exmpl06_03()
        {
            XElement xBookParticipant = new XElement("BookParticipant", 
                new XAttribute("type", "Author"));
            Console.WriteLine(xBookParticipant);

            //2
            XElement xBookParticipant2 = new XElement("BookParticipant");
            XAttribute xAttribute = new XAttribute("type", "Author");
            xBookParticipant2.Add(xAttribute);

            Console.WriteLine(xBookParticipant2);
        }

        static void Exmpl06_04()
        {
            XElement xBookParticipant = new XElement("BookParticipant", 
                new XComment("This person is retired."));
            Console.WriteLine(xBookParticipant);

            //2
            XElement xBookParticipant2 = new XElement("BookParticipant");
            XComment xComment = new XComment("This person is retired.");
            xBookParticipant2.Add(xComment);
            Console.WriteLine(xBookParticipant2);

        }

    


        static void Exmpl06_05()
        {
            XDocument xDocument = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"), 
                new XElement("BookParticipant"));
            Console.WriteLine(xDocument);

            //2
            XDocument xDocument2 = new XDocument(new XElement("BookParticipant"));
            XDeclaration xDeclaration = new XDeclaration("1.0", "UTF-8", "yes");
            xDocument2.Declaration = xDeclaration;
            Console.WriteLine(xDocument2);

        }

        static void Exmpl06_06()
        {
            XDocument xDocument = new XDocument(
                new XDocumentType("BookParticipants", 
                null, "BookParticipants.dtd", null),
                new XElement("BookParticipant")
                );

            Console.WriteLine(xDocument);


            //2
            XDocument xDocument2 = new XDocument();
            XDocumentType documentType = new XDocumentType("BookParticipants", null, "BookParticipants.dtd", null);
            xDocument2.Add(documentType, new XElement("BookParticipants"));

            Console.WriteLine(xDocument2);

        }

        static void Exmpl06_07()
        {
            XDocument xDocument = new XDocument();

            Console.WriteLine(xDocument);


            //2
            XDocument xDocument2 = new XDocument(
                new XDeclaration("1.0", "UTF-8", "yes"),
                new XDocumentType("BookParticipants", null, "BookParticipants.dtd", null),
                new XProcessingInstruction("BookCataloger", "out-of-print"),
                new XElement("BookParticipants")
                );

            Console.WriteLine(xDocument2);

        }

        static void Exmpl06_08()
        {
            XElement xBookParticipant = new XElement("BookParticipant");
            Console.WriteLine(xBookParticipant);

            //2
            XNamespace ns = "http://www.linqdev.com/Books";
            XElement xBookParticipant2 = new XElement(ns + "BookParticipant");
            Console.WriteLine(xBookParticipant2);

        }

        static void Exmpl06_09()
        {
            XDocument xDocument = new XDocument(
                new XProcessingInstruction("BookCataloger", "out-of-print"),
                new XElement("BookParticipants",
                    new XElement("BookParticipant",
                    new XProcessingInstruction("ParticipantDeleter", "delete"),
                    new XElement("FirstName", "Joe"),
                    new XElement("LastName", "Rattz")))
                    );
            Console.WriteLine(xDocument);

            //2
            XDocument xDocument2 = new XDocument(
                new XElement("BookParticipants",
                    new XElement("BookParticipant",
                    new XElement("FirstName", "Joe"),
                    new XElement("LastName", "Rattz")))
                    );

            XProcessingInstruction xPI1 = new XProcessingInstruction("BookCataloger", "out-of-print");
            xDocument2.AddFirst(xPI1);

            XProcessingInstruction xPI2 = new XProcessingInstruction("ParticipantDeleter", "delete");
            XElement outOfPrintParticipant = xDocument.Element("BookParticipants").Elements("BookParticipant").Where(e => ((string)((XElement)e).Element("FirstName")) == "Joe" && ((string)((XElement)e).Element("LastName")) == "Rattz").Single<XElement>();

            outOfPrintParticipant.AddFirst(xPI2);
            Console.WriteLine(xDocument);

        }

        static void Exmpl06_10()
        {
            XElement xErrorMessage = new XElement("HTMLMessage",
                new XCData("<H1>Invalid user id or password.</H1>"));

            Console.WriteLine(xErrorMessage);

        }

       

        static void Exmpl07_01()
        {
            XDocument xDocument = new XDocument(
                new XElement("BookParticipants",
                    new XElement("BookParticipant",
                    new XAttribute("type", "Author"),
                    new XAttribute("experience", "first-time"),
                    new XAttribute("language", "English"),
                    new XElement("FirstName", "Joe"),
                    new XElement("LastName", "Rattz")))
                    );

            xDocument.Save("bookparticipants.xml");

            //2
            xDocument.Save("bookparticipants2.xml", 
                SaveOptions.DisableFormatting);
        }

        static void Exmpl07_02()
        {
            XElement bookParticipants = new XElement("BookParticipants",
                new XElement("BookParticipant",
                    new XAttribute("type", "Author"),
                    new XAttribute("experience", "first-time"),
                    new XAttribute("language", "English"),
                    new XElement("FirstName", "Joe"),
                    new XElement("LastName", "Rattz"))
                    );

            bookParticipants.Save("bookparticipants3.xml");

        }

        static void Exmpl08_01()
        {
            XDocument xDocument = 
                XDocument.Load("bookparticipants.xml",
                LoadOptions.SetBaseUri | LoadOptions.SetLineInfo);

            Console.WriteLine(xDocument);

            XElement firstName = xDocument.
                Descendants("FirstName").First();

            Console.WriteLine("FirstName Строка:{0} - Позиция:{1}",
                ((IXmlLineInfo)firstName).LineNumber,
                ((IXmlLineInfo)firstName).LinePosition);

            Console.WriteLine("FirstName Базовый URI:{0}", 
                firstName.BaseUri);

        }

        static void Exmpl08_02()
        {
            string xml = "<?xml version=\"1.0\" encoding=\"utf-8\"?><BookParticipants>" + "<BookParticipant type=\"Author\" experience=\"first-time\" language=" + "\"English\"><FirstName>Joe</FirstName><LastName>Rattz</LastName>" + "</BookParticipant></BookParticipants>";
            XElement xElement = XElement.Parse(xml);

            Console.WriteLine(xElement);

        }

        // Это используется для хранения ссылки на один из элементов в дереве XML. 
        static XElement firstParticipant;

        static XDocument Exmpl09_01BaseXml()
        {
           

            XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XDocumentType("BookParticipants", null, "BookParticipants.dtd", null),
                new XProcessingInstruction("BookCataloger", "out-of-print"),

                // Обратите внимание, что в следующей строке сохраняется   
                // ссылка на первый элемент BookParticipant.  

                new XElement("BookParticipants",
                firstParticipant = new XElement("BookParticipant",
                                                                    new XAttribute("type", "Author"),
                                                                    new XElement("FirstName", "Joe"),
                                                                    new XElement("LastName", "Rattz")
                                               ),
                new XElement("BookParticipant",
                                                new XAttribute("type", "Editor"),
                                                new XElement("FirstName", "Ewan"),
                                                new XElement("LastName", "Buckingham")
                            )
                            ));

            //Console.WriteLine(xDocument);

            return xDocument;
        }

        static void Exmpl09_02()
        {
            XElement firstParticipant;
            // Полный документ со всеми мелочами. 
            XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XDocumentType("BookParticipants", null, "BookParticipants.dtd", null),
                new XProcessingInstruction("BookCataloger", "out-of-print"),
                // Обратите внимание, что в следующей строке сохраняется   
                // ссылка на первый элемент BookParticipant.  
                new XElement("BookParticipants", 
                firstParticipant =
                new XElement("BookParticipant",
                    new XAttribute("type", "Author"),
                    new XElement("FirstName", "Joe"),
                    new XElement("LastName", "Rattz")),

                new XElement("BookParticipant",
                    new XAttribute("type", "Editor"),
                    new XElement("FirstName", "Ewan"),
                    new XElement("LastName", "Buckingham")))
                    );

            Console.WriteLine(firstParticipant.NextNode);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(firstParticipant.NextNode.PreviousNode);
            Console.WriteLine("-----------------------------------------");
        }

        static void Empl09_03()
        {
            XDocument doc = Exmpl09_01BaseXml();
            Console.WriteLine(doc.Document);
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(firstParticipant.Parent);
            Console.WriteLine("-----------------------------------------");
        }

        static void Exmpl10_01()
        {
            // Полный документ со всеми мелочами. 
            XDocument xDocument = new XDocument(  
                new XDeclaration("1.0", "UTF-8", "yes"),  
                new XDocumentType("BookParticipants", null, "BookParticipants.dtd", null),  
                new XProcessingInstruction("BookCataloger", "out-of-print"),  
                // Обратите внимание, что в следующей строке сохраняется   
                // ссылка на первый элемент BookParticipant.  
                
                new XElement("BookParticipants", 
                    firstParticipant =    new XElement("BookParticipant",      
                        new XAttribute("type", "Author"),      
                        new XElement("FirstName", "Joe"),      
                        new XElement("LastName", "Rattz")),

                        new XElement("BookParticipant", 
                        new XAttribute("type", "Editor"), 
                        new XElement("FirstName", "Ewan"), 
                        new XElement("LastName", "Buckingham")))
                        );

            foreach (XNode node in firstParticipant.Nodes())
            {
                Console.WriteLine(node);
            }
        }

        static void Exmpl10_02()
        {
            // Полный документ со всеми мелочами. 
            XDocument xDocument = new XDocument(  
                new XDeclaration("1.0", "UTF-8", "yes"),  
                new XDocumentType("BookParticipants", null, "BookParticipants.dtd", null),  
                new XProcessingInstruction("BookCataloger", "out-of-print"),  
                // Обратите внимание, что в следующей строке сохраняется   
                // ссылка на первый элемент BookParticipant.  
                new XElement("BookParticipants", firstParticipant =    
                    new XElement("BookParticipant", 
                         
                        new XComment("This is a new author."),      
                        new XProcessingInstruction("AuthorHandler", "new"),      
                        new XAttribute("type", "Author"),      
                        new XElement("FirstName", "Joe"),      
                        new XElement("LastName", "Rattz")),    
                        
                        new XElement("BookParticipant",      
                            new XAttribute("type", "Editor"),      
                            new XElement("FirstName", "Ewan"),      
                            new XElement("LastName", "Buckingham"))));

            Console.WriteLine("---------------------------------------------------");
            foreach (XNode node in firstParticipant.Nodes())
            {
                Console.WriteLine(node);
            }

            //Теперь можно видеть комментарий и инструкцию обработки. 
            //Но что, если нужны только узлы определенного типа, например, только элементы? 

            //Использование операции OfType для возврата только элементов 
            Console.WriteLine("---------------------------------------------------");
            foreach (XNode node in firstParticipant.Nodes().OfType<XElement>())
            {
                Console.WriteLine(node);
            }

            //Использование операции OfType для возврата только комментариев 
            Console.WriteLine("---------------------------------------------------");
            foreach (XNode node in firstParticipant.Nodes().OfType<XComment>())
            {
                Console.WriteLine(node);
            }
            Console.WriteLine("---------------------------------------------------");

        }

       
        static void Exmpl10_03()
        {
            // Полный документ со всеми мелочами. 
            XDocument xDocument = new XDocument(  
                new XDeclaration("1.0", "UTF-8", "yes"),  
                new XDocumentType("BookParticipants", null, "BookParticipants.dtd", null),  
                new XProcessingInstruction("BookCataloger", "out-of-print"),  
                // Обратите внимание, что в следующей строке сохраняется   
                // ссылка на первый элемент BookParticipant.  
                new XElement("BookParticipants", firstParticipant =    
                    new XElement("BookParticipant",      
                        new XComment("This is a new author."),      
                        new XProcessingInstruction("AuthorHandler", "new"),      
                        new XAttribute("type", "Author"),      
                        new XElement("FirstName", "Joe"),      
                        new XElement("LastName", "Rattz")),    
                        
                        new XElement("BookParticipant",      
                            new XAttribute("type", "Editor"),      
                            new XElement("FirstName", "Ewan"),      
                            new XElement("LastName", "Buckingham"))));

            Console.WriteLine("=============================================");
            foreach (XNode node in firstParticipant.Elements())
            {
                Console.WriteLine(node);
            }
            Console.WriteLine("=============================================");
            //Метод Elements также имеет перегруженную версию, которая позволяет передавать имя искомого элемента
            foreach (XNode node in firstParticipant.Elements("FirstName"))
            {
                Console.WriteLine(node);
            }
            Console.WriteLine("=============================================");
        }

        static void Exmpl10_04()
        {
            // Полный документ со всеми мелочами. 
            XDocument xDocument = new XDocument(  
                new XDeclaration("1.0", "UTF-8", "yes"),  
                new XDocumentType("BookParticipants", null, "BookParticipants.dtd", null),  
                new XProcessingInstruction("BookCataloger", "out-of-print"),  
                // Обратите внимание, что в следующей строке сохраняется   
                // ссылка на первый элемент BookParticipant.  
                new XElement("BookParticipants", firstParticipant =    
                    new XElement("BookParticipant",      
                    new XComment("This is a new author."),      
                    new XProcessingInstruction("AuthorHandler", "new"),     
                    new XAttribute("type", "Author"),      
                    new XElement("FirstName", "Joe"),      
                    new XElement("LastName", "Rattz")),  
                      
                    new XElement("BookParticipant",      
                    new XAttribute("type", "Editor"),      
                    new XElement("FirstName", "Ewan"),      
                    new XElement("LastName", "Buckingham"))));

            Console.WriteLine(firstParticipant.Element("FirstName"));

        }

        static void Exmpl11_01()
        {
            // Документ с одним участником подготовки книги. 
            XDocument xDocument = new XDocument(  new XElement("BookParticipants",    
                new XElement("BookParticipant",      
                new XAttribute("type", "Author"),      
                new XElement("FirstName", "Joe"),      
                new XElement("LastName", "Rattz"))));

            Console.WriteLine(xDocument);
        }

        //7.63
        static void Exmpl11_02()
        {
            // Документ с одним участником подготовки книги. 
            XDocument xDocument = new XDocument(  
                
                new XElement("BookParticipants",    
                    new XElement("BookParticipant",      
                        new XAttribute("type", "Author"),      
                        new XElement("FirstName", "Joe"),      
                        new XElement("LastName", "Rattz"))));

            xDocument.Element("BookParticipants").Add(  
                new XElement("BookParticipant",    
                    new XAttribute("type", "Editor"),    
                    new XElement("FirstName", "Ewan"),    
                    new XElement("LastName", "Buckingham")));


            Console.WriteLine(xDocument);

        }

        static void Exmpl11_03()
        {
            // Документ с одним участником подготовки книги. 
            XDocument xDocument = new XDocument(

                new XElement("BookParticipants",
                    new XElement("BookParticipant",
                        new XAttribute("type", "Author"),
                        new XElement("FirstName", "Joe"),
                        new XElement("LastName", "Rattz"))));

            xDocument.Element("BookParticipants").AddFirst(
                new XElement("BookParticipant",
                    new XAttribute("type", "Editor"),
                    new XElement("FirstName", "Ewan"),
                    new XElement("LastName", "Buckingham")));


            Console.WriteLine(xDocument);

        }

        static void Exmpl11_04()
        {
            // Документ с одним участником подготовки книги. 
            XDocument xDocument = new XDocument(

                new XElement("BookParticipants",
                    new XElement("BookParticipant",
                        new XAttribute("type", "Author"),
                        new XElement("FirstName", "Joe"),
                        new XElement("LastName", "Rattz"))));

            xDocument.Element("BookParticipants").Add(
                new XElement("BookParticipant",
                    new XAttribute("type", "Editor"),
                    new XElement("FirstName", "Ewan"),
                    new XElement("LastName", "Buckingham")));


            xDocument.Element("BookParticipants").Elements("BookParticipant")
                .Where(e => ((string)e.Element("FirstName")) == "Ewan")
                .Single<XElement>()
                .AddBeforeSelf(new XElement("BookParticipant", 
                    new XAttribute("type", "Technical Reviewer"), 
                    new XElement("FirstName", "Fabio"), 
                    new XElement("LastName", "Ferracchiati")));

            Console.WriteLine(xDocument);

        }

        static void Exmpl11_05()
        {
            // Документ с одним участником подготовки книги. 
            XDocument xDocument = new XDocument(  
                new XElement("BookParticipants",    
                    new XElement("BookParticipant",      
                    new XAttribute("type", "Author"),      
                    new XElement("FirstName", "Joe"),      
                    new XElement("LastName", "Rattz"))));

            xDocument.Element("BookParticipants").Add(  
                new XElement("BookParticipant",    
                new XAttribute("type", "Editor"),    
                new XElement("FirstName", "Ewan"),    
                new XElement("LastName", "Buckingham")));

            xDocument.Element("BookParticipants").Element("BookParticipant")
                .AddAfterSelf(      
                new XElement("BookParticipant",        
                new XAttribute("type", "Technical Reviewer"),        
                new XElement("FirstName", "Fabio"),        
                new XElement("LastName", "Ferracchiati")));

            Console.WriteLine(xDocument);

        }

        static void Exmpl11_06()
        {
            // Это используется для сохранения ссылки на один из элементов дерева XML. 
            XElement firstParticipant;
            Console.WriteLine(System.Environment.NewLine + "Перед удалением узла");

            XDocument xDocument = new XDocument(  
                new XElement("BookParticipants", 
                firstParticipant =    
                new XElement("BookParticipant",      
                    new XAttribute("type", "Author"),      
                    new XElement("FirstName", "Joe"),      
                    new XElement("LastName", "Rattz")),    
                
                new XElement("BookParticipant",      
                    new XAttribute("type", "Editor"),      
                    new XElement("FirstName", "Ewan"),      
                    new XElement("LastName", "Buckingham"))));

            Console.WriteLine(xDocument);
            firstParticipant.Remove();

            Console.WriteLine(System.Environment.NewLine +
                "После удаления узла");

            Console.WriteLine(xDocument);

        }

        static void Exmpl11_07()
        {
            XDocument xDocument = new XDocument(
                new XElement("BookParticipants", 
                firstParticipant = 
                    new XElement("BookParticipant", 
                    new XAttribute("type", "Author"), 
                    new XElement("FirstName", "Joe"), 
                    new XElement("LastName", "Rattz")), 
                
                new XElement("BookParticipant", 
                    new XAttribute("type", "Editor"), 
                    new XElement("FirstName", "Ewan"), 
                    new XElement("LastName", "Buckingham"))));

            xDocument.Descendants().
                Where(e => e.Name == "FirstName").Remove();

            Console.WriteLine(xDocument);

        }

        static void Exmpl11_08()
        {
            XDocument xDocument = new XDocument(
                new XElement("BookParticipants", 
                firstParticipant = 
                new XElement("BookParticipant", 
                    new XAttribute("type", "Author"), 
                    new XElement("FirstName", "Joe"), 
                    new XElement("LastName", "Rattz")), 
                
                new XElement("BookParticipant", 
                    new XAttribute("type", "Editor"),
                     new XElement("FirstName", "Ewan"), 
                     new XElement("LastName", "Buckingham"))));

            Console.WriteLine(System.Environment.NewLine + "Перед удалением содержимого.");
            Console.WriteLine(xDocument);

            xDocument.Element("BookParticipants").RemoveAll();

            Console.WriteLine(System.Environment.NewLine + "После удаления содержимого.");
            Console.WriteLine(xDocument);


        }

        static void Exmpl12_01()
        {
            // Это используется для сохранения ссылки на один из элементов дерева XML. 
            XElement firstParticipant;
            XDocument xDocument = new XDocument(  
                new XElement("BookParticipants", 
                    firstParticipant =    
                    new XElement("BookParticipant",      
                        new XAttribute("type", "Author"),      
                        new XElement("FirstName", "Joe"),      
                        new XElement("LastName", "Rattz"))));

            Console.WriteLine("Перед обновлением узлов:");
            Console.WriteLine(xDocument);

            // Теперь обновим элемент, комментарий и текстовый узел. 
            firstParticipant.Element("FirstName").Value = "Joey";
            firstParticipant.Nodes().OfType<XComment>()
                .Single().Value =  "Author of Pro: Language Integrated Query";

        }

      
    }

    public class BookParticipant
    {
        public string FirstName;
        public string LastName;
        public ParticipantTypes ParticipantType;
    }
}
