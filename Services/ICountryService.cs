﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICountryService
    {
        List<Country> GetCountries();
        void InsertCountry(Country country);
        void UpdateCountry(Country country);
        void DeleteCountry(Country country);
        Country? GetCountryById(string countryId);
    }
}
