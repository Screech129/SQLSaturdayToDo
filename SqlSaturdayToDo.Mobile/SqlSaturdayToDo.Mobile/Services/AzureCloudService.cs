using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using SqlSaturdayToDo.Mobile.Interfaces;
using SqlSaturdayToDo.Mobile.Models;
using SqlSaturdayToDo.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SqlSaturdayToDo.Backend.Services
{
    public class AzureCloudService : ICloudService
    {
        private MobileServiceClient client;

        public AzureCloudService()
        {
            client = new MobileServiceClient("https://sqlsaturdaybr.azurewebsites.net");
        }

        async Task InitializeAsync()
        {
            //If Datbase is already initialized, skip
            if (client.SyncContext.IsInitialized)
                return;

            //Reference to local SQLite store
            var store = new MobileServiceSQLiteStore("offlinecache.db");

            //Define schema for local database
            store.DefineTable<TodoItem>();

            //Create local store and update schema
            await client.SyncContext.InitializeAsync(store);
        }

        public async Task<ICloudTable<T>> GetTableAsync<T>() where T : TableData
        {
            await InitializeAsync();
            return new AzureCloudTable<T>(client);
        }

        public Task LoginAsync()
        {
            //Lookup the platofrm dependant login proivder and login with it
            var loginProvider = DependencyService.Get<ILoginProvider>();
            return loginProvider.LoginAsync(client);
        }

        public async Task SyncOfflineCacheAsync()
        {
            await InitializeAsync();

            if (!(await CrossConnectivity.Current.IsRemoteReachable(client.MobileAppUri.Host, 443)))
            {
                Debug.WriteLine($"Cannot connect to {client.MobileAppUri} right now - offline");
                return;
            }
            try
            {
                // Push the Operations Queue to the mobile backend
                await client.SyncContext.PushAsync();
            }
            catch (MobileServicePushFailedException ex)
            {
                if (ex.PushResult != null)
                {
                    foreach (var error in ex.PushResult.Errors)
                    {
                        await ResolveConflictAsync(error);
                    }
                }

            }


            // Pull each sync table
            var taskTable = await GetTableAsync<TodoItem>();
            await taskTable.PullAsync();
        }

        private async Task ResolveConflictAsync(MobileServiceTableOperationError error)
        {
            var serverItem = error.Result.ToObject<TodoItem>();
            var localItem = error.Item.ToObject<TodoItem>();

            // Note that you need to implement the public override Equals(TodoItem item)
            // method in the Model for this to work
            if (serverItem.Equals(localItem))
            {
                // Items are the same, so ignore the conflict
                await error.CancelAndDiscardItemAsync();
                return;
            }

            // Client Always Wins
            localItem.Version = serverItem.Version;
            await error.UpdateOperationAsync(JObject.FromObject(localItem));

            // Server Always Wins
            // await error.CancelAndDiscardItemAsync();
        }
    }
}