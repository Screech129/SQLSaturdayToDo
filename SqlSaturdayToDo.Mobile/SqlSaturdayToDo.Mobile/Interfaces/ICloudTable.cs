using SqlSaturdayToDo.Mobile.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SqlSaturdayToDo.Mobile.Interfaces
{
    //CRUD interface into a table
    public interface ICloudTable<T> where T : TableData
    {
        Task<T> CreateItemAsync(T item);
        Task<T> ReadItemAsync(string id);
        Task<T> UpdateItemAsync(T item);
        Task DeleteItemAsync(T item);
        Task<ICollection<T>> ReadAllItemsAsync();
        Task PullAsync();
    }
}