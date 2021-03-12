using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x020000DC RID: 220
	internal static class HttpProxyStrings
	{
		// Token: 0x0600076E RID: 1902 RVA: 0x0002EE80 File Offset: 0x0002D080
		static HttpProxyStrings()
		{
			HttpProxyStrings.stringIDs.Add(594155080U, "ErrorInternalServerError");
			HttpProxyStrings.stringIDs.Add(3579904699U, "ErrorAccessDenied");
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0002EEE3 File Offset: 0x0002D0E3
		public static LocalizedString ErrorInternalServerError
		{
			get
			{
				return new LocalizedString("ErrorInternalServerError", HttpProxyStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0002EEFA File Offset: 0x0002D0FA
		public static LocalizedString ErrorAccessDenied
		{
			get
			{
				return new LocalizedString("ErrorAccessDenied", HttpProxyStrings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0002EF11 File Offset: 0x0002D111
		public static LocalizedString GetLocalizedString(HttpProxyStrings.IDs key)
		{
			return new LocalizedString(HttpProxyStrings.stringIDs[(uint)key], HttpProxyStrings.ResourceManager, new object[0]);
		}

		// Token: 0x040004EA RID: 1258
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(2);

		// Token: 0x040004EB RID: 1259
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.HttpProxy.Strings", typeof(HttpProxyStrings).GetTypeInfo().Assembly);

		// Token: 0x020000DD RID: 221
		public enum IDs : uint
		{
			// Token: 0x040004ED RID: 1261
			ErrorInternalServerError = 594155080U,
			// Token: 0x040004EE RID: 1262
			ErrorAccessDenied = 3579904699U
		}
	}
}
