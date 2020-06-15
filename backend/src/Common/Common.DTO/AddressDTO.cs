/*
* Copyright (c) Akveo 2019. All Rights Reserved.
* Licensed under the Single Application / Multi Application License.
* See LICENSE_SINGLE_APP / LICENSE_MULTI_APP in the ‘docs’ folder for license information on type of purchased license.
*/

namespace Common.DTO
{
    public class AddressDTO
    {
        public AddressDTO() { }

        public AddressDTO(string city, string street, string zipCode, double? lat, double? lng)
        {
            Street = street;
            City = city;
            ZipCode = zipCode;
            Lat = lat;
            Lng = lng;
        }

        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
    }
}
