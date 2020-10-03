﻿using Acr.UserDialogs;
using DBTest.Models;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using KeertanPothi.model;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KeertanPothi
{
    public class Util
    {
        #region Theme

        internal static string DarkThemeBgColor = "#202124"; /** Main background color Black **/
        internal static string GrayThemeBgColor = "#FEFEFE"; /** Main background color Gray **/
        internal static string DarkThemeFontColor = "#eef0f1";
        internal static string DarkThemePunjabiTranlationColor = "#aad0ef";
        internal static string DarkThemeEnglishTranlationColor = "#f7f2d0";
        internal static string DarkThemeEnglishTransliterationColor = "#eae1f7";
        internal static string DarkThemeSelectedItemBg = "#545a66";

        internal static string LightThemeBgColor = "#cfe2f3"; /** Main background color blue **/
        internal static string LightThemeFontColor = "#00008B";
        internal static string LightThemePunjabiTranlationColor = "#255fbc";
        internal static string LightThemeEnglishTranlationColor = "#2b2a27"; //292825
        internal static string LightThemeEnglishTransliterationColor = "#481b96";
        internal static string LightThemeSelectedItemBg = "#aad0ef";

        #endregion

        #region Magic strings

        internal static string NewLineChar = "&#10;";
        internal static string PunjabiFontKey = "PunjabiFont";
        internal static string HandFontKey = "HandFont";
        internal static string SampleHTMLXaml = "This is &lt;strong style=&quot;color:red&quot;&gt;HTML&lt;/strong&gt; text.";
        internal static string SampleHTMLCS = "This is <strong style=\"color:red\">HTML</strong> text.";
        internal static string MainVishraamColor = "#f97b4d";
        internal static string SecondVishraamColor = "#1f991f";
        internal static string VishraamSource = "sttm";
        internal static string VishraamSource2 = "igurbani";
        internal static string VishraamSource3 = "sttm2";
        internal static string PrefDataExistsKey = "DataExists";
        internal static string PrefCurrentDbVersionKey = "CurrentDbVersion";
        internal static int CurrentDbVersion = 13;
        internal static string ListSelectionColor = "#9dcaf2"; 

        public enum SettingName
        {
            GurmukhiVisible,
            GurmukhiFontSize,
            EngTranslationVisible,
            EngTranslationFontSize,
            PunTranslationVisible,
            PunTranslationFontSize,
            TransliterationVisible,
            TransliterationFontSize,
            FullScreen,
            NormalFont,
            HandFont,
            GeneralChange,
            Ladivaar,
            Vishraam,
            Theme
        }

        #endregion

        #region keys
        /****** Keys *******/
        private static string PrefGurmukhiVisibleKey = "GurmukhiVisible";
        private static string PrefGurmukhiFontSizeKey = "GurmukhiFontSize";
        private static string PrefGurmukhiFontNameKey = "GurmukhiFontName";

        private static string PrefEnglishVisibleKey = "EnglishVisible";
        private static string PrefEnglishFontSizeKey = "EnglishFontSize";

        private static string PrefPunjabiVisibleKey = "PunjabiVisible";
        private static string PrefPunjabiFontSizeKey = "PunjabiFontSize";

        private static string PrefTransliterationVisibleKey = "TransliterationVisible";
        private static string PrefTransliterationFontSizeKey = "TransliterationFontSize";

        private static string PrefShabadListFontSizeKey = "ShabadListFontSize";

        private static string PrefDarkThemeKey = "PrefDarkTheme";


        /****** Init Values ******/
        private static bool PrefGurmukhiVisibleInit = true;
        private static int PrefGurmukhiFontSizeInit = 28;
        private static string PrefGurmukhiFontNameInit = "PunjabiFont";

        private static bool PrefEnglishVisibleInit = false;
        private static int PrefEnglishFontSizeInit = 16;

        private static bool PrefPunjabiVisibleInit = true;
        private static int PrefPunjabiFontSizeInit = 18;

        private static bool PrefTransliterationVisibleInit = false;
        private static int PrefTransliterationFontSizeInit = 18;


        private static int PrefShabadListFontSizeInit = 26;

        private static string PrefDarkThemeInit = "BLACK";


        #endregion

        #region Prefrences

        public static bool PrefGurmukhiVisible
        {
            get
            {
                return Preferences.Get(Util.PrefGurmukhiVisibleKey, true);
            }
            set
            {
                Preferences.Set(Util.PrefGurmukhiVisibleKey, value);
            }
        }
        public static int PrefGurmukhiFontSize
        {
            get
            {
                return Preferences.Get(Util.PrefGurmukhiFontSizeKey, 24);
            }
            set
            {
                Preferences.Set(Util.PrefGurmukhiFontSizeKey, value);
            }
        }
        public static string PrefGurmukhiFontName
        {
            get
            {
                return Preferences.Get(Util.PrefGurmukhiFontNameKey, PrefGurmukhiFontNameInit);
            }
            set
            {
                Preferences.Set(Util.PrefGurmukhiFontNameKey, value);
            }
        }
        public static bool PrefEnglishVisible
        {
            get
            {
                return Preferences.Get(Util.PrefEnglishVisibleKey, true);
            }
            set
            {
                Preferences.Set(Util.PrefEnglishVisibleKey, value);
            }
        }
        public static bool PrefTransliterationVisible
        {
            get
            {
                return Preferences.Get(Util.PrefTransliterationVisibleKey, PrefTransliterationVisibleInit);
            }
            set
            {
                Preferences.Set(Util.PrefTransliterationVisibleKey, value);
            }
        }

        public static int PrefTransliterationFontSize
        {
            get
            {
                return Preferences.Get(Util.PrefTransliterationFontSizeKey, PrefTransliterationFontSizeInit);
            }
            set
            {
                Preferences.Set(Util.PrefGurmukhiFontSizeKey, value);
            }
        }

        public static int PrefEnglishFontSize
        {
            get
            {
                return Preferences.Get(Util.PrefEnglishFontSizeKey, 14);
            }
            set
            {
                Preferences.Set(Util.PrefEnglishFontSizeKey, value);
            }
        }

        internal static void SetThemeOnPage(Page page)
        {
            switch (PrefDarkTheme)
            {
                case "BLACK":
                    page.BackgroundColor = Color.FromHex(Util.DarkThemeBgColor);
                    break;
                case "GRAY":
                    page.BackgroundColor = Color.FromHex(Util.GrayThemeBgColor);
                    break;
                case "BLUE":
                    page.BackgroundColor = Color.FromHex(Util.LightThemeBgColor);
                    break;
            }
        }

        public static bool PrefPunjabiVisible
        {
            get
            {
                return Preferences.Get(Util.PrefPunjabiVisibleKey, true);
            }
            set
            {
                Preferences.Set(Util.PrefPunjabiVisibleKey, value);
            }
        }
        public static int PrefPunjabiFontSize
        {
            get
            {
                return Preferences.Get(Util.PrefPunjabiFontSizeKey, PrefPunjabiFontSizeInit);
            }
            set
            {
                Preferences.Set(Util.PrefPunjabiFontSizeKey, value);
            }
        }

        public static int PrefShabadListFontSize
        {
            get
            {
                return Preferences.Get(Util.PrefShabadListFontSizeKey, PrefShabadListFontSizeInit);
            }
        }


        private static string prefShowVishramKey = "PrefShowVishraam";
        private static bool prefShowVishramInit = true;
        public static bool PrefShowVishraam
        {
            get
            {
                return Preferences.Get(Util.prefShowVishramKey, prefShowVishramInit);
            }
            set
            {
                Preferences.Set(Util.prefShowVishramKey, value);
            }
        }

        private static string prefShowLadivaarKey = "PrefShowLadivaar";
        private static bool prefShowLadivaarInit = false;
        public static bool PrefShowLadivaar
        {
            get
            {
                return Preferences.Get(Util.prefShowLadivaarKey, prefShowLadivaarInit);
            }
            set
            {
                Preferences.Set(Util.prefShowLadivaarKey, value);
            }
        }

        private static string PrefIsAdminKey = "IsAdmin";
        private static bool PrefIsAdminInit = false;
        internal static readonly string AutoSaveFileName = "AutoSave.json";
        public static readonly string LogFileName = "KeertanPothi.log";

        public static bool PrefIsAdmin
        {
            get
            {
                return Preferences.Get(Util.PrefIsAdminKey, PrefIsAdminInit);
            }
            set
            {
                Preferences.Set(Util.PrefIsAdminKey, value);
            }
        }

        internal static int PrefCurrentDbVersion
        {
            get { return Preferences.Get(Util.PrefCurrentDbVersionKey, 1); }
            set { Preferences.Set(Util.PrefCurrentDbVersionKey, value); }
        }

        public static string PrefDarkTheme
        {
            get
            {
                return Preferences.Get(Util.PrefDarkThemeKey, string.Empty);
            }
            set
            {
                Preferences.Set(Util.PrefDarkThemeKey, value);
            }
        }

        #endregion

        #region StaticLists
        public static List<SideMenu> GetStaticMenu()
        {
            bool isAdmin = PrefIsAdmin;
            List<SideMenu> menu = new List<SideMenu>();
            menu.Add(new SideMenu("Search", "Search", true, "search.png", true));
            menu.Add(new SideMenu("Random Shabad", "ShabadDetails", true, "Random.png", true));
            menu.Add(new SideMenu("Keertan Pothi", "KirtanPothiList", true, "Book.png", true));
            menu.Add(new SideMenu("Sundar Gutka", "NitnemList", true, "Nitnem.png", true));
            //menu.Add(new SideMenu("Shabad Set", "CreateSet", true, "KeertanPothi.images.search.png"));
            menu.Add(new SideMenu("Writer List", "WriterList", true, "writer.png", true));
            menu.Add(new SideMenu("Raag List", "RaagList", true, "Raag.png", true));
            //menu.Add(new SideMenu("Ang", "AngSearch", true, "images/Number.png", true));
            menu.Add(new SideMenu("History", "History", true, "History.png", true));
            menu.Add(new SideMenu("Help/Contact", "ContactUs", true, "help.png", true));

            //menu.Add(new SideMenu("Test", "TestPage", true, "KeertanPothi.images.search.png", true));
            //menu.Add(new SideMenu("SQL", "SqlView", true, "KeertanPothi.images.search.png", isAdmin));
            //menu.Add(new SideMenu("Admin", "Admin", true, "KeertanPothi.images.search.png", isAdmin));
            return menu;
        }
        //public static List<NitnemBani> GetNitnemBanis()
        //{
        //List<NitnemBani> menu = new List<NitnemBani>();
        //menu.Add(new NitnemBani(1, "jpjI swihb", "Japji Sahib", true, "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32,33,34,35,36,37,38,39"));
        //menu.Add(new NitnemBani(2, "jwpu swihb", "Jaap Sahib", true, "7402,7403,7404,7405,7406,7407,7408,7409,7410,7411,7412,7413,7414,7415,7416,7417,7418,7419,7420,7421,7422,7423"));
        //menu.Add(new NitnemBani(3, "qÍ pRswid ] sv`Xy ]", "Tav Prasad  Savayiye", true, "7426"));
        //menu.Add(new NitnemBani(4, "bynqI cOpeI", "Benti Chaupai", true, "12794,12795"));
        //menu.Add(new NitnemBani(5, "Anµdu swihb", "Anand Sahib", true, "333375,3375,333376"));
        //menu.Add(new NitnemBani(6, "rhrwis swihb", "Rehraas Sahib", false, "1721, 40, 41, 42, 43, 44, 45, 46, 47, 48, 12794, 12795, 8095, 8096, 333375, 333376,5538, 5539, 1943,1944"));
        //menu.Add(new NitnemBani(7, "soihlw swihb", "Sohila Sahib", false, "49,50,51,52,53"));
        //menu.Add(new NitnemBani(8, "suKmnI swihb", "Sukhmani Sahib", false, "871,1086"));
        //return menu;
        //SukhmaniSahib ; 
        //}

        #endregion

        #region Methods

        internal static string ReplacePunjabiUnicode(string search)
        {
            return search.Replace('ੳ', 'a').Replace('ਅ', 'A').Replace('ੲ', 'e').Replace('ਸ', 's').Replace('ਹ', 'h')
                    .Replace('ਕ', 'k').Replace('ਖ', 'K').Replace('ਗ', 'g').Replace('ਘ', 'G')//.Replace('ਙ', '')
                    .Replace('ਚ', 'c').Replace('ਛ', 'C').Replace('ਜ', 'j').Replace('ਝ', 'J')//.Replace('ਞ', '')
                    .Replace('ਟ', 't').Replace('ਠ', 'T').Replace('ਡ', 'f').Replace('ਢ', 'd').Replace('ਣ', 'x')
                    .Replace('ਤ', 't').Replace('ਥ', 'q').Replace('ਦ', 'd').Replace('ਧ', 'D').Replace('ਨ', 'n')
                    .Replace('ਪ', 'p').Replace('ਫ', 'P').Replace('ਬ', 'b').Replace('ਭ', 'B').Replace('ਮ', 'm')
                    .Replace('ਯ', 'X').Replace('ਰ', 'r').Replace('ਲ', 'l').Replace('ਵ', 'v').Replace('ੜ', 'V').Replace('ਸ਼', 'S');

        }

        internal static void SetInitPreferences()
        {
            Preferences.Set(Util.PrefGurmukhiFontSizeKey, Util.PrefGurmukhiFontSizeInit);
            Preferences.Set(Util.PrefEnglishFontSizeKey, Util.PrefEnglishFontSizeInit);
            Preferences.Set(Util.PrefPunjabiFontSizeKey, Util.PrefPunjabiFontSizeInit);

            Preferences.Set(Util.PrefGurmukhiVisibleKey, Util.PrefGurmukhiVisibleInit);
            Preferences.Set(Util.PrefEnglishVisibleKey, Util.PrefEnglishVisibleInit);
            Preferences.Set(Util.PrefPunjabiVisibleKey, Util.PrefPunjabiVisibleInit);

            Preferences.Set(Util.PrefShabadListFontSizeKey, Util.PrefShabadListFontSizeInit);
            Preferences.Set(Util.prefShowVishramKey, Util.prefShowVishramInit);
            Preferences.Set(Util.prefShowLadivaarKey, Util.prefShowLadivaarInit);
            Preferences.Set(Util.PrefIsAdminKey, Util.PrefIsAdminInit);
            Preferences.Set(Util.PrefGurmukhiFontNameKey, Util.PrefGurmukhiFontNameInit);
            Preferences.Set(Util.PrefCurrentDbVersionKey, CurrentDbVersion);
            Preferences.Set(Util.PrefDarkThemeKey, Util.PrefDarkThemeInit);
        }

        public static void ShowRoast(string msg)
        {
            DependencyService.Get<IMessage>().LongAlert(msg);
        }

        internal async static void ToggleToolbar(StackLayout layout, bool isVisible, uint duration)
        {
            if (!isVisible)
            {
                await layout.TranslateTo(0, 50, duration, Easing.SinOut);
                await Task.Delay(Convert.ToInt32(duration));
                layout.IsVisible = false;
            }
            else
            {
                layout.IsVisible = true;
                await layout.TranslateTo(0, 0, duration, Easing.SinOut);
            }
        }

        public static void CopyDBFile()
        {
            var fileName = "banidbC.zip";
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentPath, fileName);
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
            Stream stream = assembly.GetManifestResourceStream("KeertanPothi.DB.banidb.zip");

            if (!File.Exists(path))
            {
                using (BinaryReader br = new BinaryReader(stream))
                {
                    using (BinaryWriter bw = new BinaryWriter(new FileStream(path, FileMode.Create)))
                    {
                        byte[] buffer = new byte[2048];
                        int len = 0;
                        while ((len = br.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            bw.Write(buffer, 0, len);
                        }
                    }
                }
            }
            try
            {
                ExtractZipFile(path, string.Empty, documentPath);
                Preferences.Set(Util.PrefDataExistsKey, true);
                File.Delete(path);
            }
            catch (Exception ex)
            {
                ShowRoast("Error copying gurbani database file");
            }
        }

        public static void ExtractZipFile(string archivePath, string password, string outFolder)
        {
            using (Stream fsInput = File.OpenRead(archivePath))
            using (ZipFile zf = new ZipFile(fsInput))
            {
                foreach (ZipEntry zipEntry in zf)
                {
                    if (!zipEntry.IsFile)
                    {
                        continue;
                    }
                    String entryFileName = zipEntry.Name;
                    var fullZipToPath = Path.Combine(outFolder, entryFileName);
                    var directoryName = Path.GetDirectoryName(fullZipToPath);
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    var buffer = new byte[4096];

                    using (var zipStream = zf.GetInputStream(zipEntry))
                    using (Stream fsOutput = File.Create(fullZipToPath))
                    {
                        StreamUtils.Copy(zipStream, fsOutput, buffer);
                    }
                }
            }
        }

        public async static void ShareFile(string json, string fileName, string title)
        {
            var fn = fileName; // "Pothi.json";
            var file = Path.Combine(FileSystem.CacheDirectory, fn);
            File.WriteAllText(file, json);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = title,
                File = new ShareFile(file)
            });//"Share Pothi"
        }

        public async static void ShareText(string text)
        {
            await Share.RequestAsync(text, "Shared from Keertan Pothi");
        }

        public static void Log(string text)
        {
            string append = "------------------------------\r\n";
            append += DateTime.Now + "\r\n";
            SaveFile(LogFileName, append+text, true);
        }

        public async static Task<string> GetPothiJson(Pothi pothi)
        {
            if (pothi == null || pothi.PothiId == null || pothi.PothiId < 1)
                return string.Empty;
            else
            {
                SQLiteAsyncConnection _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
                List<PothiShabad> ps = await _con.QueryAsync<PothiShabad>($"Select * from PothiShabad where PothiId = {pothi.PothiId}");
                PothiShabadExt ext = new PothiShabadExt() { pothi = pothi, shabadList = ps };
                string json = JsonConvert.SerializeObject(ext);
                return json;
            }
        }

        public static void SaveFile(string fileName, string json, bool autoSave)
        {
            DependencyService.Get<ISaveFile>().SaveFile(fileName, json, autoSave);
        }

        public static void AutoLoad()
        {
            try
            {
                DependencyService.Get<ISaveFile>().ReadFile(true);
            }
            catch
            {
                ShowRoast("Failed to read file");
            }
        }

        public static List<string> ReadFile()
        {
            try
            {
                return DependencyService.Get<ISaveFile>().ReadFile(false);
            }
            catch
            {
                ShowRoast("Failed to read file");
                return null;
            }
        }
    }

    #endregion

    public class Queries
    {
        public static SQLiteAsyncConnection _con;
        public static string SearchQ(string searchString)
        {
            return $@"
				select vr.ID VerseID, trim(vr.Gurmukhi, 15) Gurmukhi, vr.English, vr.WriterID, vr.RaagID, wr.WriterEnglish, wr.WriterGurmukhi, rg.RaagEnglish, vr.PageNo
				from shabad sh 
				inner join verse vr on sh.verseID = vr.ID 
				left join Raag rg on vr.raagID = rg.RaagID 
				left join Writer wr on vr.WriterID = wr.writerID 
				where vr.FirstLetterStr like '%{searchString}%'
				 order by vr.id";
        }
        public static string MainLetterSearch(string searchString)
        {
            return $@"
				select vr.ID VerseID, trim(vr.Gurmukhi, 15) Gurmukhi, vr.English, vr.WriterID, vr.RaagID, wr.WriterEnglish, wr.WriterGurmukhi, rg.RaagEnglish, vr.PageNo
				from shabad sh 
				inner join verse vr on sh.verseID = vr.ID 
				left join Raag rg on vr.raagID = rg.RaagID 
				left join Writer wr on vr.WriterID = wr.writerID 
				where vr.MainLetters like '%{searchString}%'
				 order by vr.id";
        }
        public static string ExactSearch(string searchString)
        {
            return $@"
				select vr.ID VerseID, trim(vr.Gurmukhi, 15) Gurmukhi, vr.English, vr.WriterID, vr.RaagID, wr.WriterEnglish, wr.WriterGurmukhi, rg.RaagEnglish, vr.PageNo
				from shabad sh 
				inner join verse vr on sh.verseID = vr.ID 
				left join Raag rg on vr.raagID = rg.RaagID 
				left join Writer wr on vr.WriterID = wr.writerID 
				where vr.Gurmukhi like '%{searchString}%'
				 order by vr.id";
        }

        public static string ShabadById(int shabadId)
        {
            //case when id = 7001 then '#abc123' else '#ffffff' end bgcolor ,
            return $@"select wr.writerenglish, rg.raagenglish, sc.sourceenglish, sh.shabadid,
                    REPLACE(vr.gurmukhi,'<>','&lt;&gt;') gurmukhiHtml,  vr.* 
                    from shabad sh join verse vr on sh.verseID = vr.ID 
                    left join Writer wr on vr.writerId = wr.writerId
                    left join Raag rg on vr.RaagId = rg.RaagId
                    left join Source sc on vr.SourceId = sc.SourceId
                    Where sh.shabadID = {shabadId} order by vr.id";
        }

        public static string NitnemBaniShabads(int id)
        {
            return $@"select wr.writerenglish, rg.raagenglish, sc.sourceenglish, sh.shabadid, 
                    REPLACE(vr.gurmukhi,'<>','&lt;&gt;') gurmukhiHtml,  vr.* 
                    from shabad sh join verse vr on sh.verseID = vr.ID 
                    left join Writer wr on vr.writerId = wr.writerId
                    left join Raag rg on vr.RaagId = rg.RaagId
                    left join Source sc on vr.SourceId = sc.SourceId
                    inner join Nitnem n on n.verseid = vr.id
                    Where n.id = {id} order by n.sort, n.verseid";
        }

        public static string GetAllBanis(bool showAll)
        {
            string str = "select * from BaniName ";
            if (!showAll)
                str += "Where IsVisible = true;";

            return str;
        }

        public static string GetNextPrevBanis(int baniId, bool next)
        {
            string sign = next ? ">" : "<";
            string desc = string.Empty;
            if (!next)
                desc = "desc";
            string str = $"SELECT * FROM BaniName where id {sign} {baniId} and isvisible = true ORDER by id {desc} limit 1";
            return str;
        }

        public static string VerseByAng(int angNo, string sourceId = "G")
        {
            return $@"select wr.writerenglish, rg.raagenglish, sc.sourceenglish, sh.shabadid, 
                    REPLACE(vr.gurmukhi,'<>','&lt;&gt;') gurmukhiHtml,  vr.* 
                    from shabad sh join verse vr on sh.verseID = vr.ID 
                    left join Writer wr on vr.writerId = wr.writerId
                    left join Raag rg on vr.RaagId = rg.RaagId
                    left join Source sc on vr.SourceId = sc.SourceId  
                    Where vr.PageNo = {angNo} and vr.SourceId = '{sourceId}'  
                    order by vr.id";
        }

        public static string GetAllWriters()
        {
            return $"select * from Writer where WriterID > 0;";
        }

        public static string GetWriteInfoByid(int? writerId)
        {
            return $"select * from WriterInfo where WriterID = {writerId}";
        }

        public static string ShabadByWriterId(int? writerId)
        {
            return $@"
				select vr.ID VerseID, trim(vr.Gurmukhi, 15) Gurmukhi, vr.English, vr.WriterID, vr.RaagID, wr.WriterEnglish, wr.WriterGurmukhi, rg.RaagEnglish, vr.PageNo, sh.ShabadId
				from shabad sh 
				inner join verse vr on sh.verseID = vr.ID 
				inner join Raag rg on vr.raagID = rg.RaagID 
				inner join Writer wr on vr.WriterID = wr.writerID 
				where vr.writerId = {writerId}
				GROUP BY sh.shabadid
				HAVING MIN(vr.ID)
				ORDER BY vr.ID";
        }

        public static string GetAllRaags()
        {
            return $"select 'Ang ' || substr(r.raagwithpage, instr(r.raagwithpage,'('), instr(r.raagwithpage,')') - instr(r.raagwithpage,'(') + 1) PageNos, R.* from Raag R where RaagID > 0 and raagwithpage is not null;";
        }

        public static string ShabadByRaagId(int raagId)
        {
            return $@"
				select vr.ID VerseID, trim(vr.Gurmukhi, 15) Gurmukhi, vr.English, vr.WriterID, vr.RaagID, wr.WriterEnglish, wr.WriterGurmukhi, rg.RaagEnglish, vr.PageNo, sh.ShabadId
				from shabad sh 
				inner join verse vr on sh.verseID = vr.ID 
				inner join Raag rg on vr.raagID = rg.RaagID 
				inner join Writer wr on vr.WriterID = wr.writerID 
				where vr.raagID = {raagId}
				GROUP BY sh.shabadid
				HAVING MIN(vr.ID)
				ORDER BY vr.ID";
        }

        internal static string GetAllPothis()
        {
            return @"SELECT P.POTHIID, P.name, P.createdon, COUNT(PS.shabadid) ShabadCount
	                 FROM POTHI P
                     Left JOIN PothiShabad PS ON P.POTHIID = PS.PothiId  
                     GROUP BY P.POTHIID, P.name, P.createdon";
        }

        internal static string GetAllPothisWithSortOrder()
        {
            return @"SELECT P.POTHIID, P.name, Max(PS.SortOrder) MaxSortOrder
	                 FROM POTHI P
                     Left JOIN PothiShabad PS ON P.POTHIID = PS.PothiId  
                     GROUP BY P.POTHIID, P.name";
        }

        internal static string ShabadByPothiId(int pothiId)
        {
            return $@"select ps.VerseID, Gurmukhi, vr.English, vr.WriterID, vr.RaagID, wr.WriterEnglish, wr.WriterGurmukhi, rg.RaagEnglish, vr.PageNo, sh.ShabadId, ps.SortOrder, ps.Notes
				from Pothi P 
                left JOIN PothiShabad PS ON P.POTHIID = PS.PothiId
                left JOIN shabad sh ON PS.shabadid = SH.ShabadID
				left JOIN verse vr on sh.verseID = vr.ID 
				left JOIN Raag rg on vr.raagID = rg.RaagID 
				left JOIN Writer wr on vr.WriterID = wr.writerID 
				where P.POTHIID = {pothiId}
                and (ps.VerseId is null or sh.verseid = ps.VerseId)
                order by ps.sortorder";
        }

        internal static string BookmarksByNitnemId(int baniId)
        {
            return $@"select v.Id VerseID, case when b.text is not null then b.text else v.gurmukhi end Gurmukhi 
                        from BaniBookmark b inner join verse v on b.verseid = v.id 
                        where b.baniid = {baniId} order by b.sort ";
        }

        internal static string DeletePothi(int pothiId)
        {
            return $@"Delete from Pothi where pothiId = {pothiId};";
        }

        internal static string DeletePothiShabad(int pothiId, int shabadId)
        {
            return $@"Delete from PothiShabad where pothiId = {pothiId} and shabadId = {shabadId};";
        }

        public static string NitnemBani(string shabadIdList)
        {
            string[] ids = shabadIdList.Split(',');
            string orClause = string.Empty;
            foreach (string s in ids)
            {
                orClause += $" shabadid = {Convert.ToInt32(s)} or";
            }
            orClause = orClause.Substring(0, orClause.Length - 2);
            return $"select vr.gurmukhi as GurmukhiHtml, vr.*, sh.* from shabad sh join verse vr on sh.verseID = vr.ID Where ({orClause}) order by vr.id";
        }

        public static string MovePothiShabads(int oldPothiId, int newPothiId, int shabadId)
        {
            return $"UPDATE PothiShabad Set PothiId = {newPothiId} Where PothiId = {oldPothiId} and ShabadId = {shabadId}";
        }

        public static string DeletePothiShabad(int pothiId)
        {
            return $"Delete from PothiShabad where pothiId = {pothiId}";
        }

        public async static Task<bool> DeleteMovedShabads(List<PothiShabad> selected)
        {
            foreach (PothiShabad p in selected)
            {
                string query = DeletePothiShabad(p.PothiId, p.ShabadId);
                try
                {
                    await _con.ExecuteAsync(query);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }

        public async static Task<int?> GetMaxSortId(int pothiId)
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            string str = $"Select max(SortOrder) From PothiShabad where pothiId = {pothiId}";
            int? maxId = await _con.ExecuteScalarAsync<int?>(str);
            return maxId;
        }

        internal async static void SaveShabadToHistory(int shabadId, int? verseId)
        {
            try
            {
                _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
                History history = new History(shabadId, verseId);
                _ = await _con.InsertAsync(history);
            }
            catch (Exception ex)
            {
                //Util.ShowRoast("Save history error: " + ex.Message);
            }
        }

        internal async static Task<int> GetLastId()
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            string sql = @"select last_insert_rowid()";
            int lastId = await _con.ExecuteScalarAsync<int>(sql);
            return lastId;
        }

        internal static string GetShabadHistory(int count = 50)
        {
            return $@"Select * from History h 
	                    inner join shabad s on case when h.verseid is not null then h.verseid = s.verseid else h.ShabadId = s.shabadid end
                        inner join verse vr on s.verseid = vr.ID
                        left JOIN Raag rg on vr.raagID = rg.RaagID 
				        left JOIN Writer wr on vr.WriterID = wr.writerID 
                        WHERE vr.writerid is not null
                        GROUP BY s.shabadid
                        HAVING MIN(vr.ID)
                    order by h.CreatedOn Desc Limit { count }";
        }

        public async static Task<bool> ExportPothis(List<Pothi> PothiObs = null, bool autoSave = false)
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            if (PothiObs == null)
                PothiObs = await _con.QueryAsync<Pothi>(Queries.GetAllPothis());
            if ((PothiObs != null && PothiObs.Count > 0) || autoSave)
            {
                string folder = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string path = System.IO.Path.Combine(folder, "Pothis.json");
                try
                {
                    List<PothiShabadExt> pothis = new List<PothiShabadExt>();
                    foreach (Pothi pothi in PothiObs)
                    {
                        List<PothiShabad> ps = await _con.QueryAsync<PothiShabad>($"Select * from PothiShabad where PothiId = {pothi.PothiId}");
                        PothiShabadExt ext = new PothiShabadExt() { pothi = pothi, shabadList = ps };
                        pothis.Add(ext);
                    }
                    string json = JsonConvert.SerializeObject(pothis);
                    if(autoSave)
                        Util.SaveFile(Util.AutoSaveFileName, json, true);
                    else
                        Util.SaveFile("Pothis.json", json, false);
                    //Util.ShowRoast("Pothi(s) exported successfully to Internal Storage/KeertanPothi/Pothis.json.");
                    return true;
                }
                catch (Exception exception)
                {
                    //Util.ShowRoast("Unable to export pothis");
                    return false;
                }
            }
            else
                return false;
        }

        public async static Task<bool> ImportPothi()
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            List<string> jsons = Util.ReadFile();

            if (jsons != null && jsons.Count > 0)
            {
                using (UserDialogs.Instance.Loading("Importing pothi(s)..."))
                {
                    await Task.Delay(100);
                    List<PothiShabadExt> pothis = null;
                    foreach (string js in jsons)
                    {
                        pothis = JsonConvert.DeserializeObject<List<PothiShabadExt>>(js);

                        if (pothis.Count > 0)
                        {
                            foreach (PothiShabadExt cur in pothis)
                            {
                                await SavePothFromPothiShabadExt(cur);
                            }
                        }
                    }
                }
            }
            return true;
        }

        internal async static Task<bool> SavePothFromPothiShabadExt(PothiShabadExt cur)
        {
            _con = DependencyService.Get<ISqliteDb>().GetSQLiteConnection();
            Pothi pothi = new Pothi();
            pothi.Name = cur.pothi.Name;
            pothi.CreatedOn = DateTime.Now;
            int cnt = await _con.InsertAsync(pothi);
            if (cnt > 0)
            {
                int pothiId = await Queries.GetLastId();
                foreach (PothiShabad shabad in cur.shabadList)
                {
                    PothiShabad pothiShabad = shabad;
                    pothiShabad.PothiId = pothiId;
                    await _con.InsertAsync(pothiShabad);
                }
            }
            return true;
        }

        internal static string ToggleNitnemBani(int? Id, bool visible)
        {
            string str = $"Update BaniName Set IsVisible = { visible } ";
            if (Id != null && Id > 0)
                str += $"Where Id = { Id }";

            return str;
        }

        internal static string UpdateSortOrder(int pothiId, int shabadId, int sortOrder)
        {
            return $"Update PothiShabad Set SortOrder = {sortOrder} Where PothiId = {pothiId} and ShabadId = {shabadId}";
        }

        internal static string UpdateNotes(int pothiId, int shabadId, string note)
        {
            return $"Update PothiShabad Set Notes = '{note}' Where PothiId = {pothiId} and ShabadId = {shabadId}";
        }

        internal static string GetNotes(int pothiId, int shabadId)
        {
            return $"Select notes from PothiShabad Where PothiId = {pothiId} and ShabadId = {shabadId}";
        }

    }

    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
