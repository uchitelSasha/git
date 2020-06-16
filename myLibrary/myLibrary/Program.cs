using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myLibrary
{
    public class bookObject
    {
        private int daysToOwn;
        private string bookTitle;
        public bookObject(string bookTitle, int daysToOwn)
        {
            this.daysToOwn = daysToOwn;
            this.bookTitle = bookTitle;
        }
        public override string ToString()
        {
            return "TITLE: " + bookTitle.ToString() + " ; days left: " + daysToOwn.ToString();
        }
    }
    public class mUser
    {
        static string bFilePath = "bList.md";
        public static List<Object> ownedBooks = new List<object>();
        public static void ownBook()
        {
            int daysToOwn = 0;
            string[] lines = File.ReadAllLines(bFilePath, Encoding.UTF8);
            int selectedBook = 0;
            bool done = false;
            while (!done)
            {
                for (int i = 0; i < lines.Count(); i++)
                {
                    if (selectedBook == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                    Console.WriteLine(lines[i]);

                    Console.ResetColor();
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        selectedBook = Math.Max(0, selectedBook - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        selectedBook = Math.Min(lines.Count() - 1, selectedBook + 1);
                        break;
                    case ConsoleKey.Enter:
                        done = true;
                        break;
                }

                if (!done)
                    Console.CursorTop = Console.CursorTop - lines.Count();
            }
            Console.WriteLine("Enter days you want to own the {0}:", lines[selectedBook]);
            daysToOwn = Convert.ToInt32(Console.ReadLine());
            ownedBooks.Add(new bookObject(lines[selectedBook], daysToOwn));
            Console.WriteLine("You successfully owned {0} for {1} day(s)", lines[selectedBook], daysToOwn);
        }
        public static void listOwned()
        {
            if (ownedBooks.Count >= 1)
            {
                foreach (var item in ownedBooks)
                {
                    Console.WriteLine(item.ToString());
                }
            }
            else
            {
                Console.WriteLine("You got nothing, good day, sir!");
            }
        }
        public static void deleteBook()
        {
            if (ownedBooks.Count > 0)
            {
                int selected = 0;
                bool done = false;
                while (!done)
                {
                    for (int i = 0; i < ownedBooks.Count(); i++)
                    {
                        if (selected == i)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("> ");
                        }
                        else
                        {
                            Console.Write("  ");
                        }

                        Console.WriteLine(ownedBooks[i].ToString());

                        Console.ResetColor();
                    }

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            selected = Math.Max(0, selected - 1);
                            break;
                        case ConsoleKey.DownArrow:
                            selected = Math.Min(ownedBooks.Count() - 1, selected + 1);
                            break;
                        case ConsoleKey.Enter:
                            done = true;
                            break;
                    }

                    if (!done)
                        Console.CursorTop = Console.CursorTop - ownedBooks.Count();
                }
            }
            else
            {
                Console.WriteLine("You got nothing, good day, sir!");
            }
        }
    }

    public class mBook
    {
        static string bFilePath = "bList.md";
        public static void addBook()
        {
            Console.Clear();
            string title, author;
            Console.WriteLine("BOOK TITLE: ");
            title = Console.ReadLine();
            Console.WriteLine("BOOK AUTHOR: ");
            author = Console.ReadLine();
            StreamWriter w = File.AppendText(bFilePath);
            w.WriteLine("{0} by {1}", title, author);
            w.Dispose();
            Console.WriteLine("{0} by {1} was successfully cached", title, author);
            title = null;
            author = null;
        }
        public static void listBooks()
        {
            Console.Clear();
            string[] lines = File.ReadAllLines(bFilePath, Encoding.UTF8);
            for (int i = 0; i < lines.Length; i++)
            {
                Console.WriteLine(lines[i]);
            }
        }
    }
    class Program
    {
        private static void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }
        private static void callMainMenu()
        {
            string[] options = new string[]
              {
            "ADD",
            "GET",
            "LIST",
            "LIST MY",
            "DELETE MY",
            "RETURN"
           };
            int selected = 0;
            bool done = false;
            while (!done)
            {
                for (int i = 0; i < options.Count(); i++)
                {
                    if (selected == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write("> ");
                    }
                    else
                    {
                        Console.Write("  ");
                    }

                    Console.WriteLine(options[i]);

                    Console.ResetColor();
                }

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.UpArrow:
                        selected = Math.Max(0, selected - 1);
                        break;
                    case ConsoleKey.DownArrow:
                        selected = Math.Min(options.Count() - 1, selected + 1);
                        break;
                    case ConsoleKey.Enter:
                        done = true;
                        break;
                }

                if (!done)
                    Console.CursorTop = Console.CursorTop - options.Count();
            }
            switch (selected)
            {
                case 0:
                    mBook.addBook();
                    break;
                case 1:
                    mUser.ownBook();
                    break;
                case 2:
                    mBook.listBooks();
                    break;
                case 3:
                    mUser.listOwned();
                    break;
                case 4:
                    mUser.deleteBook();
                    break;
                case 5:
                    System.Environment.Exit(0);
                    break;
            }


        }
        static void Main(string[] args)
        {
            Console.WriteLine("PRESS ENTER TO START...");
            while (true)
            {
                Console.WriteLine("TAP ENTER TO CALL MAIN MENU");
                Console.ReadKey();
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    Console.Clear();
                    callMainMenu();
                }
            }
        }
    }
}

