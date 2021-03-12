using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace Microsoft.Exchange.Management.PowerShell
{
	// Token: 0x02000009 RID: 9
	internal static class Strings
	{
		// Token: 0x0600001D RID: 29 RVA: 0x00011568 File Offset: 0x0000F768
		static Strings()
		{
			Strings.stringIDs.Add(2344500137U, "ExchangePSSnapInDescription");
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000115B7 File Offset: 0x0000F7B7
		public static string ExchangePSSnapInDescription
		{
			get
			{
				return Strings.ResourceManager.GetString("ExchangePSSnapInDescription");
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000115C8 File Offset: 0x0000F7C8
		public static string GetLocalizedString(Strings.IDs key)
		{
			return Strings.ResourceManager.GetString(Strings.stringIDs[(uint)key]);
		}

		// Token: 0x04000012 RID: 18
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(1);

		// Token: 0x04000013 RID: 19
		private static ResourceManager ResourceManager = new ResourceManager("Microsoft.Exchange.Management.PowerShell.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200000A RID: 10
		public enum IDs : uint
		{
			// Token: 0x04000015 RID: 21
			ExchangePSSnapInDescription = 2344500137U
		}
	}
}
