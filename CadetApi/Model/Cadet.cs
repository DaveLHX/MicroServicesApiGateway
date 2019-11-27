using System;

namespace CadetApi.Model
{
    public class Cadet
    {
        public int Id { get; set; }

        public DateTime BirthDate { get; set; }

        public int CurrentRank { get; set; }

        public int Element { get; set; }

        public int Program { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    
}
