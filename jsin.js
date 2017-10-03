﻿/*
 * Name:        jsIn
 * Version:     1.2
 * Description: A simple implementation of i18n(internationalization) in javascript
 * Author:      Nikolaz TANG (www.zomeoff.com)
 * Created:     27-JUL-2011
 * Language:    Javascript
 * License:     FREE software
 * Contact:     http://www.zomeoff.com/about
 * 
 * Copyright 2011 Nikolaz TANG, all rights reserved.
 *
 * This source file is free software. You can do what you like.
 * 
 * This source file is distributed in the hope that it will be useful, but 
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY 
 * or FITNESS FOR A PARTICULAR PURPOSE.
 * 
 * For details please refer to: http://www.zomeoff.com/jsin/
 *
 * ----------------------------------------------------------------------
 * changelog:
 * 1.2  [2017-09-01] : Changed replacement characters in format strings from {%0} to {0} to be compatible with .Net (peter)
 * 1.2  [2011-07-27] : Further optimized code for smaller size and better performance.
 * 1.1  [2011-07-19] : Use full sentence format in JSON attributes, the indexing key conversion is no longer needed. Performance is significantly improved.
 * 1.0  [2011-07-09] : First release.
 * ----------------------------------------------------------------------
 * 
 * examples of usage :
 * 1. load the dictionary / translation map
 *   
 *   -dict in object format   
 *   jsIn.addDict({
 *       "hello_world"      : "Hallo Welt!",
 *       "my_name_is"       : "Mein Name ist {0}.",
 *       "Hello world!"     : "Hallo Welt!",
 *       "My name is {0}." : "Mein Name ist {0}."
 *   }); 
 *   
 *   You can load multiple dictionaries. The dictionary entries will be added into a central dictionary.
 *   The latest entry will overwrite the previous entry if there is conflict in the dictionary index.
 *
 * 2. get the translated text
 *   -translation by keys
 *       alert(__('hello_world'));           => 'Hello Welt!'
 *       alert(__('my_name_is',['John'] ));  => 'Mein Name ist John.'
 *       alert(__('key_not_exist_in_dict')); => 'key_not_exist_in_dict'
 *       
 *   -translation by complete sentences    
 *       alert(__('Hello world!'));                  => 'Hello Welt!'
 *       alert(__('My name is {0}.',['John'] ));    => 'Mein Name ist John.'
 *       alert(__('This key doesn\'t exist'));       => 'This key doesn't exist.'
 *       
 * 
 */

(function() {

    /**
    the central dictionary object
    format of dict : {"key1":"text1", "key2":"text2",...}
    */
    var _dict = {};

    /**
     *Whether error is reported when the key is not found.
     **/
    var _notFoundError = false;

    window.jsIn = {
        /**
         *add a dictionary:
         *  the format of DictInObject : {"key1":"text1", "key2":"text2",...}
         **/
        addDict: function(dict) {
            for (key in dict) { _dict[key] = dict[key]; }
        },

        /**
         *get or set whether an error is shown when the dict entry is not found.
         **/
        showNotFoundError: function(show) {
            if (typeof show != 'undefined')
                _notFoundError = (show === true);

            return _notFoundError;
        },

        /**
        translate the text (optionally with variable replacement)
        input:  text - the key (or text) to be translated
                param - values of placeholders. in the format ['var1', 'var2'] where 
                        ('var1', 'var2' ...) will replace the placeholder ({0}, {1} .. {n-1}) in the translated text 
        */
        translate: function(text, param) {
            newText = _dict[text] || ((!_notFoundError) ? text : (function() { throw "No entry found for key {" + text + "}"; })());
            return jsIn.format(newText, param);
        },
        format: function(text, args) {
            if (args) {
                for (var i = 0, maxi = args.length; i < maxi; i++) {
                    var _regex = new RegExp('\\{' + i + '\\}', 'g');
                    text = text.replace(_regex, args[i]);
                }
            }
            return text;
        }
    };

    //create the global translate function
    window._ = jsIn.translate;

})();