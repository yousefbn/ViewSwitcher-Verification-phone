using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using Android.Views.Animations;
using System;
using Android.Animation;
using Android.Telephony;
using Android.Content;
using Java.Lang;

namespace App7
{
    [Activity(Label = "App7", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        ViewSwitcher vs;
        Button b,b2;
        string msg;
        EditText n1, n2, n3, n4, n5, phone;

        

        public object NavigationService { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);
            vs = FindViewById<ViewSwitcher>(Resource.Id.viewSwitcher1);
            b = FindViewById<Button>(Resource.Id.myButton);
            n1 = FindViewById<EditText>(Resource.Id.editText1);
            n2 = FindViewById<EditText>(Resource.Id.editText2);
            n3 = FindViewById<EditText>(Resource.Id.editText3);
            n4 = FindViewById<EditText>(Resource.Id.editText4);
            n5 = FindViewById<EditText>(Resource.Id.editText5);
            phone = FindViewById<EditText>(Resource.Id.etphone);
            b2 = FindViewById<Button>(Resource.Id.btt);


            Animation Anin= AnimationUtils.LoadAnimation(this, Android.Resource.Animation.SlideInLeft);
            Animation AnOut=AnimationUtils.LoadAnimation(this, Android.Resource.Animation.SlideOutRight);

            b.Click += B_Click;
            b2.Click += B2_Click;

        }

        private void B2_Click(object sender, EventArgs e)
        {
            CheckResult(); 
        }

        private void B_Click(object sender, System.EventArgs e)
        {
            SendtoSecond();
        }
        public string GenerateRandomNo()
        {
            int _min = 00000;
            int _max = 99999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max) + "";
        }

        public void SendtoSecond() {


            if (phone.Text == "")
            {
                Toast.MakeText(this, "Pleas Enter Your phone", ToastLength.Long).Show();
            }
            else if (phone.Text != "")
            {
                var phoneNum = phone.Text;
                 msg = GenerateRandomNo();
                var piSent = PendingIntent.GetBroadcast(this, 0, new Intent("SMS_SENT"), 0);
                var piDelivered = PendingIntent.GetBroadcast(this, 0, new Intent("SMS_DELIVERED"), 0);

#pragma warning disable CS0618 // Type or member is obsolete
                SmsManager.Default.SendTextMessage(phoneNum, null,
#pragma warning restore CS0618 // Type or member is obsolete
                msg, null, null);

                vs.ShowNext();
                b.Visibility = ViewStates.Invisible;
            }
        }

        public void CheckResult() {

           
            StringBuilder Code = new StringBuilder();
            Code.Append(n1.Text);
            Code.Append(n2.Text);
            Code.Append(n3.Text);
            Code.Append(n4.Text);
            Code.Append(n5.Text);


            if (Code.ToString() == msg)
            {

                Toast.MakeText(this, "Valid Code", ToastLength.Long).Show();

            }
            else
            {

                AlertDialog.Builder inValid = new AlertDialog.Builder(this);
                inValid.SetTitle("invalid");
                inValid.SetMessage("Your Code is incorrect");
                inValid.SetCancelable(true);
                inValid.SetNegativeButton("Ok", delegate { SetContentView(Resource.Layout.Main); });
                inValid.Show();

            }


        }
    }
}

