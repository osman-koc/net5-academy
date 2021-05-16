using NET5Academy.Shared.Domain;
using System.Collections.Generic;

namespace NET5Academy.Services.Order.Domain.OrderAggregate
{
    public class Address : OkValueObject
    {
        public string City { get; private set; }
        public string County { get; private set; }
        public string Street { get; private set; }
        public string ZipCode { get; private set; }
        public string Detail { get; private set; }

        public Address() { }
        public Address(string city, string county, string street, string zipCode, string detail)
        {
            City = city;
            County = county;
            Street = street;
            ZipCode = zipCode;
            Detail = detail;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return City;
            yield return County;
            yield return Street;
            yield return ZipCode;
            yield return Detail;
        }
    }
}
