using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace FinalProj_MobileApp.DB
{
    public class LocalDBService
    {
        private const string DB_NAME = "crime_db.db3";
        private readonly SQLiteAsyncConnection _connection;

        public LocalDBService()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            _connection.CreateTableAsync<Crime>();
        }

        public async Task<List<Crime>> GetCrimes()
        {
            return await _connection.Table<Crime>().ToListAsync();
        }

        public async Task<Crime> GetById(int id)
        {
            return await _connection.Table<Crime>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Crime crime)
        {
            await _connection.InsertAsync(crime);
        }

        public async Task Update(Crime crime)
        {
            await _connection.UpdateAsync(crime);
        }

        public async Task Delete(Crime crime)
        {
            await _connection.DeleteAsync(crime);
        }

        public async Task SeedMockDataIfEmpty()
        {
            var existingCrimes = await GetCrimes();
            if (existingCrimes != null && existingCrimes.Any())
            {
                return;
            }

            var mockData = CrimeNotificationService.GetMockNotifications();
            foreach (var item in mockData)
            {
                var crime = new Crime
                {
                    Title = item.Title,
                    Description = item.Description,
                    LocationName = item.LocationName,
                    Date = item.Date,
                    Severity = item.Severity,
                    Status = item.Status,
                    Latitude = item.Latitude,
                    Longitude = item.Longitude
                };

                await Create(crime);
            }
        }

    }
}
