using SqlSaturdayToDo.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSaturdayToDo.Mobile.Interfaces
{
    //This is used for initializing the connection to Azure and getting a table definition
    public interface ICloudService
    {
        Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData;

        Task LoginAsync();

        Task SyncOfflineCacheAsync();
    }
}
