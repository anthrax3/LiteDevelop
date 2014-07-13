﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using LiteDevelop.Framework.FileSystem;
using LiteDevelop.Framework.FileSystem.Projects;

namespace LiteDevelop.Framework.Mui
{
    /// <summary>
    /// Represents a language pack for the Multilingual User Interface (MUI) of LiteDevelop.
    /// </summary>
    public sealed class UILanguagePack : SettingsMap 
    {

        public UILanguagePack(FilePath filePath, UILanguagePack fallBackPack)
            : base(filePath)
        {
            FallbackMap = fallBackPack;
        }

        public override string DocumentRoot
        {
            get { return "UILanguagePack"; }
        }

        /// <summary>
        /// Gets a language specific string by its identifier.
        /// </summary>
        /// <param name="path">The identifier to use.</param>
        /// <param name="parameters">Additional parameters.</param>
        /// <returns></returns>
        public string GetValue(string path, IDictionary<string, string> parameters)
        {
            string value;
            if (TryGetValue(path, parameters, out value))
            {
                return value;
            }
            throw new ArgumentException("The id is not present in the language pack.");
        }
        
        /// <summary>
        /// Tries to get a language specific string by its identifier.
        /// </summary>
        /// <param name="path">The identifier to use.</param>
        /// <param name="parameters">Additional parameters.</param>
        /// <param name="value">The language specific string, if found.</param>
        /// <returns></returns>
        public bool TryGetValue(string path, IDictionary<string, string> parameters, out string value)
        {
            if (this.TryGetValue<string>(path, out value))
            {
                if (parameters.Count > 0)
                {
                    var evaluator = new DictionaryStringEvaluator(parameters);
                    value = evaluator.EvaluateString(value);
                }

                return true;
            }

            return false;
        }
    }
}
