
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BankingEntities
{
    /// <summary>
    /// Represent a data structure for address.
    /// </summary>
    public  readonly struct Address
    {
        public readonly string City;
        public readonly string Street;
        public readonly string Country;
        public readonly int Number;

        public Address(string city, string street, string country, int number)
        {
            this.City = city;
            this.Street = street;
            this.Country = country;
            this.Number = number;
            
        }

        public override string ToString() { return $"Address: {this.Country} {this.City} {this.Street} {this.Number}"; }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is Address address && address.GetHashCode().Equals(this.GetHashCode());
        }

        public static bool operator ==(Address a, Address b)
        {
            return a.Equals(b);
        }

        public static bool operator != (Address a, Address b)
        {
            return !a.Equals(b);
        }

    }
}
