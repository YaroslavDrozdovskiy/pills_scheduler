using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using pills_scheduler.Models;
using SQLite;


namespace pills_scheduler.Data
{
    class PillsDatabase
    {
        private static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<PillsDatabase> Instance = new AsyncLazy<PillsDatabase>(async () =>
        {
            var instance = new PillsDatabase();
            CreateTableResult result = await Database.CreateTableAsync<Pills>();
            return instance;
        });

        public PillsDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DataBasePath, Constants.Flags);
        }

        //Получение данных о всех таблетках
        public Task<List<Pills>> GetItemsAsync()
        {
            Task<List<Pills>> result = Database.Table<Pills>().ToListAsync();
            if (result != null) {
                Console.WriteLine("Данные о всех таблетках получены.");
            }
            return result;

        }

        // Получение данных о таблетке по id
        public Task<Pills> GetItemAsync(int id)
        {
            return Database.Table<Pills>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        // Сохранение/обновление данных
        public Task<int> SaveItemAsync(Pills pill)
        {
            if (pill.ID != 0)
            {
                return Database.UpdateAsync(pill);
            }
            else
            {
                return Database.InsertAsync(pill);
            }
        }

        // Удалние данных
        public Task<int> DeletePillAsync(Pills pill)
        {
            return Database.DeleteAsync(pill);
        }

    }
}
