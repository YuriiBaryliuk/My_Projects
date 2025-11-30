using System;
using System.Text;

namespace Biblioteka_Projekt
{
    internal class Book
    {
        static int nextID = 1;
        public int m_ID{ get; private set; }
        public string m_author{ get; private set; }
        public string m_title{ get; private set; }
        public int m_yearOfRelease{ get; private set; }
        public string m_genre{ get; private set; }

        public Book(string author, string title, int yearOfRelese, string genre)
        {
            m_author = author;
            m_title = title;
            m_yearOfRelease = yearOfRelese;
            m_genre = genre;
            m_ID = nextID++;
        }

        public void printData()
        {
            Console.WriteLine($"ID: {m_ID}\nAuthor: {m_author}\nTitle: {m_title}\n" +
                $"Date of release: {m_yearOfRelease}\nGenre: {m_genre}\n");
        }
    }
}
