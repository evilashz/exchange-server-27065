using System;
using System.Collections.Concurrent;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000B8 RID: 184
	public class ThemeManagerFactory
	{
		// Token: 0x06000735 RID: 1845 RVA: 0x000165F4 File Offset: 0x000147F4
		public static ThemeManager GetInstance(string owaVersion)
		{
			if (!ThemeManagerFactory.themeManagerCollection.ContainsKey(owaVersion))
			{
				ThemeManager value = new ThemeManager(owaVersion);
				ThemeManagerFactory.themeManagerCollection.TryAdd(owaVersion, value);
			}
			return ThemeManagerFactory.themeManagerCollection[owaVersion];
		}

		// Token: 0x040003FB RID: 1019
		private static ConcurrentDictionary<string, ThemeManager> themeManagerCollection = new ConcurrentDictionary<string, ThemeManager>();
	}
}
