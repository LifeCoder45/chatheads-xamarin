using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using XamarinChatHeads.Services;

namespace XamarinChatHeads.Activites
{
    [Activity(Label = "XamarinChatHeads", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.Main);

            FindViewById<Button>(Resource.Id.StartServiceButton).Click += delegate
            {
                StartService(new Intent(this, typeof (ChatHeadService)));
            };

            FindViewById<Button>(Resource.Id.StopServiceButton).Click += delegate
            {
                StopService(new Intent(this, typeof(ChatHeadService)));
            };
        }
    }
}