﻿// This source code is a part of Koromo Copy Project.
// Copyright (C) 2019. dc-koromo. Licensed under the MIT Licence.

using Koromo_Copy.App.DataBase;
using Koromo_Copy.Framework.Extractor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Koromo_Copy.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DownloadInfoPage : ContentPage
    {
        public DownloadInfoPage(DownloadDBModel dbm)
        {
            InitializeComponent();

            if (!string.IsNullOrWhiteSpace(dbm.ShortInfo))
                Information.Text = dbm.ShortInfo;
            else
                Information.Text = dbm.Url;

            var model = ExtractorManager.Instance.GetExtractor(dbm.Url);
            if (model != null)
                Type.Text = model.GetType().Name.Replace("Extractor", "추출기");
            else
            {
                Type.Text = "찾을 수 없음";
                Type.TextColor = Color.Red;
            }

            Date.Text = dbm.StartsTime.ToString();

            switch (dbm.State)
            {
                case DownloadDBState.Aborted:
                    State.Text = "다운로드 취소됨";
                    State.TextColor = Color.Orange;
                    break;

                case DownloadDBState.Downloaded:
                    State.Text = "다운로드됨";
                    break;

                case DownloadDBState.ErrorOccured:
                    State.Text = "다운로드 중 오류가 발생함";
                    State.TextColor = Color.Red;
                    break;

                case DownloadDBState.Downloading:
                    State.Text = "다운로드 도중 중단됨";
                    State.TextColor = Color.Orange;
                    break;
            }

            Capacity.Text = $"{dbm.CountOfFiles}개 항목 [{DownloadElement.convert_bytes2string(dbm.SizeOfContents)}]";

            Thumbnail.Success += (s,e) =>
            {
                var h = e.ImageInformation.OriginalHeight;
                var w = e.ImageInformation.OriginalWidth;

                if (h < 200 && w < 200)
                {
                    ThumbnailFrame.HeightRequest = h;
                    ThumbnailFrame.WidthRequest = w;
                }
                else
                {
                    ThumbnailFrame.HeightRequest = 300;
                    ThumbnailFrame.WidthRequest = 300;
                }
            };

            if (!string.IsNullOrWhiteSpace(dbm.ThumbnailCahce))
            {
                Task.Run(() =>
                {
                    Thread.Sleep(100);
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var thumbnail = dbm.ThumbnailCahce;
                        Thumbnail.HeightRequest = Height;
                        Thumbnail.IsVisible = true;
                        Thumbnail.Source = thumbnail;

                        Background.HeightRequest = Height;
                        Background.WidthRequest = Width;
                        Background.IsVisible = true;
                        Background.Source = thumbnail;
                    });
                });
            }
        }
    }
}