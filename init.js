// require('jQuery');
// require('jsgettext');
// typical use for loading translation into jsIn(jsgettext)
$.ajax({
    url: "/Home/GetLanguageDictionary",
    async: false,
    dataType: "json",
    success: function(d) {
        jsIn.addDict(d);
    }
});

// now you can translate in js using the same syntax as in C#:
var s = _("Save changes");
var t = _("Today is {0}", [new Date()]);