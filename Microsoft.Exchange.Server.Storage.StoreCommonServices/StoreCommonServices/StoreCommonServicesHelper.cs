using System;
using System.Reflection;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x0200015B RID: 347
	public static class StoreCommonServicesHelper
	{
		// Token: 0x06000D8D RID: 3469 RVA: 0x00044371 File Offset: 0x00042571
		public static Version GetAssemblyVersion()
		{
			return Assembly.GetExecutingAssembly().GetName().Version;
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00044382 File Offset: 0x00042582
		public static string GetAssemblyName()
		{
			return Assembly.GetExecutingAssembly().GetName().Name;
		}
	}
}
