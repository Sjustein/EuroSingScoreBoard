﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bas.EuroSing.ScoreBoard.Model;

namespace Bas.EuroSing.ScoreBoard.Services
{
    internal sealed class DataService : IDataService
    {
        public async Task<int> AddCountryAsync(Country country)
        {
            var db = new ScoreBoardDbContext();

            db.Countries.Add(country);
            await db.SaveChangesAsync();

            return country.Id;
        }

        public Collection<Country> GetAllCountries()
        {
            var db = new ScoreBoardDbContext();

            return new Collection<Country>(db.Countries.ToList());
        }
    }
}
