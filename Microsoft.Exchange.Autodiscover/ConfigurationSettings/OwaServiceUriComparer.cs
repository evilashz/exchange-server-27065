using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x0200003A RID: 58
	internal class OwaServiceUriComparer : IComparer<OwaService>
	{
		// Token: 0x06000197 RID: 407 RVA: 0x000086F0 File Offset: 0x000068F0
		public int Compare(OwaService x, OwaService y)
		{
			int num = Uri.Compare(x.Url, y.Url, UriComponents.AbsoluteUri, UriFormat.SafeUnescaped, StringComparison.OrdinalIgnoreCase);
			if (num == 0)
			{
				return x.AuthenticationMethod - y.AuthenticationMethod;
			}
			return num;
		}
	}
}
