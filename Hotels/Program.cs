using HW_Hotels.Data;
using HW_Hotels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HW_Hotels;


public class Program
{
   

    static void Main()
    {
        bool Doing = true;
        while (Doing)
        {

            int choice;
            Console.WriteLine("Hotels \n 1 - Add booking \n 2 - Cancel booking \n 0 - exit");
            choice = int.Parse(Console.ReadLine()!);
            var BookFunctions = new BookFuncs();

            switch (choice)
            {
                case 1:
                    var NewBooking = new Booking();
                    Console.WriteLine("Enter the ID of a Hotel:");
                    NewBooking.HotelId = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter the ID of a Room:");
                    NewBooking.RoomId = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter the start date:");
                    NewBooking.StartDate = DateTime.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter the end date:");
                    NewBooking.EndDate = DateTime.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter your name:");
                    NewBooking.BookerName = Console.ReadLine()!;
                    BookFunctions.CreateBooking(NewBooking);
                    break;
                case 2:
                    Console.WriteLine("Enter the booking ID: ");
                    BookFunctions.CancelBooking(int.Parse(Console.ReadLine()!));
                    break;
                case 3:
                    Console.WriteLine("Enter the ID of preffered Hotel:");
                    int hid = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter the preffered start date:");
                    DateTime tsd = DateTime.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter the preffered end date:");
                    DateTime ted = DateTime.Parse(Console.ReadLine()!);
                    var results = BookFunctions.GetAvailableRooms(tsd, ted, hid);
                    foreach (var room in results)
                    {
                        Console.WriteLine($"ID: {room.Id}, Type: {room.Type}");
                    }

                    break;
                case 0:
                    Doing = false;
                    break;
            }
           
        }
    }
}

public class BookFuncs
{
    public HotelDbContext db = new HotelDbContext();
    public bool IsRoomAvailable(int roomId, DateTime startDate, DateTime endDate)
    {
        return !db.Bookings.Any(b => b.RoomId == roomId &&
            ((startDate >= b.StartDate && startDate < b.EndDate) ||
            (endDate > b.StartDate && endDate <= b.EndDate) ||
            (startDate <= b.StartDate && endDate >= b.EndDate)));
    }

    public void CreateBooking(Booking booking)
    {
        if (!IsRoomAvailable(booking.RoomId, booking.StartDate, booking.EndDate))
        {
            Console.WriteLine("The room is not available for the selected dates");
        }

        db.Bookings.Add(booking);
        db.SaveChanges();
    }


    public void CancelBooking(int id)
    {
        var booking = db.Bookings.FirstOrDefault(b => b.Id == id);

        if (booking == null)
            Console.WriteLine("Booking not found");

        db.Bookings.Remove(booking);
        db.SaveChanges();
    }

    public List<Room> GetAvailableRooms(DateTime startDate, DateTime endDate, int hotelId)
    {
       
        var roomsInHotel = db.Rooms.Where(r => r.HotelId == hotelId).ToList();

      
        var bookedRoomIds = db.Bookings
            .Where(b => b.HotelId == hotelId &&
                        ((startDate >= b.StartDate && startDate < b.EndDate) || 
                         (endDate > b.StartDate && endDate <= b.EndDate) ||
                         (startDate <= b.StartDate && endDate >= b.EndDate)))
            .Select(b => b.RoomId)
            .ToList();

       
        return roomsInHotel.Where(r => !bookedRoomIds.Contains(r.Id)).ToList();
    }


}