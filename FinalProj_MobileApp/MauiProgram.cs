using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Controls.Maps;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;

namespace FinalProj_MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseLocalNotification()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                })
                .UseMauiMaps();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            StartRandomNotificationTask();

            return app;
        }

        public static void StartRandomNotificationTask()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    // Wait for 30 seconds between notifications (for testing)
                    await Task.Delay(TimeSpan.FromSeconds(30));

                    var notification = new NotificationRequest
                    {
                        NotificationId = new Random().Next(1000),
                        Title = "Gunshot Wound Reported",
                        Description = "Police responding for report of a person with a gunshot wound. If safe, stay at your location. Be observant/take action as needed.",
                        Android = new AndroidOptions
                        {
                            ChannelId = "random_alert_channel", 
                            Priority = AndroidPriority.High,  
                            VisibilityType = AndroidVisibilityType.Public, 
                            IconSmallName = new AndroidIcon
                            {
                                ResourceName = "ic_launcher",  
                                Type = AndroidIcon.DefaultType
                            },
                            Color = new AndroidColor { ResourceName = "notification_gray" }, 
                            VibrationPattern = new long[] { 0, 500, 1000, 500 }  
                        },
                        Schedule = new NotificationRequestSchedule
                        {
                            NotifyTime = DateTime.Now.AddSeconds(1)  
                        }
                    };

                    LocalNotificationCenter.Current.Show(notification);
                }
            });
        }
    }
}