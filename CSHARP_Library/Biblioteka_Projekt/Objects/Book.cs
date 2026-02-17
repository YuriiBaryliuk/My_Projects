// Book type of data that represent book
// Has 7 members that store:
    // Next book id (special counter for IDs),
    // Book ID,
    // Author name,
    // Book title,
    // Year of book release,
    // Genre ID,
    // Description of a book
// Has 1 method
// Has 1 constructor

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
        public int m_genreID{ get; private set; }
        public string m_description{ get; private set; }

        public Book(string author, string title, int yearOfRelese, int genre, string description = "-")
        {
            m_author = author;
            m_title = title;
            m_yearOfRelease = yearOfRelese;
            m_genreID = genre;
            m_description = description;
            m_ID = nextID++;
        }

        public void printData()
        {
            Console.WriteLine($"ID: {m_ID}\nAuthor: {m_author}\nTitle: {m_title}\n" +
                $"Date of release: {m_yearOfRelease}\nGenre: {m_genreID}\nDescription: {m_description}\n");
        }
    }
}
