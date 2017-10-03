using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace i18n.Controllers
{
    public partial class InternationalController : Controller
    {
        // This method is never called, but is used to make xgettext/PoEdit put these strings into the .po file so they can be translated
        // Typically, these are strings that are used in html attributes where xgettext/PoEdit can't parse them correctly or where 
        // the original string value is "hidden" in a variable

        // HTML example (xgettext/POEdit won't parse this):
        // <input type="submit" value="@T._("Save changes")" class="btn btn-success" /> 
        //
        // "Hidden" value example (xgettext/POEdit won't parse this):
        // var roles = _webSecurity.GetAllTranslatedRoles(t => _(t));
        private void ManualTranslate()
        {
            _("Save changes");
            _("Edit");
            _("Edit/View");
            // etc
            // examples of plural, specific and plural specific
            //_n("One", "More", 2);
            //_n("None", "One", 0);
            //_p("Menu|File", "Open");
            //_p("Menu|Print", "Open");
            //_("Open");
            //_pn("Menu|File", "Recent file", "Recent files", 0);
        }

        // call this from the frontend to use all the translations in js as well
        public virtual JsonResult GetLanguageDictionary(string languageCode = "")
        {
            var dct = T.Translations(languageCode);
            return Json(dct.ToDictionary(t => t.Key, t => t.Value.FirstOrDefault()), JsonRequestBehavior.AllowGet);
        }

        public string _(string text)
        {
            return T._(text);
        }

        public string _(string text, params object[] args)
        {
            return T._(text, args);
        }

        public IEnumerable<string> _(IEnumerable<string> items)
        {
            return T._(items);
        }

        public string _n(string text, string pluralText, long n)
        {
            return T._n(text, pluralText, n);
        }

        public string _n(string text, string pluralText, long n, params object[] args)
        {
            return T._n(text, pluralText, n, args);
        }

        public string _p(string context, string text)
        {
            return T._p(context, text);
        }

        public string _p(string context, string text, params object[] args)
        {
            return T._p(context, text, args);
        }

        public string _pn(string context, string text, string pluralText, long n)
        {
            return T._pn(context, text, pluralText, n);
        }

        public string _pn(string context, string text, string pluralText, long n, params object[] args)
        {
            return T._pn(context, text, pluralText, n, args);
        }

    }

}