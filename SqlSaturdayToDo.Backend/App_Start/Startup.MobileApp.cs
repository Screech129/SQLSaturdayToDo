using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using SqlSaturdayToDo.Backend.DataObjects;
using SqlSaturdayToDo.Backend.Models;
using Owin;
using System.Data.Entity.Migrations;

namespace SqlSaturdayToDo.Backend
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            var mobileConfig = new MobileAppConfiguration();

            mobileConfig.AddTablesWithEntityFramework()
            .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            var migrator = new DbMigrator(new Migrations.Configuration());
            migrator.Update();

            app.UseWebApi(config);
        }
    }

   
}

