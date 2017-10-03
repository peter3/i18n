using NGettext;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Web;
using System.Linq;
using System.Web.Mvc;

namespace i18n
{
    public interface IInternational
    {
        string _(string text);
        string _(string text, params object[] args);
        IEnumerable<string> _(IEnumerable<string> items);
        string _n(string text, string pluralText, long n);
        string _n(string text, string pluralText, long n, params object[] args);
        string _p(string context, string text);
        string _p(string context, string text, params object[] args);
        string _pn(string context, string text, string pluralText, long n);
        string _pn(string context, string text, string pluralText, long n, params object[] args);
    }
    // a class to wrap the ngettext catalog concept
    public static class T
    {
        // this path assumes your po/mo files are stored in your project in this structure:
        // <approot>/locale/<langCode>/LC_MESSAGES
        private static readonly ICatalog _Catalog = new Catalog("messages", Path.Combine(HttpRuntime.AppDomainAppPath, "locale"));

        public static Dictionary<string, string[]> Translations(string languageCode = "")
        {
            return string.IsNullOrWhiteSpace(languageCode) ? 
                ((Catalog)_Catalog).Translations :
                new Catalog("messages", Path.Combine(HttpRuntime.AppDomainAppPath, "locale"), new CultureInfo(languageCode)).Translations;
        }

        public static CultureInfo CultureInfo
        {
            get
            {
                return ((Catalog)_Catalog).CultureInfo;
            }
        }
        public static string GetString(string text)
        {
            return _Catalog.GetString(text);
        }

        public static string _(string text)
        {
            return _Catalog.GetString(text);
        }

        public static IEnumerable<string> GetString(IEnumerable<string> items)
        {
            foreach (var text in items)
                yield return _Catalog.GetString(text);
        }

        public static IEnumerable<string> _(IEnumerable<string> items)
        {
            foreach (var text in items)
                yield return _Catalog.GetString(text);
        }

        public static string GetString(string text, params object[] args)
        {
            return _Catalog.GetString(text, args);
        }

        public static string _(string text, params object[] args)
        {
            return _Catalog.GetString(text, args);
        }

        public static string GetPluralString(string text, string pluralText, long n)
        {
            return _Catalog.GetPluralString(text, pluralText, n);
        }

        public static string _n(string text, string pluralText, long n)
        {
            return _Catalog.GetPluralString(text, pluralText, n);
        }

        public static string GetPluralString(string text, string pluralText, long n, params object[] args)
        {
            return _Catalog.GetPluralString(text, pluralText, n, args);
        }

        public static string _n(string text, string pluralText, long n, params object[] args)
        {
            return _Catalog.GetPluralString(text, pluralText, n, args);
        }

        public static string GetParticularString(string context, string text)
        {
            return _Catalog.GetParticularString(context, text);
        }

        public static string _p(string context, string text)
        {
            return _Catalog.GetParticularString(context, text);
        }

        public static string GetParticularString(string context, string text, params object[] args)
        {
            return _Catalog.GetParticularString(context, text, args);
        }

        public static string _p(string context, string text, params object[] args)
        {
            return _Catalog.GetParticularString(context, text, args);
        }

        public static string GetParticularPluralString(string context, string text, string pluralText, long n)
        {
            return _Catalog.GetParticularPluralString(context, text, pluralText, n);
        }

        public static string _pn(string context, string text, string pluralText, long n)
        {
            return _Catalog.GetParticularPluralString(context, text, pluralText, n);
        }

        public static string GetParticularPluralString(string context, string text, string pluralText, long n, params object[] args)
        {
            return _Catalog.GetParticularPluralString(context, text, pluralText, n, args);
        }

        public static string _pn(string context, string text, string pluralText, long n, params object[] args)
        {
            return _Catalog.GetParticularPluralString(context, text, pluralText, n, args);
        }

        #region HTML helpers
        public static IHtmlString _<TModel>(this HtmlHelper<TModel> helper, string text)
        {
            return new HtmlString(_(text));
        }

        public static IHtmlString _<TModel>(this HtmlHelper<TModel> helper, string text, params object[] args)
        {
            return new HtmlString(_(text, args));
        }

        public static IEnumerable<IHtmlString> _<TModel>(this HtmlHelper<TModel> helper, IEnumerable<string> texts)
        {
            return _(texts).Select(t => new HtmlString(t));
        }
        #endregion
    }
}