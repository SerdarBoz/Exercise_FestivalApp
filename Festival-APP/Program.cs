using System.Collections.Immutable;
using System.ComponentModel.Design;

namespace Festival_APP
{
    internal class Program
    {
        private static string MENU = 
@"Beheer festival.
A> Groep boeken
B> Groep verwijderen
C> Boekingen bekijken
D> Rider toevoegen
E> Rider bekijken
Q> Verlaten";
        static void Main(string[] args)
        {
            List<Booking> bookings = new List<Booking>();
            int bookingCount = 1;
            int id;
            Console.WriteLine();
            while(true) 
            {
                Console.WriteLine(MENU);
                Console.Write("> ");
                string input = Console.ReadLine().ToUpper();
                bool conflicts = false, found = false;

                try
                {
                    switch (input) 
                    {
                    case "A":
                        Console.Write("Groep: ");
                        string name = Console.ReadLine();
                        Console.Write("Uur start: ");
                        int startHour = int.Parse(Console.ReadLine());
                        Console.Write("Uur einde: ");
                        int endHour = int.Parse(Console.ReadLine());
                        Console.WriteLine();
                        foreach (Booking book in bookings)
                        {
                            if (book.conflictsWith(new Booking(bookingCount, name, startHour, endHour)))
                            {
                                conflicts = true;
                                break;
                            }
                        }
                        if (conflicts)
                        {
                            Console.WriteLine("Uren zijn al bezet");
                        }
                        else
                        {
                            bookings.Add(new Booking(bookingCount++, name, startHour, endHour));
                        }
                        break;
                    case "B":
                        foreach (Booking book in bookings)
                        {
                            Console.WriteLine($"{book.Id} - {book.GroupName} van {book.StartHour} tot {book.EndHour} ({book.Duration} uur)");
                        }
                        Console.WriteLine("Verwijder");
                        Console.Write("> ");
                        id = int.Parse(Console.ReadLine());

              
                        foreach (Booking book in bookings)
                        {
                            if (book.Id == id)
                            {
                                found = true;
                                bookings.Remove(book);
                                Console.WriteLine($"Keuze {id}  is verwijderd");
                                break;
                            }
                        }
                        if (!found){
                            Console.WriteLine("Uw keuze bestaat niet");
                        }
                        break;
                    case "C":
                        foreach (Booking book in bookings) 
                        {
                            Console.WriteLine($"{book.Id} - {book.GroupName} van {book.StartHour} tot {book.EndHour} ({book.Duration} uur)");
                        }
                        break;
                    case "D":
                        Console.WriteLine("Kies een groep om rider te bewerken");
                        foreach (Booking book in bookings)
                        {
                            Console.WriteLine($"{book.Id} - {book.GroupName} van {book.StartHour} tot {book.EndHour}  ({book.Duration} uur)");
                        }
                        Console.Write("> ");
                        id = int.Parse(Console.ReadLine());

                        foreach (Booking book in bookings)
                        {
                            if (book.Id == id)
                            {
                                found = true;
                                Console.WriteLine("Geef een beschrijving: ");
                                string itemAdd = Console.ReadLine();
                                book.addRiderItem(itemAdd);
                                Console.WriteLine($"\"{itemAdd}\" toegevoegd aan rider");
                                break;
                            }
                        }
                        if (!found)
                        {
                            Console.WriteLine("Uw keuze bestaat niet");
                        }
                        break;
                    case "E":
                        Console.WriteLine("Kies een groep om rider te bekijken");
                        foreach (Booking book in bookings)
                        {
                            Console.WriteLine($"{book.Id} - {book.GroupName} van {book.StartHour} tot {book.EndHour}  ({book.Duration} uur)");
                        }
                        id = int.Parse(Console.ReadLine());

         
                        foreach (Booking book in bookings)
                        {
                            if (book.Id == id)
                            {
                                found = true;
                                foreach (string riderItem in book.RiderItems)
                                {
                                    Console.WriteLine(riderItem);
                                }
                                break;

                            }
                    
                        }
                        if(!found)
                        {
                            Console.WriteLine("Uw keuze bestaat niet");
                        }
                        break;
                    case "Q":
                        Console.WriteLine("Tot ziens!");
                        return;
                    default:
                        Console.WriteLine("Ongeldige input");
                        break;
                    }
                }
                catch (BookingException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}