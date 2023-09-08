﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServerHost.Quickstart.UI
{
    public class RegisterViewModel : RegisterInputModel
    {
        public bool EnableLocalLogin { get; set; } = true;

        public string[] Roles { get; set; } = new string[0];

        public IEnumerable<ExternalProvider> ExternalProviders { get; set; } = Enumerable.Empty<ExternalProvider>();
        public IEnumerable<ExternalProvider> VisibleExternalProviders => ExternalProviders.Where(x => !String.IsNullOrWhiteSpace(x.DisplayName));

        public bool IsExternalLoginOnly => EnableLocalLogin == false && ExternalProviders?.Count() == 1;
        public string ExternalLoginScheme => IsExternalLoginOnly ? ExternalProviders?.SingleOrDefault()?.AuthenticationScheme : null;
    }
}
