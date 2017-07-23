using Microsoft.WindowsAzure.MobileServices;
using SqlSaturdayToDo.Mobile.Interfaces;
using SqlSaturdayToDo.Mobile.Models;
using SqlSaturdayToDo.Mobile.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlSaturdayToDo.Backend.Services
{
    public class AzureCloudService : ICloudService
    {
        private MobileServiceClient client;

        public AzureCloudService()
        {
            client = new MobileServiceClient("https://sqlsaturdaybr.azurewebsites.net");
        }
        public ICloudTable<T> GetTable<T>() where T : TableData
        {
            return new AzureCloudTable<T>(client);
        }
    }
}