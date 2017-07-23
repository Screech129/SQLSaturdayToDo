using Microsoft.WindowsAzure.MobileServices;
using SqlSaturdayToDo.Mobile.Interfaces;
using SqlSaturdayToDo.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSaturdayToDo.Mobile.Services
{
    public class AzureCloudTable<T> : ICloudTable<T> where T : TableData
    {
        MobileServiceClient client;
        IMobileServiceTable<T> table;

        public AzureCloudTable(MobileServiceClient client)
        {
            this.client = client;
            this.table = client.GetTable<T>();
        }

        public async Task<T> CreateItemAsync(T item)
        {
            await table.InsertAsync(item);
            return item;
        }

        public async Task DeleteItemAsync(T item) => await table.DeleteAsync(item);

        public async Task<ICollection<T>> ReadAllItemsAsync() => await table.ToListAsync();

        public async Task<T> ReadItemAsync(string id) => await table.LookupAsync(id);

        public async Task<T> UpdateItemAsync(T item)
        {
            await table.UpdateAsync(item);
            return item;
        }
    }
}
