/*Задание 4:
Добавьте к предыдущему заданию возможность создания
массива журналов.
Измените функциональность из второго задания таким образом,
чтобы она учитывала массив журналов.
Выбор конкретного формат сериализации необходимо сделать
вам. Обращаем ваше внимание, что выбор должен быть
обоснованным.*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[Serializable]
public class Article
{
    public string Title { get; set; }
    public int Character_Count { get; set; }
    public string Announcement { get; set; }

    public Article() { }

    public Article(string title, int character_Count, string announcement)
    {
        Title = title;
        Character_Count = character_Count;
        Announcement = announcement;
    }

    public void Print_Info()
    {
        Console.WriteLine($"Название статьи: {Title}");
        Console.WriteLine($"Количество символов: {Character_Count}");
        Console.WriteLine($"Анонс статьи: {Announcement}");
    }
}

[Serializable]
public class Journal
{
    public string Title { get; set; }
    public string Publisher { get; set; }
    public DateTime Release_Date { get; set; }
    public int Page_Count { get; set; }
    public List<Article> Articles { get; set; }

    public Journal() { }

    public Journal(string title, string publisher, DateTime release_Date, int page_Count, List<Article> articles)
    {
        Title = title;
        Publisher = publisher;
        Release_Date = release_Date;
        Page_Count = page_Count;
        Articles = articles;
    }

    public void PrintInfo()
    {
        Console.WriteLine($"Название журнала: {Title}");
        Console.WriteLine($"Издательство: {Publisher}");
        Console.WriteLine($"Дата выпуска: {Release_Date.ToShortDateString()}");
        Console.WriteLine($"Количество страниц: {Page_Count}");

        if (Articles != null && Articles.Count > 0)
        {
            Console.WriteLine("\nСтатьи в журнале:");
            foreach (var article in Articles)
            {
                article.Print_Info();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("В журнале нет статей.");
        }
    }
}

class Program
{
    static List<Journal> journals = new List<Journal>();

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Ввод информации о журналах");
            Console.WriteLine("2. Вывод информации о журналах");
            Console.WriteLine("3. Добавление статьи в журнал");
            Console.WriteLine("4. Вывод информации о статьях в журнале");
            Console.WriteLine("5. Работа с массивом журналов");
            Console.WriteLine("6. Сериализация журналов и сохранение в файл");
            Console.WriteLine("7. Загрузка сериализованных журналов из файла и десериализация");
            Console.WriteLine("8. Выход");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Input_Journals_Info();
                    break;
                case "2":
                    Print_Journals_Info();
                    break;
                case "3":
                    Add_Article_To_Journal();
                    break;
                case "4":
                    Print_Articles();
                    break;
                case "5":
                    Manage_Journal_Array();
                    break;
                case "6":
                    Serialize_And_Save();
                    break;
                case "7":
                    Deserialize_From_File();
                    break;
                case "8":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                    break;
            }
        }
    }

    static void Input_Journals_Info()
    {
        Console.Write("Введите количество журналов: ");
        int journal_Count;
        while (!int.TryParse(Console.ReadLine(), out journal_Count) || journal_Count <= 0)
        {
            Console.WriteLine("Некорректный ввод. Повторите попытку.");
        }

        for (int i = 0; i < journal_Count; i++)
        {
            Console.WriteLine($"\nВведите информацию о журнале {i + 1}:");

            Console.Write("Введите название журнала: ");
            string title = Console.ReadLine();

            Console.Write("Введите название издательства: ");
            string publisher = Console.ReadLine();

            Console.Write("Введите дату выпуска (гггг-мм-дд): ");
            DateTime release_Date;
            while (!DateTime.TryParse(Console.ReadLine(), out release_Date))
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            Console.Write("Введите количество страниц: ");
            int page_Count;
            while (!int.TryParse(Console.ReadLine(), out page_Count) || page_Count <= 0)
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            List<Article> articles = Input_Articles();

            journals.Add(new Journal(title, publisher, release_Date, page_Count, articles));
        }
    }

    static void Manage_Journal_Array()
    {
        while (true)
        {
            Console.WriteLine("\nМеню работы с массивом журналов:");
            Console.WriteLine("1. Вывод информации о всех журналах");
            Console.WriteLine("2. Вывод информации о конкретном журнале");
            Console.WriteLine("3. Выход в главное меню");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Print_Journals_Info();
                    break;
                case "2":
                    Print_Journal_Info();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Некорректный выбор. Попробуйте еще раз.");
                    break;
            }
        }
    }

    static List<Article> Input_Articles()
    {
        List<Article> articles = new List<Article>();
        Console.WriteLine("Введите информацию о статьях (для завершения введите 'exit'):");

        while (true)
        {
            Console.Write("Введите название статьи: ");
            string title = Console.ReadLine();

            if (title.ToLower() == "exit")
                break;

            Console.Write("Введите количество символов: ");
            int character_Count;
            while (!int.TryParse(Console.ReadLine(), out character_Count) || character_Count <= 0)
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            Console.Write("Введите анонс статьи: ");
            string announcement = Console.ReadLine();

            articles.Add(new Article(title, character_Count, announcement));
        }

        return articles;
    }

    static void Print_Journals_Info()
    {
        if (journals != null && journals.Count > 0)
        {
            Console.WriteLine("\nИнформация о всех журналах:");
            foreach (var journal in journals)
            {
                journal.PrintInfo();
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("Информация о журналах отсутствует. Введите данные сначала.");
        }
    }

    static void Print_Journal_Info()
    {
        if (journals != null && journals.Count > 0)
        {
            Console.Write("Введите номер журнала: ");
            int journal_Index;
            while (!int.TryParse(Console.ReadLine(), out journal_Index) || journal_Index < 1 || journal_Index > journals.Count)
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            Journal selectedJournal = journals[journal_Index - 1];
            selectedJournal.PrintInfo();
        }
        else
        {
            Console.WriteLine("Информация о журналах отсутствует. Введите данные сначала.");
        }
    }

    static void Add_Article_To_Journal()
    {
        if (journals != null && journals.Count > 0)
        {
            Console.Write("Введите номер журнала, в который нужно добавить статью: ");
            int journal_Index;
            while (!int.TryParse(Console.ReadLine(), out journal_Index) || journal_Index < 1 || journal_Index > journals.Count)
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            Journal selected_Journal = journals[journal_Index - 1];

            if (selected_Journal.Articles == null)
            {
                selected_Journal.Articles = new List<Article>();
            }

            Console.WriteLine("Добавление новой статьи:");

            Console.Write("Введите название статьи: ");
            string title = Console.ReadLine();

            Console.Write("Введите количество символов: ");
            int character_Count;
            while (!int.TryParse(Console.ReadLine(), out character_Count) || character_Count <= 0)
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            Console.Write("Введите анонс статьи: ");
            string announcement = Console.ReadLine();

            selected_Journal.Articles.Add(new Article(title, character_Count, announcement));

            Console.WriteLine("Статья успешно добавлена.");
        }
        else
        {
            Console.WriteLine("Информация о журналах отсутствует. Введите данные сначала.");
        }
    }

    static void Print_Articles()
    {
        if (journals != null && journals.Count > 0)
        {
            Console.Write("Введите номер журнала, статьи которого нужно вывести: ");
            int journal_Index;
            while (!int.TryParse(Console.ReadLine(), out journal_Index) || journal_Index < 1 || journal_Index > journals.Count)
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
            }

            Journal selected_Journal = journals[journal_Index - 1];

            if (selected_Journal.Articles != null && selected_Journal.Articles.Count > 0)
            {
                Console.WriteLine("\nИнформация о статьях в журнале:");
                foreach (var article in selected_Journal.Articles)
                {
                    article.Print_Info();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("В журнале нет статей.");
            }
        }
        else
        {
            Console.WriteLine("Информация о журналах отсутствует. Введите данные сначала.");
        }
    }

    static void Serialize_And_Save()
    {
        if (journals != null && journals.Count > 0)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Journal>));
            using (TextWriter writer = new StreamWriter("journals.xml"))
            {
                serializer.Serialize(writer, journals);
                Console.WriteLine("\nМассив журналов успешно сериализован в XML и сохранен в файл journals.xml");
            }
        }
        else
        {
            Console.WriteLine("Информация о журналах отсутствует. Введите данные сначала.");
        }
    }

    static void Deserialize_From_File()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Journal>));

        try
        {
            using (TextReader reader = new StreamReader("journals.xml"))
            {
                List<Journal> deserialized_Journals = (List<Journal>)serializer.Deserialize(reader);
                Console.WriteLine("\nМассив журналов успешно загружен из файла и десериализован.");
                journals = deserialized_Journals;
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Файл не найден.");
        }
    }
}
