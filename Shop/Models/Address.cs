using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Neighborhood { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Telephone { get; set; }

        public int UserId { get; set; }

        public User User { get; set; } 

        public Address ()
        {

        }

        public Address(int id, string state, string city, string neighborhood, string street, string number, string telephone, User user)
        {
            Id = id;
            State = state;
            City = city;
            Neighborhood = neighborhood;
            Street = street;
            Number = number;
            Telephone = telephone;
            User = user;
        }
    }
}
