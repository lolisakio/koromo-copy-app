﻿// This source code is a part of Koromo Copy Project.
// Copyright (C) 2019. dc-koromo. Licensed under the MIT Licence.

using Koromo_Copy.Framework;
using Koromo_Copy.Framework.CL;
using Koromo_Copy.Framework.Log;
using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Koromo_Copy.Console
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            //args = new string[] { "net", "--download-html", "https://naver.com", /*"--disable-auto-redirection"*/ };
            //args = new string[] { "--test", "pixiv" };
            //args = new string[] { "https://e-hentai.org/g/1491793/45f9e85e48/", "--extract-info", "--print-process" };
            //args = new string[] { "https://e-hentai.org/g/1491793/45f9e85e48/",  };
            //args = new string[] { "https://gall.dcinside.com/board/view/?id=programming&no=1132197&page=1", "--print-process" };
            //args = new string[] { "https://hitomi.la/galleries/1344592.html", "--print-process" };
            //args = new string[] { "https://gall.dcinside.com/mgallery/board/lists?id=purikone_redive", "--gaenyum" };
            //args = new string[] { "https://www.instagram.com/parlovetati/" };
            //args = new string[] { "https://www.instagram.com/zennyrt/", "-p" };
            //args = new string[] { "https://www.instagram.com/haneul__haneul/", "-p" };
            //args = new string[] { "https://exhentai.org/g/1495578/5568045b16/", "-p" };
            //args = new string[] { "https://manamoa15.net/bbs/board.php?bo_table=manga&wr_id=1994867", "-p" };
            //args = new string[] { "https://manamoa15.net/bbs/page.php?hid=manga_detail&manga_id=11000", "-p" };
            //args = new string[] { "https://twitter.com/hashtag/%EB%A1%9C%EB%A6%AC", "-p" };
            //args = new string[] { "https://twitter.com/sCQ5fV6ocBfxyQS/media", "-p" };
            //args = new string[] { "https://twitter.com/priconne_redive", "-p" };
            //args = new string[] { "https://www.instagram.com/ravi.me/", "-p" };
            //args = new string[] { "https://xn--9w3b15m8vo.asia/reader/1496049#1-", "-p" };
            //args = new string[] { "https://danbooru.donmai.us/posts?tags=oryou", "-p", "--end-page", "3" };
            //args = new string[] { "https://funbe19.com/왕의-딸로-태어났다고-합니다", "-p"  };
            //args = new string[] { "https://www.pixiv.net/member.php?id=115051", "-p" };
            //args = new string[] { "https://comic.naver.com/webtoon/list.nhn?titleId=626907&weekday=wed" };
            //args = new string[] { "https://manamoa.net/bbs/page.php?hid=manga_detail&manga_id=10953" };
            //args = new string[] { "https://www.instagram.com/chuu__chloe/?hl=ko" };
            //args = new string[] { "https://jmana25.com/book/패러렐 파라다이스" };
            //args = new string[] { "https://hitomi.la/doujinshi/mofurefre-%ED%95%9C%EA%B5%AD%EC%96%B4-1542081.html" };
            //args = new string[] { "http://www.hn-hn.co.kr/shop/shopbrand.html?xcode=013&type=N&mcode=003" };
            args = new string[] { "https://attrangs.co.kr/shop/list.php?cate=2301" };
#endif

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            AppProvider.Initialize();

            Logs.Instance.AddLogErrorNotify((s, e) => {
                var tuple = s as Tuple<DateTime, string, bool>;
                CultureInfo en = new CultureInfo("en-US");
                System.Console.Error.WriteLine($"[{tuple.Item1.ToString(en)}] [Error] {tuple.Item2}");
            });

            try
            {
                Runnable.Start(args);
            }
            catch (Exception e)
            {
                System.Console.WriteLine("An error occured! " + e.Message);
                System.Console.WriteLine(e.StackTrace);
                System.Console.WriteLine("Please, check log.txt file.");
            }

            AppProvider.Deinitialize();

            Environment.Exit(0);
        }
    }
}
