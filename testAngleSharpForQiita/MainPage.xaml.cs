using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;

namespace testAngleSharpForQiita
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {

            //Cookieを有効化
            var config = Configuration.Default.WithCookies().WithDefaultLoader();
            var context = BrowsingContext.New(config);
            //URLを取得
            await context.OpenAsync("https://qiita.com/login");

            //ログイン前のURL表示
            Console.WriteLine(context.Active.Location);

            //submit
            var document = await context.Active.Forms[0].SubmitAsync(new
            {
                identity = "Qiitaのユーザー名またはメールアドレス",
                password = "Qiitaのパスワード"
            });

            //ログイン後のURL表示
            Console.WriteLine(context.Active.Location);

            //通知件数取得
            var notifications = document.QuerySelector(".st-Header_notifications");

            Console.WriteLine(notifications.InnerHtml);
        }
    }
}
