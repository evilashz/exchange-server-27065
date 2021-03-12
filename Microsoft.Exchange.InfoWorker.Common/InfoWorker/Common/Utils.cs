using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common
{
	// Token: 0x0200000B RID: 11
	internal static class Utils
	{
		// Token: 0x06000023 RID: 35 RVA: 0x000026BC File Offset: 0x000008BC
		public static object SafeGetProperty(StoreObject message, PropertyDefinition propertyDefinition, object defaultValue)
		{
			if (message == null)
			{
				return defaultValue;
			}
			object obj = message.TryGetProperty(propertyDefinition);
			if (obj == null || obj is PropertyError)
			{
				return defaultValue;
			}
			return obj;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026E4 File Offset: 0x000008E4
		internal static string GetOUFromLegacyDN(string legacyDN)
		{
			string text = legacyDN.ToUpper();
			int num = text.IndexOf("OU=") + 3;
			int num2 = text.IndexOf("/CN=");
			return text.Substring(num, num2 - num);
		}
	}
}
