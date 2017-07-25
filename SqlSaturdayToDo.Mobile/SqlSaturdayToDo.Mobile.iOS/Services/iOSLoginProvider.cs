using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SqlSaturdayToDo.Mobile.Interfaces;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using SqlSaturdayToDo.Mobile.iOS.Services;

[assembly: Xamarin.Forms.Dependency(typeof(iOSLoginProvider))]
namespace SqlSaturdayToDo.Mobile.iOS.Services
{
    public class iOSLoginProvider : ILoginProvider
    {
        public async Task LoginAsync(MobileServiceClient client)
        {
            await client.LoginAsync(RootView, "microsoftaccount");
        }

        public UIViewController RootView => UIApplication.SharedApplication.KeyWindow.RootViewController;

    }
}