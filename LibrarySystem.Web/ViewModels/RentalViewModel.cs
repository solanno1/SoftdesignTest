using System;

namespace LibrarySystem.Web.ViewModels
{
    public class RentalViewModel
    {
        public int Id { get; set; } 
        public string BookTitle { get; set; }  
        public DateTime RentDate { get; set; }  
        public DateTime? ReturnDate { get; set; }  
        public bool IsReturned => ReturnDate.HasValue; 
    }
}