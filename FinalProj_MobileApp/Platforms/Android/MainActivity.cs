using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Plugin.LocalNotification;  

namespace FinalProj_MobileApp
{
    [Activity(
        Theme = "@style/Maui.SplashTheme",
        MainLauncher = true,
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges =
            ConfigChanges.ScreenSize
          | ConfigChanges.Orientation
          | ConfigChanges.UiMode
          | ConfigChanges.ScreenLayout
          | ConfigChanges.SmallestScreenSize
          | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        const int REQUEST_POST_NOTIFICATIONS = 1000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            LocalNotificationCenter.MainActivity = this;

            LocalNotificationCenter.CreateNotificationChannel();

            if ((int)Build.VERSION.SdkInt >= 33
                && CheckSelfPermission(Manifest.Permission.PostNotifications)
                   != Permission.Granted)
            {
                RequestPermissions(
                  new[] { Manifest.Permission.PostNotifications },
                  REQUEST_POST_NOTIFICATIONS);
            }

            LocalNotificationCenter.NotifyNotificationTapped(Intent);
        }

        protected override void OnNewIntent(Intent intent)
        {
            LocalNotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
        }

        public override void OnRequestPermissionsResult(
            int requestCode,
            string[] permissions,
            Permission[] grantResults)
        {
            if (requestCode == REQUEST_POST_NOTIFICATIONS
                && grantResults.Length > 0
                && grantResults[0] != Permission.Granted)
            {
            }

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
