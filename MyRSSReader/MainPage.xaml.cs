using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Syndication;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace MyRSSReader
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            //缓存大小,先这样吧
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //用了书上的回调
            if (rssURL.Text != "")
            {
                RssRequest.GetRssItems(rssURL.Text,
                   async (items) =>
                   {
                       await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                       {
                           rsslistbox.ItemsSource = items;
                       });
                   },
                   async (exception) =>
                   {
                       await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                       {
                           await new MessageDialog(exception).ShowAsync();
                       });

                   });
            }
            else
            {
                await new MessageDialog("请输入RSS地址").ShowAsync();
            }
        }

        private void rsslistbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (rsslistbox.SelectedItem == null)
                return;
            var template = (RssModel)rsslistbox.SelectedItem;
            Frame.Navigate(typeof(FullPage), template);
            rsslistbox.SelectedItem = null;
        }
    }
}