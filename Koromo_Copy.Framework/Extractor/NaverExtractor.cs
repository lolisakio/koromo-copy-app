﻿// This source code is a part of Koromo Copy Project.
// Copyright (C) 2019. dc-koromo. Licensed under the MIT Licence.

using HtmlAgilityPack;
using Koromo_Copy.Framework.Network;
using Koromo_Copy.Framework.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static Koromo_Copy.Framework.Extractor.IExtractorOption;

namespace Koromo_Copy.Framework.Extractor
{
    public class NaverExtractorOption : IExtractorOption
    {
    }

    public class NaverExtractor : ExtractorModel
    {
        public NaverExtractor()
        {
            HostName = new Regex(@"(comic|blog)\.naver\.com");
            ValidUrl = new Regex(@"^https?://(comic|blog)\.naver\.com/(webtoon|.*?)/((list|detail)\.nhn\?|.*)\??(titleId\=(\d+)\&no=(\d+))?(.*?)$");
        }
        
        public override IExtractorOption RecommendOption(string url)
        {
            var match = ValidUrl.Match(url).Groups;

            if (match[1].Value == "comic")
            {
                if (match[4].Value == "detail")
                {
                    return new NaverExtractorOption { Type = NaverExtractorOption.ExtractorType.EpisodeImages };
                }
                else if (match[4].Value == "list")
                {
                    return new NaverExtractorOption { Type = NaverExtractorOption.ExtractorType.ComicIndex };
                }
            }
            else if (match[1].Value == "blog")
            {
                return new NaverExtractorOption { Type = NaverExtractorOption.ExtractorType.Images };
            }

            return new NaverExtractorOption { Type = NaverExtractorOption.ExtractorType.Images };
        }

        public override string RecommendFormat(IExtractorOption option)
        {
            return "%(title)s/%(episode)s/%(file)s.%(ext)s";
        }

        public override (List<NetTask>, ExtractedInfo) Extract(string url, IExtractorOption option = null)
        {
            if (option == null)
                option = RecommendOption(url);

            //
            //  Extract Webtoon
            //

            if (option.Type == ExtractorType.EpisodeImages)
            {
                var html = NetTools.DownloadString(url);

                var document = new HtmlDocument();
                document.LoadHtml(html);
                var node = document.DocumentNode;

                var title = node.SelectSingleNode("/html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/h2[1]").MyText();
                var sub_title = node.SelectSingleNode("/html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[2]/div[1]/h3[1]").InnerText.Trim();
                var author = node.SelectSingleNode("/html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[1]/div[2]/h2[1]/span[1]").InnerText.Trim();

                var imgs = node.SelectNodes("/html[1]/body[1]/div[1]/div[2]/div[1]/div[1]/div[3]/div[1]/img");
                var result = new List<NetTask>();

                int count = 1;
                foreach (var img in imgs)
                {
                    var durl = img.GetAttributeValue("src", "");
                    var task = NetTask.MakeDefault(durl);
                    task.SaveFile = true;
                    task.Filename = durl.Split('/').Last();
                    task.Format = new ExtractorFileNameFormat
                    {
                        Title = title,
                        Episode = sub_title,
                        FilenameWithoutExtension = count++.ToString("000"),
                        Extension = Path.GetExtension(task.Filename).Replace(".", "")
                    };
                    result.Add(task);
                }

                return (result, null);
            }

            return (null, null);
        }
    }
}
