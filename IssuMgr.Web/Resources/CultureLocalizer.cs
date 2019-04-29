﻿using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IssuMgr.Web.Resources {
    public class CultureLocalizer {
        private readonly IStringLocalizer _localizer;
        public CultureLocalizer(IStringLocalizerFactory factory) {
            var type = typeof(IssuMgrResources);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("IssuMgrResources", assemblyName.Name);
        }

        // if we have formatted string we can provide arguments         
        // e.g.: @Localizer.Text("Hello {0}", User.Name)
        public LocalizedString Text(string key, params string[] arguments) {
            return arguments == null
                ? _localizer[key]
                : _localizer[key, arguments];
        }
    }
}
