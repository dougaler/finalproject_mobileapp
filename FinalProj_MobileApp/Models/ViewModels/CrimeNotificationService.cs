using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.LocalNotification;

namespace FinalProj_MobileApp
{
    public static class CrimeNotificationService
    {
        public static Task SendNotificationAsync(CrimeNotification notification)
        {
            var request = new NotificationRequest
            {
                NotificationId = notification.Id,
                Title = notification.Title,
                Description = notification.Description,
                ReturningData = $"crimeId={notification.Id}",
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now
                }
            };

            return LocalNotificationCenter.Current.Show(request);
        }

        public static List<CrimeNotification> GetMockNotifications()
        {
            return new List<CrimeNotification>
            {
                new CrimeNotification
                {
                    Id = 1,
                    Title = "Gunshot Wound Reported",
                    Description = "Police responding for report of a person with a gunshot wound. If safe, stay at your location. Be observant/take action as needed.",
                    LocationName = "Sigma Sigma Commons",
                    Date = new DateTime(2025, 4, 3, 20, 10, 0),
                    Severity = "Critical",
                    Status = "Active Investigation",
                    Latitude = 39.1320,
                    Longitude = -84.5165
                },
                new CrimeNotification
                {
                    Id = 2,
                    Title = "Emergency at CVS - Taft & Vine",
                    Description = "Police responding to emergency reported at CVS - Taft and Vine. If safe, stay at your location. Be observant/take action as needed.",
                    LocationName = "CVS - Taft and Vine",
                    Date = new DateTime(2025, 4, 2, 18, 45, 0),
                    Severity = "High",
                    Status = "Responding",
                    Latitude = 39.1298,
                    Longitude = -84.5099
                },
                new CrimeNotification
                {
                    Id = 3,
                    Title = "Shots Fired - Stratford Ave",
                    Description = "Police on scene of emergency after reports of shots fired near 2725 Stratford. Avoid the area.",
                    LocationName = "2725 Stratford Ave",
                    Date = new DateTime(2025, 3, 30, 23, 15, 0),
                    Severity = "Critical",
                    Status = "Scene Cleared",
                    Latitude = 39.1350,
                    Longitude = -84.5042
                },
                new CrimeNotification
                {
                    Id = 4,
                    Title = "Emergency Reported - Bishop St.",
                    Description = "Police responding to emergency reported on 3239 Bishop. If safe, stay at your location. Be observant/take action as needed.",
                    LocationName = "3239 Bishop St.",
                    Date = new DateTime(2025, 3, 29, 21, 50, 0),
                    Severity = "High",
                    Status = "Investigation Ongoing",
                    Latitude = 39.1329,
                    Longitude = -84.5180
                },
                new CrimeNotification
                {
                    Id = 5,
                    Title = "Suspicious Activity - Langsam Library",
                    Description = "Suspicious individual seen loitering outside the Langsam Library. Officers dispatched, area searched.",
                    LocationName = "Langsam Library",
                    Date = new DateTime(2025, 3, 28, 17, 25, 0),
                    Severity = "Medium",
                    Status = "Cleared",
                    Latitude = 39.1315,
                    Longitude = -84.5151
                }
            };
        }
    }
}
